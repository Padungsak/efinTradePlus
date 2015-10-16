using i2TradePlus.Information;
using i2TradePlus.MyDataSet;
using ITSNet.Common.BIZ;
using ITSNet.Common.BIZ.RealtimeMessage;
using ITSNet.Common.BIZ.RealtimeMessage.TFEX;
using System;
using System.Runtime.CompilerServices;
using System.Data;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using ITSNet.Common.BIZ.AutoTradeMessage;

namespace i2TradePlus
{
    internal class AutoTradeManager : IRealtimeMessage
    {
        internal delegate void OnStartAutoTradeHandler(string message);
        internal delegate void OnEndAutoTradeHandler();
        
        private enum OprationSign : byte {Add = 1, Subtract};
        private BackgroundWorker bgwAutoSendOrder = null;
        private List<LocalAutoTradeItem> _itemList;
        private LocalAutoTradeItem _currentAutoItem = null;
        private int _refCount = 0;
        private static AutoTradeManager instance = null;
        internal static AutoTradeManager Instance
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get
            {
                if (AutoTradeManager.instance == null)
                {
                    AutoTradeManager.instance = new AutoTradeManager();
                }
                return AutoTradeManager.instance;
            }
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
                AutoTradeManager.instance = value;
            }
        }

        public List<LocalAutoTradeItem> ItemList
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            get
            {
                return _itemList;
            }
        }

        internal AutoTradeManager.OnStartAutoTradeHandler _OnStartAutoTrade;
        internal event AutoTradeManager.OnStartAutoTradeHandler OnStartAutoTrade
        {
            [MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
            add
            {
                this._OnStartAutoTrade += value;
            }
            [MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
            remove
            {
                this._OnStartAutoTrade -= value;
            }
        }

        internal AutoTradeManager.OnEndAutoTradeHandler _OnEndAutoTrade;
        internal event AutoTradeManager.OnEndAutoTradeHandler OnEndAutoTrade
        {
            [MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
            add
            {
                this._OnEndAutoTrade += value;
            }
            [MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
            remove
            {
                this._OnEndAutoTrade -= value;
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal AutoTradeManager()
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void Init()
        {
            try
            {
                this.bgwAutoSendOrder = new BackgroundWorker();
                this.bgwAutoSendOrder.WorkerReportsProgress = true;
                this.bgwAutoSendOrder.DoWork += new DoWorkEventHandler(this.bgwAutoSendOrder_DoWork);
                this.bgwAutoSendOrder.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.bgwAutoSendOrder_RunWorkerCompleted);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void bgwAutoSendOrder_DoWork(object sender, DoWorkEventArgs e)
        {
            if (ApplicationInfo.IsEquityAccount)
            {
                try
                {
                    StockList.StockInformation stockInformation = ApplicationInfo.StockInfo[_currentAutoItem.StockName];
                     
                     if (this._OnStartAutoTrade != null)
                     {
                         this._OnStartAutoTrade("Start sending auto order of " + stockInformation.Symbol);
                     }

                     ApplicationInfo.SendNewOrderResult newOrderRet = ApplicationInfo.SendNewOrder(stockInformation.Symbol,
                                                                                                     _currentAutoItem.OrdSide,
                                                                                                     _currentAutoItem.OrdVolume,
                                                                                                     _currentAutoItem.OrdPrice,
                                                                                                     0,
                                                                                                     _currentAutoItem.OrdCondition,
                                                                                                     0,
                                                                                                     "",
                                                                                                     false);
                     string addedMessage = string.Empty;
                     if (_currentAutoItem.OrdSide == "B")
                     {
                         addedMessage = "Buy " + _currentAutoItem.StockName + " Price " + _currentAutoItem.OrdPrice + " Volume " + _currentAutoItem.OrdVolume;
                     }
                     else
                     {
                         addedMessage = "Sell " + _currentAutoItem.StockName + " Price " + _currentAutoItem.OrdPrice + " Volume " + _currentAutoItem.OrdVolume;

                     } 

                     if (newOrderRet.OrderNo > 0L)
                     {
                         ApplicationInfo.AddOrderNoToAutoRefreshList(newOrderRet.OrderNo.ToString(), newOrderRet.IsFwOfflineOrder ? 3 : 1);          
                         UpdateExecutedItem(_currentAutoItem.RefNo, AutoTradeConstant.STATUS_SUCCESSFULL, addedMessage);
                     }
                     else
                     {
                         //Indeicate some error message
                         addedMessage += " ERROR: " + newOrderRet.ResultMessage;
                         UpdateExecutedItem(_currentAutoItem.RefNo, AutoTradeConstant.STATUS_FAIL, addedMessage);
                     }


                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void bgwAutoSendOrder_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                string data = string.Empty;
                try
                {
                    if (this._OnEndAutoTrade != null)
                    {
                        System.Threading.Thread.Sleep(3000);           
                        this._OnEndAutoTrade();
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void UpdateItemList(LocalAutoTradeItem itemArg)
        {
            if (_itemList == null)
            {
                _itemList = new List<LocalAutoTradeItem>();
            }
            itemArg.RefNo = _refCount;
            itemArg.Status = AutoTradeConstant.STATUS_WAIT;

            try
            {
                StockList.StockInformation stockInformation = ApplicationInfo.StockInfo[itemArg.StockName];
                string returnPage = ApplicationInfo.WebService.StockByPricePage(stockInformation.Number, 1);
                DataSet stockPageInfo = new DataSet();
                MyDataHelper.StringToDataSet(returnPage, stockPageInfo);
                DataRow dataRow = stockPageInfo.Tables["security_stat"].Rows[0];
                itemArg.FirstBidVol = Convert.ToDecimal(dataRow["bid_volume1"]);
                itemArg.SecondBidVol = Convert.ToDecimal(dataRow["bid_volume2"]);
                itemArg.FirstOfferVol = Convert.ToDecimal(dataRow["offer_volume1"]);
                itemArg.SecondOfferVol = Convert.ToDecimal(dataRow["offer_volume2"]);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            _itemList.Add(itemArg);
            ++_refCount;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void ReceiveMessage(IBroadcastMessage message, StockList.StockInformation realtimeStockInfo)
        {
            //if (ApplicationInfo.AutoTradeType > 0)
            //{
            //    return;
            //}
            try
            {
                string messageType = message.MessageType;
                if (messageType != null)
                {                   
                    if (messageType == "L+")
                    {
                        this.Execute((LSAccumulate)message, realtimeStockInfo);
                    }
                    else if (messageType == "TP")
                    {
                        UpdateVolum((TPMessage)message, realtimeStockInfo);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void ReceiveTfexMessage(IBroadcastMessage message, SeriesList.SeriesInformation realtimeSeriesInfo)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void UpdateVolum(TPMessage msgLS, StockList.StockInformation realtimeStockInfo)
        {
            foreach (LocalAutoTradeItem item in this._itemList)
            {
                if (item.StockName == realtimeStockInfo.Symbol &&
                    item.Status == AutoTradeConstant.STATUS_WAIT)
                {
                    if (msgLS.Side == "B")
                    {
                        item.FirstBidVol = msgLS.Volume1;
                        item.SecondBidVol = msgLS.Volume2;
                    }
                    else if (msgLS.Side == "S")
                    {
                        item.FirstOfferVol = msgLS.Volume1;
                        item.SecondOfferVol = msgLS.Volume2;
                    }
                    else
                    {
                        //Don't know if there is any side
                    }

                }
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void Execute(LSAccumulate msgLS, StockList.StockInformation realtimeStockInfo)
        {
            foreach (LocalAutoTradeItem item in this._itemList)
            {
                if (item.StockName == realtimeStockInfo.Symbol &&
                    item.Status == AutoTradeConstant.STATUS_WAIT)
                {
                    
                    if (IsConditionMeet(msgLS, item, realtimeStockInfo))
                    {
                        if (!this.bgwAutoSendOrder.IsBusy)
                        {
                            if (item.ConditionType == LocalAutoTradeItem.AutoTradeCondition.FOLLOW_BIGLOT)
                            {
                                item.OrdPrice = msgLS.LastPrice.ToString();
                            }
                            _currentAutoItem = item;
                            this.bgwAutoSendOrder.RunWorkerAsync();
                        }
                    }
                }
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public bool IsConditionMeet(LSAccumulate msgLS, LocalAutoTradeItem autoItem, StockList.StockInformation realtimeStockInfo)
        {
            switch (autoItem.ConditionType)
            {
                case LocalAutoTradeItem.AutoTradeCondition.COMMON :
                    return CheckCommonCondition(msgLS, autoItem, realtimeStockInfo);
                case LocalAutoTradeItem.AutoTradeCondition.FOLLOW_BIGLOT :
                    return CheckBiglotCondition(msgLS, autoItem, realtimeStockInfo);
            }
            return false;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public bool CheckBiglotCondition(LSAccumulate msgLS, LocalAutoTradeItem autoItem, StockList.StockInformation realtimeStockInfo)
        {
            if (autoItem.OrdSide == "S")
            {
                if ((msgLS.Side == "S") && (autoItem.Value < (msgLS.Volume * realtimeStockInfo.BoardLot) ))
                {
                    return true;
                }
            }
            else
            {
                if ((msgLS.Side == "B") && (autoItem.Value < (msgLS.Volume * realtimeStockInfo.BoardLot)))
                {
                    return true;
                }
            }
            return false;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public bool CheckCommonCondition(LSAccumulate msgLS, LocalAutoTradeItem autoItem, StockList.StockInformation realtimeStockInfo)
        {
            switch (autoItem.OperatorType)
            {
                case AutoTradeConstant.OPERATOR_LESSER_EQUAL:
                    {
                        if (autoItem.OrdSide == "S")
                        {
                            if (msgLS.Side == "S")
                            {
                                decimal priceToCompare = autoItem.Value;

                                if (msgLS.LastPrice == priceToCompare)
                                {
                                    autoItem.FirstBidVol -= msgLS.Volume;
                                    if ((autoItem.FirstBidVol / 5) <= (autoItem.OrdVolume / realtimeStockInfo.BoardLot))
                                    {
                                        return true;
                                    }

                                }

                                decimal bidPrice1;
                                Decimal.TryParse(realtimeStockInfo.BidPrice1, out bidPrice1);
                                if (bidPrice1 < priceToCompare)
                                {
                                    return true;
                                }
                            }
                        }         
                        return false;
                    }
            }
            return false;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void CancelOrder(int refNo)
        {
            foreach (LocalAutoTradeItem item in this._itemList)
            {
                if (item.RefNo == refNo)
                {
                    _itemList.Remove(item);
                    return;
                }
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private decimal AdjustPrice(decimal basePrice, int numberOfGab, OprationSign sign)
        {
            decimal ret = basePrice;
            for (int gab = 0; gab < numberOfGab; ++gab)
            {
                if (basePrice < 2)
                {
                    ret = (sign == OprationSign.Add) ? (ret + 0.01m) : (ret - 0.01m); 
                }
                else if (basePrice < 5)
                {
                    ret = (sign == OprationSign.Add) ? (ret + 0.02m) : (ret - 0.02m); 
                }
                else if (basePrice < 10)
                {
                    ret = (sign == OprationSign.Add) ? (ret + 0.05m) : (ret - 0.05m);
                }
                else if (basePrice < 25)
                {
                    ret = (sign == OprationSign.Add) ? (ret + 0.1m) : (ret - 0.1m);
                }
                else if (basePrice < 100)
                {
                    ret = (sign == OprationSign.Add) ? (ret + 0.25m) : (ret - 0.25m);
                }
                else if (basePrice < 200)
                {
                    ret = (sign == OprationSign.Add) ? (ret + 0.5m) : (ret - 0.5m);
                }
                else if (basePrice < 400)
                {
                    ret = (sign == OprationSign.Add) ? (ret + 1m) : (ret - 1m);
                }
                else
                {
                    ret = (sign == OprationSign.Add) ? (ret + 2m) : (ret - 2m);
                }
            }
            return ret;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void UpdateExecutedItem(int refNo, string newStatus, string message)
        {
            foreach (LocalAutoTradeItem item in this._itemList)
            {
                if (item.RefNo == refNo)
                {
                    item.Status = newStatus;
                    item.Mtime = DateTime.Now;
                    item.Message = message;
                    return;
                }
            }
        }
    }
}
