using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easy.Domain.ActiveMqDomainEvent
{
    public interface IActiveMqClientIdHelper
    {
        string Get();
    }


    public class FileActiveMqClientIdHelper : IActiveMqClientIdHelper
    {
        private string fileName;
        public FileActiveMqClientIdHelper(string fileName)
        {
            this.fileName = fileName;
        }
        public FileActiveMqClientIdHelper() : this("activeMqClientId.txt") { }

        public string Get()
        {
            string file = this.GetFile();
            if (File.Exists(file))
            {
                return File.ReadAllText(file, Encoding.UTF8).Trim();
            }
            int activeMqClientId = Guid.NewGuid().GetHashCode();
            File.WriteAllText(file, activeMqClientId.ToString(), Encoding.UTF8);

            return activeMqClientId.ToString();
        }

        private string GetFile()
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this.fileName);
            return path;
        }
    }
}
