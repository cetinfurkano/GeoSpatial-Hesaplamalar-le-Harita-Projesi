using Entities.Abstracts;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Abstracts
{
    public abstract class  DatabaseOperations<T> where T : class, IEntity, new()
    {
        public abstract void Add(T data);
        public abstract void Delete(T data);
        public abstract void Update(T data);
        public abstract List<T> GetAll();
        public abstract T GetWithGeometry(T data);
        public abstract T GetWithId(T data);

    }
}
