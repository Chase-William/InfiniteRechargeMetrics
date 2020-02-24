using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace InfiniteRechargeMetrics
{
    public static class EmailService
    {
        public static async Task SendEmail(string subject, string body, List<string> recipients)
        {
            try
            {
                var message = new EmailMessage
                {
                    Subject = subject,
                    Body = body,
                    To = recipients
                };
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
