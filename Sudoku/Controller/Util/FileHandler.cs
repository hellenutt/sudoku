using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace Controller.Util
{
    public class FileHandler
    {
        private string _content;

        public FileHandler(String filePath)
        {
            string line;
            var file = new StreamReader(filePath);
            while ((line = file.ReadLine()) != null)
            {
                _content += line;
            }
            file.Close();
        }

        public string Content
        {
            get
            {
                return _content;
            }
        }
    }
}
