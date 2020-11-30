using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Newtonsoft.demo
{
    public partial class Form1 : Form
    {
        private entity1 _entity1 = null;
        private entity2 _entity2 = null;

        public Form1()
        {
            InitializeComponent();
            #region 数据初始化
            _entity1 = new entity1
            {
                _bool = true,
                _datetime = DateTime.Now,
                _int = 1,
                _string = "_string"
            };
            _entity2 = new entity2
            {
                _bool = true,
                _datetime = DateTime.Now,
                _int = 1,
                _string = "_string",
                _entity1 = new entity1
                {
                    _bool = true,
                    _datetime = DateTime.Now,
                    _int = 1,
                    _string = "_string"
                }
            };
            #endregion
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string result = "";
            {
                // 普通转换
                result = JsonConvert.SerializeObject(_entity1);
                Console.WriteLine("01: " + result);
                // {"_int":1,"_string":"_string","_datetime":"2014-09-14T22:06:21.0449302+08:00","_bool":true}
            }


            {
                // 设置时间格式
                JsonSerializerSettings jss = new JsonSerializerSettings();
                jss.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                result = JsonConvert.SerializeObject(_entity1, jss);
                Console.WriteLine("02: " + result);
                // {"_int":1,"_string":"_string","_datetime":"2014-09-14 22:17:53","_bool":true}
            }


            {
                // 嵌套对象
                JsonSerializerSettings jss = new JsonSerializerSettings();
                jss.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                result = JsonConvert.SerializeObject(_entity2, jss);
                Console.WriteLine("03: " + result);
                // {"_int":1,"_string":"_string","_datetime":"2014-09-14 22:28:30","_bool":true,"_entity1":
                // {"_int":1,"_string":"_string","_datetime":"2014-09-14 22:28:30","_bool":true}}
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            {
                string str = "{\"_int\":1,\"_string\":\"_string\",\"_datetime\":\"2014-09-14 22:17:53\",\"_bool\":true}";
                entity1 entity = JsonConvert.DeserializeObject<entity1>(str);
            }

            {
                string str = "[{\"_int\":1,\"_string\":\"_string\",\"_datetime\":\"2014-09-14 22:17:53\",\"_bool\":true}";
                str += ",{\"_int\":1,\"_string\":\"_string\",\"_datetime\":\"2014-09-14 22:17:53\",\"_bool\":true}]";
                List<entity1> entity = JsonConvert.DeserializeObject<List<entity1>>(str);
            }

            {
                string str = "{\"_int\":1,\"_string\":\"_string\",\"_datetime\":\"2014-09-14 22:17:53\",\"_bool\":true}";
                JObject jo = JObject.Parse(str);
                string _str = jo["_string"].ToString();
                int _int = Convert.ToInt32(jo["_int"].ToString());
            }

            {
                string str = "{\"code\": 1000, \"msg\": \"\", \"member\": {\"username\": \"zzy_igtest1\", \"balance\": 6252.6}}";
                JObject jo = JObject.Parse(str);
                JToken member = jo["member"];
                double d = member["balance"].Value<double>();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            Random ran = new Random((int)DateTime.Now.Ticks);
            int count = ran.Next(1, 10);
            for (int i = 0; i < count; i++)
            {
                dict.Add(string.Format("para{0}", i), string.Format("value{0}", i));
            }

            JArray arrs = new JArray();
            foreach (var item in dict)
            {
                JObject o = new JObject();
                o[item.Key] = item.Value;
                arrs.Add(o);
            }
            string json = JsonConvert.SerializeObject(arrs, Formatting.Indented);
            Console.WriteLine(json);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            Random ran = new Random((int)DateTime.Now.Ticks);
            int count = ran.Next(1, 10);
            for (int i = 0; i < count; i++)
            {
                dict.Add(string.Format("para{0}", i), string.Format("value{0}", i));
            }

            JArray arrs = new JArray();
            foreach (var item in dict)
            {
                JObject o = new JObject();
                o[item.Key] = item.Value;
                o["para"] = ran.Next(20, 100).ToString();
                arrs.Add(o);
            }
            string json = JsonConvert.SerializeObject(arrs, Formatting.Indented);
            Console.WriteLine(json);
        }
    }
}
