// <copyright file="GeoCoordinatesPropertyValueConverter.cs" company="Logikfabrik">
//   Copyright (c) 2015 anton(at)logikfabrik.se. Licensed under the MIT license.
// </copyright>

namespace Logikfabrik.Umbraco.Jet.Maps
{
    using System;
    using System.Globalization;
    using Newtonsoft.Json.Linq;
    using Web.Data.Converters;

    /// <summary>
    /// Property value converter for geographical coordinates.
    /// </summary>
    public class GeoCoordinatesPropertyValueConverter : IPropertyValueConverter
    {
        /// <summary>
        /// Gets whether or not this converter can convert a value based on type.
        /// </summary>
        /// <param name="uiHint">A UI hint.</param>
        /// <param name="from">Type to convert from.</param>
        /// <param name="to">Type to convert to.</param>
        /// <returns>True if the converter can convert a value; otherwise false.</returns>
        public bool CanConvertValue(string uiHint, Type from, Type to)
        {
            return uiHint == GeoCoordinates.Editor || to == typeof(GeoCoordinates);
        }

        /// <summary>
        /// Converts the given value.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="to">The type to convert to.</param>
        /// <returns>A converted value.</returns>
        public object Convert(object value, Type to)
        {
            var obj = value as JObject;

            if (obj == null)
            {
                return null;
            }

            double lat;
            double lng;

            var format = CultureInfo.InvariantCulture.NumberFormat;

            if (!double.TryParse(obj.Value<string>("lat"), NumberStyles.AllowDecimalPoint, format, out lat))
            {
                return null;
            }

            return !double.TryParse(obj.Value<string>("lng"), NumberStyles.AllowDecimalPoint, format, out lng)
                ? null
                : new GeoCoordinates { Lat = lat, Lng = lng };
        }
    }
}