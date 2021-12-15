using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using R5T.Magyar.IO;

using R5T.D0105;
using R5T.T0092.X001;


namespace R5T.S0025
{
    /// <summary>
    /// Create tuples of (EMB extension, EMB, project), filter by selected project and selected EMB, then output all functionality names to a file.
    /// Functionality names are: {EMB}_{EMB extension method name}_{project name}.
    /// </summary>
    public class O005a_OutputEmbFunctionalityNames : T0020.IOperation
    {
        private INotepadPlusPlusOperator NotepadPlusPlusOperator { get; }
        private IRepository Repository { get; }


        public O005a_OutputEmbFunctionalityNames(
            INotepadPlusPlusOperator notepadPlusPlusOperator,
            IRepository repository)
        {
            this.NotepadPlusPlusOperator = notepadPlusPlusOperator;
            this.Repository = repository;
        }

        public async Task Run()
        {
            var (
                selectedProjects,
                selectedEmbs,
                ignoredEmbs,
                embExtensions,
                toEmbMappings,
                toProjectMappings
                )
                = await TaskHelper.WhenAll(
                    // Use pre-computed selections instead of re-computing selections from ignored and duplicate name data.
                    this.Repository.ProjectRepository.GetSelectedProjects(),
                    // Use pre-computed selections instead of re-computing selections from ignored and duplicate name data.
                    this.Repository.ExtensionMethodBaseRepository.GetSelectedExtensionMethodBases(),
                    this.Repository.ExtensionMethodBaseExtensionRepository.GetAllIgnoredExtensionMethodBaseNamespacedTypeNames(),
                    this.Repository.ExtensionMethodBaseExtensionRepository.GetAllExtensionMethodBaseExtensions(),
                    this.Repository.ExtensionMethodBaseExtensionRepository.GetAllToExtensionMethodBaseMappings(),
                    this.Repository.ExtensionMethodBaseExtensionRepository.GetAllToProjectMappings());

            // There are extension method bases for which all extensions should be ignored for this purpose.
            var ignoredEmbsHash = new HashSet<string>(ignoredEmbs);

            var selectedEmbsForEmbExtensions = selectedEmbs
                .ExceptWhere(x => ignoredEmbsHash.Contains(x.NamespacedTypeName))
                ;

            var tuples = Instances.Operation.GetEmbExtensionEmbAndProjectTuples(
                embExtensions,
                selectedEmbsForEmbExtensions,
                selectedProjects,
                toEmbMappings,
                toProjectMappings);

            var embExtensionFunctionalityNames = Instances.Operation.GetEmbExtensionFunctionalityNames(tuples);
            var embExtensionFunctionalityLines = embExtensionFunctionalityNames
                .Select(x => x.ToTokenizedRepresentation())
                .OrderAlphabetically()
                ;

            var outputFilePath = @"C:\Temp\Extension Method Base Extensions-Functionality.txt";

            await FileHelper.WriteAllLines(
                outputFilePath,
                embExtensionFunctionalityLines);

            await this.NotepadPlusPlusOperator.OpenFilePath(outputFilePath);
        }
    }
}
