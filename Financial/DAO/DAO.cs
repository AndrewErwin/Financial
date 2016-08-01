using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Financial.Models.Context;

namespace Financial.DAO
{
    public abstract class DAO<T, I>
    {
        protected FinancialContext Context { get; private set; }

        public DAO(FinancialContext context)
        {
            this.Context = context;
        }

        public abstract List<T> List(params Expression<Func<T, object>>[] includes);
        public abstract void Add(T entity);
        public abstract void Update(T entity);
        public abstract void Delete(T entity);
        public abstract T GetById(I entityId, params Expression<Func<T, object>>[] includes);
    }
}