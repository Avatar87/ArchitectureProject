using Microsoft.AspNetCore.Identity;

namespace AuthServer.Models
{
    public class User : IdentityUser
    {
        /// <summary>
        /// Пароль
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Администратор
        /// </summary>
        public bool IsAdmin { get; set; }

        /// <summary>
        /// Привилегированный
        /// </summary>
        public bool IsPrivileged { get; set; }
    }
}
