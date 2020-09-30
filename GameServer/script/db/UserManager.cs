using GameCommon.Utils;
using GameServer.script.logic;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace GameServer.script.db
{
    public class UserManager
    {
        public static List<User> users = new List<User>();
        public static string file_path = "../../table/";
        public static string file_name = "user.tb";

        public static void Load()
        {
            string content = IOUtils.LoadFromFile_Binary(file_path + file_name);
            users = JsonConvert.DeserializeObject<List<User>>(content);
        }
        public static void Save()
        {
            if (!Directory.Exists(file_path))
            {
                Directory.CreateDirectory(file_path);
            }


            string content = JsonConvert.SerializeObject(users);
            IOUtils.SaveToFile_Binary(file_path + file_name, content);
        }


        public static bool Check(User user, out string str)
        {
            List<User> lists = users.Where(orgUser => orgUser.Username == user.Username).ToList();
            if (lists.Count > 0)
            {
                str = "用户名已存在";
                return false;
            }
            str = "";
            return true;

        }
        public static bool Add(User user)
        {
            users.Add(user);
            new Thread(new ThreadStart(
                () =>
                {
                    Save();
                }
                ))
            .Start();
            return true;
        }

        public static bool Update(User user)
        {
            string str = "";
            User user1 = Find(user.Userid, out str);
            if (user1 != null)
            {
                if (!string.IsNullOrEmpty(user.Username))
                {
                    user1.Username = user.Username;

                }
                if (!string.IsNullOrEmpty(user.Password))
                {
                    user1.Password = user.Password;

                }
                user1.Score = user.Score;

            }
            new Thread(new ThreadStart(
                () =>
                {
                    Save();
                }
                ))
            .Start();
            return true;
        }
        public static bool Remove(User user)
        {
            users.Remove(user);
            new Thread(new ThreadStart(
                () =>
                {
                    Save();
                }
                ))
            .Start();
            return true;
        }
        public static User Find(string id, out string str)
        {
            List<User> lists = users.Where(user => user.Userid == id).ToList();
            if (lists.Count == 0)
            {
                str = "查询用户为空";
                return null;
            }
            if (lists.Count > 1)
            {
                str = "查询用户不唯一";
                return null;
            }
            str = "查询成功";
            return lists[0];
        }
        public static User Login(string username, string password, out string str)
        {
            List<User> lists = users.Where(user => user.Username == username && user.Password == password).ToList();
            if (lists.Count == 0)
            {
                str = "用户名或密码错误";
                return null;
            }
            if (lists.Count > 1)
            {
                str = "查询用户不唯一";
                return null;
            }
            str = "登录成功";
            return lists[0];
        }
    }
}
