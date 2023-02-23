using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

using FakeItEasy;
using FakeXrmEasy;

using myCustomArticlePlugin;
using Microsoft.Xrm.Sdk;

namespace CustomArticlePluginUnitTestProject
{
    [TestClass]
    public class KnowledgeArticlePluginUnitTest
    {
        [TestMethod]
        public void TestArticlePluginIncludesUpdatedAttributes()
        {
            var fakedContext = new XrmFakedContext();

            var guid1 = Guid.NewGuid();
            
            var target = new Entity("knowledgearticle") { Id = guid1 };
            target["title"] = "Hello Article";
            target["content"] = "Hello World";

            //Execute our plugin against a target that doesn't contains the accountnumber attribute
            var fakedPlugin = fakedContext.ExecutePluginWithTarget<ArticleContentUpdate>(target);

            //Assert that the target contains the attributes updated by the plugin
            Assert.IsTrue(target.Attributes.ContainsKey("chili_wordcount"));
            Assert.IsTrue(target.Attributes.ContainsKey("chili_debugoutput"));
            
        }
    }
}
