using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Castle.ActiveRecord;

namespace Training_wmqr.Models
{
    [ActiveRecord]
    public class User : ActiveRecordValidationBase<User>
    {
        [PrimaryKey]
        public int Id { get; set; }

        [Property]
        public string Username { get; set; }

        [Property]
        public string Email { get; set; }
    }
}