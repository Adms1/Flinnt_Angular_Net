namespace Flinnt.Interfaces.Background
{
    public interface IBackgroundMailerJobs : IBackgroundJobs
    {
        void SendOtpEmail(string otp, string emailTo);
    }
}