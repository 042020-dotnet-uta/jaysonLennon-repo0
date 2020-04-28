using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoreDb
{
    public enum DiscountType
    {
        PercentOff,
        ValueOff,
    }

    public class Promotion
    {
        private int promotionId;
        public int PromotionId
        {
            get { return promotionId; }
            set { promotionId = value; }
        }

        private int productId;
        public int ProductId
        {
            get { return productId; }
            set { productId = value; }
        }

        private int locationId;
        public int LocationId
        {
            get { return locationId; }
            set { locationId = value; }
        }

        private DateTime promoStart;
        public DateTime PromoStart
        {
            get { return promoStart; }
            set { promoStart = value; }
        }

        private DateTime promoEnd;
        public DateTime PromoEnd
        {
            get { return promoEnd; }
            set { promoEnd = value; }
        }


        private DiscountType discountType;
        public DiscountType DiscountType
        {
            get { return discountType; }
            set { discountType = value; }
        }

        private double discountValue;
        public double DiscountValue
        {
            get { return discountValue; }
            set { discountValue = value; }
        }



    }
}
