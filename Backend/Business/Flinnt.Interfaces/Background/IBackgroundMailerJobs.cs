namespace Flinnt.Interfaces.Background
{
    public interface IBackgroundMailerJobs : IBackgroundJobs
    {
        void SendOtpEmail(string otp, string emailTo);
        void SendImportParentSummaryEmail(string message, string emailTo);
        void SendImportStudentSummaryEmail(string message, string emailTo);
    }
}