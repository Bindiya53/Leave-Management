﻿using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI;

namespace Leave_Management.Leave_Management.Web.Configurations
{
    public class EmailSender : IEmailSender
    {
        //public string SendGridSecret { get; set; }

        // public EmailSender(IConfiguration _config) {
        //     SendGridSecret = _config.GetValue<string>("SendGrid:SecretKey");
        // }

        // public Task SendEmailAsync(string email, string subject, string htmlMessage) {
        //     //logic to send email

        //     var client = new SendGridClient(SendGridSecret);

        //     var from = new EmailAddress("hello@dotnetmastery.com", "Bulky Book");
        //     var to = new EmailAddress(email);
        //     var message = MailHelper.CreateSingleEmail(from, to, subject, "", htmlMessage);
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            return Task.CompletedTask;
        }
    }
}


