using Flinnt.Business.Helpers;
using Microsoft.IdentityModel.Protocols;
using RazorEngine.Configuration;
using RazorEngine.Templating;
using SendGrid.Helpers.Mail.Model;
using SendGrid.Helpers.Mail;
using SendGrid;
using System;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks; // use TPL for asynchronous operations
using SendGrid; // SendGrid 
using SendGrid.Helpers.Mail; // used for MailHelper

namespace Flinnt.Mail
{
    public class Mail
    {
        private static readonly IRazorEngineService RazorEngine;

        static Mail()
        {
            var config = new TemplateServiceConfiguration
            {
                TemplateManager = new EmbeddedTemplateManager(typeof(Mail).Namespace + ".Templates"),
                Namespaces = { "Flinnt.Mail", "Flinnt.Mail.Models" },
                CachingProvider = new DefaultCachingProvider()
            };
            RazorEngine = RazorEngineService.Create(config);
        }

        public Mail(string templateName)
        {
            TemplateName = templateName;
            ViewBag = new DynamicViewBag();
        }

        public string TemplateName { get; set; }

        public object Model { get; set; }

        public DynamicViewBag ViewBag { get; set; }

        public string GenerateBody()
        {
            var layout = RazorEngine.RunCompile("_Layout", Model.GetType(), Model);
            var body = RazorEngine.RunCompile(TemplateName, Model.GetType(), Model);
            return layout.Replace("{{BODY}}", body);
        }

        public async Task<Response> SendAsync(string to, string subject, string cc = null)
        {
            try
            {
                // Your SendGrid API Key you created above.
                string apiKey = "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";

                // Create an instance of the SendGrid Mail Client using the valid API Key
                var client = new SendGridClient(apiKey);

                // Use the From Email as the Email you verified above
                var senderEmail = new EmailAddress("navin.goradara@admsystems.net", "admsbuild");

                // The recipient of the email
                var recieverEmail = new EmailAddress(to);

                // Define the Email Subject
                string emailSubject = subject;

                // HTML content -> for clients supporting HTML, this is default
                string htmlContent = GenerateBody();

                // construct the email to send via the SendGrid email api
                var msg = MailHelper.CreateSingleEmail(senderEmail, recieverEmail, emailSubject, "", htmlContent);

                // send the email asyncronously
                var resp = await client.SendEmailAsync(msg).ConfigureAwait(false);

                return resp;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }

    public class Mail<TModel> : Mail where TModel : class
    {
        public Mail(string templateName, TModel mailModel) : base(templateName)
        {
            Model = mailModel;
        }
    }
}