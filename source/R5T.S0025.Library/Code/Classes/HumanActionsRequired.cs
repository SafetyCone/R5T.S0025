using System;


namespace R5T.S0025.Library
{
    public class HumanActionsRequired
    {
        public bool ReviewNewEmbExtensions { get; set; }
        public bool ReviewNewToEmbMappings { get; set; }
        public bool ReviewNewToProjectMappings { get; set; }
        public bool ReviewDepartedEmbExtensions { get; set; }
        public bool ReviewDepartedToEmbMappings { get; set; }
        public bool ReviewDepartedToProjectMappings { get; set; }
        public bool ReviewEmbExtensionsUnmappableToEmbs { get; set; }
        public bool ReviewEmbExtensionsUnmappedToEmb { get; set; }
        public bool ReviewInvalidToEmbMappings { get; set; }
        public bool ReviewEmbExtensionsUnmappedToProject { get; set; }
        public bool ReviewInvalidToProjectMappings { get; set; }
    }
}
