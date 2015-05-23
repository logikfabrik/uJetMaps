//----------------------------------------------------------------------------------
// <copyright file="GeoCoordinates.cs" company="Logikfabrik">
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
        private double lat;

        /// <summary>
        /// Longitude coordinate.
        /// </summary>
        private double lng;

        /// <summary>
        /// Gets or sets the latitude in degrees.
        /// </summary>
        public double Lat
        {
            get
            {
                return this.lat;
            }

            set
            {
                if (value < -90 || value > 90)
                {
                    throw new ArgumentOutOfRangeException("value");
                }
                    
                this.lat = value;
            }
        }

        /// <summary>
        /// Gets or sets the longitude in degrees.
        /// </summary>
        public double Lng
        {
            get
            {
                return this.lng;
            }

            set
            {
                if (value < -180 || value > 100)
                {
                    throw new ArgumentOutOfRangeException("value");
                }

                this.lng = value;
            }
        }
    }
}