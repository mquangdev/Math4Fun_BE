using Math4FunBackedn.DBContext;
using Math4FunBackedn.Entities;
using Math4FunBackedn.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Xml.Linq;

namespace Math4FunBackedn.Repositories.MailRepo
{
    public class MailRepository: IMailRepository
    {
        private readonly IEmailSender _emailSender;
        private readonly IConfiguration _configuration;
        private readonly MyDbContext _context;
        public MailRepository(IEmailSender emailSender, IConfiguration configuration, MyDbContext iContext)
        {
            _emailSender = emailSender;
            _configuration = configuration;
            _context = iContext;
        }
        public virtual async Task SendPasswordResetMail([FromQuery(Name = "email")] string email)
        {
            var otp = GenerateOTP(6);
            string decodedEmail = Uri.UnescapeDataString(email);
            var user = await _context.User.FirstOrDefaultAsync(u => u.Email == decodedEmail);
            var account = await _context.Account.FirstOrDefaultAsync(acc => acc.Email == decodedEmail);
            //var temp = _configuration.GetValue<string>("EmailTemplate:PasswordReset");
            string body = $"<div>\r\n  <div style=\"font-style: italic; font-weight: 500; font-size: 14px\">\r\n    Xin chào {user.Fullname}, Đây là tin nhắn tự động từ hệ thống Math4Fun\r\n  </div>\r\n  <div style=\"display: flex; align-items: center;\">\r\n    <div style=\"color: red; font-size: 16px; margin-right: 4px;\">OTP của bạn:</div>\r\n    {otp}\r\n  </div>\r\n  <div style=\"color: red\">*Lưu ý: Mã OTP sẽ hết hạn trong 60s</div>\r\n</div>";
            account.Otp = otp;
            await _context.SaveChangesAsync();
            await _emailSender.SendEmailAsync(decodedEmail, "Email xác thực quên mật khẩu", body);
        }
        public async Task<bool> CheckOTP([FromQuery(Name = "email")] string email, string otp)
        {
            string decodedEmail = Uri.UnescapeDataString(email);
            var account = await _context.Account.FirstOrDefaultAsync(acc => acc.Email == decodedEmail);
            if(account == null)
            {
                throw new Exception("Không tìm thấy tài khoản");
            }
            if (account.Otp == otp)
            {
                return true;
            }
            return false;
        }

        public static string GenerateOTP(int length)
        {
            Random random = new Random();
            const string characters = "0123456789";
            string otp = new string(Enumerable.Repeat(characters, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
            return otp;
        }
    }
}
