using System;
using System.Collections.Generic;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using R5T.T0045;
using R5T.T0092;


// Use specific namespace name.
namespace R5T.S0025.Library
{
    public static class ICompilationUnitGeneratorExtensions
    {
        public static CompilationUnitSyntax GetIExtensionMethodBaseFunctionalityExtensions(this ICompilationUnitGenerator _,
            IEnumerable<NamedIdentified> extensionMethodBaseFunctionalities,
            string namespaceName)
        {
            var compilationUnit = _.InNewNamespace(
                namespaceName,
                (xNamespace, xNamespaceNames) =>
                {
                    var iExtensionMethodBaseFunctionalityNamespacedTypeName = Instances.NamespacedTypeName.IExtensionMethodBaseFunctionality();
                    var iExtensionMethodBaseFunctionalityNamespaceName = Instances.NamespacedTypeName.GetNamespaceName(iExtensionMethodBaseFunctionalityNamespacedTypeName);

                    xNamespaceNames.AddRange(
                        iExtensionMethodBaseFunctionalityNamespaceName);

                    var iProjectPathExtensions = Instances.ClassGenerator.GetIExtensionMethodBaseFunctionalityExtensions(extensionMethodBaseFunctionalities);

                    var outputNamespace = xNamespace.AddClass(iProjectPathExtensions);
                    return outputNamespace;
                });

            return compilationUnit;
        }
    }
}
