using Microsoft.Extensions.Localization;
using System.Reflection;

namespace blogdeneme.ResourceServices
{
   
        public class PrivacyResource
        {

        }
        public class PrivacyService
        {
            private readonly IStringLocalizer _localizer;

            public PrivacyService(IStringLocalizerFactory factory)
            {
                var type = typeof(PrivacyResource);

                var assemblyName = new AssemblyName(type.GetTypeInfo().Assembly.FullName);

                _localizer = factory.Create(nameof(PrivacyResource), assemblyName.Name);


            }

            public LocalizedString GetKey(string key)
            {
                return _localizer[key];
            }


        }
    }

