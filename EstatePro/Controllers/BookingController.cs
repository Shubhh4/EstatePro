using EstatePro.Models;
using EstatePro.Repository;
using Microsoft.AspNetCore.Mvc;
using Razorpay.Api;

namespace EstatePro.Controllers
{
    public class BookingController : Controller
    {
        // Uncomment this after sameer adds Booking------------->>
        //IUserRepository userRepository;
        //IPropertyRepository propertyRepository;
        //IBookingRepository bookingRepository;

        //public BookingController(IBookingRepository bookingRepository, IPropertyRepository propertyRepository, IUserRepository userRepository)
        //{
        //    this.bookingRepository = bookingRepository;
        //    this.propertyRepository = propertyRepository;
        //    this.userRepository = userRepository;
        //}--------------------------------------------------------x

        // Comment this after sameer adds Booking------->
        IBookingRepository bookingRepository;
        public BookingController(IBookingRepository bookingRepository)
        {
            this.bookingRepository = bookingRepository;
        }
        //-----------------------------------------------x

        public IActionResult Index()
        {
            var data = bookingRepository.GetAllBooking();
            return View(data);
        }

        public IActionResult Delete(int id)
        {
            bookingRepository.DeleteBooking(id);
            return RedirectToAction("Index");
        }

        public IActionResult CreateOrder(int bookingId)
        {
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

        public IActionResult PaymentSuccess(int bookingId, string paymentId)
        {
            bookingRepository.MarkBookingAsPaid(bookingId, paymentId);
            return RedirectToAction("Index", "Booking");
        }

        //public IActionResult PaymentSuccess(int bookingId, string paymentId)
        //{
        //    try
        //    {
        //        bookingRepository.MarkBookingAsPaid(bookingId, paymentId);
        //        TempData["Success"] = "Payment successful!";
        //        return RedirectToAction("PaymentConfirmation", new { id = bookingId });
        //    }
        //    catch (Exception ex)
        //    {
        //        TempData["Error"] = ex.Message;
        //        return RedirectToAction("Details", new { id = bookingId });
        //    }
        //}

        public IActionResult BookingRequests(Booking booking)
        {
            var data = bookingRepository.GetAllBooking().ToList();
            return View(data);
        }

        [HttpPost]
        public IActionResult BookingRequest(Booking booking)
        {
            var data = bookingRepository.GetAllBooking().ToList();
            return View(data);
        }
    }
}
