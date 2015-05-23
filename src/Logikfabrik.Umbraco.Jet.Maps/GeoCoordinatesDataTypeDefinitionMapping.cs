//----------------------------------------------------------------------------------
// <copyright file="GeoCoordinatesDataTypeDefinitionMapping.cs" company="Logikfabrik">
//     The MIT License (MIT)
//
//     Copyright (c) 2015 anton(at)logikfabrik.se
//
//     Permission is hereby granted, free of charge, to any person obtaining a copy
//     of this software and associated documentation files (the "Software"), to deal
//     in the Software without restriction, including without limitation the rights
//     to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//     copies of the Software, and to permit persons to whom the Software is
//     furnished to do so, subject to the following conditions:
//
//     The above copyright notice and this permission notice shall be included in
//     all copies or substantial portions of the Software.
//
//     THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//     IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//     FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//     AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//     LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//     OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
//     THE SOFTWARE.
// </copyright>
//----------------------------------------------------------------------------------

namespace Logikfabrik.Umbraco.Jet.Maps
{
    using System;
    using System.Linq;
    using Mappings;
    using global::Umbraco.Core;
    using global::Umbraco.Core.Models;
    
    /// <summary>
    /// Data type definition mapping for geographical coordinates.
    /// </summary>
    public class GeoCoordinatesDataTypeDefinitionMapping : DataTypeDefinitionMapping
    {
        /// <summary>
        /// Gets supported types.
        /// </summary>
        protected override Type[] SupportedTypes
        {
            get { return new[] { typeof(GeoCoordinates) }; }
        }

        /// <summary>
        /// Gets a mapped data type definition.
        /// </summary>
        /// <param name="fromType">Type to get a mapped definition for.</param>
        /// <returns>A data type definition.</returns>
        public override IDataTypeDefinition GetMappedDefinition(Type fromType)
        {
            return !this.CanMapToDefinition(fromType)
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
                throw new ArgumentException("Editor cannot be null or white space.", "editor");
            }
                
            return ApplicationContext.Current.Services.DataTypeService.GetDataTypeDefinitionByPropertyEditorAlias(editor).Single();
        }
    }
}
