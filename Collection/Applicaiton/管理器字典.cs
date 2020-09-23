using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Collection.Applicaiton
{
    // 万物基类 Object
    class Pokemon
    {
        public int id = 0;
        public string name;
        public int level;

        public Pokemon(string name, int level)
        {
            this.name = name;
            this.level = level;
        }

        public override string ToString()
        {
            return string.Format("{0}:{1} {2}", id, name, level);
        }
    }

    class PokemonManager
    {
        Dictionary<int, Pokemon> dict = new Dictionary<int, Pokemon>();
        Dictionary<string, List<int>> dictName2 = new Dictionary<string, List<int>>();
        int idCounter = 1;

        public void AddPokemon(Pokemon p)
        {
            if (p.id != 0)
            {
                Console.WriteLine("重复添加精灵 " + p);
                return;
            }
            p.id = idCounter;
            dict.Add(p.id, p);
            //添加name id
            List<int> lists;
            bool isSuccess = dictName2.TryGetValue(p.name, out lists);
            if (isSuccess)
            {
                lists.Add(p.id);
            }
            else
            {
                lists = new List<int>();
                lists.Add(p.id);
                dictName2.Add(p.name, lists);
            }

            idCounter++;
        }

        public bool RemoveByID(int id)
        {
            //失败的2种可能 1.name不存在 直接异常
            //2 id在该name下不存在 不存在等同于删除
            string name = dict[id].name;
            bool v = dictName2[name].Remove(id);
            if (v)
            {
                //失败的情况 key不存在
                bool v1 = dict.Remove(id);
                if (!v1)
                {
                    //回滚
                    dictName2[name].Add(id);

                }
                return v1;
            }
            else
            {
                return v;
            }

        }

        public Pokemon FindByID(int id)
        {
            return dict[id];
        }

        public List<Pokemon> FindByName(string name)
        {
            List<int> lists = dictName2[name];
            return lists.Select(id => dict[id]).ToList(); ;
        }

        public void UpdateNameById(int id, string name)
        {
            Pokemon pokemon = dict[id];
            string oldName = pokemon.name;
            //改名
            pokemon.name = name;
            // 删除
            List<int> oldList = dictName2[oldName];
            oldList.Remove(id);
            //新增
            List<int> lists;
            bool isSuccess = dictName2.TryGetValue(name, out lists);
            if (isSuccess)
            {
                lists.Add(id);
            }
            else
            {
                lists = new List<int>();
                lists.Add(id);
                dictName2.Add(name, lists);
            }



        }
        public void Print()
        {
            // 打印所有精灵
            foreach (var item in dict)
            {
                Console.WriteLine(item.Value);
            }
        }

        public void savePath(String path)
        {
            string content = JsonConvert.SerializeObject(dict);
            IOUtils.SaveToFile_Binary(path, content);
        }
        public void loadData(String path)
        {
            string content = IOUtils.LoadFromFile_Binary(path);
            Dictionary<int, Pokemon> dictionaries = JsonConvert.DeserializeObject<Dictionary<int, Pokemon>>(content);
            this.dict = dictionaries;
            this.dictName2 = ConvertNameDict(this.dict);
        }

        private Dictionary<string, List<int>> ConvertNameDict(Dictionary<int, Pokemon> dict)
        {
            Dictionary<string, List<int>> nameDict = new Dictionary<string, List<int>>();
            foreach (var item in dict.Values)
            {
                List<int> lists;
                bool isSuccess = nameDict.TryGetValue(item.name, out lists);
                if (isSuccess)
                {
                    lists.Add(item.id);
                }
                else
                {
                    lists = new List<int>();
                    lists.Add(item.id);
                    nameDict.Add(item.name, lists);
                }
            }
            return nameDict;
        }
    }


}
