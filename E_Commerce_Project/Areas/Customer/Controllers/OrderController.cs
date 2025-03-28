using E_Commerce_Project.Enums;
using E_Commerce_Project.Models;
using E_Commerce_Project.Repository;
using E_Commerce_Project.Repository.Irepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using Stripe.Checkout;
using Stripe.Climate;

namespace E_Commerce_Project.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize]
    public class OrderController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderItemRepository _orderItemRepository;

        public OrderController(UserManager<ApplicationUser> userManager, IOrderRepository orderRepository, IOrderItemRepository orderITemRepository)
        {
            this._userManager = userManager;
            this._orderRepository = orderRepository;
            this._orderItemRepository = orderITemRepository;
        }
        public IActionResult Index()
        {
            var user = _userManager.GetUserId(User);
            var order = _orderRepository.Get(e => e.ApplicationUserId == user && e.status == OrderStatus.Pending, includes: [e => e.ApplicationUser]);
            var approved = _orderRepository.Get(e => e.ApplicationUserId == user && e.status == OrderStatus.Approved, includes: [e => e.ApplicationUser]);
            var waiting = _orderRepository.Get(e => e.ApplicationUserId == user && e.ordershippedstatus == ShippingStatus.Waiting, includes: [e => e.ApplicationUser]);
            var shipped = _orderRepository.Get(e => e.ApplicationUserId == user && e.ordershippedstatus == ShippingStatus.Shipped, includes: [e => e.ApplicationUser]);
            var Arrived = _orderRepository.Get(e => e.ApplicationUserId == user && e.ordershippedstatus == ShippingStatus.Arrived, includes: [e => e.ApplicationUser]);
            var canceled = _orderRepository.Get(e => e.ApplicationUserId == user && e.ordershippedstatus == ShippingStatus.canceled, includes: [e => e.ApplicationUser]);
            ViewBag.approved = approved;
            ViewBag.waiting = waiting;
            ViewBag.shipped = shipped;
            ViewBag.Arrived = Arrived;
            ViewBag.canceled = canceled;

            return View(order.ToList());
        }
        public IActionResult CancelOrder(int OrderId) {
            var order = _orderRepository.GetOne(e => e.Id == OrderId);
            if (order != null && order.PayementStripId != null)
            {
                var service = new SessionService();
                var session = service.Get(order.SessionId);
                var refundoptions = new RefundCreateOptions
                {
                    PaymentIntent = order.PayementStripId,
                    Amount = (long)order.ordertotal,
                    Reason = RefundReasons.RequestedByCustomer,
                };
                var refundservie = new RefundService();
                var refundsession = refundservie.Create(refundoptions);
            }
            order.ordershippedstatus = (order.ordershippedstatus = ShippingStatus.canceled);
            order.status = OrderStatus.Canceled;
            order.PayementStripId = null;
            _orderRepository.Edit(order);

            _orderRepository.comit();
            return RedirectToAction(nameof(Index));


        }

    }
   
        }
    

