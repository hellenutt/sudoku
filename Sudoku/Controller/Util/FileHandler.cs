using System;
using System.IO;

namespace Controller.Util
{
    public class FileHandler
    {
        private readonly string _content;

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
