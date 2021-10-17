﻿using Microsoft.Extensions.Logging;
using Sss.Umb9.Mutobo.Configuration;
using Sss.Umb9.Mutobo.Interfaces;
using Sss.Umb9.Mutobo.PoCo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Web;

namespace Sss.Umb9.Mutobo.Services
{
    public class MailService : BaseService, IMailService
    {

        private readonly IConfigurationService _configurationService;

        public MailService(ILogger<MailService> logger, IUmbracoContextAccessor contextAccessor, IConfigurationService configurationService) : base(logger, contextAccessor)
        {
            _configurationService = configurationService;
        }

        public void SendConfirmationMail(ContactFormData model, IPublishedContent customer)
        {
            var custConfig = new MailConfiguration(customer);

            var strBuilder = new StringBuilder();

            strBuilder.Append("<html>");
            strBuilder.Append("<head>");
            strBuilder.Append("</head>");
            strBuilder.Append("<body>");

            strBuilder.Append(custConfig.MailHeader);
            strBuilder.Append(custConfig.MailBody);
            strBuilder.Append($"<p>Name: {model.Name}<br />");
            strBuilder.Append($"Email: {model.Email}<br />");
            strBuilder.Append($"Nachricht: {model.Comment}<br /></p>");
            strBuilder.Append(custConfig.MailFooter);


            strBuilder.Append("</body>");
            strBuilder.Append("</html>");


            var sendMail = new MailMessage()
            {
                From = new MailAddress(custConfig.SenderMail),
                Subject = custConfig.MailSubject,
                Body = strBuilder.ToString(),
                IsBodyHtml = true
            };

            sendMail.To.Add(model.Email);



            SendMail(sendMail);
        }

        public void SendContactMail(ContactFormData model, IPublishedContent receiver)
        {
            var recConfig = new MailConfiguration(receiver);

            var strBuilder = new StringBuilder();

            strBuilder.Append("<html>");
            strBuilder.Append("<head>");
            strBuilder.Append("</head>");
            strBuilder.Append("<body>");

            strBuilder.Append(recConfig.MailHeader);
            strBuilder.Append(recConfig.MailBody);
            strBuilder.Append($"<p>Name: {model.Name}<br />");
            strBuilder.Append($"Email: {model.Email}<br />");
            strBuilder.Append($"Nachricht: {model.Comment}<br /></p>");
            strBuilder.Append(recConfig.MailFooter);

            strBuilder.Append("</body>");
            strBuilder.Append("</html>");

            var sendMail = new MailMessage()
            {
                From = new MailAddress(recConfig.SenderMail),
                Subject = recConfig.MailSubject,
                Body = strBuilder.ToString(),
                IsBodyHtml = true
            };

            string[] multipleMails = recConfig.ReceiverMail.Split(';');
            foreach (string tmpMails in multipleMails)
            {
                sendMail.To.Add(tmpMails);
            }


            SendMail(sendMail);
        }

        private void SendMail(MailMessage mail)
        {
            var mailServer = _configurationService.GetAppSettingValue("Toolbox.MailServer");
            var smtpClient = new SmtpClient(mailServer);

            smtpClient.Send(mail);
        }
    }
}
