﻿using System.Collections.Generic;
using System.Linq;

namespace Stripe
{
	public class StripeCustomerService
	{
		private string ApiKey { get; set; }

		public StripeCustomerService(string apiKey = null)
		{
			ApiKey = apiKey;
		}

		public virtual StripeCustomer Create(StripeCustomerCreateOptions createOptions)
		{
			var url = ParameterBuilder.ApplyAllParameters(createOptions, Urls.Customers + "?expand[]=default_card");

			var response = Requestor.PostString(url, ApiKey);

			return Mapper<StripeCustomer>.MapFromJson(response);
		}

		public virtual StripeCustomer Get(string customerId)
		{
			var url = string.Format("{0}/{1}?expand[]=default_card", Urls.Customers, customerId);

			var response = Requestor.GetString(url, ApiKey);

			return Mapper<StripeCustomer>.MapFromJson(response);
		}

		public virtual StripeCustomer Update(string customerId, StripeCustomerUpdateOptions updateOptions)
		{
            var url = string.Format("{0}/{1}?expand[]=default_card", Urls.Customers, customerId);
			url = ParameterBuilder.ApplyAllParameters(updateOptions, url);

			var response = Requestor.PostString(url, ApiKey);

			return Mapper<StripeCustomer>.MapFromJson(response);
		}

		public virtual void Delete(string customerId)
		{
			var url = string.Format("{0}/{1}", Urls.Customers, customerId);

			Requestor.Delete(url, ApiKey);
		}

		public virtual IEnumerable<StripeCustomer> List(int count = 10, int offset = 0)
		{
			var url = Urls.Customers;
			url = ParameterBuilder.ApplyParameterToUrl(url, "count", count.ToString());
			url = ParameterBuilder.ApplyParameterToUrl(url, "offset", offset.ToString());

			var response = Requestor.GetString(url, ApiKey);

			return Mapper<StripeCustomer>.MapCollectionFromJson(response);
		}

		public virtual StripeSubscription UpdateSubscription(string customerId, StripeCustomerUpdateSubscriptionOptions updateOptions)
		{
			var url = string.Format("{0}/{1}/subscription", Urls.Customers, customerId);
			url = ParameterBuilder.ApplyAllParameters(updateOptions, url);

			var response = Requestor.PostString(url, ApiKey);

			return Mapper<StripeSubscription>.MapFromJson(response);
		}

		public virtual StripeSubscription CancelSubscription(string customerId, bool cancelAtPeriodEnd = false)
		{
			var url = string.Format("{0}/{1}/subscription", Urls.Customers, customerId);
			url = ParameterBuilder.ApplyParameterToUrl(url, "at_period_end", cancelAtPeriodEnd.ToString());

			var response = Requestor.Delete(url, ApiKey);

			return Mapper<StripeSubscription>.MapFromJson(response);
		}
	}
}