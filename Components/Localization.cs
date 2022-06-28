using System.Collections.Generic;

namespace InternetSwitcher {

    static internal class dict {

        public static Dictionary<string, string> local = new Dictionary<string, string> {

            ["catch_ru"] = "Не удалось запустить cmd.exe, что-то мешает работоспособности Программы",
            ["catch_en"] = "Failed to start cmd.exe, something is interfering with the Program's performance",


            ["iOn_ru"] = "Интернет включен!",
            ["iOn_en"] = "Ethernet Enabled!",

            ["iOff_ru"] = "Интернет выключен!",
            ["iOff_en"] = "Ethernet Disabled!",


            ["exp1_ru"] = "Возникло исключение в:\n",
            ["exp1_en"] = "An exception occurred in:\n",

            ["exp2_ru"] = "\n\nСкопировать ошибку в буфер обмена и сообщить Разработчику?",
            ["exp2_en"] = "\n\nCopy exception message to clipboard and inform the Developer?",

        };
    }
}
