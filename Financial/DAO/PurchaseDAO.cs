using System;
using System.Collections.Generic;
using System.Linq;
using Financial.Models.Context;
using Financial.Models.Entities;
using Microsoft.Data.Entity;
using WebMatrix.WebData;

namespace Financial.DAO
{
    public class PurchaseDAO : DAO<Purchase, Guid>
    {
        public PurchaseDAO(FinancialContext context) : base(context) { }

        public override Guid TryParseToEntityId(object id)
        {
            Guid entityId = Guid.Empty;
            Guid.TryParse(id.ToString(), out entityId);

            return entityId;
        }

        public override List<Purchase> List(params System.Linq.Expressions.Expression<Func<Purchase, object>>[] includes)
        {
            var purchases = this.Context.Purchases;
            return includes.Aggregate(
                        purchases.AsQueryable(),
                        (query, include) => query.Include(include)
                    ).Where(p => p.OwnerId == WebSecurity.CurrentUserId).OrderByDescending(p => p.PurchasedOn).OrderBy(p => p.Description).ToList();
        }

        public override void Add(Purchase entity)
        {
            entity.OwnerId = WebSecurity.CurrentUserId;
            this.Context.Purchases.Add(entity);
            this.Context.SaveChanges();
        }

        public override void Update(Purchase entity)
        {
            this.Context.Purchases.Update(entity);
            this.Context.SaveChanges();
        }

        public override void Delete(Purchase entity)
        {
            this.Context.Purchases.Remove(entity);
            this.Context.SaveChanges();
        }

        public override Purchase GetById(Guid entityId, params System.Linq.Expressions.Expression<Func<Purchase, object>>[] includes)
        {
            var purchase = this.Context.Purchases;
            return includes.Aggregate(
                        purchase.AsQueryable(),
                        (query, include) => query.Include(include)
                    ).Where(p => p.OwnerId == WebSecurity.CurrentUserId && p.Id == entityId).FirstOrDefault();
        }
    }
}