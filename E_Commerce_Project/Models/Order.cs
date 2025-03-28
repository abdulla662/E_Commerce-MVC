
using E_Commerce_Project.Enums;
using System.ComponentModel.DataAnnotations;

namespace E_Commerce_Project.Models
    {
        public class Order
        {
            public int Id { get; set; }
            public string? Name { get; set; }
            public string ApplicationUserId { get; set; }
            public ApplicationUser ApplicationUser { get; set; }

            public DateTime orderdate { get; set; }

             public OrderStatus status { get; set; }


            public double ordertotal { get; set; }

            public bool PaymentStatus { get; set; }
        
            public ShippingStatus ordershippedstatus { get; set; }


        [Required(ErrorMessage = "Carrier is required.")]
        public string carrier { get; set; } = string.Empty; 

        [Required(ErrorMessage = "Tracking Number is required.")]
        public string TrackingNumber { get; set; } = string.Empty; 

        public string? SessionId { get; set; }

            public string? PayementStripId { get; set; }

        }
    }
