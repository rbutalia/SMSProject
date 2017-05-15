
using System;
using System.Web.Mvc;
using Notifications.Models;
using System.Threading.Tasks;
using Notifications.Services;
using System.Collections.Generic;

namespace Notifications.Controllers
{
    public class NotificationsController : Controller
    {
        private readonly ISubscriberService _subscriberService;
        private readonly INotificationService _notificationService;

        public NotificationsController(ISubscriberService subscriberService, INotificationService notificationService)
        {
            _subscriberService = subscriberService;
            _notificationService = notificationService;
        }

        // GET: Notifications/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Notifications/Create
        [HttpPost]
        public async Task<ActionResult> Create(NotificationViewModel model)
        {
            if (ModelState.IsValid)
            {
                //var model = new 
                var mediaUrl = new List<Uri> { new Uri(model.ImageUrl) };
                var subscribers = _subscriberService.ActiveSubscribersByCompanyID(model.CompanyId);
                foreach (var subscriber in subscribers)
                {
                    await _notificationService.SendMessageAsync(
                        subscriber.PhoneNumber,
                        model.Message,
                        mediaUrl);
                }

                ModelState.Clear();
                ViewBag.FlashMessage = "Messages on their way!";
                return View();
            }
           
            return View(model);
        }
    }
}