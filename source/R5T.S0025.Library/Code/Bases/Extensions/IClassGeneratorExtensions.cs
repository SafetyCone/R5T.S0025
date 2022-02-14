using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using R5T.T0045;
using R5T.T0092;


// Use specific namespace name.
namespace R5T.S0025.Library
{
    public static class IClassGeneratorExtensions
    {
        public static ClassDeclarationSyntax GetIExtensionMethodBaseFunctionalityExtensions(this IClassGenerator _,
            IEnumerable<NamedIdentified> extensionMethodBaseFunctionalities)
        {
            var indentation = Instances.Indentation.Method();

            var methods = extensionMethodBaseFunctionalities
                .OrderAlphabetically(x => x.Name)
                .Select(xProject => Instances.MethodGenerator.GetProjectPathExtension(xProject))
                .ToArray();

            var output = _.GetPublicStaticClass(
                Instances.TypeName.IExtensionMethodBaseFunctionalityExtensions())
                .AddMembersWithBlankLineSeparation(methods)
                ;

            return output;
        }
    }
}
