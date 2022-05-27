using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace RJHWebsite.Pages.Contact
{
    public class ContactModel : PageModel
    {
        public bool FormIsSubmitted { get; set; }
        [BindProperty, Required, DisplayName("full name"), StringLength(200, MinimumLength = 2, ErrorMessage = "Full name must be between 2 and 200 characters")]
        public string FullName { get; set; }
        [Required, BindProperty, DisplayName("email address"),]
        public string EmailAddress { get; set; }
        [Required, BindProperty, DisplayName("subject"), StringLength(120, MinimumLength = 2, ErrorMessage = "Subject must be between 2 and 120 characters")]
        public string Subject { get; set; }
        [BindProperty, Required, DisplayName("email body"), StringLength(600, MinimumLength = 10, ErrorMessage = "Email body must be between 10 and 400 characters")]
        public string Body { get; set; }
        public void OnGet(string subject)
        {
            Subject = subject != null ? subject.Replace("-", " ") : null;
        }
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> OnPost() 
        {
            if (ModelState.IsValid)
            {
                FormIsSubmitted = true;
                string fromEmail = ConfigurationManager.AppSettings["fromEmailAddress"];
                string toEmail = ConfigurationManager.AppSettings["toEmailAddress"];
                var apiKey = ConfigurationManager.AppSettings["sendGridAPIKey"];
                var client = new SendGridClient(apiKey);

                List<string> toEmails = new List<string>() { toEmail, "danielwood1412@gmail.com" };
                foreach(string email in toEmails)
                {
                    var msg = new SendGridMessage()
                    {
                        From = new EmailAddress(fromEmail, "No Reply"),
                        Subject = Subject + " - Website Enquiry",
                        PlainTextContent = "Enquiry from " + FullName + "\n\nEmail address: " + EmailAddress + "\n\nMessage: " + Body
                    };
                    msg.AddTo(new EmailAddress(email, "RJH Sales"));
                    var response = await client.SendEmailAsync(msg);
                }
                FullName = EmailAddress = Subject = Body = "";
            }
            return Page();
        }
    }
}
