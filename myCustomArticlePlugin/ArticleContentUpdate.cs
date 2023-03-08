using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xrm.Sdk;

namespace Chilibeck.myCustomArticlePlugin
{
    /* This plugin will be registered against the preoperation step for the same entity so there is no need to perform an explicit save or update
     * operation in the plugin code as dynamics platform will take care of that for me. The actual parsing logic was also added to another class
     * with static methods which i think leads to a cleaner, more understandable design
     */

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
