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
    public class SubscriptionDAO : DAO<Subscription, Guid>
    {
        public SubscriptionDAO(FinancialContext context) : base(context) { }

        public override List<Subscription> List(params Expression<Func<Subscription, object>>[] includes)
        {
            var subscriptions = this.Context.Subscriptions;
            return includes.Aggregate(
                        subscriptions.AsQueryable(),
                        (query, include) => query.Include(include)
                    ).Where(p => p.OwnerId == WebSecurity.CurrentUserId).OrderBy(s => s.Description).ToList();
        }

        public override void Add(Subscription entity)
        {
            entity.OwnerId = WebSecurity.CurrentUserId;
            this.Context.Subscriptions.Add(entity);
            this.Context.SaveChanges();
        }

        public override void Update(Subscription entity)
        {
            this.Context.Subscriptions.Update(entity);
            this.Context.SaveChanges();
        }

        public override void Delete(Subscription entity)
        {
            this.Context.Subscriptions.Remove(entity);
            this.Context.SaveChanges(); throw new NotImplementedException();
        }

        public override Subscription GetById(Guid entityId, params Expression<Func<Subscription, object>>[] includes)
        {
            var subscription = this.Context.Subscriptions;
            return includes.Aggregate(
                        subscription.AsQueryable(),
                        (query, include) => query.Include(include)
                    ).Where(s => s.OwnerId == WebSecurity.CurrentUserId && s.Id == entityId).FirstOrDefault();
        }
    }
}