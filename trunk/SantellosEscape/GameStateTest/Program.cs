using System;

namespace GameStateTest
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (SantellosEscape game = new SantellosEscape())
            {
                game.Run();
            }
        }
    }
}

