using Financial.Models.Context;
using Financial.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Financial.DAO
{
    public class UserDAO : DAO<User>
    {
        public UserDAO(FinancialContext context) : base(context) { }

        public override List<User> List()
        {
            throw new NotImplementedException();
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

        public override User GetById(int entityId)
        {
            throw new NotImplementedException();
        }
    }
}