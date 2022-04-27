using System;
using System.Threading.Tasks;

using R5T.T0064;


namespace R5T.S0025
{
    /// <inheritdoc cref="IExtensionMethodBaseFunctionalityExtensionMethodBaseProjectPathProvider"/>
    [ServiceImplementationMarker]
    public class ConstructorBasedExtensionMethodBaseFunctionalityExtensionMethodBaseProjectPathProvider :
        IExtensionMethodBaseFunctionalityExtensionMethodBaseProjectPathProvider,
        IServiceImplementation
    {
        private string ExtensionMethodBaseFunctionalityExtensionMethodBaseProjectPath { get; }


        public ConstructorBasedExtensionMethodBaseFunctionalityExtensionMethodBaseProjectPathProvider(
            [NotServiceComponent] string extensionMethodBaseFunctionalityExtensionMethodBaseProjectPath)
        {
            this.ExtensionMethodBaseFunctionalityExtensionMethodBaseProjectPath = extensionMethodBaseFunctionalityExtensionMethodBaseProjectPath;
        }

        public Task<string> GetExtensionMethodBaseFunctionalityExtensionMethodBaseProjectPath()
        {
            return Task.FromResult(this.ExtensionMethodBaseFunctionalityExtensionMethodBaseProjectPath);
        }
    }
}
