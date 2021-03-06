﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Castle.ActiveRecord;
using NHibernate.Criterion;

namespace Training_wmqr.Models
{
    [ActiveRecord]
    public class User : ActiveRecordValidationBase<User>
    {
        [PrimaryKey]
        public int Id { get; set; }

        [Property]
        public string Name { get; set; }

        [Property]
        public string Username { get; set; }

        [Property]
        public string Email { get; set; }

        [HasMany(Cascade = ManyRelationCascadeEnum.All, Lazy = true)]
        public IList<Document> Documents { get; set; }

        [HasMany(Cascade = ManyRelationCascadeEnum.All, Lazy = true)]
        public IList<Favourite> Favourites { get; set; }

        public void TagFavourite(int documentId)
        {
            var fav = new Favourite
                          {
                              CreatedAt = DateTime.Now,
                              User = this,
                              Document = Document.Find(documentId)
                          };
            Favourites.Add(fav);
        }

        public static User FindByUsername(string username)
        {
            var user = FindFirst(Restrictions.Eq("Username", username));
            if(user == null)
            {
                throw new NotFoundException(String.Format("'{0}' not found", username));
            }
            return user;
        }

        public void AddFavourite(int id)
        {
            if(Favourites.Any(f => f.Document.Id == id))
                return;
            Favourites.Add(new Favourite
            {
                Document = Document.Find(id),
                CreatedAt = DateTime.Now
            });
            Save();
        }

        public void RemoveFavourite(int id)
        {
            var favourite = Favourites.First(f => f.Document.Id == id);
            Favourites.Remove(favourite);
            favourite.Delete();
            
        }
    }
}