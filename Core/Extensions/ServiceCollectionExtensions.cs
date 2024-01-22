using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Extensions
{
    //Extension yazabilmek için o class'ın static olması gerekir.
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDependencyResolvers
            (this IServiceCollection serviceCollection, ICoreModule[] modules)
            //This neyi genişletmek istediğimiz, ICoreModule ise parametre
        {
            foreach (var module in modules)//Modulesteki her bir modül için
            {
                module.Load(serviceCollection);//Modül yükle
            }

            return ServiceTool.Create(serviceCollection);
        }
    }
}
