using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Monacs.Core;
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

        public Result<IEnumerable<T>> GetAll() =>
            Monacs.Core.Result.TryCatch(
                () => File.Exists(FullPath) ? JsonConvert.DeserializeObject<IEnumerable<T>>(File.ReadAllText(FullPath)) : Enumerable.Empty<T>(),
                ex => Errors.Error(exception: ex));

        public Result<T> Get(Guid id) =>
            GetAll()
                .Bind(r => r
                    .FirstOrDefault(item => item.Id == id)
                    .ToResult(() => Errors.Error($"Object of id {id} was not found!")));

        public Result<T> Create(T newItem)
        {
            return Monacs.Core.Result.Error<T>(Errors.Error());
        }

        public Result<T> Update(Guid id, T itemToUpdate)
        {
            return Monacs.Core.Result.Error<T>(Errors.Error());
        }

        public Result<Unit> Delete(Guid id)
        {
            return Monacs.Core.Unit.Result.Ok();
        }
    }

    public class CarStorage : FileStorage<Cars.Car>
    {
        public CarStorage() : base("data", "cars")
        {
        }
    }
}