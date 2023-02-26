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
      
        public void OutputResults(Entity target)
        {
            Console.Write(target["chili_wordcount"]);
            Console.WriteLine("");
            Console.Write(target["chili_debugoutput"]);
        }

        [TestMethod]
        public void TestArticlePluginWithHtmlConent()
        {

            var fakedContext = new XrmFakedContext();
            var guid1 = Guid.NewGuid();
            var target = new Entity("knowledgearticle") { Id = guid1 };

            target["title"] = "My Test Article";

            var KbContent = "<p style=font-style: normal; font-variant: normal; line-height: 18px;>Whether math problems, an English paper, or a science fair project.</span>";
            target["content"] = KbContent;

            //Execute our plugin against a target that contains the KnowledgeArticle content to be parsed
            var fakedPlugin = fakedContext.ExecutePluginWithTarget<ArticleContentUpdate>(target);


            //Assert that the target contains the attributes updated by the plugin
            Assert.IsTrue(target.Attributes.ContainsKey("chili_wordcount"));
            Assert.IsTrue(target.Attributes.ContainsKey("chili_debugoutput"));

            OutputResults(target);
        }

        [TestMethod]
        public void TestArticlePluginWithNullContent()
        {
            var fakedContext = new XrmFakedContext();

            var guid1 = Guid.NewGuid();

            var target = new Entity("knowledgearticle") { Id = guid1 };
            target["title"] = "My Test Article";
            
            //Execute our plugin against a target that contains the KnowledgeArticle content to be parsed
            var fakedPlugin = fakedContext.ExecutePluginWithTarget<ArticleContentUpdate>(target);
            var contentCount = target["chili_wordcount"];

            Assert.AreEqual("0", contentCount);

            OutputResults(target);
        }

        [TestMethod]
        public void TestArticlePluginIncludesUpdatedAttributesAfterExecution()
        {
            var fakedContext = new XrmFakedContext();

            var guid1 = Guid.NewGuid();
            
            var target = new Entity("knowledgearticle") { Id = guid1 };
            target["title"] = "My Test Article";
            target["content"] = "Hello World";

            //Execute our plugin against a target that contains the KnowledgeArticle content to be parsed
            var fakedPlugin = fakedContext.ExecutePluginWithTarget<ArticleContentUpdate>(target);

            
            //Assert that the target contains the attributes updated by the plugin
            Assert.IsTrue(target.Attributes.ContainsKey("chili_wordcount"));
            Assert.IsTrue(target.Attributes.ContainsKey("chili_debugoutput"));

            OutputResults(target);
        }

        [TestMethod]
        public void TestArticlePluginParsesPlainContentCorrectly()
        {
            var fakedContext = new XrmFakedContext();

            var guid1 = Guid.NewGuid();

            var target = new Entity("knowledgearticle") { Id = guid1 };
            target["title"] = "My Test Article";
            target["content"] = "My name is Vitoli and I live in Halifax.";

            //Execute our plugin against a target that contains the KnowledgeArticle content to be parsed
            var fakedPlugin = fakedContext.ExecutePluginWithTarget<ArticleContentUpdate>(target);

            var contentCount = target["chili_wordcount"];

            Assert.AreEqual("9", contentCount);

            OutputResults(target);
        }

    }
}
