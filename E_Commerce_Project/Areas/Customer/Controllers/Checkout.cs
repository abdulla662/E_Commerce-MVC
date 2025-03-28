using E_Commerce_Project.Enums;
using E_Commerce_Project.Repository.Irepositories;
using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;

namespace E_Commerce_Project.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class Checkout : Controller
    {
        private readonly IOrderRepository _orderRepository;
        public Checkout(IOrderRepository orderRepository)
        {
            this._orderRepository = orderRepository;
        }
        public IActionResult Sucess(int orderId)
        {

           var order= _orderRepository.GetOne(e=> e.Id== orderId);
            if (order != null) {
                var service = new SessionService();
               var session= service.Get(order.SessionId);
                order.PayementStripId=session.PaymentIntentId;
                order.PaymentStatus=true;
                order.status=OrderStatus.Approved;
                _orderRepository.comit();
            }
            return View();
        }
        public IActionResult Cancel(int orderId)
        {
            var order=_orderRepository.GetOne(e=> e.Id== orderId);
            if (order != null && order.status == OrderStatus.Pending) {
                order.status = OrderStatus.Canceled;
                _orderRepository.comit();
            }
            return View();
        }
    }
}
