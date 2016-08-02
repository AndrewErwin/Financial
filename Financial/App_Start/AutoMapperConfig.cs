﻿using System;
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
        }
    }
}