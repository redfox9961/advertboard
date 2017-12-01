using System;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLogic.Model.Dtos;
using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace BusinessLogicLayer.Services
{
    // Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.
    public class ApplicationUserService : UserManager<ApplicationUser>, IApplicationUserService
    {
        public ApplicationUserService(IUserStore<ApplicationUser> userStore)
            : base(userStore)
        {

        }

        public static ApplicationUserService Create(IdentityFactoryOptions<ApplicationUserService> options, IOwinContext context)
        {
            var manager = (ApplicationUserService)CommonContainer.CommonContainer.Instance.Resolve<IApplicationUserService>();

            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<ApplicationUser>(manager)
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
            manager.RegisterTwoFactorProvider("Phone Code", new PhoneNumberTokenProvider<ApplicationUser>
            {
                MessageFormat = "Your security code is {0}"
            });
            manager.RegisterTwoFactorProvider("Email Code", new EmailTokenProvider<ApplicationUser>
            {
                Subject = "Security Code",
                BodyFormat = "Your security code is {0}"
            });
     
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider =
                    new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }

        /// <summary>
        /// Зарегистрировать нового пользователя
        /// </summary>
        /// <param name="dto">модель создания нового пользователя</param>
        /// <returns>успех или кидает ошибку</returns>
        public async Task<IdentityResult> Register(RegisterUserDto dto)
        {
            var user = Mapper.Map<ApplicationUser>(dto);
            return await CreateAsync(user, dto.Password);
        }
    }
}
