using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml.XPath;

namespace Easy.Domain.RepositoryFramework
{
    public class RepositoryFactoryBuilder : BaseFactoryBuilder
    {
        public RepositoryFactory Build(FileInfo fileinfo)
        {
            if (!fileinfo.Exists)
            {
                throw new FileNotFoundException(fileinfo.FullName);
            }
            var navi = CreateXpathNavi(fileinfo);

            return new RepositoryFactory(navi);
        }
        public  RepositoryFactory Build(Stream stream)
        {
            var navi = CreateXpathNavi(stream);
            return new RepositoryFactory(navi);
        }
    }
}
