using Flinnt.Mail.Models;

namespace Flinnt.Interfaces.Background
{
    public interface IBackgroundMailerJobs : IBackgroundJobs
    {
        void SendOtpEmail(OtpEmail otpEmail);
    }
}