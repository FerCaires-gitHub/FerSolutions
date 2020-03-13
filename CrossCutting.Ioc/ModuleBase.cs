using Application.Service;
using Domain.Service;
using SimpleInjector;
using SimpleInjector.Lifestyles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossCutting.Ioc
{
    public class ModuleBase
    {
        public static Container Config()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();
            container.Register<IBetService, BetService>(Lifestyle.Scoped);
            container.Verify();
            return container;
        }
    }
}
