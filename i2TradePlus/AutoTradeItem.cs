using System;
using System.Runtime.CompilerServices;

namespace i2TradePlus
{
	public class LocalAutoTradeItem
	{
        public enum AutoTradeCondition
        {
            COMMON,
            SMA,
            FOLLOW_BIGLOT
        }
        private int refNo;
        private string stockName;
        private string fieldType;
        private int operatorType;
        private decimal price;
        private string ordSide;
        private string ordPrice;
        private long ordVolume;
        private string ordCondition;
        private string status;
        private DateTime time;
        private DateTime mtime;
        private AutoTradeCondition conditionType;
        private string limit;
        private decimal firstBidVol;
        private decimal firstOfferVol;
        private decimal secondBidVol;
        private decimal secondOfferVol;

        public int RefNo
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get
            {
                return this.refNo;
            }
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
                this.refNo = value;
            }
        }

        public string StockName
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get
            {
                return this.stockName;
            }
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
                this.stockName = value;
            }
        }
        public string FieldType
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get
            {
                return this.fieldType;
            }
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
                this.fieldType = value;
            }
        }
        public int OperatorType
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get
            {
                return this.operatorType;
            }
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
                this.operatorType = value;
            }
        }
        public decimal Price
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get
            {
                return this.price;
            }
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
                this.price = value;
            }
        }
        public string OrdSide
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get
            {
                return this.ordSide;
            }
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
                this.ordSide = value;
            }
        }

        public string OrdPrice
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get
            {
                return this.ordPrice;
            }
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
                this.ordPrice = value;
            }
        }

        public long OrdVolume
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get
            {
                return this.ordVolume;
            }
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
                this.ordVolume = value;
            }
        }

        public string OrdCondition
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get
            {
                return this.ordCondition;
            }
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
                this.ordCondition = value;
            }
        }
        public string Status
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get
            {
                return this.status;
            }
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
                this.status = value;
            }
        }
        public DateTime Time
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get
            {
                return this.time;
            }
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
                this.time = value;
            }
        }
        public DateTime Mtime
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get
            {
                return this.mtime;
            }
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
                this.mtime = value;
            }
        }

        public AutoTradeCondition ConditionType
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get
            {
                return this.conditionType;
            }
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
                this.conditionType = value;
            }
        }

        public string Limit
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get
            {
                return this.limit;
            }
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
                this.limit = value;
            }
        }

        public decimal FirstBidVol
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get
            {
                return this.firstBidVol;
            }
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
                this.firstBidVol = value;
            }
        }

        public decimal FirstOfferVol
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get
            {
                return this.firstOfferVol;
            }
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
                this.firstOfferVol = value;
            }
        }

        public decimal SecondBidVol
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get
            {
                return this.secondBidVol;
            }
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
                this.secondBidVol = value;
            }
        }

        public decimal SecondOfferVol
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get
            {
                return this.secondOfferVol;
            }
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
                this.secondOfferVol = value;
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
		public LocalAutoTradeItem()
		{
		}
		[MethodImpl(MethodImplOptions.NoInlining)]
        public LocalAutoTradeItem(LocalAutoTradeItem item)
		{
            this.refNo = item.refNo;
            this.stockName = item.stockName;
            this.fieldType = item.fieldType;
            this.operatorType = item.operatorType;
            this.price = item.price;
            this.ordSide = item.ordSide;
            this.ordVolume = item.ordVolume;
            this.ordPrice = item.ordPrice;
            this.ordCondition = item.ordCondition;
            this.status = item.status;
            this.time = item.time;
            this.mtime = item.mtime;
            this.conditionType = item.conditionType;
            this.limit = item.limit;
            this.firstBidVol = item.firstBidVol;
            this.firstOfferVol = item.firstOfferVol;
            this.secondBidVol = item.secondBidVol;
            this.secondOfferVol = item.secondOfferVol;
		}
	}
}
