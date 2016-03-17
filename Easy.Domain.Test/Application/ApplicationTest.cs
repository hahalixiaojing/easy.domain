using Easy.Domain.Application;
using Easy.Domain.Test.Application;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Easy.Domain.Test.ApplicationTest
{
    public class ApplicationTest
    {
        [NUnit.Framework.Test]
        public void GetReturnTransformerTest()
        {
            ApplicationFactory.Instance().Register(new DemoApplication());

            IReturn result = ApplicationFactory.Instance().Get<DemoApplication>().FindById();

            var transformer1 = result.GetReturnTransformer<DemoModel1>(new ReturnContext() { SystemId = "demo1" });

            DemoModel1 demoModel1 = result.Result(transformer1, result);

            Assert.AreEqual(typeof(DemoModel1), demoModel1.GetType());

            var transformer2 = result.GetReturnTransformer<DemoModel2>(new ReturnContext() { SystemId = "demo2" });

            DemoModel2 demoModel2 = result.Result(transformer2, result);

            Assert.AreEqual(typeof(DemoModel2), demoModel2.GetType());

        }
    }
}
