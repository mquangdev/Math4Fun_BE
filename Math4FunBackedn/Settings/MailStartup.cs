using Jhipster.Infrastructure.Configuration;

namespace Math4FunBackedn.Settings
{
    public static class MailConfiguration
    {
        public static IServiceCollection AddMailModule(this IServiceCollection services, IConfiguration configuration)
        {
            // Use this to load settings from appSettings file
            services.AddTransient<IEmailSender, MailKitEmailSender>();
            services.Configure<MailKitEmailSenderOptions>(options =>
            {
                options.Host_Address = configuration["ExternalProviders:MailKit:SMTP:Address"];
                options.Host_Port = Convert.ToInt32(configuration["ExternalProviders:MailKit:SMTP:Port"]);
                options.Host_Username = configuration["ExternalProviders:MailKit:SMTP:Account"];
                options.Host_Password = configuration["ExternalProviders:MailKit:SMTP:Password"];
                options.Sender_EMail = configuration["ExternalProviders:MailKit:SMTP:SenderEmail"];
                options.Sender_Name = configuration["ExternalProviders:MailKit:SMTP:SenderName"];
                options.ClientId = configuration["ExternalProviders:MailKit:SMTP:ClientID"];
                options.ClientSecret = configuration["ExternalProviders:MailKit:SMTP:ClientSecret"];
                options.SendMailTo = configuration["ExternalProviders:MailKit:SMTP:SendMailTo"];
            });
            return services;

        }
    }
}
