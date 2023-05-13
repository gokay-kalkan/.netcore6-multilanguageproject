using Microsoft.Extensions.Localization;
using System.Reflection;

namespace blogdeneme.ResourceServices
{
   
        public class ContactResource
        {

        }
        public class ContactService
        {
            private readonly IStringLocalizer _localizer;

            public ContactService(IStringLocalizerFactory factory)
            {
                var type = typeof(ContactResource);

                var assemblyName = new AssemblyName(type.GetTypeInfo().Assembly.FullName);

                _localizer = factory.Create(nameof(ContactResource), assemblyName.Name);


            }

            public LocalizedString GetKey(string key)
            {
                return _localizer[key];
            }


        }
    }

