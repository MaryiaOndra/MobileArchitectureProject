using CodeBase.Services.Input;
using UnityEngine;

namespace CodeBase.Infrastructure
{
    public class Game
    {
        public static InputService inputService;
        
        public Game()
        {
            RegisterInputService();
        }

        private static void RegisterInputService()
        {
            if (Application.isEditor)
                inputService = new StandaloneInputService();
            else
                inputService = new MobileInputService();
        }
    }
}