// The MIT License (MIT)

// Copyright (c) 2015 anton(at)logikfabrik.se

// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:

// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.

// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using Logikfabrik.Umbraco.Jet.Web.Data.Converters;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace Logikfabrik.Umbraco.Jet.Maps
{
    public class GeoCoordinatesPropertyValueConverter : IPropertyValueConverter
    {
        public bool CanConvertValue(string uiHint, Type from, Type to)
        {
            return uiHint == GeoCoordinates.Editor && from == typeof(JObject) && to == typeof(GeoCoordinates);
        }

        public object Convert(object value, Type to)
        {
            if (value == null)
                return new GeoCoordinates();

            var json = JsonConvert.DeserializeObject<dynamic>(value.ToString());

            throw new NotImplementedException();
        }
    }
}
