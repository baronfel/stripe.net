﻿using Machine.Specifications;

namespace Stripe.Tests
{
	public class when_deleting_an_invoiceitem
	{
		private static StripeInvoiceItemService _stripeInvoiceItemService;
		private static string _createdInvoiceItemId;

		Establish context = () =>
		{
			var stripeCustomerService = new StripeCustomerService();
            StripeCustomer stripeCustomer = stripeCustomerService.Create(test_data.stripe_customer_create_options.ValidCard()).Await();

			_stripeInvoiceItemService = new StripeInvoiceItemService();
			var stripeInvoiceItemCreateOptions = test_data.stripe_invoiceitem_create_options.Valid(stripeCustomer.Id);

            StripeInvoiceItem stripeInvoiceItem = _stripeInvoiceItemService.Create(stripeInvoiceItemCreateOptions).Await();
			_createdInvoiceItemId = stripeInvoiceItem.Id;
		};

		Because of = () =>
			_stripeInvoiceItemService.Delete(_createdInvoiceItemId);

		It should_throw_exception_when_getting = () =>
		{
            var exception = Catch.Exception(() => _stripeInvoiceItemService.Get(_createdInvoiceItemId).Await());
			exception.Message.ShouldContain("No such invoiceitem");
		};
	}
}