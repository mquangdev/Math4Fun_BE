namespace Math4FunBackedn.Repositories.MailRepo
{
    public interface IMailRepository
    {
        Task SendPasswordResetMail(string email);
        Task<bool> CheckOTP(string email, string otp);
    }
}
