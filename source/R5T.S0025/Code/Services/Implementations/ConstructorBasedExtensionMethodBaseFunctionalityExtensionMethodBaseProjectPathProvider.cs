using System;
using System.Threading.Tasks;


namespace R5T.S0025
{
    /// <inheritdoc cref="IExtensionMethodBaseFunctionalityExtensionMethodBaseProjectPathProvider"/>
    public class ConstructorBasedExtensionMethodBaseFunctionalityExtensionMethodBaseProjectPathProvider : IExtensionMethodBaseFunctionalityExtensionMethodBaseProjectPathProvider
    {
        private string ExtensionMethodBaseFunctionalityExtensionMethodBaseProjectPath { get; }


        public ConstructorBasedExtensionMethodBaseFunctionalityExtensionMethodBaseProjectPathProvider(
            string extensionMethodBaseFunctionalityExtensionMethodBaseProjectPath)
        {
            this.ExtensionMethodBaseFunctionalityExtensionMethodBaseProjectPath = extensionMethodBaseFunctionalityExtensionMethodBaseProjectPath;
        }

        public Task<string> GetExtensionMethodBaseFunctionalityExtensionMethodBaseProjectPath()
        {
            return Task.FromResult(this.ExtensionMethodBaseFunctionalityExtensionMethodBaseProjectPath);
        }
    }
}
