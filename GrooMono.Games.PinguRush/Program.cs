using System;

namespace GrooMono.Games.PinguRush
{
#if WINDOWS || LINUX
    /// <summary>
    ///     The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            using (PinguGame pinguGame = new PinguGame())
            {
                pinguGame.Run();
            }
        }
    }
#endif
}