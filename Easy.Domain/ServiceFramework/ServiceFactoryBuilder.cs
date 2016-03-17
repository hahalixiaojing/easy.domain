using Easy.Domain.RepositoryFramework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Easy.Domain.ServiceFramework
{

    public class ServiceFactoryBuilder: BaseFactoryBuilder
    {
        public ServiceFactory Build(FileInfo fileinfo)
        {
            if (!fileinfo.Exists)
            {
                throw new FileNotFoundException(fileinfo.FullName);
            }
            var navi = CreateXpathNavi(fileinfo);
            return new ServiceFactory(navi);
        }
        public ServiceFactory Build(Stream stream)
        {
            var navi = CreateXpathNavi(stream);
            return new ServiceFactory(navi);
        }
    }
}
