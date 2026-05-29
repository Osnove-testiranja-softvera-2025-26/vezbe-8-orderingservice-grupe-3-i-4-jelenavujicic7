using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingService.Test
{
    [TestFixture]
    public class CalculationServiceTest
    {
        private FakeOrderService fakeOrderService;
        private FakeCouponService fakeCouponService;
        private FakeLoggerService fakeLoggerService;
        private CalculationService calculationService;

        [SetUp]
        public void SetUp()
        {
            fakeOrderService=new FakeOrderService();
            fakeCouponService=new FakeCouponService();
            fakeLoggerService=new FakeLoggerService();
            calculationService=new CalculationService(fakeOrderService, fakeCouponService);
            calculationService.LoggerService=fakeLoggerService;
        }
        [TestCase(5,400,300,false,true)]
        [TestCase(-3, 400, 300, false, false)]
        [TestCase(5, 200, 300, false, false)]
        [TestCase(5, 400, 300, true, false)]

        public void CheckCouponValidaty_CouponValid_Success(int expirtionDateHours,double orderTotal, double couponMinimalRequiredOrderTotal,bool couponUsed,bool expected)
        {
            fakeOrderService.Orders = new List<Order>
            {
                new Order
                {
                    Total=orderTotal,
                }
            };
            fakeCouponService.Coupon = new Coupon
            {
                ExpirationDate = DateTime.Now.Addhours(expirtionDateHours),
                MinimalRequiredOrderTotal = couponMinimalRequiredOrderTotal,
                Used = couponUsed
            };
            bool actual = calculationService.CheckCouponValidaty(Guid.NewGuid(), Guid.NewGuid());
            Assert.AreEqual(expected, actual);
        }

    }
}
