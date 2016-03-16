// <copyright file="JetMapsApplicationHandler.cs" company="Logikfabrik">
//   Copyright (c) 2016 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Umbraco.Jet.Maps
{
    using global::Umbraco.Core;
    using Mappings;
    using Web.Data.Converters;

    /// <summary>
    /// The <see cref="JetMapsApplicationHandler" /> class. Application handler responsible for setting up the geographical coordinates data type.
    /// </summary>
    public class JetMapsApplicationHandler : JetApplicationHandler
    {
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
                DataTypeDefinitionMappingRegistrar.Register<GeoCoordinates>(new GeoCoordinatesDataTypeDefinitionMapping());
                PropertyValueConverterRegistrar.Register<GeoCoordinates>(new GeoCoordinatesPropertyValueConverter());

                configured = true;
            }
        }
    }
}
