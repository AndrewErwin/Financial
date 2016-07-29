using Financial.Models.Context;
using System.Collections.Generic;

namespace Financial.DAO
{
    public abstract class DAO<T>
    {
        protected FinancialContext Context { get; private set; }

        public DAO(FinancialContext context)
        {
            this.Context = context;
        }

        public abstract List<T> List();
        public abstract void Add(T entity);
        public abstract void Update(T entity);
        public abstract void Delete(T entity);
        public abstract T GetById(int entityId);
    }
}