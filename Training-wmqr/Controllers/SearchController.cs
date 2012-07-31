using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using Microsoft.Practices.ServiceLocation;
using SolrNet;
using SolrNet.Commands.Parameters;
using Training_wmqr.Models;

namespace Training_wmqr.Controllers
{
    public class SearchController : ApiController
    {
        private ISolrOperations<Dictionary<string, object>> solr;

        public SearchController()
        {
            solr = ServiceLocator.Current.GetInstance<ISolrOperations<Dictionary<string, object>>>();
        }
        
        public List<SolrDocument> Get(string q)
        {
            var results = solr.Query(q, new QueryOptions
            {
                Highlight = new HighlightingParameters
                {
                    Fields = new[] { "text" },
                    Snippets = 10,
                    Fragsize = 400
                }
            });
            var h =  results.Highlights;


            var output = new List<SolrDocument>();
            foreach (Dictionary<string, object> t in results)
            {
                var id = t["id"].ToString();
                var textFound = new List<string>();
                foreach (var snippet in results.Highlights[id])
                    textFound = snippet.Value.ToArray().ToList();

                output.Add(new SolrDocument()
                               {
                                   Title = ((ArrayList) t["title"])[0].ToString(),
                                   Id = id,
                                   Results = textFound
                               });
            }

            return output;
        }

    }
}
