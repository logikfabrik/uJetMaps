// <copyright file="GeoCoordinatesDataTypeDefinitionMapping.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Umbraco.Jet.Maps
{
    using System;
    using System.Linq;
    using global::Umbraco.Core;
    using global::Umbraco.Core.Models;
    using Mappings;

    /// <summary>
    /// The <see cref="GeoCoordinatesDataTypeDefinitionMapping" /> class. Mapping for the <see cref="GeoCoordinates" /> class.
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
                : GetDefinition();
        }

        /// <summary>
        /// Gets the data type definition.
        /// </summary>
        /// <returns>The data type definition.</returns>
        private static IDataTypeDefinition GetDefinition()
        {
            return ApplicationContext.Current.Services.DataTypeService.GetDataTypeDefinitionByPropertyEditorAlias(GeoCoordinates.Editor).Single();
        }
    }
}
