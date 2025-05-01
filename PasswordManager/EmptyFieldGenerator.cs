using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Format
{
    class EmptyFieldGenerator
    {
        public static void GenerateFields(int amount)
        {
            int emptyFields = amount;

            for (int i = 0; i <= emptyFields; i++)
            {
                UIHelper.PrintTextCentered(" ");
            }
        }
    }
}
