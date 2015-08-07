using SecretariaControl.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SecretariaControl.WebApp.Core.Context
{
    public static class ApplicationContext
    {
        public static SecretariaControlEntities DataBaseContext
        {
            get
            {
                return HttpContext.Current.Items["DataBaseContext"] as SecretariaControlEntities;
            }
            set
            {
                HttpContext.Current.Items["DataBaseContext"] = value;
            }
        }

    }
}