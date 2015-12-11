using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Easy.Domain.ServiceFramework
{

    public class ServiceFactoryBuilder
    {
        public ServiceFactory Build(FileInfo fileinfo)
        {
            return new ServiceFactory(fileinfo);
        }
    }
}
