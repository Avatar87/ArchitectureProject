namespace AuthServer.Models
{
    public class Game
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Игроки
        /// </summary>
        public IEnumerable<User> Users { get; set; }
    }
}
