using System;

namespace StarComet
{
    public static class Program
    {
        [STAThread]
        
        static void Main()
          {
            using (var game = new StarComet())
                game.Run();
        }
    }
}
