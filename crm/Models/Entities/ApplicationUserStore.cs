﻿using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace crm.Models.Entities
{
    public class ApplicationUserStore :
    UserStore<ApplicationUser, ApplicationRole, string, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>,
    IUserStore<ApplicationUser>,
    IDisposable
    {
        public ApplicationUserStore(ApplicationDbContext context) : base(context) { }
    }
}