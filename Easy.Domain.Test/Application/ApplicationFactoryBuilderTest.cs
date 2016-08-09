using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Easy.Domain.Application;
using NUnit.Framework;

namespace Easy.Domain.Test.Application
{
    public class ApplicationFactoryBuilderTest
    {
        [Test]
        public void LoadTest()
        {
            var builder = new ApplicationFactoryBuilder();

            string file = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Application/application.config");

            ApplicationFactory factory = builder.Build(new FileInfo(file));


            String value = factory.GetByInterface<IDemo2Service>().TestDefaultValue().ResultDefault<string>();

            Assert.AreEqual("defaultvalue", value);
          
        }
    }
}
