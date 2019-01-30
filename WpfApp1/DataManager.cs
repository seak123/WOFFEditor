using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    class DataManager
    {
        public static DataManager GetInstance()
        {
            if (instance != null) return instance;
            else
            {
                instance = new DataManager();
            }
            return instance;
        }

        public string rootPath = @"D:\";

        private static DataManager instance;
    }
}
