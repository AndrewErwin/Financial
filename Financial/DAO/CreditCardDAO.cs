using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Financial.Content;
using Financial.Models;
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

        public InvoiceModel GetInvoice(Guid creditCardId, int? month = null, int? year = null)
        {
            DateTime
                invoiceBegin = new DateTime(year ?? DateTime.Now.Year, month ?? DateTime.Now.Month, 1).Date,
                invoiceEnd = invoiceBegin.AddMonths(1).AddDays(-1)
            ;

            var purchases = this.Context.Purchases
                                .Where(p => p.CreditCardId == creditCardId)
                                .Where(p => (
                                                (
                                                    p.InstalmentSplit > 1
                                                    && p.PurchasedOn.Date <= invoiceBegin
                                                    && invoiceBegin <= p.PurchasedOn.AddMonths(p.InstalmentSplit - 1).Date
                                                )
                                                ||
                                                (
                                                    p.InstalmentSplit == 1
                                                    && invoiceBegin <= p.PurchasedOn.Date
                                                    && p.PurchasedOn <= invoiceEnd
                                                )
                                            )
                                        );

            var invoice = purchases.Select(p => new InvoiceDetailModel()
                            {
                                Id = p.Id,
                                Date = p.PurchasedOn,
                                Description = p.Description,
                                AtualSplit = (((invoiceBegin.Year * 12) + invoiceBegin.Month) - ((p.PurchasedOn.Year * 12) + p.PurchasedOn.Month)) + 1,
                                Splits = p.InstalmentSplit,
                                Value = (p.TotalAmount / p.InstalmentSplit),
                                Icon = Icons.Purchase,
                                RouteToEntity = "EditPurchase"
                            });

            return
                new InvoiceModel()
                {
                    Begin = invoiceBegin,
                    End = invoiceEnd,
                    Details = invoice.OrderBy(i => i.Date.Day).ToList()
                };
        }
    }
}