using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Monacs.Core;
using Monacs.Core.Unit;
using Newtonsoft.Json;
using static Monacs.Core.Result;

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
            await AsyncResult.TryCatchAsync(
                async () => File.Exists(FullPath) ? JsonConvert.DeserializeObject<IEnumerable<T>>(await File.ReadAllTextAsync(FullPath)) : Enumerable.Empty<T>(),
                ex => Errors.Error(exception: ex));

        public async Task<Result<T>> Get(Guid id) =>
            await GetAll()
                .BindAsync(r => r
                    .FirstOrDefault(item => item.Id == id)
                    .ToResult(() => Errors.Error($"Object of id {id} was not found!")));

        public async Task<Result<T>> Create(T newItem) =>
            await GetAll()
                .MapAsync(items => items.Append(newItem))
                .BindAsync(StoreAll)
                .MapAsync(_ => newItem);

        public async Task<Result<T>> Update(Guid id, T itemToUpdate) =>
            await GetAll()
                .MapAsync(items => items.Where(x => x.Id != id).Append(itemToUpdate))
                .BindAsync(StoreAll)
                .MapAsync(_ => itemToUpdate);

        public async Task<Result<Unit>> Delete(Guid id) =>
            await GetAll()
                .MapAsync(items => items.Where(x => x.Id != id))
                .BindAsync(StoreAll);

        private async Task<Result<Unit>> StoreAll(IEnumerable<T> items) =>
            await AsyncResult.TryCatchAsync(
                async () => {
                    var dir = Path.GetDirectoryName(FullPath);
                    if (!Directory.Exists(dir))
                        Directory.CreateDirectory(dir);
                    await File.WriteAllTextAsync(FullPath, JsonConvert.SerializeObject(items));
                    return Unit.Default;
                },
                ex => Errors.Error(exception: ex));
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