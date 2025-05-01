using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Format
{
    class ConsoleMenu
    {
        public static void ShowLogin()
        {
            EmptyFieldGenerator.GenerateFields(3);

            string[] logo = {
                " .--------.",
                " / .------. \\",
                " / /        \\ \\",
                " | |        | |",
                " _| |________| |_",
                ".' |_|        |_| '.",
                "'._____ ____ _____.'",
                "|     .'____'.     |",
                "'.__.'.'    '.'.__.'",
                "'.__  |      |  __.'",
                "|   '.'.____.'.'   |",
                "'.____'.____.'____.'",
                "'.________________.'"
            };

            string dev = " @ibrazqrj";

            foreach (string line in logo)
            {
                UIHelper.PrintTextCentered(line);
            }

            UIHelper.PrintTextCentered(dev);
            EmptyFieldGenerator.GenerateFields(2);
        }
    }
}
