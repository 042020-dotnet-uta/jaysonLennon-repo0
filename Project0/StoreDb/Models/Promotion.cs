using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace StoreDb
{
    public enum PromotionTypeDetail
    {
        PercentOff,
        ValueOff
    }

    public class Promotion
    {
        public Guid PromotionId { get; set; }
        public virtual Location Location { get; set; }
        public virtual Product Product { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public virtual PromotionType PromotionType { get; set; }
        public double Amount { get; set; }
    }

    public class PromotionType
    {
        public Guid PromotionTypeId { get; set; }
        public PromotionTypeDetail PromotionTypeDetail { get; set; }
        public string Name { get; set; }
    }
}
