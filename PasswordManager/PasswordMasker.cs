using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Format
{
    class PasswordMasker
    {
        public static string HidePassword()
        {
            string pass = "";
            ConsoleKeyInfo key;
            int consoleWidth = Console.WindowWidth;

            do
            {
                key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.Backspace && pass.Length > 0)
                {
                    pass = pass[..^1];
                }
                else if (!char.IsControl(key.KeyChar))
                {
                    pass += key.KeyChar;
                }

                int startX = (consoleWidth / 2) - (pass.Length / 2);
                int startY = Console.CursorTop;

                Console.SetCursorPosition(0, startY);
                Console.Write(new string(' ', consoleWidth));
                Console.SetCursorPosition(startX, startY);
                Console.Write(new string('*', pass.Length));

            } while (key.Key != ConsoleKey.Enter);

            return pass;
        }
    }
}
