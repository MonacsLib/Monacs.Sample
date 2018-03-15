using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Monacs.Core;
using Monacs.Core.Async;
using Monacs.Core.Unit;
using Newtonsoft.Json;

namespace FuelTracker.Api.Shared
{
    public class FileStorage<T> where T : class, IModel, new()
    {
        private readonly string _rootPath;
        private readonly string _fileName;

        public FileStorage(string rootPath, string fileName)
        {
            _rootPath = rootPath;
            _fileName = fileName;
        }

        private string FullPath => Path.Combine(_rootPath, $"{_fileName}.json");

        public async Task<Result<IEnumerable<T>>> GetAll() =>
            await Monacs.Core.Async.Result.TryCatchAsync(
                async () => File.Exists(FullPath) ? JsonConvert.DeserializeObject<IEnumerable<T>>(await File.ReadAllTextAsync(FullPath)) : Enumerable.Empty<T>(),
                ex => Errors.Error(exception: ex));

        public async Task<Result<T>> GetAsync(Guid id) =>
            await GetAll()
                .BindAsync(r => r
                    .FirstOrDefault(item => item.Id == id)
                    .ToResult(() => Errors.Error($"Object of id {id} was not found!")));

        public Task<Result<T>> Create(T newItem)
        {
            // TODO implement
            return Task.FromResult(Monacs.Core.Result.Error<T>(Errors.Error()));
        }

        public Task<Result<T>> Update(Guid id, T itemToUpdate)
        {
            // TODO implement
            return Task.FromResult(Monacs.Core.Result.Error<T>(Errors.Error()));
        }

        public Task<Result<Unit>> Delete(Guid id)
        {
            // TODO implement
            return Task.FromResult(Monacs.Core.Unit.Result.Ok());
        }
    }

    public class CarStorage : FileStorage<Cars.Car>
    {
        public CarStorage() : base("data", "cars")
        {
        }
    }

    public class FuelingStorage : FileStorage<Fuelings.Fueling>
    {
        public FuelingStorage() : base("data", "fuelings")
        {
        }
    }
}