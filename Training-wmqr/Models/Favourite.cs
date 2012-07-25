using System;
using Castle.ActiveRecord;

namespace Training_wmqr.Models
{
    [ActiveRecord]
    public class Favourite : ActiveRecordValidationBase<Favourite>
    {
        [PrimaryKey]
        public int Id { get; set; }

        [Property]
        public DateTime CreatedAt { get; set; }

        [BelongsTo]
        public User User { get; set; }

        [BelongsTo]
        public Document Document { get; set; }
    }
}