﻿using Machine.Specifications;

namespace Stripe.Tests
{
	public class when_getting_a_charge
	{
		protected static StripeChargeCreateOptions StripeChargeCreateOptions;
		protected static StripeCharge StripeCharge;
		protected static StripeCard StripeCard;

		private static StripeChargeService _stripeChargeService;
		private static string _createdStripeChargeId;

		Establish context = () =>
		{
			_stripeChargeService = new StripeChargeService();
			StripeChargeCreateOptions = test_data.stripe_charge_create_options.ValidCard();

			StripeCharge stripeCharge = _stripeChargeService.Create(StripeChargeCreateOptions).Await();
			_createdStripeChargeId = stripeCharge.Id;
		};

		Because of = () =>
		{
			StripeCharge = _stripeChargeService.Get(_createdStripeChargeId).Await();
			StripeCard = StripeCharge.StripeCard;
		};

		Behaves_like<charge_behaviors> behaviors;
	}
}