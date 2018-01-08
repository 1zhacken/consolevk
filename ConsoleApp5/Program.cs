using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xNet;
using System.Net;
using Newtonsoft.Json;
using System.IO;
using System.Text;
using Newtonsoft.Json.Linq;
using System.Security.Cryptography;

namespace ConsoleApp5
{
    class Program
    {
        public static string token = null;
        public static bool enc;
        public static bool prx;
        public static string password = "fdathftyrhfgrteyrtfhgGTREhvkTRf56432HLn&6HFRT56GtYTR6TrETGHjy68YfGhTrGHfdathftyrhfgrteyrtfhgGTREhvkTRf56432HLn&6HFRT56GtYTR6TrETGHjy68YfGhTrGHfdathftyrhfgrteyrtfhgGTREhvkTRf56432HLn&6HFRT56GtYTR6TrETGHjy68YfGhTrGHtfg56dhfyrtghmnjUYTgfandftygjcmju&6gfnvjgluytrghF";
        public static FileStream file = new FileStream("tkn.orb", FileMode.Open);
        public static string ss = "https://goo.gl/R9tuYZ";
        public static string code = null;
        static void Main(string[] args)
        {
            StreamReader reader = new StreamReader(file);
            token = base64dt(reader.ReadToEnd());
            reader.Close();

            if (token == "" || token == null || token == " " ) help();
            
            string com = Com();
            while (com != "exit")
            {
                if (com == "tkn")
                {
                    auth();
                }
                else if (com == "exit") break;
                else if (com == "gm") GetMessages();
                else if (com == "del") del();
                else if (com == "sm") send();
                else if (com == "enc") encr();
                else if (com == "prx") proxy();
                else if (com == "wc") wallc();
                else if (com == "gt") gt();
                else if (com == "gc") gc();
                else er();
                com = Com();
            }
        }
        static void help()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Привет! Прежде чем ты сможешь писать сообщения, тебе нужно авторизоваться. Для этого не нужен твой логин или пароль, только ключ доступа. Для того, чтобы получить его, открой в браузере эту ссылку "+ss+", после чего скопируй все, что находится после token= и до &. Потом в консоли напиши tkn и введи туда свое значение. Да, на странице написано не передавайте данные. Но мне просто лень писать метод аутентификации. Извини. Ну и конечно же, я не буду трогать твой аккаунт. Для выхода напиши exit. Список команд ты найдешь в файле readme.txt. Нажми Enter, если понял");
            Console.WriteLine(ss);
            Console.ReadKey();
        }
        static void auth()
        {
            int b64 = 0;
            Console.Write("Повторяю еще раз,");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(" ЭТО ПОЛНОСТЬЮ БЕЗОПАСНО, Я НЕ ХОЧУ ТЕБЯ ВЗЛОМАТЬ!");
            Console.ResetColor();
            Console.Write("Твой код >>> ");
            token = Console.ReadLine();
            token = base64e(token);
            FileStream file1 = new FileStream("tkn.orb", FileMode.Append);
            StreamWriter writer = new StreamWriter(file1);
            writer.Write(token);
            writer.Close();
            Console.WriteLine("Спсасибо :). Если захочешь сменить аккаунт, введи команду del");
        }
        static string Com()
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write("Твоя команда >>> ");
            Console.ResetColor();
            string com = Console.ReadLine();
            return com;
        }
        static void er()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Упс, что-то пошло не так. Введи команду снова");
            Console.ResetColor();
        }
        static void GetMessages()
        {
            WebClient cl = new WebClient();
            string response = null;
            cl.Encoding = Encoding.UTF8;
            if (prx == true) response = cl.DownloadString("https://vk-api-proxy.xtrafrancyz.net/method/messages.get?count=5&access_token=" + token + "&v=5.69");
            else response = cl.DownloadString("https://api.vk.com/method/messages.get?count=5&access_token=" + token + "&v=5.69");
            //Console.WriteLine(response);
            dynamic etc = JObject.Parse(response);
            try
            {
                for (int i = 4; i >= 0; i--)
                {
                    string text1 = null;
                    string text = etc.response.items[i].body;
                    if (enc == true)
                    {
                        text1 = base64d(text);
                    }
                    string id = etc.response.items[i].user_id;
                    string res = null;
                    if (prx == true) res = cl.DownloadString("https://vk-api-proxy.xtrafrancyz.net/method/users.get?user_ids=" + id + "&access_token=" + token + "&v=5.69");
                    else res = cl.DownloadString("https://api.vk.com/method/users.get?user_ids=" + id + "&access_token=" + token + "&v=5.69");
                    //Console.WriteLine(res);
                    dynamic atc = JObject.Parse(res);
                    string fname = atc.response[0].first_name;
                    string lname = atc.response[0].last_name;
                    if (enc == true) Console.WriteLine("Новое сообщение! От " + fname + " " + lname + ": " + text1);
                    else Console.WriteLine("Новое сообщение! От " + fname + " " + lname + ": " + text);
                }
            }
            catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException e)
            {
                Console.WriteLine("выключай прокси.");
                prx = false;
            }

        }
        static void del()
        {
            File.Delete("tkn.orb");
            Console.WriteLine("Готово! вызови tkn снова, чтобы авторизоваться! ");
            Console.WriteLine("https://goo.gl/R9tuYZ");
        }
        static void send()
        {
            Console.Write("Текст >>> ");
            
            string text = Console.ReadLine();
            //Console.WriteLine(token);
            if (enc == true) text = base64e(text);
            WebClient cl = new WebClient();
            string[] ids = new string[5];
            cl.Encoding = Encoding.UTF8;
            string response = null;
            if (prx == true) response = cl.DownloadString("https://vk-api-proxy.xtrafrancyz.net/method/messages.get?count=5&access_token=" + token + "&v=5.69");
            else response = cl.DownloadString("https://api.vk.com/method/messages.get?count=5&access_token=" + token + "&v=5.69");
            //Console.WriteLine(response);
            dynamic a = JObject.Parse(response);
            for (int i = 0; i < 4; i++) ids[i] = a.response.items[i].user_id;
            string ids1 = ids[0] + "," + ids[1] + "," + ids[2] + "," + ids[3] + "," + ids[4];
            string[] respo = new string[5];
            for (int i = 0; i <= ids.Length; i++)
            {
                try
                {
                    if (prx == true) respo[i] = cl.DownloadString("https://vk-api-proxy.xtrafrancyz.net/method/users.get?user_ids=" + ids[i] + "&access_token=" + token + "&v=5.69");
                    else respo[i] = cl.DownloadString("https://api.vk.com/method/users.get?user_ids=" + ids[i] + "&access_token=" + token + "&v=5.69");
                }
                catch (System.IndexOutOfRangeException e) { break; }
            }
            //response = cl.DownloadString("https://api.vk.com/method/users.get?user_ids=" + ids1 + "&access_token=" + token + "&v=5.69");            
            Console.WriteLine("недавние собеседники: ");
            for (int i = 0; i <= 4; i++)
            {
                a = JObject.Parse(respo[i]);
                try
                {
                    string[] names = new string[5];
                    names[i] = a.response[0].first_name;
                    string[] surnames = new string[5];
                    surnames[i] = a.response[0].last_name;

                    Console.WriteLine(names[i] = " " + surnames[i] + " - " + ids[i]);
                }
                catch (ArgumentOutOfRangeException e)
                {
                    break;
                }
            }
            //Console.WriteLine(a.response[0].first_name + " " + a.response[0].last_name + " - " + a.response[0].id);
            //Console.WriteLine(a.response[1].first_name + " " + a.response[1].last_name + " - " + a.response[1].id);

            Console.Write("id >>> ");
            string rerspon = null;
            string id = Console.ReadLine();
            if (prx == true) rerspon = cl.DownloadString("https://vk-api-proxy.xtrafrancyz.net/method/messages.send?user_id=" + id + "&message=" + text + "&access_token=" + token + "&v=5.69");
            else rerspon = cl.DownloadString("https://api.vk.com/method/messages.send?user_id=" + id + "&message=" + text + "&access_token=" + token + "&v=5.69");

        }
        static string base64e(string text)
        {
            byte[] text1 = Encoding.UTF8.GetBytes(text);
            text = Convert.ToBase64String(text1);
            Console.WriteLine(text);
            return text;
        }

        static string base64d(string text)
        {
            try
            {
                byte[] text1 = Convert.FromBase64String(text);
                string s =  Encoding.UTF8.GetString(text1);
                s += " [расшифровано]";
                return s;
            }
            catch (FormatException e)
            {
                return text;
            }
        }
        static string base64dt(string text)
        {
            try
            {
                byte[] text1 = Convert.FromBase64String(text);
                string s = Encoding.UTF8.GetString(text1);
               // s += " [расшифровано]";
                return s;
            }
            catch (FormatException e)
            {
                return text;
            }
        }
        static void encr()
        {
            Console.WriteLine("Использовать шифрование? Y/n");
            string a = Console.ReadLine();
            if (a == "Y" || a == "y") enc = true;
            else if (a == "n" || a == "N") enc = false;
            //else enc = false;
        }
        static void proxy()
        {
            Console.WriteLine("Использовать прокси? Y/n");
            string a = Console.ReadLine();
            if (a == "Y" || a == "y") prx = true;
            else if (a == "n" || a == "N") prx = false;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Внимание! Прокси может работать нестабильно! В случае проблем с соединением (долго грузятся сообщения) перезапусти программу и не включай прокси!");
            Console.ResetColor();
        }
        static void wallc()
        {
            WebClient cl = new WebClient();
            cl.Encoding = Encoding.UTF8;
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write("Текст >>> ");
            Console.ResetColor();
            string text = Console.ReadLine();
            if (enc == true) text = base64e(text);
            Console.WriteLine("Где публиковать?");
            dynamic a = JObject.Parse(cl.DownloadString("https://api.vk.com/method/users.get?access_token=" + token + "&v=5.69"));
            string id = a.response[0].id;
            dynamic b = JObject.Parse(cl.DownloadString("https://api.vk.com/method/groups.get?access_token=" + token + "&filter=admin&v=5.69"));
            for (int i = 0; i <= Int32.Parse(Convert.ToString(b.response.count))-1; i++) {
                string group_id = b.response.items[i];
                dynamic c = JObject.Parse(cl.DownloadString("https://api.vk.com/method/groups.getById?group_ids=" + group_id + "&v=5.69"));
                string group_name = c.response[0].name;
                Console.WriteLine(group_name + " - " + group_id);
                }
            Console.WriteLine("Страница - " + id);
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write("id >>> ");
            Console.ResetColor();
            string pid = Console.ReadLine();
           if(pid == id) cl.DownloadString("https://api.vk.com/method/wall.post?owner_id="+pid+"&message="+text+"&access_token="+token+"&v=5.69");
           else cl.DownloadString("https://api.vk.com/method/wall.post?owner_id=-" + pid + "&message=" + text + "&from_group=1&access_token=" + token + "&v=5.69");
        }
        static void gt()
        {
            Console.WriteLine(token);
        }
        static void gc()
        {
            WebClient cl = new WebClient();
            cl.Encoding = Encoding.UTF8;
            Console.ForegroundColor = ConsoleColor.DarkRed;
            dynamic c;
            string[] ids = new string[5];
            string res1 = null;
            if (prx == true) res1 = cl.DownloadString("https://vk-api-proxy.xtrafrancyz.net/method/messages.get?count=5&access_token=" + token + "&v=5.69");
            else res1 = cl.DownloadString("https://api.vk.com/method/messages.get?count=5&access_token=" + token + "&v=5.69");
            //Console.WriteLine(response);
            c = JObject.Parse(res1);
            for (int i = 0; i < 4; i++) ids[i] = c.response.items[i].user_id;
            string ids1 = ids[0] + "," + ids[1] + "," + ids[2] + "," + ids[3] + "," + ids[4];
            string[] respo = new string[5];
            for (int i = 0; i <= ids.Length; i++)
            {
                try
                {
                    if (prx == true) respo[i] = cl.DownloadString("https://vk-api-proxy.xtrafrancyz.net/method/users.get?user_ids=" + ids[i] + "&access_token=" + token + "&v=5.69");
                    else respo[i] = cl.DownloadString("https://api.vk.com/method/users.get?user_ids=" + ids[i] + "&access_token=" + token + "&v=5.69");
                }
                catch (System.IndexOutOfRangeException e) { break; }
            }
            //response = cl.DownloadString("https://api.vk.com/method/users.get?user_ids=" + ids1 + "&access_token=" + token + "&v=5.69");            
            Console.WriteLine("недавние собеседники: ");
            for (int i = 0; i <= 4; i++)
            {
                dynamic d = JObject.Parse(respo[i]);
                try
                {
                    string[] names1 = new string[5];
                    names1[i] = d.response[0].first_name;
                    string[] surnames = new string[5];
                    surnames[i] = d.response[0].last_name;

                    Console.WriteLine(names1[i] = " " + surnames[i] + " - " + ids[i]);
                }
                catch (ArgumentOutOfRangeException e)
                {
                    break;
                }
            }
            Console.Write("id (если это беседа, то перед id беседы укажи с) >>> ");
            Console.ResetColor();
            string uid = Console.ReadLine();
            
            //Console.WriteLine(response);
            string text = null;
            string[] msgs = new string[25];
            string[] names = new string[25];
            string name = null;
            dynamic a;
            dynamic b;
            string resp = null;
            string[] msg = new string[25];
           /* int m;
            dynamic zz = JObject.Parse(response);
            string s = null;
            s = zz.response.response.items[24].id;
            m = Int32.Parse(s);*/
            
            while (true){
                string response = cl.DownloadString("https://api.vk.com/method/messages.getHistory?user_id=" + uid + "&v=5.69&rev=0&count=25&access_token=" + token);
                for (int i = 0; i <= 24; i++)
                {
                    if (i == 0) Console.Write("Загрузка сообщений...");
                    a = JObject.Parse(response);
                    string s = a.response.items[i].body;



                    if (enc == true)
                    {
                        msgs[i] = base64d(s);
                        
                    }
                    if (a.response.items[i].read_state == "0") msgs[i] += " [не прочитано]";
                    if (a.response.items[i].body == "" || a.response.items[i].body == " " || a.response.items[i].body == null) msgs[i] = "%вложение%";
                    names[i] = a.response.items[i].from_id;
                    resp = cl.DownloadString("https://api.vk.com/method/users.get?user_ids=" + names[i] + "&access_token=" + token + "&v=5.69");
                    //Console.WriteLine(resp);
                    b = JObject.Parse(resp);
                    name = b.response[0].first_name + " " + b.response[0].last_name;
                    msg[i] = (name + " " + ": " + msgs[i]);
                    if (i == 24) Console.Clear();
                }
                for (int i = 0; i <= 24; i++) Console.WriteLine(msg[i]);
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.Write("текст сообщения >>> ");
                Console.ResetColor();
                text = Console.ReadLine();
                if (enc == true) text = base64d(text);
                if (text == "exit" || text == base64d("exit")) break;
                else
                {

                    
                    a = JObject.Parse(response);
                    string id = a.response.items[0].user_id;
                    cl.DownloadString("https://api.vk.com/method/messages.send?user_id=" + id + "&message=" + text + "&access_token=" + token + "&v=5.69");
                    Console.Clear();
                }
               
            }

        }
    }
       

}