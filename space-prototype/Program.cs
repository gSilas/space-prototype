using System;
using Microsoft.Xna.Framework;
using space_prototype.GameStates;

namespace space_prototype
{
#if WINDOWS || LINUX
    /// <summary>
    ///     The main class.
    /// </summary>
    public static class Program
    {
        public enum Gamestates
        {
            MainMenu,
            Game,
            GameOver,
            Win,
            Credits
        }

        /// <summary>
        ///     The main entry point for the application.
        /// </summary>

        [STAThread]
        private static void Main()
        {
            using (var game = new Controller())
            game.Run();
        }

        public static void NextGameState(Gamestates target)
        {
            switch (target)
            {
                case Gamestates.MainMenu:
                    _game.Exit();
                    _game = new MainMenu();
                    _game.Run();
                    break;
                case Gamestates.Game:
                    _game.Exit();
                    _game = new MainGame();
                    _game.Run();
                    break;
                case Gamestates.GameOver:
                    _game.Exit();
                    _game = new GameOverScreen();
                    _game.Run();
                    break;
                case Gamestates.Win:
                    _game.Exit();
                    _game = new WinScreen();
                    _game.Run();
                    break;
                case Gamestates.Credits:
                    _game.Exit();
                    _game = new Credits();
                    _game.Run();
                    break;
            }
        }
    }
#endif
}