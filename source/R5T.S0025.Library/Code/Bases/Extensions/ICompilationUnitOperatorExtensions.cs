using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using R5T.Magyar;

using R5T.T0045;
using R5T.T0100;


// Use specific namespace name.
namespace R5T.S0025.Library
{
    public static class ICompilationUnitOperatorExtensions
    {
        public static (string namespacedTypedParameterizedMethodName, ExtensionMethodBase extensionMethodBase)[] GetExtensionMethodBaseExtensionNameTuples(this ICompilationUnitOperator _,
            CompilationUnitSyntax compilationUnit,
            IDictionary<string, ExtensionMethodBase> extensionMethodBasesByNamespacedTypeName)
        {
            var output = _.GetExtensionMethodTuples(compilationUnit)
                .Select(extensionMethodTuple =>
                {
                    var namespacedTypedParameterizedMethodName = Instances.MethodNameOperator.GetNamespacedTypedParameterizedMethodName(extensionMethodTuple);

                    // Try to determine what namespaced type name of the extended type of the extension method based on available namespaces.
                    var extensionParameter = extensionMethodTuple.ExtensionMethod.GetExtensionParameter();

                    var extensionParameterTypeName = extensionParameter.GetTypeName();

                    // Include usings that may be in the namespace.
                    var usingDirectivesSpecification = compilationUnit.GetUsingDirectivesSpecification(extensionMethodTuple.Namespace);

                    // First check if the extension parameter type name is a direct using alias directive.
                    var hasAliasDirectiveForExtensionParameterTypeName = usingDirectivesSpecification.HasNameAliasFor(extensionParameterTypeName);
                    if (hasAliasDirectiveForExtensionParameterTypeName)
                    {
                        var aliasedNamespacedTypeName = hasAliasDirectiveForExtensionParameterTypeName.Result.SourceNameExpression;

                        var embExists = extensionMethodBasesByNamespacedTypeName.TryGetValue(aliasedNamespacedTypeName, out var emb);
                        if(embExists)
                        {
                            return (namespacedTypedParameterizedMethodName, emb);
                        }
                    }

                    // Now, for each available namespace (including the empty namespace to allow the extension parameter type name to be fully namespaced), guess the namespaced type name.
                    foreach (var namespaceName in usingDirectivesSpecification.GetUsingNamespaceNamesIncludingEmpty())
                    {
                        var possibleNamespacedTypeName = Instances.NamespacedTypeName.GetNamespacedName(
                            namespaceName,
                            extensionParameterTypeName);

                        var embExists = extensionMethodBasesByNamespacedTypeName.TryGetValue(possibleNamespacedTypeName, out var emb);
                        if (embExists)
                        {
                            return (namespacedTypedParameterizedMethodName, emb);
                        }
                    }

                    // Else, the extended type of the extension method does not exist in the set of extension method bases.
                    return (namespacedTypedParameterizedMethodName, default);
                })
                .ToArray();

            return output;
        }
    }
}
