using System;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLogic.Model.Dtos;
using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Entities;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;

namespace BusinessLogicLayer.Services
{
    // Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.
    public class ApplicationSignInService : SignInManager<ApplicationUser, string>, ISignInService
    {
        public ApplicationSignInService(ApplicationUserService userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(ApplicationUser user)
        {
            return user.GenerateUserIdentityAsync((ApplicationUserService)UserManager); ;
        }

        public static ApplicationSignInService Create(IdentityFactoryOptions<ApplicationSignInService> options, IOwinContext context)
        {
            return new ApplicationSignInService(context.GetUserManager<ApplicationUserService>(), context.Authentication);
        }

        public Task SignIn(RegisterUserDto userDto, bool isPersistent, bool rememberBrowser)
        {
            var applicationUser = Mapper.Map<RegisterUserDto, ApplicationUser>(userDto);
            return SignInAsync(applicationUser, isPersistent, rememberBrowser);
        }

        public Task<SignInStatus> PasswordSignIn(string userName, string password, bool isPersistent,
            bool shouldLockout)
        {
            return PasswordSignInAsync(userName, password, isPersistent, shouldLockout);
        }

        public Task<bool> HasBeenVerified()
        {
            return HasBeenVerifiedAsync();
        }
        
    }
}
