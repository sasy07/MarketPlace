using System;

namespace MarketPlace.DataLayer.Entities.Common
{
    public class BaseEntity
    {
        public long Id { get; set; }
        public bool IsDelete { get; set; }
        public DateTime CreateDateTime { get; set; }
        public DateTime LastUpdateDate{ get; set; }
    }
}