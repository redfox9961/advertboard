using System;
using System.Threading.Tasks;
using BusinessLogic.Model.Dtos;
using Microsoft.AspNet.Identity;

namespace BusinessLogicLayer.Interfaces
{
    /// <summary>
    /// Сервис для работы с объявлениями.
    /// </summary>
    public interface IApplicationUserService: IDisposable
    {
        /// <summary>
        /// Зарегистрировать нового пользователя
        /// </summary>
        /// <param name="dto">модель создания нового пользователя</param>
        /// <returns>успех или кидает ошибку</returns>
        Task<IdentityResult> Register(RegisterUserDto dto);
    }
}
