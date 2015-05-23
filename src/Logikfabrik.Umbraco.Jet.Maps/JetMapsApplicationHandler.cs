//----------------------------------------------------------------------------------
// <copyright file="JetMapsApplicationHandler.cs" company="Logikfabrik">
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
    using System.Collections.Generic;
    using Mappings;
    using global::Umbraco.Core;
    using Web.Data.Converters;

    /// <summary>
    /// Application handler responsible for setting up the geographical coordinates data type.
    /// </summary>
    public class JetMapsApplicationHandler : JetApplicationHandler
    {
        /// <summary>
        /// Sync lock.
        /// </summary>
        private static readonly object Lock = new object();

        /// <summary>
        /// Whether or not the application handler has finished its configuration.
        /// </summary>
        private static bool configured;

        /// <summary>
        /// Method called when the application is starting.
        /// </summary>
        /// <param name="umbracoApplication">An application.</param>
        /// <param name="applicationContext">A application context.</param>
        public override void OnApplicationStarting(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            if (!this.IsInstalled)
            {
                return;
            }

            if (configured)
            {
                return;
            }
                
            lock (Lock)
            {
                DataTypeDefinitionMappings.Mappings.Add(typeof(GeoCoordinates), new GeoCoordinatesDataTypeDefinitionMapping());

                IEnumerable<IPropertyValueConverter> converters;

                if (PropertyValueConverters.Converters.TryGetValue(typeof(GeoCoordinates), out converters))
                {
                    PropertyValueConverters.Converters.Remove(typeof(GeoCoordinates));
                    PropertyValueConverters.Converters.Add(
                        typeof(GeoCoordinates),
                        new List<IPropertyValueConverter>(converters) { new GeoCoordinatesPropertyValueConverter() });
                }
                else
                {
                    PropertyValueConverters.Converters.Add(
                        typeof(GeoCoordinates),
                        new[] { new GeoCoordinatesPropertyValueConverter() });
                }
                
                configured = true;
            }
        }
    }
}
