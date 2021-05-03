using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DontWreckHouse.Core
{
    public class FileLogger : ILogger
    {
        private string _filepath;
        public FileLogger(string filepath)
        {
            _filepath = Path.Combine(filepath, DateTime.Today.ToString());
        }
        public void Log(string log)
        {
            using StreamWriter writer = new StreamWriter(_filepath);
            writer.WriteLine(log);

         

       
        }
    }
}
