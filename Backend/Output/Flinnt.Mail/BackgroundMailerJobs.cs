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

        public void SendOtpEmail(OtpEmail otpEmail)
        {
            var mail = new Mail<OtpEmail>("OtpEmail", otpEmail);
            lock (MailServiceLock)
            {
                var sentMailData = mail.Send(otpEmail.RecipientMail, "Welcome to Flinnt");
                //_mailHistoryService.InsertMailHistory(sentMailData.To.ToString(), sentMailData.Subject, sentMailData.Body, MailTypeEnum.Registration.ToString());
            }
        }
    }
}