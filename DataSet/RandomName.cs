using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSet
{
    class RandomName
    {

        List<string> NameList = new List<string>()
        {
            "Maria",
            "Vika",
            "Anton",
            "Artem",
            "Marina",
            "Stepan",
            "Mickail"
        };

        string[] CompanyMas = new string[] 
        {
            "Yandex",
            "Mail",
            "RosLogistic",
            "H&Sh",
            "Hermes",
            "MakDoc",
            "KFC"
        };

        public string Name { get; set; }
        public int Age { get; set; }

        public  RandomName()
        {
            Random rnd = new Random();
            int rndNumber = rnd.Next(NameList.Count - 1);
            Name = NameList[rndNumber];
            Age = rnd.Next(0, 100);
        }
    }
}
