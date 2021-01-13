using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;


namespace Connect4.Extensions
{
    public static class ConversionExtension
    {
        public static ValueConverter DbIntListConverter()
        {
            return new ValueConverter<List<int>, string>(c => string.Join(',', c),
                    c => (c.Split(',', StringSplitOptions.RemoveEmptyEntries)).ParseAll());
        }

        public static ValueConverter JsonConverter<T>()
        {
            return new ValueConverter<T, string>(
                v => JsonConvert.SerializeObject(v, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }),
                v => JsonConvert.DeserializeObject<T>(v, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }));
        }
    }
}
