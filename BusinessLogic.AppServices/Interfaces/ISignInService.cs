using System;
using System.Threading.Tasks;
using BusinessLogic.Model.Dtos;

namespace BusinessLogicLayer.Interfaces
{
    /// <summary>
    /// Сервис 
    /// </summary>
    public interface ISignInService: IDisposable
    {
        Task SignIn(RegisterUserDto user, bool isPersistent, bool rememberBrowser);
    }
}
