using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Easy.Domain.RepositoryFramework
{
    public class RepositoryFactoryBuilder
    {
        public RepositoryFactory Build(FileInfo fileinfo)
        {
            return new RepositoryFactory(fileinfo);
        }
    }
}
