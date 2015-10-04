// <copyright file="GeoCoordinatesDataTypeDefinitionMapping.cs" company="Logikfabrik">
//   Copyright (c) 2015 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Umbraco.Jet.Maps
{
    using System;
    using System.Linq;
    using global::Umbraco.Core;
    using global::Umbraco.Core.Models;
    using Mappings;

    /// <summary>
    /// Data type definition mapping for geographical coordinates.
    /// </summary>
    public class GeoCoordinatesDataTypeDefinitionMapping : DataTypeDefinitionMapping
    {
        /// <summary>
        /// Gets supported types.
        /// </summary>
        protected override Type[] SupportedTypes => new[] { typeof(GeoCoordinates) };

        /// <summary>
        /// Gets a mapped data type definition.
        /// </summary>
        /// <param name="fromType">Type to get a mapped definition for.</param>
        /// <returns>A data type definition.</returns>
        public override IDataTypeDefinition GetMappedDefinition(Type fromType)
        {
            return !CanMapToDefinition(fromType)
                ? null
                : GetDefinition(GeoCoordinates.Editor);
        }

        /// <summary>
        /// Gets a data type definition.
        /// </summary>
        /// <param name="editor">The editor alias to use for getting a data type definition.</param>
        /// <returns>A data type definition.</returns>
        private static IDataTypeDefinition GetDefinition(string editor)
        {
            if (string.IsNullOrWhiteSpace(editor))
            {
                throw new ArgumentException("Editor cannot be null or white space.", nameof(editor));
            }

            return ApplicationContext.Current.Services.DataTypeService.GetDataTypeDefinitionByPropertyEditorAlias(editor).Single();
        }
    }
}
