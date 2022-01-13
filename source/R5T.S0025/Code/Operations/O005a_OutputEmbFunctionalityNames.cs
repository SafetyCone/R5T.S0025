using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using R5T.D0105;
using R5T.T0020;
using R5T.T0092.X001;


namespace R5T.S0025
{
    /// <summary>
    /// Create tuples of (EMB extension, EMB, project), filter by selected project and selected EMB, then output all functionality names to a file.
    /// Functionality names are: {EMB}_{EMB extension method name}_{project name}.
    /// </summary>
    [OperationMarker]
    public class O005a_OutputEmbFunctionalityNames : IActionOperation
    {
        private IListingFilePathProvider ListingFilePathProvider { get; }
        private INotepadPlusPlusOperator NotepadPlusPlusOperator { get; }
        private IRepository Repository { get; }


        public O005a_OutputEmbFunctionalityNames(
            IListingFilePathProvider listingFilePathProvider,
            INotepadPlusPlusOperator notepadPlusPlusOperator,
            IRepository repository)
        {
            this.ListingFilePathProvider = listingFilePathProvider;
            this.NotepadPlusPlusOperator = notepadPlusPlusOperator;
            this.Repository = repository;
        }

        public async Task Run()
        {
            // Get extension method base functionality names (with paired EMB extension identity).
            var (
                projects,
                extensionMethodBases,
                extensionMethodBaseExtensions,
                toProjectMappings,
                toExtensionMethodBaseMappings
                )
                = await this.Repository.GetExtensionMethodBaseFunctionalityData();

            var tuples = Instances.Operation.GetExtensionMethoBaseExtensionTuples(
                projects,
                extensionMethodBases,
                extensionMethodBaseExtensions,
                toProjectMappings,
                toExtensionMethodBaseMappings);

            var embExtensionFunctionalityNames = Instances.Operation.GetEmbExtensionFunctionalityNames(tuples);

            var embExtensionFunctionalityLines = embExtensionFunctionalityNames
                .Select(x => x.ToTokenizedRepresentation())
                .OrderAlphabetically()
                ;

            var listingFilePath = await this.ListingFilePathProvider.GetListingFilePath();

            await FileHelper.WriteAllLines(
                listingFilePath,
                embExtensionFunctionalityLines);

            await this.NotepadPlusPlusOperator.OpenFilePath(listingFilePath);
        }
    }
}
