using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace OrderAndAccountBook
{


    public class AccountBook
    {
        protected Dictionary<int, OrderForm> dict = new Dictionary<int, OrderForm>();
        protected Dictionary<string, List<OrderForm>> dict_name = new Dictionary<string, List<OrderForm>>();

        int id_counter = 1;

        public void AddOrder(OrderForm order)
        {

            if (order.Id == 0)
            {
                order.Id = id_counter;
            }
            else
            {
                if (dict.ContainsKey(order.Id))
                {
                    Console.WriteLine("重复ID无法添加 " + order.Id);
                    return;
                }
            }

            dict[order.Id] = order;

            id_counter = Math.Max(id_counter + 1, order.Id + 1);

            if (!dict_name.ContainsKey(order.Name))
            {
                dict_name.Add(order.Name, new List<OrderForm>());
            }
            List<OrderForm> l = dict_name[order.Name];
            l.Add(order);

            //dict.Add(id_counter, order);
        }

        public OrderForm GetOrder(int id)
        {
            OrderForm temp = null;
            dict.TryGetValue(id, out temp);
            return temp;
        }

        public List<OrderForm> GetOrder(string name)
        {
            if (!dict_name.ContainsKey(name))
            {
                return null;
            }
            return dict_name[name];
        }

        public double GetTotal()
        {
            double total = 0.0;
            foreach (var pair in dict)
            {
                total += pair.Value.GetPrice();
            }
            return total;
        }

        public void PrintAll()
        {
            Console.WriteLine("账本内容：");
            foreach (var pair in dict)
            {
                Console.WriteLine(pair.Value);
            }
            Console.WriteLine("======END======");
        }

        public bool SaveToFile()
        {
            string s = "";
            foreach (var pair in dict)
            {
                s += pair.Value.ToString();
                s += "\n";
            }
            //Console.WriteLine("------111----------\n" + s + "\n=====================");
            Debug.WriteLine(s);

            // 打开文件，获得文件流对象file
            FileStream file = null;
            try
            {
                file = new FileStream("..\\test.txt", FileMode.Create);
                // 字符串s转为byte[]
                byte[] bytes = Encoding.UTF8.GetBytes(s);
                file.Write(bytes, 0, bytes.Length);     // byte[]写入文件
            }
            catch (IOException e)
            {
                Console.WriteLine("IO error:" + e);
            }
            catch (Exception e)
            {
                Console.WriteLine("error:" + e);
            }
            finally
            {
                if (file != null)
                {
                    file.Close();
                }
            }
            return true;
        }

        public bool WriteToFile()
        {


            // 打开文件，获得文件流对象file
            FileStream fs = null;
            try
            {
                fs = new FileStream("..\\test.txt", FileMode.Create);

                Serializer.Serialize<Dictionary<int, OrderForm>>(fs, dict);

                //关闭此文件 
                // 字符串s转为byte[]
                ;     // byte[]写入文件
            }
            catch (IOException e)
            {
                Console.WriteLine("IO error:" + e);
            }
            catch (Exception e)
            {
                Console.WriteLine("error:" + e);
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                }


            }
            return true;
        }

        public bool LoadFile()
        {


            // 打开文件，获得文件流对象file
            FileStream fs = null;
            Dictionary<int, OrderForm> dictionaries = new Dictionary<int, OrderForm>();
            try
            {
                fs = new FileStream("..\\test.txt", FileMode.Create);

                dictionaries = Serializer.Deserialize<Dictionary<int, OrderForm>>(fs, dict);



                //关闭此文件 
                // 字符串s转为byte[]
                ;     // byte[]写入文件
            }
            catch (IOException e)
            {
                Console.WriteLine("IO error:" + e);
            }
            catch (Exception e)
            {
                Console.WriteLine("error:" + e);
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                }


            }
            foreach (var item in dictionaries)
            {
                AddOrder(item.Value);
            }
            return true;
        }


        public int LoadFromFile()
        {
            int len = 0;
            FileStream file = null;
            byte[] bytes = new byte[10240];
            using (file = new FileStream("..\\test.txt", FileMode.Open))
            {
                len = file.Read(bytes, 0, 10240);
            }
            //try             // 可能发生错误的代码
            //{
            //    file = new FileStream("..\\test.txt", FileMode.Open);
            //    len = file.Read(bytes, 0, 10240);
            //    //Debug.Write(bytes[10240]);
            //}
            //catch (FileNotFoundException e)      // catch有多个，每个处理一种异常
            //{
            //    Debug.WriteLine("FileNotFoundException " + e.FileName);
            //}
            //catch (Exception e)
            //{
            //    Debug.WriteLine("Exception " + e);
            //}
            //finally     // 一定会执行，把一些不得不做的收尾（清理）工作放在这里
            //{
            //    if (file != null)
            //    {
            //        file.Close();
            //    }
            //}
            // byte[]转string
            string source = Encoding.UTF8.GetString(bytes, 0, len);
            Console.WriteLine("Load File:" + source);


            string[] l = source.Split('\n');
            for (int i = 0; i < l.Length; ++i)
            {
                OrderForm order = new OrderForm("", 0, 0);
                //bool success = order.Deseriallize(l[i]);
                string[] ll = l[i].Split('|');

                order.Id = int.Parse(ll[0]);
                order.Name = ll[1];
                order.Num = int.Parse(ll[2]);
                order.Price = double.Parse(ll[3]);

                //if (!success)
                //{
                //    Console.WriteLine("反序列化错误" + l[i] + ".");
                //    continue;
                //}
                AddOrder(order);
            }

            return len;
        }

    }
}
