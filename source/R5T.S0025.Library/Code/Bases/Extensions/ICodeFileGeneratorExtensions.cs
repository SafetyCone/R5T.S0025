﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using R5T.T0045;
using R5T.T0092;


// Use specific namespace name.
namespace R5T.S0025.Library
{
    public static class ICodeFileGeneratorExtensions
    {
        public static async Task CreateIExtensionMethodBaseFunctionalityExtensions(this ICodeFileGenerator _,
            IEnumerable<NamedIdentified> extensionMethodBaseFunctionalities,
            string namespaceName,
            string filePath)
        {
            var compilationUnit = Instances.CompilationUnitGenerator.GetIExtensionMethodBaseFunctionalityExtensions(
                extensionMethodBaseFunctionalities,
                namespaceName);

            await compilationUnit.WriteTo(filePath);
        }
    }
}
