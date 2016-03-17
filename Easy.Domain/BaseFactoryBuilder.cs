using Easy.Domain.RepositoryFramework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;

namespace Easy.Domain
{
    public abstract class BaseFactoryBuilder
    {
        protected XPathNavigator CreateXpathNavi(Stream stream)
        {
            XPathDocument doc = new XPathDocument(stream);
            XPathNavigator navigator = doc.CreateNavigator();
            return navigator;
        }
        protected XPathNavigator CreateXpathNavi(FileInfo fileInfo)
        {
            if (!fileInfo.Exists)
            {
                throw new FileNotFoundException(fileInfo.FullName);
            }
            XPathDocument doc = new XPathDocument(fileInfo.FullName);
            XPathNavigator navigator = doc.CreateNavigator();
            return navigator;
        }
    }
}
