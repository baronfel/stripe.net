﻿using System.Collections.Generic;
using System.Linq;
using Machine.Specifications;

namespace Stripe.Tests
{
	public class when_listing_coupons
	{
		private static IEnumerable<StripeCoupon> _stripeCouponList;
		private static StripeCouponService _stripeCouponService;

		Establish context = () =>
		{
			_stripeCouponService = new StripeCouponService();

            _stripeCouponService.Create(test_data.stripe_coupon_create_options.Valid()).Await();
            _stripeCouponService.Create(test_data.stripe_coupon_create_options.Valid()).Await();
            _stripeCouponService.Create(test_data.stripe_coupon_create_options.Valid()).Await();
            _stripeCouponService.Create(test_data.stripe_coupon_create_options.Valid()).Await();
		};

		Because of = () =>
            _stripeCouponList = _stripeCouponService.List().Result;

		It should_have_atleast_4_entries = () =>
			_stripeCouponList.Count().ShouldBeGreaterThanOrEqualTo(4);
	}
}