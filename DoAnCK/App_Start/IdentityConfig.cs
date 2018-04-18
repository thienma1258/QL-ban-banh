using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using project.Models;
using DAL.Models;
using DAL.Context;
using System.Net.Mail;
using System.IO;

namespace project
{
    public class EmailService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // Plug in your email service here to send an email.
            return Task.FromResult(0);
        }
        public static bool sendEmailTocustomer(Bill bill,HttpServerUtilityBase path )
        {
            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
            mail.To.Add(bill.Email);
            mail.From = new MailAddress("thienma1258z@gmail.com", "Confirm your booking hotel", System.Text.Encoding.UTF8);
            mail.Subject = "This is email to cofirm your";
            mail.SubjectEncoding = System.Text.Encoding.UTF8;
            string body = string.Empty;
            using (StreamReader reader = new StreamReader(path.MapPath("~/Views/")+"Confirmemail.html"))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{{now}}", DateTime.Now.ToString());
            body = body.Replace("{{Tongtien}}", bill.Totalprice.ToString());

            body = body.Replace("{{xacnhandonhang}}", "http://localhost:9999/Bill/Confirm/"+bill.confirmEmail);
            var noidung = "";
            foreach(var item in bill.billdetails)
            {
                noidung = noidung + "<tr ><td>" + item.Bakery.Name + "</td>" + "<td>" + item.Bakery.Price + "</td>" + "<td>" + item.quality + "</td></tr>";
            }
            body = body.Replace("{{noidung}}", noidung);
            body = body.Replace("{{name}}", bill.Nameuser);
            mail.Body = body;
            mail.BodyEncoding = System.Text.Encoding.UTF8;
            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.High;
            SmtpClient client = new SmtpClient();
            client.Credentials = new System.Net.NetworkCredential("thienma1258z@gmail.com", "manga24h.com");
            client.Port = 587;
            client.Host = "smtp.gmail.com";
            client.EnableSsl = true;
            try
            {
                client.Send(mail);
                return true;
            }
            catch (Exception ex)
            {
                var ex1 = ex.Message;
                return false;

            }
        }
    }

    public class SmsService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // Plug in your SMS service here to send a text message.
            return Task.FromResult(0);
        }
       
    }

    // Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.
    public class ApplicationUserManager : UserManager<BakeryUser>
    {
        public ApplicationUserManager(IUserStore<BakeryUser> store)
            : base(store)
        {
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context) 
        {
            var manager = new ApplicationUserManager(new UserStore<BakeryUser>(context.Get<BakeryContext>()));
            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<BakeryUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
            };

            // Configure user lockout defaults
            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;

            // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
            // You can write your own provider and plug it in here.
            manager.RegisterTwoFactorProvider("Phone Code", new PhoneNumberTokenProvider<BakeryUser>
            {
                MessageFormat = "Your security code is {0}"
            });
            manager.RegisterTwoFactorProvider("Email Code", new EmailTokenProvider<BakeryUser>
            {
                Subject = "Security Code",
                BodyFormat = "Your security code is {0}"
            });
            manager.EmailService = new EmailService();
            manager.SmsService = new SmsService();
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider = 
                    new DataProtectorTokenProvider<BakeryUser>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }
    }

    // Configure the application sign-in manager which is used in this application.
    public class ApplicationSignInManager : SignInManager<BakeryUser, string>
    {
        public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(BakeryUser user)
        {
            return user.GenerateUserIdentityAsync((ApplicationUserManager)UserManager,user);
        }

        public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context)
        {
            return new ApplicationSignInManager(context.GetUserManager<ApplicationUserManager>(), context.Authentication);
        }
    }
}
