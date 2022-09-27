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

        public void SendOtpEmail(string otp, string emailTo)
        {
            var otpEmail = new OtpEmail
            {
                Otp = otp,
                RecipientMail=emailTo
            };
            var mail = new Mail<OtpEmail>("OtpEmail", otpEmail);
            lock (MailServiceLock)
            {
                var sentMailData = mail.SendAsync(otpEmail.RecipientMail, "Welcome to Flinnt");
                //_mailHistoryService.InsertMailHistory(sentMailData.To.ToString(), sentMailData.Subject, sentMailData.Body, MailTypeEnum.Registration.ToString());
            }
        }
    }
}