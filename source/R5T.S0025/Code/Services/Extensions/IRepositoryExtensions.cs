using System;
using System.Threading.Tasks;

using R5T.T0097;
using R5T.T0100;
using R5T.T0102;


namespace R5T.S0025
{
    public static class IRepositoryExtensions
    {
        public static async Task<(
            Project[] selectedProjects,
            ExtensionMethodBase[] selectedEmbs,
            string[] ignoredEmbNames,
            ExtensionMethodBaseExtension[] embExtensions,
            ExtensionMethodBaseExtensionToExtensionMethodBaseMapping[] toEmbMappings,
            ExtensionMethodBaseExtensionToProjectMapping[] toProjectMappings)>
        GetAllFunctionalityData(this IRepository repository)
        {
            var output = await TaskHelper.WhenAll(
                   // Use pre-computed selections instead of re-computing selections from ignored and duplicate name data.
                   repository.ProjectRepository.GetSelectedProjects(),
                   // Use pre-computed selections instead of re-computing selections from ignored and duplicate name data.
                   repository.ExtensionMethodBaseRepository.GetSelectedExtensionMethodBases(),
                   repository.ExtensionMethodBaseExtensionRepository.GetAllIgnoredExtensionMethodBaseNamespacedTypeNames(),
                   repository.ExtensionMethodBaseExtensionRepository.GetAllExtensionMethodBaseExtensions(),
                   repository.ExtensionMethodBaseExtensionRepository.GetAllToExtensionMethodBaseMappings(),
                   repository.ExtensionMethodBaseExtensionRepository.GetAllToProjectMappings());

            return output;
        }

        public static async Task<(
            Project[] projects,
            ExtensionMethodBase[] extensionMethodBases,
            ExtensionMethodBaseExtension[] embExtensions,
            ExtensionMethodBaseExtensionToProjectMapping[] toProjectMappings,
            ExtensionMethodBaseExtensionToExtensionMethodBaseMapping[] toEmbMappings)>
        GetExtensionMethodBaseFunctionalityData(this IRepository repository)
        {
            var output = await TaskHelper.WhenAll(
                   // Use pre-computed selections instead of re-computing selections from ignored and duplicate name data.
                   repository.ProjectRepository.GetAllProjects(),
                   // Use pre-computed selections instead of re-computing selections from ignored and duplicate name data.
                   repository.ExtensionMethodBaseRepository.GetAllExtensionMethodBases(),
                   repository.ExtensionMethodBaseExtensionRepository.GetAllExtensionMethodBaseExtensions(),
                   repository.ExtensionMethodBaseExtensionRepository.GetAllToProjectMappings(),
                   repository.ExtensionMethodBaseExtensionRepository.GetAllToExtensionMethodBaseMappings());

            return output;
        }
    }
}
