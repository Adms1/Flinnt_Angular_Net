using Flinnt.Interfaces.Background;
using Flinnt.Mail.Models;
using System;
using static System.Net.WebRequestMethods;

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

        public void SendImportParentSummaryEmail(string message, string emailTo)
        {
            var parentSummary = new ImportParentSummary
            {
                Message = "Parent import successfully!",
                RecipientMail = emailTo
            };
            var mail = new Mail<ImportParentSummary>("ParentImportEmail", parentSummary);
            lock (MailServiceLock)
            {
                var sentMailData = mail.SendAsync(parentSummary.RecipientMail, "Parent Import Summary");
                //_mailHistoryService.InsertMailHistory(sentMailData.To.ToString(), sentMailData.Subject, sentMailData.Body, MailTypeEnum.Registration.ToString());
            }
        }

        public void SendImportStudentSummaryEmail(string message, string emailTo)
        {
            var studentSummary = new ImportStudentSummary
            {
                Message = "Student import successfully!",
                RecipientMail = emailTo
            };
            var mail = new Mail<ImportStudentSummary>("StudentImportEmail", studentSummary);
            lock (MailServiceLock)
            {
                var sentMailData = mail.SendAsync(studentSummary.RecipientMail, "Student Import Summary");
                //_mailHistoryService.InsertMailHistory(sentMailData.To.ToString(), sentMailData.Subject, sentMailData.Body, MailTypeEnum.Registration.ToString());
            }
        }
    }
}