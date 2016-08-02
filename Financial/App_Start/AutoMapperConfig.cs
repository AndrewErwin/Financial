using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Financial.Models;
using Financial.Models.Entities;
using WebMatrix.WebData;

namespace Financial.App_Start
{
    public class AutoMapperConfig
    {
        public static void ConfigureAutoMapper()
        {
            #region :: Credit Card Maps ::
            Mapper.CreateMap<CreditCard, CreditCardFormModel>();
            Mapper.CreateMap<CreditCardFormModel, CreditCard>().
                ForMember(c => c.Network, o =>
                {
                    o.MapFrom(m => new CreditCardNetwork { Id = m.NetworkId });
                    o.Condition(m => m.NetworkId != Guid.Empty);
                }).
                ForMember(c => c.Owner, o =>
                {
                    o.MapFrom(m => new User() { Id = WebSecurity.CurrentUserId });
                });

            Mapper.CreateMap<CreditCard, AnyGuidConfirmDelete>().
                ForMember(c => c.Name, o => o.MapFrom(m => m.Nickname));
            Mapper.CreateMap<AnyGuidConfirmDelete, CreditCard>().
                ForMember(c => c.Nickname, o => o.MapFrom(m => m.Name)).
                ForMember(c => c.Owner, o =>
                {
                    o.MapFrom(m => new User() { Id = WebSecurity.CurrentUserId });
                });

            #endregion

            #region :: Purchase Maps ::

            Mapper.CreateMap<Purchase, PurchaseFormModel>();
            Mapper.CreateMap<PurchaseFormModel, Purchase>().
                ForMember(p => p.CreditCard, o =>
                {
                    o.MapFrom(m => new CreditCard() { Id = m.CreditCardId ?? Guid.Empty });
                    o.Condition(m => m.CreditCardId != Guid.Empty);
                }).
                ForMember(c => c.Owner, o =>
                {
                    o.MapFrom(m => new User() { Id = WebSecurity.CurrentUserId });
                });

            Mapper.CreateMap<Purchase, AnyGuidConfirmDelete>().
                ForMember(p => p.Name, o => o.MapFrom(m => m.Description));
            Mapper.CreateMap<AnyGuidConfirmDelete, Purchase>().
                ForMember(p => p.Description, o => o.MapFrom(m => m.Name)).
                ForMember(p => p.Owner, o =>
                {
                    o.MapFrom(m => new User() { Id = WebSecurity.CurrentUserId });
                });

            #endregion
        }
    }
}