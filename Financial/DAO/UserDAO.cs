using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Financial.Models.Context;
using Financial.Models.Entities;
using Microsoft.Data.Entity;

namespace Financial.DAO
{
    public class UserDAO : DAO<User, int>
    {
        public UserDAO(FinancialContext context) : base(context) { }

        public override List<User> List(params Expression<Func<User, object>>[] includes)
        {
            var users = this.Context.Users;
            return includes.Aggregate(
                        users.AsQueryable(),
                        (query, include) => query.Include(include)
                    ).OrderBy(c => c.Name).OrderBy(c => c.Login).ToList();
        }

        public override void Add(User entity)
        {
            // USE WebSecurity.CreateUserAndAccount()
            throw new NotImplementedException();
        }

        public override void Update(User entity)
        {
            throw new NotImplementedException();
        }

        public override void Delete(User entity)
        {
            throw new NotImplementedException();
        }

        public override User GetById(int entityId, params Expression<Func<User, object>>[] includes)
        {
            var user = this.Context.Users;
            return includes.Aggregate(
                        user.AsQueryable(),
                        (query, include) => query.Include(include)
                    ).Where(c => c.Id == entityId).FirstOrDefault();
        }

        public User GetByLogin(String username, params Expression<Func<User, object>>[] includes)
        {
            var user = this.Context.Users;
            return includes.Aggregate(
                        user.AsQueryable(),
                        (query, include) => query.Include(include)
                    ).Where(c => c.Login == username).FirstOrDefault();
        }
    }
}