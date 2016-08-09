using Easy.Domain.Application;
using Easy.Domain.Test.Application;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;

namespace Easy.Domain.Test.ApplicationTest
{
    public class ApplicationTest
    {
     
        [Test]
        public void DemoTest()
        {
            var mock = new Moq.Mock<Demo2Application>();

            mock.Setup<IReturn>(m => m.Write<String>(It.IsAny<string>(), It.IsAny<string>())).Returns(() => { return null; });


            mock.Object.TestDemo();
        }
    }
}
