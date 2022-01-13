using System;
using System.Threading.Tasks;

using R5T.T0064;


namespace R5T.S0025
{
    /// <summary>
    /// Provides the project file path containing the IExtensionMethodBaseFunctionality extension method base type definition.
    /// This path is required for code that wants to add a reference to the IExtensionMethodBaseFunctionality project, perhaps as part of automatically write IExtensionMethodBaseFunctionality extension methods.
    /// </summary>
    [ServiceDefinitionMarker]
    public interface IExtensionMethodBaseFunctionalityExtensionMethodBaseProjectPathProvider : IServiceDefinition
    {
        Task<string> GetExtensionMethodBaseFunctionalityExtensionMethodBaseProjectPath();
    }
}
