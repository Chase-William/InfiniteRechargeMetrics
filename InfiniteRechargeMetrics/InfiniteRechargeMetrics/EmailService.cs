using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace InfiniteRechargeMetrics
{
    public static class EmailService
    {
        public static async Task SendEmail(string subject, string body, List<string> recipients, string imagePath = "")
        {
            try
            {
                ExperimentalFeatures.Enable(ExperimentalFeatures.EmailAttachments, ExperimentalFeatures.ShareFileRequest);

                var message = new EmailMessage
                {
                    Subject = subject,
                    Body = body,
                    To = recipients
                };

                if (imagePath != "")
                    message.Attachments.Add(new EmailAttachment(imagePath));

                await Email.ComposeAsync(message);
            }
            catch
            {
                // Email is not supported on this device
                await App.Current.MainPage.DisplayAlert("Error", "Emailing is not support on this device", "OK");
            }
        }
    }
}
