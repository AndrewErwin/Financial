using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Financial.Models.Context;
using Financial.Models.Entities;

namespace Financial.DAO
{
    public class CreditCardNetworkDAO : DAO<CreditCardNetwork>
    {
        public CreditCardNetworkDAO(FinancialContext context) : base(context) { }

        public override List<CreditCardNetwork> List()
        {
            return this.Context.CreditCardNetworks.OrderBy(n => n.DisplayName).ToList();
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

        public override CreditCardNetwork GetById(Guid entityId)
        {
            return this.Context.CreditCardNetworks.FirstOrDefault(n => n.Id == entityId);
        }
    }
}