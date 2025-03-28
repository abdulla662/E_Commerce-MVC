using E_Commerce_Project.Models;
using E_Commerce_Project.Repository.Irepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;

namespace E_Commerce_Project.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize]
    public class CartController : Controller
    {
        private readonly ICartRepository _CartRepository;
        private readonly UserManager<ApplicationUser> _UserManger;
        private readonly IOrderRepository _OrderRepository;
        private readonly IOrderItemRepository _OrderItemRepository;

        public CartController(ICartRepository cartRepository, UserManager<ApplicationUser> usermanger,IOrderItemRepository orderItemRepository,IOrderRepository orderRepository)
        {
            this._CartRepository = cartRepository;
            this._UserManger = usermanger;
            this._OrderRepository = orderRepository;
            this._OrderItemRepository = orderItemRepository;
        }
        public IActionResult AddToCart(int ProductId, int Count)
        {
            var user = _UserManger.GetUserId(User);
            var cart = new Cart()
            {
                ApplicationUserId = user,
                ProductId = ProductId,
                Count = Count

            };
            var cartDB = _CartRepository.GetOne(e => e.ProductId == ProductId && e.ApplicationUserId == user);
            if (cartDB != null)
            {
                cartDB.Count += Count;
            }
            else
                _CartRepository.Create(cart);
            _CartRepository.comit();
            TempData["Notification"] = "Product Added Succefully";
            return RedirectToAction("Index", "Home", new { Area = "Customer" });

        }
        public IActionResult index()
        {
            var data = _CartRepository.Get(e => e.ApplicationUserId == _UserManger.GetUserId(User), includes: [e => e.Product, e => e.ApplicationUser]);
            var totalprice = data.Sum(e => e.Count*e.Product.Price);
           ViewBag.totalprice=totalprice;
            return View(data);
        }
        public IActionResult increment(int ProductId)
        {
            var user = _UserManger.GetUserId(User);
            var cart = _CartRepository.GetOne(e => e.ApplicationUserId == user && e.ProductId == ProductId);
            if (cart != null)
            {
                cart.Count++;
                _CartRepository.comit();
            }
            return RedirectToAction(nameof(index));
        }
        public IActionResult decrement(int ProductId)
        {
            var user = _UserManger.GetUserId(User);
            var cart = _CartRepository.GetOne(e => e.ApplicationUserId == user && e.ProductId == ProductId);
            if (cart != null && cart.Count > 1)
            {
                cart.Count--;
                _CartRepository.comit();
            }
            return RedirectToAction(nameof(index));
        }
        public IActionResult Delete(int ProductId)
        {
            var user = _UserManger.GetUserId(User);
            var cart = _CartRepository.GetOne(e => e.ApplicationUserId == user && e.ProductId == ProductId);
            if (cart != null)
            {
              _CartRepository.Delete(cart);
                _CartRepository.comit();
                
            }
            return RedirectToAction(nameof(index));
        }
        public IActionResult pay() {
            var username = _UserManger.GetUserName(User);
            var userId = _UserManger.GetUserId(User);
            var cart = _CartRepository.Get(e => e.ApplicationUserId ==userId, includes: [e => e.Product, e => e.ApplicationUser]);
            var order = new Order();
            order.ApplicationUserId = userId;
            order.orderdate = DateTime.Now;
            order.ordertotal = (double)cart.Sum(e => e.Product.Price * e.Count);
            order.Name = username;
            order.status=Enums.OrderStatus.Pending;
            _OrderRepository.Create(order);
            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                LineItems = new List<SessionLineItemOptions>(),
                Mode = "payment",
                SuccessUrl = $"{Request.Scheme}://{Request.Host}/Customer/Checkout/Sucess?orderId={order.Id}",
                CancelUrl = $"{Request.Scheme}://{Request.Host}/Customer/Checkout/cancel",
            };
            foreach (var item in cart) {
                options.LineItems.Add(
                    new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            Currency = "EGP",
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = item.Product.Name,
                                Description = item.Product.Description,
                            },
                            UnitAmount = (long)item.Product.Price,
                        },
                        Quantity =item.Product.Quantity,
                    }

                   );
            }
           
            var service = new SessionService();
            var session = service.Create(options);
            order.SessionId=session.Id;
            _OrderRepository.comit();
            List<OrderItem> orderItems = [];
            foreach (var item in cart)
            {
                var orderitem = new OrderItem
                {
                    OrderId = order.Id,
                    Count = item.Count,
                    price = (double)item.Product.Price,
                    ProductId = item.ProductId,
                };
                orderItems.Add(orderitem);
            }
            _OrderItemRepository.CreateRange(orderItems);
            _OrderItemRepository.comit();
            return Redirect(session.Url);

        }
   
    }
}

