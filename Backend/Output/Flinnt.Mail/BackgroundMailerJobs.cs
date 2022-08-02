using Flinnt.Interfaces.Background;
using Flinnt.Mail.Models;
using System;

namespace Flinnt.Mail
{
    public class BackgroundMailerJobs : IBackgroundMailerJobs
    {
        #region Properties

        //ToDo for add mail history
        //private readonly IMailHistoryService _mailHistoryService;
        private static readonly object MailServiceLock = new object();

        #endregion Properties

        #region Constructor

        public BackgroundMailerJobs()
        {
            //_mailHistoryService = mailHistoryService;
        }

        #endregion Constructor

        public void SendWelcomeEmail()
        {
            var welcomeEmailModel = new WelcomeEmail
            {
                RecipientMail = "vaishnanik@gmail.com",
                DisplayName = "Kishan" + " " + "Vaishnani",
            };
            var mail = new Mail<WelcomeEmail>("WelcomeEmail", welcomeEmailModel);
            lock (MailServiceLock)
            {
                var sentMailData = mail.Send(welcomeEmailModel.RecipientMail, "Welcome to Fullstack network");
                //_mailHistoryService.InsertMailHistory(sentMailData.To.ToString(), sentMailData.Subject, sentMailData.Body, MailTypeEnum.Registration.ToString());
            }
        }
    }
}