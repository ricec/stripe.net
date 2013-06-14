using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Stripe
{
    [JsonObject]
    public class StripeList<T> : IEnumerable<T>
    {
        [JsonProperty("data")]
        public List<T> Data { get; set; }

        public IEnumerator<T> GetEnumerator()
        {
            return Data.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return ((System.Collections.IEnumerable)Data).GetEnumerator();
        }
    }
}
