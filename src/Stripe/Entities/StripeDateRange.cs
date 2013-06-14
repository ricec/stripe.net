using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Stripe
{
    public class StripeDateRange
    {
        private DateTime? min { get; set; }
        private DateTime? max { get; set; }
        private bool minInclusive { get; set; }
        private bool maxInclusive { get; set; }

        public StripeDateRange EqualTo(DateTime? d)
        {
            min = d;
            max = d;
            minInclusive = maxInclusive = false;
            return this;
        }

        public StripeDateRange GreaterThan(DateTime? d)
        {
            max = d;
            maxInclusive = false;
            return this;
        }

        public StripeDateRange LessThan(DateTime? d)
        {
            min = d;
            minInclusive = false;
            return this;
        }

        public StripeDateRange GreaterThanOrEqualTo(DateTime? d)
        {
            max = d;
            maxInclusive = true;
            return this;
        }

        public StripeDateRange LessThanOrEqualTo(DateTime? d)
        {
            min = d;
            minInclusive = true;
            return this;
        }

        public string ApplyQueryStringParams(string url, string param)
        {
            if (min == max && min.HasValue)
            {
                return ParameterBuilder.ApplyParameterToUrl(url, param, UtcTimestamp(min.Value));
            }

            if (min.HasValue)
            {
                string lt = minInclusive ? "lte" : "lt";
                url = ParameterBuilder.ApplyParameterToUrl(url, string.Format("{0}[{1}]", param, lt), UtcTimestamp(min.Value));
            }

            if (max.HasValue)
            {
                string gt = maxInclusive ? "gte" : "gt";
                url = ParameterBuilder.ApplyParameterToUrl(url, string.Format("{0}[{1}]", param, gt), UtcTimestamp(max.Value));
            }

            return url;
        }

        private string UtcTimestamp(DateTime d)
        {
            var diff = d.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            return ((int)Math.Floor(diff.TotalSeconds)).ToString();
        }
    }
}
