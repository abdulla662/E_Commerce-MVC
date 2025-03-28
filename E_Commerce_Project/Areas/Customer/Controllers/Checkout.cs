using E_Commerce_Project.Enums;
using E_Commerce_Project.Models;
using E_Commerce_Project.Repository.Irepositories;
using E_Commerce_Project.Utility;
using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;

namespace E_Commerce_Project.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class Checkout : Controller
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IEmailSender _emailSender;

        public Checkout(IOrderRepository orderRepository,IEmailSender emailsender)
        {
            this._orderRepository = orderRepository;
            this._emailSender = emailsender;
        }
        public async Task<IActionResult>  Sucess(int orderId)
        {

           var order= _orderRepository.GetOne(e=> e.Id== orderId, includes: [e=> e.ApplicationUser]);
            if (order != null) {
                var service = new SessionService();
               var session= service.Get(order.SessionId);
                order.PayementStripId=session.PaymentIntentId;
                order.PaymentStatus=true;
                order.status=OrderStatus.Approved;
                _orderRepository.comit();
                string subject = "Order Confirmation";
                string message = $"Dear Customer, your order #{order.Id} has been approved successfully.";
                await _emailSender.SendEmailAsync(order.ApplicationUser.Email, subject, message);
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
