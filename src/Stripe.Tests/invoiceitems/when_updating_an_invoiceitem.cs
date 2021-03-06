﻿using Machine.Specifications;

namespace Stripe.Tests
{
	public class when_updating_an_invoiceitem
	{
		private static StripeInvoiceItemUpdateOptions StripeInvoiceItemUpdateOptions;
		private static StripeInvoiceItem _stripeInvoiceItem;
		private static string _stripeInvoiceItemId;
		private static StripeInvoiceItemService _stripeInvoiceItemService;

		Establish context = () =>
		{
			var stripeCustomerService = new StripeCustomerService();
            StripeCustomer stripeCustomer = stripeCustomerService.Create(test_data.stripe_customer_create_options.ValidCard()).Await();

			_stripeInvoiceItemService = new StripeInvoiceItemService();
			var _stripeInvoiceItemCreateOptions = test_data.stripe_invoiceitem_create_options.Valid(stripeCustomer.Id);

            StripeInvoiceItem createdInvoice = _stripeInvoiceItemService.Create(_stripeInvoiceItemCreateOptions).Await();
			_stripeInvoiceItemId = createdInvoice.Id;

			StripeInvoiceItemUpdateOptions = test_data.stripe_invoiceitem_update_options.Valid();
		};

		Because of = () =>
            _stripeInvoiceItem = _stripeInvoiceItemService.Update(_stripeInvoiceItemId, StripeInvoiceItemUpdateOptions).Await();

		It should_have_the_correct_amount = () =>
			_stripeInvoiceItem.AmountInCents.ShouldEqual(test_data.stripe_invoiceitem_update_options.Valid().AmountInCents);

		It should_have_the_correct_description = () =>
			_stripeInvoiceItem.Description.ShouldEqual(test_data.stripe_invoiceitem_update_options.Valid().Description);

		It should_have_metadata = () =>
			_stripeInvoiceItem.Metadata.Count.ShouldBeGreaterThan(0);

		It should_have_the_correct_metadata = () =>
			_stripeInvoiceItem.Metadata.ShouldContainOnly(StripeInvoiceItemUpdateOptions.Metadata);
	}
}