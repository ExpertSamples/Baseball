﻿using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MvcBaseBall.Startup))]
namespace MvcBaseBall
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
