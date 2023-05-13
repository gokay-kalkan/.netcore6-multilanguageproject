using Microsoft.Extensions.Localization;
using System.Reflection;

namespace blogdeneme.ResourceServices
{
    public class MenuResource
    {

    }
    public class MenuService
    {
        private readonly IStringLocalizer _localizer;

        public MenuService(IStringLocalizerFactory factory)
        {
            var type = typeof(MenuResource);

            var assemblyName = new AssemblyName(type.GetTypeInfo().Assembly.FullName);

            _localizer = factory.Create(nameof(MenuResource), assemblyName.Name);


        }

        public LocalizedString GetKey(string key)
        {
            return _localizer[key];
        }


    }
}
