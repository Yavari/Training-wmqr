using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Castle.ActiveRecord;

namespace Training_wmqr.Models
{
    [ActiveRecord]
    public class Document : ActiveRecordValidationBase<Document>
    {
        [PrimaryKey]
        public int Id { get; set; }

        [Property]
        public string Title { get; set; }

        [Property]
        public string Text { get; set; }

        [BelongsTo]
        public User Author { get; set; }

        [HasMany(Cascade = ManyRelationCascadeEnum.All, Lazy = true)]
        public IList<Favourite> Favourites { get; set; } 

    }
}