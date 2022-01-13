using System;


namespace R5T.S0025.Library
{
    public static class HumanActionsRequired01Extensions
    {
        public static bool Any(this HumanActionsRequired01 humanActionsRequired)
        {
            var output = false
                || humanActionsRequired.ReviewNewExtensionMethodBaseExtensions
                || humanActionsRequired.ReviewDepartedExtensionMethodBaseExtensions
                || humanActionsRequired.ReviewNewToProjectMappings
                || humanActionsRequired.ReviewDepartedToProjectMappings
                || humanActionsRequired.ReviewNewToExtensionMethodBaseMappings
                || humanActionsRequired.ReviewDepartedToExtensionMethodBaseMappings
                ;

            return output;
        }

        public static bool AnyMandatory(this HumanActionsRequired01 humanActionsRequired)
        {
            // None are mandatory.
            var output = false;
            return output;
        }

        public static void UnsetNonMandatory(this HumanActionsRequired01 humanActionsRequired)
        {
            humanActionsRequired.ReviewNewExtensionMethodBaseExtensions = false;
            humanActionsRequired.ReviewDepartedExtensionMethodBaseExtensions = false;
            humanActionsRequired.ReviewNewToProjectMappings = false;
            humanActionsRequired.ReviewDepartedToProjectMappings = false;
            humanActionsRequired.ReviewNewToExtensionMethodBaseMappings = false;
            humanActionsRequired.ReviewDepartedToExtensionMethodBaseMappings = false;
        }
    }
}
