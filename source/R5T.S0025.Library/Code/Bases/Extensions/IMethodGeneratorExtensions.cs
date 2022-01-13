using System;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using R5T.T0045;
using R5T.T0092;


// Use specific namespace name.
namespace R5T.S0025.Library
{
    public static class IMethodGeneratorExtensions
    {
        public static MethodDeclarationSyntax GetProjectPathExtension(this IMethodGenerator _,
            NamedIdentified extensionMethodBaseFunctionality)
        {
            var text = $@"
public static string {extensionMethodBaseFunctionality.Name}(this {Instances.TypeName.IExtensionMethodBaseFunctionality()} _)
{{
    return ""{extensionMethodBaseFunctionality.Identity}"";
}}
";

            var method = _.GetMethodDeclarationFromText(text);
            return method;
        }
    }
}
