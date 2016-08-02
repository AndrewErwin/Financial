using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Financial.Models.Context;
using Financial.Models.Entities;
using Microsoft.Data.Entity;
using WebMatrix.WebData;

namespace Financial.DAO
{
    public class CreditCardDAO : DAO<CreditCard, Guid>
    {
        public CreditCardDAO(FinancialContext context) : base(context) { }

        public override List<CreditCard> List(params Expression<Func<CreditCard, object>>[] includes)
        {
            var creditCards = this.Context.CreditCards;
            return includes.Aggregate(
                        creditCards.AsQueryable(),
                        (query, include) => query.Include(include)
                    ).Where(c => c.OwnerId == WebSecurity.CurrentUserId).OrderBy(c => c.Nickname).ToList();
        }

        public override void Add(CreditCard entity)
        {
            entity.OwnerId = WebSecurity.CurrentUserId;
            this.Context.Add(entity);
            this.Context.SaveChanges();
        }

        public override void Update(CreditCard entity)
        {
            this.Context.Update(entity);
            this.Context.SaveChanges();
        }

        public override void Delete(CreditCard entity)
        {
            this.Context.Remove(entity);
            this.Context.SaveChanges();
        }

        public override CreditCard GetById(Guid entityId, params Expression<Func<CreditCard, object>>[] includes)
        {
            var creditCard = this.Context.CreditCards;
            return includes.Aggregate(
                        creditCard.AsQueryable(),
                        (query, include) => query.Include(include)
                    ).Where(c => c.OwnerId == WebSecurity.CurrentUserId && c.Id == entityId).FirstOrDefault();
        }
    }
}