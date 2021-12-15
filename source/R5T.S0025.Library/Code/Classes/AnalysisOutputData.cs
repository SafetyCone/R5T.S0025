using System;

using R5T.T0102;


namespace R5T.S0025.Library
{
    public class AnalysisOutputData
    {
        /// <summary>
        /// "New" means present in the current state of the local file system, but not in the repository.
        /// </summary>
        public ExtensionMethodBaseExtension[] NewEmbExtensions { get; set; }
        /// <summary>
        /// "Departed" means present in the repository, but not in the current state of the local file system.
        /// </summary>
        public ExtensionMethodBaseExtension[] DepartedEmbExtensions { get; set; }

        public ExtensionMethodBaseExtension[] EmbExtensionsUnmappableToEmbs { get; set; }

        public ExtensionMethodBaseExtension[] EmbExtensionsUnmappedToEmb { get; set; }

        public ExtensionMethodBaseExtensionToExtensionMethodBaseMapping[] NewToEmbMappings { get; set; }
        public ExtensionMethodBaseExtensionToExtensionMethodBaseMapping[] DepartedToEmbMappings { get; set; }
        public ExtensionMethodBaseExtensionToExtensionMethodBaseMapping[] InvalidToEmbMappings { get; set; }

        //public ExtensionMethodBaseExtension[] EmbExtensionsUnmappableToProject { get; set; } // Disregard for now.

        public ExtensionMethodBaseExtension[] EmbExtensionsUnmappedToProject { get; set; }

        public ExtensionMethodBaseExtensionToProjectMapping[] NewToProjectMappings { get; set; }
        public ExtensionMethodBaseExtensionToProjectMapping[] DepartedToProjectMappings { get; set; }
        public ExtensionMethodBaseExtensionToProjectMapping[] InvalidToProjectMappings { get; set; }
    }
}
