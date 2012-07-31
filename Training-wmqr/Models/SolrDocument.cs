using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SolrNet.Attributes;
using SolrNet.Impl;

namespace Training_wmqr.Models
{
    public class SolrDocument
    {
        public List<string> Results;
        public string Author { get; set; }

        public string Title { get; set; }

        public string Id { get; set; }
    }
}