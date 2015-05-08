#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
#endregion

namespace Platformer
{
#if WINDOWS || LINUX

    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new Game1())
                game.Run();
        }
    }
#endif
}
