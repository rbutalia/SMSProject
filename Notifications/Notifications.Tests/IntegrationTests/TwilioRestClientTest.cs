
using Notifications.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Notifications.Tests.IntegrationTests
{
    [TestClass]
    public class TwilioRestClientTest
    {
        [TestMethod]
        public void SendMessageFromTestEnvironment()
        {
            //SendSms(false);
            var fakeNotificationService = new NotificationService();
            fakeNotificationService.SendMessage("+17165414925", "Test message", null);
        }

        [TestMethod]
        public void SendAsyncMessageFromTestEnvironment()
        {
            var fakeNotificationService = new NotificationService();
            fakeNotificationService.SendMessageAsync("+17165414925", "Test async message", null).Wait();
        }

    //    private void SendSms(bool isLive)
    //    {
    //        var accountSid = isLive ?
    //                         ConfigurationManager.AppSettings["Twilio_Live_SID"] :
    //                         ConfigurationManager.AppSettings["Twilio_Test_SID"];
    //        var authToken = isLive ?
    //                        ConfigurationManager.AppSettings["Twilio_Live_AuthToken"] :
    //                        ConfigurationManager.AppSettings["Twilio_Test_AuthToken"];

    //        var twilioAssignedPhoneNumber = isLive ?
    //                                        ConfigurationManager.AppSettings["Twilio_Live_Phone_Number"] :
    //                                        ConfigurationManager.AppSettings["Twilio_Test_Phone_Number"];

    //        TwilioClient.Init(accountSid, authToken);

    //        var message = MessageResource.Create(
    //            to: new PhoneNumber("+16146204276"),
    //            from: new PhoneNumber(twilioAssignedPhoneNumber),
    //            body: "Hello from Twilio");
    //    }
    //    private async Task SendAsyncSms(bool isLive)
    //    {
    //        var accountSid = isLive ? 
    //                         ConfigurationManager.AppSettings["Twilio_Live_SID"] :
    //                         ConfigurationManager.AppSettings["Twilio_Test_SID"];

    //        var authToken = isLive ? 
    //                        ConfigurationManager.AppSettings["Twilio_Live_AuthToken"] :
    //                        ConfigurationManager.AppSettings["Twilio_Test_AuthToken"];

    //        var twilioAssignedPhoneNumber = isLive ? 
    //                                        ConfigurationManager.AppSettings["Twilio_Live_Phone_Number"] : 
    //                                        ConfigurationManager.AppSettings["Twilio_Test_Phone_Number"];

    //        TwilioClient.Init(accountSid, authToken);

    //        var message = await MessageResource.CreateAsync(
    //            to: new PhoneNumber("+17165414925"),
    //            from: new PhoneNumber(twilioAssignedPhoneNumber),
    //            body: "Async Hello from Twilio");
    //    }
    }
}
