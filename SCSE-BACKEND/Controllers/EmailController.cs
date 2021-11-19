using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Net.Mail;
using SCSE_BACKEND.Models;

namespace SCSE_BACKEND.Controllers
{
    public class EmailController : ApiController
    {
        [HttpPost]
        [Route("send-email")]
        public IHttpActionResult SendMail(EmailClass Email)
        {
            MailMessage mm = new MailMessage
            {
                From = new MailAddress("noreply@scse-vietnam.org")
            };
            mm.To.Add(Email.To.ToString());
            mm.Subject = Email.Subject.ToString();
            mm.Body = Email.Body.ToString();
            mm.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient("mailer-0204.inet.vn")
            {
                UseDefaultCredentials = true,
                Port = 587,
                EnableSsl = true,
                Credentials = new System.Net.NetworkCredential("noreply@scse-vietnam.org", "3M@aile2gd129be6")
            };
            smtp.Send(mm);
            return Ok();
        }
    }
}
