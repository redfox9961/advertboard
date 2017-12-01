using Castle.Windsor;

namespace CommonContainer
{
    /// <summary>
    /// Класс общедоступного отовсюду контейнера 
    /// </summary>
    public class CommonContainer
    {
        /// <summary>
        /// Общедоступный контроллер
        /// </summary>
        public static WindsorContainer Instance { get; private set; }

        public static void Initialize(WindsorContainer container)
        {
            // если контейнер ещё не проинициализирован - тогда инициализируем его
            if (Instance == null)
            {
                Instance = container;
            }
        }
    }
}
