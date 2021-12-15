using System;

using R5T.T0097;
using R5T.T0100;
using R5T.T0102;


namespace R5T.S0025.Library
{
    public class AnalysisInputData
    {
        public ExtensionMethodBaseExtension[] CurrentEmbExtensions { get; set; }
        public ExtensionMethodBaseExtension[] RepositoryEmbExtensions { get; set; }

        public ExtensionMethodBase[] ExtensionMethodBases { get; set; }

        public ExtensionMethodBaseExtensionToExtensionMethodBaseMapping[] CurrentToEmbMappings { get; set; }
        public ExtensionMethodBaseExtension[] EmbExtensionsUnmappableToEmbs { get; set; }

        public ExtensionMethodBaseExtensionToExtensionMethodBaseMapping[] RepositoryToEmbMappings { get; set; }

        public Project[] Projects { get; set; }

        public ExtensionMethodBaseExtensionToProjectMapping[] CurrentToProjectMappings { get; set; }
        //public ExtensionMethodBaseExtension[] EmbExtensionsUnmappableToProject { get; set; } // Disregard for now.

        public ExtensionMethodBaseExtensionToProjectMapping[] RepositoryToProjectMappings { get; set; }

    }
}
