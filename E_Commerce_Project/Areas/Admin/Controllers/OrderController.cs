using E_Commerce_Project.Enums;
using E_Commerce_Project.Repository.Irepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Stripe;
using Stripe.Checkout;



namespace E_Commerce_Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]

    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderItemRepository _orderItemRepository;
        public OrderController(IOrderRepository orderRepository,IOrderItemRepository orderItemRepository)
        {
            this._orderRepository = orderRepository;
            this._orderItemRepository = orderItemRepository;
        }
        public IActionResult Index()
        {
            var orders = _orderRepository.Get();
            return View(orders.ToList());
        }
        public IActionResult OrderDetails(int OrderId) { 
            var orders = _orderItemRepository.Get(e=> e.OrderId == OrderId, includes:[e=> e.product,e=> e.order]);
            return View(orders.ToList());
          
        }
        public IActionResult statustransform(int OrderId) {
            var order = _orderRepository.GetOne(e=> e.Id== OrderId);
            if (order != null) {
                order.status = (order.status == OrderStatus.Pending) ? OrderStatus.Approved : OrderStatus.Pending;

                _orderRepository.Edit(order);
                _orderRepository.comit(); 
            }
            return RedirectToAction("Index");

        }
        public IActionResult ShipAllOrders(int OrderId, string carrier, string deliveryNumber) {
            var order = _orderRepository.GetOne(e => e.Id == OrderId);
            if (ModelState.IsValid) { 
                if (order != null) {
                order.carrier = carrier;
                order.TrackingNumber = deliveryNumber;
                order.ordershippedstatus = (order.ordershippedstatus == ShippingStatus.Waiting)
                    ? ShippingStatus.Shipped
                    : ShippingStatus.Arrived;
                _orderRepository.Edit(order);
                _orderRepository.comit();
            }
            }
        
            return RedirectToAction("OrderDetails");
        }
        public IActionResult CancelOrder(int OrderId) {
            var order = _orderRepository.GetOne(e => e.Id == OrderId);
            if (order != null)
            {
                if (order.ordershippedstatus == ShippingStatus.Waiting && order.PayementStripId != null) {
                    var service = new SessionService();
                    var session = service.Get(order.SessionId);
                    var refundoptions = new RefundCreateOptions { 
                    PaymentIntent=order.PayementStripId,
                    Amount=(long)order.ordertotal,
                    Reason=RefundReasons.RequestedByCustomer,
                    };
                    var refundservie = new RefundService();
                    var refundsession = refundservie.Create(refundoptions);
                }
            order.ordershippedstatus=(order.ordershippedstatus=ShippingStatus.canceled);
                order.PayementStripId = null;
                _orderRepository.Edit(order);
                
                _orderRepository.comit();

            }
            return RedirectToAction("OrderDetails");

        }
        public IActionResult Delete(int OrderId) { 
        var order=_orderRepository.GetOne(e=>e.Id == OrderId);
        _orderRepository.Delete(order);
            _orderRepository.comit();
            return RedirectToAction("Index");
        }
    }
}
