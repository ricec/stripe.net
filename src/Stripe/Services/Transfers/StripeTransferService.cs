using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Stripe
{
    public class StripeTransferService
    {
        private string ApiKey { get; set; }

        public enum StripeTransferStatus
        {
            pending,
            paid,
            failed,
            all
        }

        public StripeTransferService(string apiKey = null)
        {
            ApiKey = apiKey;
        }

        public virtual IEnumerable<StripeTransfer> List(int count = 10, int offset = 0, string recipientId = null, StripeTransferStatus status = StripeTransferStatus.all, StripeDateRange date = null)
        {
            var url = Urls.Transfers;
            url = ParameterBuilder.ApplyParameterToUrl(url, "count", count.ToString());
            url = ParameterBuilder.ApplyParameterToUrl(url, "offset", offset.ToString());

            if (!string.IsNullOrEmpty(recipientId))
                url = ParameterBuilder.ApplyParameterToUrl(url, "recipient", recipientId);

            if (status != StripeTransferStatus.all)
                url = ParameterBuilder.ApplyParameterToUrl(url, "status", status.ToString());

            if (date != null)
                url = date.ApplyQueryStringParams(url, "date");

            var response = Requestor.GetString(url, ApiKey);

            return Mapper<StripeTransfer>.MapCollectionFromJson(response);
        }
    }
}
