using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Microsoft.Practices.ServiceLocation;
using SolrNet;
using SolrNet.Attributes;
using Training_wmqr.Models;

namespace DocumentCrawler
{
    class Program
    {
        static void Main(string[] args)
        {
            var solrUrl = @"http://localhost:8983/solr";
            var fixtures = @"D:\dev\training\Training-wmqr\data-fixtures\bwv\";
            
            // try this:
            // http://localhost:8983/solr/select?indent=on&version=2.2&q=maxwell&fq=%0D%0A&start=0&rows=100&fl=hl.*%2Cid%2Cauthor%2Ctitle%2Cscore&qt=&wt=&explainOther=&hl=on&hl.fl=text&hl.snippets=400


            Startup.Init<SolrDocument>(solrUrl);
            var solr = ServiceLocator.Current.GetInstance<ISolrOperations<SolrDocument>>();

            solr.Delete(SolrQuery.All);
            solr.Commit();
            
            
            var i = 0;
            foreach (var file in Directory.GetFiles(fixtures))
            {

                using (FileStream fileStream = File.OpenRead(file))
                {
                    var response =
                        solr.Extract(
                            new ExtractParameters(fileStream, i++.ToString())
                                {
                                    ExtractFormat = ExtractFormat.Text,
                                    ExtractOnly = false,
                                    Fields = new List<ExtractField>()
                                                 {
                                                     new ExtractField("title", Path.GetFileName(file)),
                                                     new ExtractField("author", "Baha'i World Volume")
                                                 }
                            });
                }
            }
            solr.Commit();
        }
    }
}
