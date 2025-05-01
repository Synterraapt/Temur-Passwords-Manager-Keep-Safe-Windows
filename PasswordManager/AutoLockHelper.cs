using PasswordManager.Format;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace PasswordManager.Helper
{
    public static class AutoLockHelper
    {
        private static System.Timers.Timer inactivityTimer;
        private static int timeoutSeconds = 120;

        public static void StartAutoLock(Action onTimeout)
        {
            inactivityTimer = new System.Timers.Timer(timeoutSeconds * 1000);
            inactivityTimer.Elapsed += (sender, e) =>
            {
                inactivityTimer.Stop();
                Console.Clear();
                UIHelper.PrintColoredTextCentered("Auto-Lock triggered due to inactivity!", ConsoleColor.Red);
                UIHelper.PrintColoredTextCentered("Please log in again.", ConsoleColor.Yellow);
                Thread.Sleep(2000);
                onTimeout.Invoke();
            };
            inactivityTimer.Start();
        }

        public static void ResetAutoLock()
        {
            if (inactivityTimer != null)
            {
                inactivityTimer.Stop();
                inactivityTimer.Start();
            }
        }
    }
}
