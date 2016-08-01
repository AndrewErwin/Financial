using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Financial.Models.Context;
using Financial.Models.Entities;
using Microsoft.Data.Entity;

namespace Financial.DAO
{
    public class CreditCardNetworkDAO : DAO<CreditCardNetwork, Guid>
    {
        public CreditCardNetworkDAO(FinancialContext context) : base(context) { }

        public override List<CreditCardNetwork> List(params Expression<Func<CreditCardNetwork, object>>[] includes)
        {
            var networks = this.Context.CreditCardNetworks;
            return includes.Aggregate(
                        networks.AsQueryable(),
                        (query, include) => query.Include(include)                    
                    ).OrderBy(n => n.DisplayName).ToList();
        }

        public override void Add(CreditCardNetwork entity)
        {
            this.Context.CreditCardNetworks.Add(entity);
            this.Context.SaveChanges();
        }

        public override void Update(CreditCardNetwork entity)
        {
            this.Context.CreditCardNetworks.Update(entity);
            this.Context.SaveChanges();
        }

        public override void Delete(CreditCardNetwork entity)
        {
            this.Context.CreditCardNetworks.Remove(entity);
            this.Context.SaveChanges();
        }        

        public override CreditCardNetwork GetById(Guid entityId, params Expression<Func<CreditCardNetwork, object>>[] includes)
        {
            var ccNetwork = this.Context.CreditCardNetworks;
            return includes.Aggregate(
                        ccNetwork.AsQueryable(),
                        (query, include) => query.Include(include)
                    ).Where(c => c.Id == entityId).FirstOrDefault();
        }
    }
}