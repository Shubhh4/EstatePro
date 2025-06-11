using EstatePro.Models;
using EstatePro.Repository;
using Microsoft.AspNetCore.Mvc;
using Razorpay.Api;

namespace EstatePro.Controllers
{
    public class BookingController : Controller
    {
        IBookingRepository bookingRepository;
        public BookingController(IBookingRepository bookingRepository)
        {
            this.bookingRepository = bookingRepository;
        }

        // for buyer only
        public IActionResult Index()
        {
            //var role = HttpContext.Session.GetString("UserRole");
            //if(role != "Buyer") return Unauthorized();

            //var UserId = HttpContext.Session.GetInt32("UserID");
            //if (UserId == null)  return Unauthorized();

            int id = (int)HttpContext.Session.GetInt32("UserId"); // remove when sahil adds session 7,8
            var data = bookingRepository.GetBookingsByUserId(id);
            return View(data);
        }

        public IActionResult CreateOrder(int bookingId)
        {
            //var role = HttpContext.Session.GetString("UserRole");
            //if (role != "Buyer") return Unauthorized();

            var booking = bookingRepository.GetBookingById(bookingId);
            if (booking == null || booking.Status != "Confirmed") return NotFound();

            var amount = booking.Property.Price * 100; // Convert to paisa

            RazorpayClient client = new RazorpayClient("rzp_test_Kl7588Yie2yJTV", "6dN9Nqs7M6HPFMlL45AhaTgp");

            Dictionary<string, object> options = new()
            {
                { "amount", amount },
                { "currency", "INR" },
                { "receipt", "order_rcptid_" + bookingId },
                { "payment_capture", 1 }
            };

            var order = client.Order.Create(options);
            var orderId = order["id"].ToString();

            return Json(new
            {
                key = "rzp_test_Kl7588Yie2yJTV",
                amount = amount,
                orderId = orderId
            });
        }

        public ActionResult PaymentSuccess(int bookingId, string paymentId)
        {
            bookingRepository.MarkBookingAsPaid(bookingId, paymentId);

            // Get the booking again to access the Property
            var booking = bookingRepository.GetBookingById(bookingId);
            if (booking != null && booking.Property != null)
            {
                booking.Property.Status = PropertyStatus.Sold;

                // Assuming you update Property via BookingRepository or a PropertyRepository
                bookingRepository.UpdatePropertyStatus(booking.Property);
            }

            return RedirectToAction("Index", "Booking");
        }


        public IActionResult Delete(int id)
        {
            //var role = HttpContext.Session.GetString("UserRole");
            //if(role != "Admin" && role !="Agent" && role != "Buyer") return Unauthorized();

            bookingRepository.DeleteBooking(id);
            return RedirectToAction("Index", "Booking");
        }

        // for admin and agent only
        public IActionResult BookingRequests()
        {
            //var role = HttpContext.Session.GetString("UserRole");
            //if (role != "Admin" && role != "Agent") return Unauthorized();

            var data = bookingRepository.GetAllBooking();
            return View(data);
        }

        public IActionResult DeleteRequests(int id)
        {
            //var role = HttpContext.Session.GetString("UserRole");
            //if(role != "Admin" && role !="Agent" && role != "Buyer") return Unauthorized();

            bookingRepository.DeleteBooking(id);
            return RedirectToAction("BookingRequests", "Booking");
        }

        public IActionResult Reject(int id)
        {
            //var role = HttpContext.Session.GetString("UserRole");
            //if (role != "Admin" && role != "Agent") return Unauthorized();

            var data = bookingRepository.GetBookingById(id);
            if (data != null)
            {
                data.Status = "Rejected";
                bookingRepository.UpdateBooking(data);
            }
            return RedirectToAction("BookingRequests");
        }

        public IActionResult Approve(int id)
        {
            //var role = HttpContext.Session.GetString("UserRole");
            //if (role != "Admin" && role != "Agent") return Unauthorized();

            var data = bookingRepository.GetBookingById(id);
            if (data != null)
            {
                data.Status = "Confirmed";
                bookingRepository.UpdateBooking(data);
            }
            return RedirectToAction("BookingRequests");
        }

        [HttpGet]
        [HttpGet]
        public IActionResult BookProperty(int propertyId)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (!userId.HasValue)
                return Unauthorized();

            var booking = new Booking
            {
                PropertyId = propertyId,
                UserId = userId.Value,
                Status = "Pending",
                IsPaid = false,
                BookingDate = DateTime.Now
            };

            bookingRepository.AddBooking(booking);

            return RedirectToAction("Index");
        }

    }


}

