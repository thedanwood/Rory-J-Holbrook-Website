using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace RJHWebsite.Pages.Construction
{
    public class CompleteEnquiryModel : PageModel
    {
        public class AggregateEnquiryItemModel
        {
            public string AggregateName { get; set; }
            public int Quantity { get; set; }
            public string QuantityUnit { get; set; }
        }
        public List<AggregateEnquiryItemModel> AggregatesInEnquiry { get; set; }
        [BindProperty, Required, DisplayName("full name"), StringLength(200, MinimumLength = 2, ErrorMessage = "Full name must be longer than 2 characters and less than 200 characters")]
        public string FullName { get; set; }
        [Required, BindProperty, DisplayName("email address"),]
        public string EmailAddress { get; set; }
        [Required, BindProperty, DataType(DataType.PostalCode), DisplayName("post code")]
        public string PostCode { get; set; }
        public List<AggregateEnquiryItemModel> getAggregatesInEnquiry()
        {
            List<AggregateEnquiryItemModel> aggregatesInEnquiry = new List<AggregateEnquiryItemModel>();
            if (Request.Cookies["aggregates-rjh"] != null)
            {
                string cookieString = Request.Cookies["aggregates-rjh"];
                string[] cookieArray = cookieString.Split(',');
                AggregatesInEnquiry = new List<AggregateEnquiryItemModel>();
                foreach (string cookie in cookieArray)
                {
                    string[] cookieNameQuantityUnitAndQuantity = cookie.Split('=');
                    string[] cookieNameAndQuantityUnit = cookieNameQuantityUnitAndQuantity[0].Split(':');
                    AggregateEnquiryItemModel model = new AggregateEnquiryItemModel()
                    {
                        AggregateName = cookieNameAndQuantityUnit[0].Replace("_"," ").ToString(),
                        QuantityUnit = cookieNameAndQuantityUnit[1].ToString(),
                        Quantity = int.Parse(cookieNameQuantityUnitAndQuantity[1]),
                    };
                    aggregatesInEnquiry.Add(model);
                }
            }
            return aggregatesInEnquiry;
        }
        public void BindAggregateList()
        {
            AggregatesInEnquiry = getAggregatesInEnquiry();
        }
        public void DeleteAggregateCookie()
        {
            Response.Cookies.Append("aggregates-rjh", "deleted", new CookieOptions
            {
                Expires = DateTime.UtcNow.AddDays(-1)
            });
        }
        public IActionResult OnGet()
        {
            BindAggregateList();
            if (AggregatesInEnquiry.Count() == 0 || AggregatesInEnquiry == null)
            {
                return Redirect(ConfigurationManager.AppSettings["AggregateSupply"]);
            }
            return Page();
        }
        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                List<AggregateEnquiryItemModel> aggregatesInEnquiry = getAggregatesInEnquiry();
                string aggregateEnquiryInfoString = "";
                foreach (AggregateEnquiryItemModel aggregate in aggregatesInEnquiry)
                {
                    aggregateEnquiryInfoString += aggregate.Quantity + " " + aggregate.QuantityUnit + "(s) of " + aggregate.AggregateName + "\n";
                }

                string fromEmail = ConfigurationManager.AppSettings["fromEmailAddress"];
                string toEmail = ConfigurationManager.AppSettings["toEmailAddress"];
                var apiKey = ConfigurationManager.AppSettings["sendGridAPIKey"];
                var client = new SendGridClient(apiKey);

                List<string> toEmails = new List<string>() { toEmail, "danielwood1412@gmail.com" };
                foreach (string email in toEmails)
                {
                    var msg = new SendGridMessage()
                    {
                        From = new EmailAddress(fromEmail, "No Reply"),
                        Subject = "Aggregate Quote - Website Enquiry",
                        PlainTextContent = "Enquiry from " + FullName + "\n\nEmail address: " + EmailAddress + "\n\nPost Code: " + PostCode + "\n\n" + aggregateEnquiryInfoString,
                    };
                    msg.AddTo(new EmailAddress(email, "RJH Sales"));
                    var response = await client.SendEmailAsync(msg);
                }
                

                TempData["EnquirySubmitted"] = true;
                DeleteAggregateCookie();
                return Redirect(ConfigurationManager.AppSettings["AggregateSupply"]);
            }
            BindAggregateList();
            return Page();
        }
    }
}
