using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VkNet;
using VkNet.Properties;
using VkNet.Categories;
using VkNet.Enums.Filters;

namespace SearchBotVK
{
    //Developer : Falweek
    class Auth
    {

        public bool Authorize(string login, string pass)
        {
            ulong appId = 1111111; // your application id 
            Singlet.Api = new VkApi();  
            //auth in VK
            try
            {
                Singlet.Api.Authorize(new ApiAuthParams()
                {
                    ApplicationId = appId,
                    Login = login,
                    Password = pass,
                    Settings = Settings.All
                });
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show("Не удалось авторизоваться");
                return false;
            }

        }
    }
    //this pattern i'm use for auth in all form
    public class Singlet 
    {
        public static VkApi Api { get; set; }
        private static Singlet instance;

        public static Singlet Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Singlet();
                }
                return instance;
            }
        }
        private Singlet()
        { }
    }
}

