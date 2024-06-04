using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammEasy
{
    internal class RegFlag
    {
        public static string NameFlage;
        public static string LastNameFlage;
        public static string LoginFlag;
        public static string PasswordFlag;

        public static int Namebool = 1;
        public static int LastNamebool = 1;
        public static int Logbool = 1;
        public static int Passbool = 1;
        public static int Informbool = 1;
        public static int Passwordbool = 1;
        public static int Avtorizbool = 1;

        public static string UserLogin;
        public static int IdRol;
        public static int IdUser;
        public static string UserName;
        public static string UserLastName;
        public static string RoleName;

        public static int LessonId;

        public static void ClearData()
        {
            // Присваиваем начальные значения переменным и полям
            NameFlage = null;
            LastNameFlage = null;
            LoginFlag = null;
            PasswordFlag = null;

            Namebool = 1;
            LastNamebool = 1;
            Logbool = 1;
            Passbool = 1;
            Informbool = 1;
            Passwordbool = 1;
            Avtorizbool = 1;

            UserLogin = null;
            IdRol = 0;
            IdUser = 0;
            UserName = null;
            UserLastName = null;
            RoleName = null;

            LessonId = 0;
        }
    }

}
