// <copyright file="JetMapsApplicationHandler.cs" company="Logikfabrik">
//   Copyright (c) 2015 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Umbraco.Jet.Maps
{
    using System.Collections.Generic;
    using global::Umbraco.Core;
    using Mappings;
    using Web.Data.Converters;

    /// <summary>
    /// The <see cref="JetMapsApplicationHandler" /> class. Application handler responsible for setting up the geographical coordinates data type.
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
            if (!IsInstalled)
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
