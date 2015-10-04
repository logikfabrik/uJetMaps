// <copyright file="GeoCoordinates.cs" company="Logikfabrik">
//   Copyright (c) 2015 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Umbraco.Jet.Maps
{
    using System;

    /// <summary>
    /// Geographical coordinates.
    /// </summary>
    [DataType(typeof(GeoCoordinates), Editor)]
    public class GeoCoordinates
    {
        /// <summary>
        /// Editor alias for the geographical coordinates data type.
        /// </summary>
        internal const string Editor = "Logikfabrik.Umbraco.Jet.Maps";

        /// <summary>
        /// Latitude coordinate.
        /// </summary>
        private double _lat;

        /// <summary>
        /// Longitude coordinate.
        /// </summary>
        private double _lng;

        /// <summary>
        /// Gets or sets the latitude in degrees.
        /// </summary>
        public double Lat
        {
            get
            {
                return _lat;
            }

            set
            {
                if (value < -90 || value > 90)
                {
                    throw new ArgumentOutOfRangeException(nameof(value));
                }

                _lat = value;
            }
        }

        /// <summary>
        /// Gets or sets the longitude in degrees.
        /// </summary>
        public double Lng
        {
            get
            {
                return _lng;
            }

            set
            {
                if (value < -180 || value > 100)
                {
                    throw new ArgumentOutOfRangeException(nameof(value));
                }

                _lng = value;
            }
        }
    }
}