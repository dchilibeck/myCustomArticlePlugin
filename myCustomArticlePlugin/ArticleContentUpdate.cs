using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xrm.Sdk;

namespace myCustomArticlePlugin
{
    public class ArticleContentUpdate : IPlugin
    {
        IOrganizationService service;
        IPluginExecutionContext context;
        ITracingService trace;
        Entity currentEntity;

        private void GetOrganizationService(IServiceProvider serviceProvider)
        {
            try
            {
                context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
                service = ((IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory))).CreateOrganizationService(context.UserId);
                trace = (ITracingService)serviceProvider.GetService(typeof(ITracingService));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void Execute(IServiceProvider serviceProvider)
        {
            try
            {
                GetOrganizationService(serviceProvider);

                currentEntity = (Entity)context.InputParameters["Target"];
                String KbContent = currentEntity.GetAttributeValue<string>("content");

                //int WordCount = this.CountWords(KbContent);
                //string ParsedContent = this.ParseContent(KbContent);

                int WordCount = ParseStrategy.CountWords(KbContent);
                string ParsedContent = ParseStrategy.ParseContent(KbContent);

                currentEntity["chili_wordcount"] = WordCount.ToString();
                currentEntity["chili_debugoutput"] = ParsedContent;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
