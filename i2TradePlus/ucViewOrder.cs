using i2TradePlus.Information;
using i2TradePlus.ITSNetBusinessWSTFEX;
using i2TradePlus.Properties;
using ITSNet.Common.BIZ;
using ITSNet.Common.BIZ.RealtimeMessage;
using ITSNet.Common.BIZ.WebClient;
using STIControl;
using STIControl.SortTableGrid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;

namespace i2TradePlus
{
	internal class ucViewOrder : UserControl, IRealtimeMessage
	{
		public delegate void OnDisplaySummaryOrdersHandler();

		private struct RecordData
		{
			public long OrderNumber;

			public string Stock;

			public string Side;

			public long Volume;

			public long Matched;

			public long PubVol;

			public string price;

			public string OrderStatus;

			public int TrusteeID;

			public string OrderTimes;

			public string Quote;

			public string ApprUserNo;

			public string OrderDate;

			public string OrderTime;

			public bool IsAfterCloseOrder;

			public string Position;

			public string Series;

			public string OrdType;

			public string Price
			{
				[MethodImpl(MethodImplOptions.NoInlining)]
				get
				{
					return this.price;
				}
				[MethodImpl(MethodImplOptions.NoInlining)]
				set
				{
					this.price = value.Trim();
					if (ApplicationInfo.SupportFreewill)
					{
						this.price = Utilities.ConvertPrice(this.price);
					}
				}
			}
		}

		private delegate void ShowSplashCallBack(bool visible, string message, bool isAutoClose);

		private delegate void ReceiveOrderInfoTFEXCallBack(IBroadcastMessage message);

		private delegate void ReceiveOrderInfoCallBack(IBroadcastMessage message);

		private delegate void SetGridFocusCallBack(bool forceIndex);

		private delegate void UpdateDataToControlTFEXCallBack();

		private class CancelItem
		{
			public long OrderNo = 0L;

			public string OrderDate = string.Empty;

			public string OrderTime = string.Empty;

			public string AfterClose = "N";

			public bool Complete = false;

			[MethodImpl(MethodImplOptions.NoInlining)]
			public CancelItem(long orderNo, string orderDate, string orderTime, string afterClose)
			{
				this.OrderNo = orderNo;
				this.OrderDate = orderDate;
				this.OrderTime = orderTime;
				this.AfterClose = afterClose;
			}
		}

		private delegate void ShowMessageInFormConfirmCallBack(string message, frmOrderFormConfirm.OpenStyle openStyle);

		private delegate void SelectAllOrderForCancelCallBack(bool setSelectAll);

		private const int VIEW_EQUITY_ORDER = 1;

		private const int VIEW_TFEX_ORDER = 3;

		private IContainer components = null;

		private ColumnHeader colConfirm;

		private ColumnHeader colVolume;

		private ColumnHeader colPrice;

		private ColumnHeader colTime;

		private ToolStrip tStripMenu;

		private ToolStripLabel tslbStatus;

		private ToolStripLabel tslbStock;

		private ToolStripButton tsbtnSearch;

		private ToolStripButton tsbtnCancelOrder;

		private ToolStripButton tsbtnClearCondition;

		private ToolStripComboBox tscbStatus;

		private ToolStripTextBox tstbStock;

		private ToolStripComboBox tscbSide;

		private ToolStripLabel tslbSide;

		private Label lbLoading;

		private ToolStripTextBox tstbPrice;

		private ToolStripLabel tslbPrice;

		private ContextMenuStrip contextMenuStrip1;

		private ToolStripMenuItem tsmRefresh;

		private SortGrid intzaOrderList;

		private ToolStripButton tsbtnEditOrder;

		private SortGrid intzaOrderListTFEX;

		private ToolStripButton tsbtnReloadReorder;

		private int _viewType = 1;

		private string selStatus = string.Empty;

		private string _selAccount = string.Empty;

		private string selStock = string.Empty;

		private string selSide = string.Empty;

		private string selPrice = string.Empty;

		private frmViewOrderInfo _viewOrderInfo = null;

		private frmViewOrderInfoTFEX _viewOrderInfoTfex = null;

		private DataSet tdsOrder = null;

		private bool _isSelectAll = false;

		private bool isActive = false;

		private bool isLoadingData = false;

		private System.Windows.Forms.Timer tmCloseSplash = null;

		private bool isShowLoadingControl = false;

		private bool showOnMainForm = false;

		private bool isShowToolsBar = true;

		private bool isShowNextPage = true;

		private long totalBuyVolume = 0L;

		private long totalBuyMatchedVolume = 0L;

		private decimal totalBuyMatchedValue = 0m;

		private long totalSellVolume = 0L;

		private long totalSellMatchedVolume = 0L;

		private decimal totalSellMatchedValue = 0m;

		private decimal unMatchedBuyVol = 0m;

		private decimal unMatchedSellVol = 0m;

		private BackgroundWorker bgwViewOrder = null;

		private bool _isSetHeaderDone = false;

		private bool focusFlag = false;

		private List<ucViewOrder.CancelItem> _listOfOrderCancel = null;

		private System.Windows.Forms.Timer tmTest = null;

		private frmOrderFormConfirm frmConfirm = null;

		private frmEditOrder _editOrderBox = null;

		public event ucViewOrder.OnDisplaySummaryOrdersHandler OnDisplaySummaryOrders
		{
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			add
			{
				this.OnDisplaySummaryOrders = (ucViewOrder.OnDisplaySummaryOrdersHandler)Delegate.Combine(this.OnDisplaySummaryOrders, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized | MethodImplOptions.NoInlining)]
			remove
			{
				this.OnDisplaySummaryOrders = (ucViewOrder.OnDisplaySummaryOrdersHandler)Delegate.Remove(this.OnDisplaySummaryOrders, value);
			}
		}

		public int ViewType
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this._viewType;
			}
		}

		public bool IsActive
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.isActive;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.isActive = value;
				if (!base.DesignMode)
				{
					if (this.isActive)
					{
						this.intzaOrderList.ClearAllText();
						this.intzaOrderList.EndUpdate();
					}
					else
					{
						this._selAccount = string.Empty;
					}
				}
			}
		}

		public bool IsLoadingData
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.isLoadingData;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.isLoadingData = value;
				if (!base.DesignMode)
				{
					this.ShowSplash(this.isLoadingData, "Loading...", false);
				}
			}
		}

		public bool IsShowLoadingControl
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.isShowLoadingControl;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.isShowLoadingControl = value;
			}
		}

		public bool ShowOnMainForm
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.showOnMainForm;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.showOnMainForm = value;
			}
		}

		public bool IsShowToolsBar
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.isShowToolsBar;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.isShowToolsBar = value;
			}
		}

		public bool IsShowNextPage
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.isShowNextPage;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				this.isShowNextPage = value;
			}
		}

		public long TotalBuyVolume
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.totalBuyVolume;
			}
		}

		public long TotalBuyMatchedVolume
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.totalBuyMatchedVolume;
			}
		}

		public decimal TotalBuyMatchedValue
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.totalBuyMatchedValue;
			}
		}

		public long TotalSellVolume
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.totalSellVolume;
			}
		}

		public long TotalSellMatchedVolume
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.totalSellMatchedVolume;
			}
		}

		public decimal TotalSellMatchedValue
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.totalSellMatchedValue;
			}
		}

		public decimal UnMatchedBuyVol
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.unMatchedBuyVol;
			}
		}

		public decimal UnMatchedSellVol
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return this.unMatchedSellVol;
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		private void InitializeComponent()
		{
			this.components = new Container();
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(ucViewOrder));
			ColumnItem columnItem = new ColumnItem();
			ColumnItem columnItem2 = new ColumnItem();
			ColumnItem columnItem3 = new ColumnItem();
			ColumnItem columnItem4 = new ColumnItem();
			ColumnItem columnItem5 = new ColumnItem();
			ColumnItem columnItem6 = new ColumnItem();
			ColumnItem columnItem7 = new ColumnItem();
			ColumnItem columnItem8 = new ColumnItem();
			ColumnItem columnItem9 = new ColumnItem();
			ColumnItem columnItem10 = new ColumnItem();
			ColumnItem columnItem11 = new ColumnItem();
			ColumnItem columnItem12 = new ColumnItem();
			ColumnItem columnItem13 = new ColumnItem();
			ColumnItem columnItem14 = new ColumnItem();
			ColumnItem columnItem15 = new ColumnItem();
			ColumnItem columnItem16 = new ColumnItem();
			ColumnItem columnItem17 = new ColumnItem();
			ColumnItem columnItem18 = new ColumnItem();
			ColumnItem columnItem19 = new ColumnItem();
			ColumnItem columnItem20 = new ColumnItem();
			ColumnItem columnItem21 = new ColumnItem();
			ColumnItem columnItem22 = new ColumnItem();
			ColumnItem columnItem23 = new ColumnItem();
			ColumnItem columnItem24 = new ColumnItem();
			ColumnItem columnItem25 = new ColumnItem();
			ColumnItem columnItem26 = new ColumnItem();
			ColumnItem columnItem27 = new ColumnItem();
			ColumnItem columnItem28 = new ColumnItem();
			ColumnItem columnItem29 = new ColumnItem();
			ColumnItem columnItem30 = new ColumnItem();
			ColumnItem columnItem31 = new ColumnItem();
			ColumnItem columnItem32 = new ColumnItem();
			this.colConfirm = new ColumnHeader();
			this.colVolume = new ColumnHeader();
			this.colPrice = new ColumnHeader();
			this.colTime = new ColumnHeader();
			this.tStripMenu = new ToolStrip();
			this.tslbStatus = new ToolStripLabel();
			this.tscbStatus = new ToolStripComboBox();
			this.tslbStock = new ToolStripLabel();
			this.tstbStock = new ToolStripTextBox();
			this.tslbPrice = new ToolStripLabel();
			this.tstbPrice = new ToolStripTextBox();
			this.tslbSide = new ToolStripLabel();
			this.tscbSide = new ToolStripComboBox();
			this.tsbtnClearCondition = new ToolStripButton();
			this.tsbtnCancelOrder = new ToolStripButton();
			this.tsbtnSearch = new ToolStripButton();
			this.tsbtnEditOrder = new ToolStripButton();
			this.tsbtnReloadReorder = new ToolStripButton();
			this.lbLoading = new Label();
			this.contextMenuStrip1 = new ContextMenuStrip(this.components);
			this.tsmRefresh = new ToolStripMenuItem();
			this.intzaOrderListTFEX = new SortGrid();
			this.intzaOrderList = new SortGrid();
			this.tStripMenu.SuspendLayout();
			this.contextMenuStrip1.SuspendLayout();
			base.SuspendLayout();
			this.colConfirm.Text = "Confirm#";
			this.colConfirm.Width = 67;
			this.colVolume.Text = "Volume";
			this.colVolume.Width = 78;
			this.colPrice.Text = "Price";
			this.colPrice.Width = 56;
			this.colTime.Text = "Time";
			this.colTime.Width = 64;
			this.tStripMenu.AllowMerge = false;
			this.tStripMenu.BackColor = Color.FromArgb(30, 30, 30);
			this.tStripMenu.CanOverflow = false;
			this.tStripMenu.GripMargin = new Padding(0);
			this.tStripMenu.GripStyle = ToolStripGripStyle.Hidden;
			this.tStripMenu.Items.AddRange(new ToolStripItem[]
			{
				this.tslbStatus,
				this.tscbStatus,
				this.tslbStock,
				this.tstbStock,
				this.tslbPrice,
				this.tstbPrice,
				this.tslbSide,
				this.tscbSide,
				this.tsbtnClearCondition,
				this.tsbtnCancelOrder,
				this.tsbtnSearch,
				this.tsbtnEditOrder,
				this.tsbtnReloadReorder
			});
			this.tStripMenu.LayoutStyle = ToolStripLayoutStyle.HorizontalStackWithOverflow;
			this.tStripMenu.Location = new Point(0, 0);
			this.tStripMenu.Name = "tStripMenu";
			this.tStripMenu.Padding = new Padding(1, 2, 1, 1);
			this.tStripMenu.RenderMode = ToolStripRenderMode.System;
			this.tStripMenu.Size = new Size(833, 28);
			this.tStripMenu.TabIndex = 57;
			this.tslbStatus.BackColor = Color.Transparent;
			this.tslbStatus.ForeColor = Color.Gainsboro;
			this.tslbStatus.Margin = new Padding(1);
			this.tslbStatus.Name = "tslbStatus";
			this.tslbStatus.Size = new Size(39, 23);
			this.tslbStatus.Text = "Status";
			this.tscbStatus.AutoCompleteCustomSource.AddRange(new string[]
			{
				"ALL",
				"O",
				"PO",
				"M",
				"C",
				"PX",
				"R",
				"X"
			});
			this.tscbStatus.AutoCompleteMode = AutoCompleteMode.Append;
			this.tscbStatus.AutoCompleteSource = AutoCompleteSource.CustomSource;
			this.tscbStatus.BackColor = Color.FromArgb(45, 45, 45);
			this.tscbStatus.ForeColor = Color.LightGray;
			this.tscbStatus.Items.AddRange(new object[]
			{
				"ALL",
				"O",
				"PO",
				"M",
				"C",
				"PX",
				"R",
				"X"
			});
			this.tscbStatus.Margin = new Padding(1, 0, 1, 2);
			this.tscbStatus.MaxLength = 3;
			this.tscbStatus.Name = "tscbStatus";
			this.tscbStatus.Size = new Size(75, 23);
			this.tscbStatus.KeyUp += new KeyEventHandler(this.tscbStatus_KeyUp);
			this.tscbStatus.KeyDown += new KeyEventHandler(this.tscbStatus_KeyDown);
			this.tscbStatus.Leave += new EventHandler(this.controlOrder_Leave);
			this.tscbStatus.Enter += new EventHandler(this.controlOrder_Enter);
			this.tscbStatus.TextChanged += new EventHandler(this.tscbStatus_TextChanged);
			this.tslbStock.BackColor = Color.Transparent;
			this.tslbStock.ForeColor = Color.Gainsboro;
			this.tslbStock.Margin = new Padding(1);
			this.tslbStock.Name = "tslbStock";
			this.tslbStock.Size = new Size(47, 23);
			this.tslbStock.Text = "Symbol";
			this.tstbStock.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
			this.tstbStock.AutoCompleteSource = AutoCompleteSource.CustomSource;
			this.tstbStock.BackColor = Color.FromArgb(45, 45, 45);
			this.tstbStock.BorderStyle = BorderStyle.FixedSingle;
			this.tstbStock.CharacterCasing = CharacterCasing.Upper;
			this.tstbStock.Font = new Font("Microsoft Sans Serif", 9f);
			this.tstbStock.ForeColor = Color.LightGray;
			this.tstbStock.Margin = new Padding(1, 0, 1, 2);
			this.tstbStock.MaxLength = 12;
			this.tstbStock.Name = "tstbStock";
			this.tstbStock.Size = new Size(80, 23);
			this.tstbStock.Leave += new EventHandler(this.controlOrder_Leave);
			this.tstbStock.KeyDown += new KeyEventHandler(this.tstbStock_KeyDown);
			this.tstbStock.Enter += new EventHandler(this.controlOrder_Enter);
			this.tstbStock.KeyUp += new KeyEventHandler(this.tstbStock_KeyUp);
			this.tslbPrice.BackColor = Color.Transparent;
			this.tslbPrice.ForeColor = Color.Gainsboro;
			this.tslbPrice.Margin = new Padding(1);
			this.tslbPrice.Name = "tslbPrice";
			this.tslbPrice.Size = new Size(33, 23);
			this.tslbPrice.Text = "Price";
			this.tstbPrice.BackColor = Color.FromArgb(45, 45, 45);
			this.tstbPrice.BorderStyle = BorderStyle.FixedSingle;
			this.tstbPrice.ForeColor = Color.LightGray;
			this.tstbPrice.Margin = new Padding(1, 0, 1, 2);
			this.tstbPrice.MaxLength = 8;
			this.tstbPrice.Name = "tstbPrice";
			this.tstbPrice.Size = new Size(65, 23);
			this.tstbPrice.Leave += new EventHandler(this.controlOrder_Leave);
			this.tstbPrice.Enter += new EventHandler(this.controlOrder_Enter);
			this.tstbPrice.KeyUp += new KeyEventHandler(this.tstbPrice_KeyUp);
			this.tslbSide.BackColor = Color.Transparent;
			this.tslbSide.ForeColor = Color.Gainsboro;
			this.tslbSide.Margin = new Padding(1);
			this.tslbSide.Name = "tslbSide";
			this.tslbSide.Size = new Size(29, 23);
			this.tslbSide.Text = "Side";
			this.tscbSide.AutoCompleteCustomSource.AddRange(new string[]
			{
				"ALL",
				"B",
				"S",
				"H",
				"C"
			});
			this.tscbSide.AutoCompleteMode = AutoCompleteMode.Append;
			this.tscbSide.AutoCompleteSource = AutoCompleteSource.CustomSource;
			this.tscbSide.BackColor = Color.FromArgb(45, 45, 45);
			this.tscbSide.ForeColor = Color.LightGray;
			this.tscbSide.Items.AddRange(new object[]
			{
				"ALL",
				"B",
				"S",
				"H",
				"C"
			});
			this.tscbSide.Margin = new Padding(1, 0, 1, 2);
			this.tscbSide.MaxLength = 3;
			this.tscbSide.Name = "tscbSide";
			this.tscbSide.Size = new Size(75, 23);
			this.tscbSide.KeyUp += new KeyEventHandler(this.tscbSide_KeyUp);
			this.tscbSide.KeyDown += new KeyEventHandler(this.tscbStatus_KeyDown);
			this.tscbSide.Leave += new EventHandler(this.controlOrder_Leave);
			this.tscbSide.Enter += new EventHandler(this.controlOrder_Enter);
			this.tscbSide.TextChanged += new EventHandler(this.tscbSide_TextChanged);
			this.tsbtnClearCondition.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.tsbtnClearCondition.ForeColor = Color.Gainsboro;
			this.tsbtnClearCondition.ImageTransparentColor = Color.Magenta;
			this.tsbtnClearCondition.Margin = new Padding(5, 1, 0, 2);
			this.tsbtnClearCondition.Name = "tsbtnClearCondition";
			this.tsbtnClearCondition.Size = new Size(38, 22);
			this.tsbtnClearCondition.Text = "Clear";
			this.tsbtnClearCondition.ToolTipText = "Clear Condition";
			this.tsbtnClearCondition.Click += new EventHandler(this.tsbtnClearCondition_Click);
			this.tsbtnCancelOrder.Alignment = ToolStripItemAlignment.Right;
			this.tsbtnCancelOrder.ForeColor = Color.Tomato;
			this.tsbtnCancelOrder.Image = (Image)componentResourceManager.GetObject("tsbtnCancelOrder.Image");
			this.tsbtnCancelOrder.ImageTransparentColor = Color.Magenta;
			this.tsbtnCancelOrder.Name = "tsbtnCancelOrder";
			this.tsbtnCancelOrder.Size = new Size(63, 22);
			this.tsbtnCancelOrder.Text = "Cancel";
			this.tsbtnCancelOrder.ToolTipText = "Cancel Order";
			this.tsbtnCancelOrder.Click += new EventHandler(this.tsbtnCancelOrder_Click);
			this.tsbtnSearch.Font = new Font("Microsoft Sans Serif", 9f);
			this.tsbtnSearch.ForeColor = Color.Gainsboro;
			this.tsbtnSearch.Image = Resources.refresh;
			this.tsbtnSearch.ImageTransparentColor = Color.Magenta;
			this.tsbtnSearch.Margin = new Padding(5, 1, 0, 2);
			this.tsbtnSearch.Name = "tsbtnSearch";
			this.tsbtnSearch.Size = new Size(66, 22);
			this.tsbtnSearch.Text = "Search";
			this.tsbtnSearch.Click += new EventHandler(this.tsbtnSearch_Click);
			this.tsbtnEditOrder.Alignment = ToolStripItemAlignment.Right;
			this.tsbtnEditOrder.ForeColor = Color.Yellow;
			this.tsbtnEditOrder.Image = (Image)componentResourceManager.GetObject("tsbtnEditOrder.Image");
			this.tsbtnEditOrder.ImageTransparentColor = Color.Magenta;
			this.tsbtnEditOrder.Margin = new Padding(0, 1, 5, 2);
			this.tsbtnEditOrder.Name = "tsbtnEditOrder";
			this.tsbtnEditOrder.Size = new Size(47, 22);
			this.tsbtnEditOrder.Text = "Edit";
			this.tsbtnEditOrder.ToolTipText = "Edit Order";
			this.tsbtnEditOrder.Click += new EventHandler(this.tsbtnEditOrder_Click);
			this.tsbtnReloadReorder.Alignment = ToolStripItemAlignment.Right;
			this.tsbtnReloadReorder.ForeColor = Color.LightGray;
			this.tsbtnReloadReorder.Image = Resources.refresh;
			this.tsbtnReloadReorder.ImageTransparentColor = Color.Magenta;
			this.tsbtnReloadReorder.Name = "tsbtnReloadReorder";
			this.tsbtnReloadReorder.Size = new Size(66, 22);
			this.tsbtnReloadReorder.Text = "Refresh";
			this.lbLoading.AutoSize = true;
			this.lbLoading.BackColor = Color.FromArgb(64, 64, 64);
			this.lbLoading.BorderStyle = BorderStyle.FixedSingle;
			this.lbLoading.Font = new Font("Microsoft Sans Serif", 9f, FontStyle.Regular, GraphicsUnit.Point, 222);
			this.lbLoading.ForeColor = Color.Yellow;
			this.lbLoading.Location = new Point(378, 138);
			this.lbLoading.Name = "lbLoading";
			this.lbLoading.Padding = new Padding(5, 3, 5, 3);
			this.lbLoading.Size = new Size(76, 23);
			this.lbLoading.TabIndex = 61;
			this.lbLoading.Text = "Loading ...";
			this.lbLoading.Visible = false;
			this.contextMenuStrip1.Items.AddRange(new ToolStripItem[]
			{
				this.tsmRefresh
			});
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new Size(111, 26);
			this.tsmRefresh.Image = Resources.refresh;
			this.tsmRefresh.Name = "tsmRefresh";
			this.tsmRefresh.Size = new Size(110, 22);
			this.tsmRefresh.Text = "Reload";
			this.tsmRefresh.Click += new EventHandler(this.tsmRefresh_Click);
			this.intzaOrderListTFEX.AllowDrop = true;
			this.intzaOrderListTFEX.BackColor = Color.FromArgb(30, 30, 30);
			this.intzaOrderListTFEX.CanBlink = false;
			this.intzaOrderListTFEX.CanDrag = false;
			this.intzaOrderListTFEX.CanGetMouseMove = false;
			columnItem.Alignment = StringAlignment.Center;
			columnItem.BackColor = Color.FromArgb(64, 64, 64);
			columnItem.ColumnAlignment = StringAlignment.Center;
			columnItem.FontColor = Color.LightGray;
			columnItem.MyStyle = FontStyle.Regular;
			columnItem.Name = "checkbox";
			columnItem.Text = "";
			columnItem.ValueFormat = FormatType.Bitmap;
			columnItem.Visible = true;
			columnItem.Width = 3;
			columnItem2.Alignment = StringAlignment.Near;
			columnItem2.BackColor = Color.FromArgb(64, 64, 64);
			columnItem2.ColumnAlignment = StringAlignment.Center;
			columnItem2.FontColor = Color.LightGray;
			columnItem2.MyStyle = FontStyle.Regular;
			columnItem2.Name = "order_number";
			columnItem2.Text = "Order No.";
			columnItem2.ValueFormat = FormatType.Text;
			columnItem2.Visible = true;
			columnItem2.Width = 10;
			columnItem3.Alignment = StringAlignment.Center;
			columnItem3.BackColor = Color.FromArgb(64, 64, 64);
			columnItem3.ColumnAlignment = StringAlignment.Center;
			columnItem3.FontColor = Color.LightGray;
			columnItem3.MyStyle = FontStyle.Regular;
			columnItem3.Name = "position";
			columnItem3.Text = "Pos";
			columnItem3.ValueFormat = FormatType.Text;
			columnItem3.Visible = true;
			columnItem3.Width = 6;
			columnItem4.Alignment = StringAlignment.Center;
			columnItem4.BackColor = Color.FromArgb(64, 64, 64);
			columnItem4.ColumnAlignment = StringAlignment.Center;
			columnItem4.FontColor = Color.LightGray;
			columnItem4.MyStyle = FontStyle.Regular;
			columnItem4.Name = "side";
			columnItem4.Text = "L/S";
			columnItem4.ValueFormat = FormatType.Text;
			columnItem4.Visible = true;
			columnItem4.Width = 5;
			columnItem5.Alignment = StringAlignment.Near;
			columnItem5.BackColor = Color.FromArgb(64, 64, 64);
			columnItem5.ColumnAlignment = StringAlignment.Center;
			columnItem5.FontColor = Color.LightGray;
			columnItem5.MyStyle = FontStyle.Regular;
			columnItem5.Name = "stock";
			columnItem5.Text = "Symbol";
			columnItem5.ValueFormat = FormatType.Text;
			columnItem5.Visible = true;
			columnItem5.Width = 10;
			columnItem6.Alignment = StringAlignment.Far;
			columnItem6.BackColor = Color.FromArgb(64, 64, 64);
			columnItem6.ColumnAlignment = StringAlignment.Center;
			columnItem6.FontColor = Color.LightGray;
			columnItem6.MyStyle = FontStyle.Regular;
			columnItem6.Name = "volume";
			columnItem6.Text = "Volume";
			columnItem6.ValueFormat = FormatType.Volume;
			columnItem6.Visible = true;
			columnItem6.Width = 7;
			columnItem7.Alignment = StringAlignment.Far;
			columnItem7.BackColor = Color.FromArgb(64, 64, 64);
			columnItem7.ColumnAlignment = StringAlignment.Center;
			columnItem7.FontColor = Color.LightGray;
			columnItem7.MyStyle = FontStyle.Regular;
			columnItem7.Name = "price";
			columnItem7.Text = "Price";
			columnItem7.ValueFormat = FormatType.Text;
			columnItem7.Visible = true;
			columnItem7.Width = 12;
			columnItem8.Alignment = StringAlignment.Far;
			columnItem8.BackColor = Color.FromArgb(64, 64, 64);
			columnItem8.ColumnAlignment = StringAlignment.Center;
			columnItem8.FontColor = Color.LightGray;
			columnItem8.MyStyle = FontStyle.Regular;
			columnItem8.Name = "matched";
			columnItem8.Text = "Matched";
			columnItem8.ValueFormat = FormatType.Volume;
			columnItem8.Visible = true;
			columnItem8.Width = 10;
			columnItem9.Alignment = StringAlignment.Far;
			columnItem9.BackColor = Color.FromArgb(64, 64, 64);
			columnItem9.ColumnAlignment = StringAlignment.Center;
			columnItem9.FontColor = Color.LightGray;
			columnItem9.MyStyle = FontStyle.Regular;
			columnItem9.Name = "published";
			columnItem9.Text = "Publish";
			columnItem9.ValueFormat = FormatType.Volume;
			columnItem9.Visible = true;
			columnItem9.Width = 9;
			columnItem10.Alignment = StringAlignment.Near;
			columnItem10.BackColor = Color.FromArgb(64, 64, 64);
			columnItem10.ColumnAlignment = StringAlignment.Center;
			columnItem10.FontColor = Color.LightGray;
			columnItem10.MyStyle = FontStyle.Regular;
			columnItem10.Name = "valid";
			columnItem10.Text = "Valid";
			columnItem10.ValueFormat = FormatType.Text;
			columnItem10.Visible = false;
			columnItem10.Width = 5;
			columnItem11.Alignment = StringAlignment.Center;
			columnItem11.BackColor = Color.FromArgb(64, 64, 64);
			columnItem11.ColumnAlignment = StringAlignment.Center;
			columnItem11.FontColor = Color.LightGray;
			columnItem11.MyStyle = FontStyle.Regular;
			columnItem11.Name = "status";
			columnItem11.Text = "Status";
			columnItem11.ValueFormat = FormatType.Text;
			columnItem11.Visible = true;
			columnItem11.Width = 13;
			columnItem12.Alignment = StringAlignment.Center;
			columnItem12.BackColor = Color.FromArgb(64, 64, 64);
			columnItem12.ColumnAlignment = StringAlignment.Center;
			columnItem12.FontColor = Color.LightGray;
			columnItem12.MyStyle = FontStyle.Regular;
			columnItem12.Name = "time";
			columnItem12.Text = "Time";
			columnItem12.ValueFormat = FormatType.Text;
			columnItem12.Visible = true;
			columnItem12.Width = 9;
			columnItem13.Alignment = StringAlignment.Center;
			columnItem13.BackColor = Color.FromArgb(64, 64, 64);
			columnItem13.ColumnAlignment = StringAlignment.Center;
			columnItem13.FontColor = Color.LightGray;
			columnItem13.MyStyle = FontStyle.Regular;
			columnItem13.Name = "quote";
			columnItem13.Text = "Quote";
			columnItem13.ValueFormat = FormatType.Text;
			columnItem13.Visible = true;
			columnItem13.Width = 6;
			columnItem14.Alignment = StringAlignment.Near;
			columnItem14.BackColor = Color.FromArgb(64, 64, 64);
			columnItem14.ColumnAlignment = StringAlignment.Center;
			columnItem14.FontColor = Color.LightGray;
			columnItem14.MyStyle = FontStyle.Regular;
			columnItem14.Name = "send_date";
			columnItem14.Text = "None";
			columnItem14.ValueFormat = FormatType.Text;
			columnItem14.Visible = false;
			columnItem14.Width = 10;
			columnItem15.Alignment = StringAlignment.Near;
			columnItem15.BackColor = Color.FromArgb(64, 64, 64);
			columnItem15.ColumnAlignment = StringAlignment.Center;
			columnItem15.FontColor = Color.LightGray;
			columnItem15.MyStyle = FontStyle.Regular;
			columnItem15.Name = "key";
			columnItem15.Text = "None";
			columnItem15.ValueFormat = FormatType.Text;
			columnItem15.Visible = false;
			columnItem15.Width = 10;
			columnItem16.Alignment = StringAlignment.Near;
			columnItem16.BackColor = Color.FromArgb(64, 64, 64);
			columnItem16.ColumnAlignment = StringAlignment.Center;
			columnItem16.FontColor = Color.LightGray;
			columnItem16.MyStyle = FontStyle.Regular;
			columnItem16.Name = "ordType";
			columnItem16.Text = "ordType";
			columnItem16.ValueFormat = FormatType.Bitmap;
			columnItem16.Visible = false;
			columnItem16.Width = 10;
			this.intzaOrderListTFEX.Columns.Add(columnItem);
			this.intzaOrderListTFEX.Columns.Add(columnItem2);
			this.intzaOrderListTFEX.Columns.Add(columnItem3);
			this.intzaOrderListTFEX.Columns.Add(columnItem4);
			this.intzaOrderListTFEX.Columns.Add(columnItem5);
			this.intzaOrderListTFEX.Columns.Add(columnItem6);
			this.intzaOrderListTFEX.Columns.Add(columnItem7);
			this.intzaOrderListTFEX.Columns.Add(columnItem8);
			this.intzaOrderListTFEX.Columns.Add(columnItem9);
			this.intzaOrderListTFEX.Columns.Add(columnItem10);
			this.intzaOrderListTFEX.Columns.Add(columnItem11);
			this.intzaOrderListTFEX.Columns.Add(columnItem12);
			this.intzaOrderListTFEX.Columns.Add(columnItem13);
			this.intzaOrderListTFEX.Columns.Add(columnItem14);
			this.intzaOrderListTFEX.Columns.Add(columnItem15);
			this.intzaOrderListTFEX.Columns.Add(columnItem16);
			this.intzaOrderListTFEX.CurrentScroll = 0;
			this.intzaOrderListTFEX.FocusItemIndex = -1;
			this.intzaOrderListTFEX.ForeColor = Color.Black;
			this.intzaOrderListTFEX.GridColor = Color.FromArgb(45, 45, 45);
			this.intzaOrderListTFEX.HeaderPctHeight = 80f;
			this.intzaOrderListTFEX.IsAutoRepaint = true;
			this.intzaOrderListTFEX.IsDrawFullRow = false;
			this.intzaOrderListTFEX.IsDrawGrid = true;
			this.intzaOrderListTFEX.IsDrawHeader = true;
			this.intzaOrderListTFEX.IsScrollable = true;
			this.intzaOrderListTFEX.Location = new Point(0, 124);
			this.intzaOrderListTFEX.MainColumn = "";
			this.intzaOrderListTFEX.Name = "intzaOrderListTFEX";
			this.intzaOrderListTFEX.Rows = 0;
			this.intzaOrderListTFEX.RowSelectColor = Color.FromArgb(0, 0, 128);
			this.intzaOrderListTFEX.RowSelectType = 3;
			this.intzaOrderListTFEX.RowsVisible = 0;
			this.intzaOrderListTFEX.Size = new Size(818, 47);
			this.intzaOrderListTFEX.SortColumnName = "";
			this.intzaOrderListTFEX.SortType = SortType.Desc;
			this.intzaOrderListTFEX.TabIndex = 67;
			this.intzaOrderListTFEX.Visible = false;
			this.intzaOrderListTFEX.MouseClick += new MouseEventHandler(this.intzaOrderListTFEX_MouseClick);
			this.intzaOrderListTFEX.TableMouseClick += new SortGrid.TableMouseClickEventHandler(this.intzaOrderListTFEX_TableMouseClick);
			this.intzaOrderListTFEX.TableMouseDoubleClick += new SortGrid.TableMouseDoubleClickEventHandler(this.intzaOrderListTFEX_TableMouseDoubleClick);
			this.intzaOrderList.AllowDrop = true;
			this.intzaOrderList.BackColor = Color.FromArgb(30, 30, 30);
			this.intzaOrderList.CanBlink = false;
			this.intzaOrderList.CanDrag = false;
			this.intzaOrderList.CanGetMouseMove = false;
			columnItem17.Alignment = StringAlignment.Center;
			columnItem17.BackColor = Color.FromArgb(64, 64, 64);
			columnItem17.ColumnAlignment = StringAlignment.Center;
			columnItem17.FontColor = Color.LightGray;
			columnItem17.MyStyle = FontStyle.Regular;
			columnItem17.Name = "checkbox";
			columnItem17.Text = "";
			columnItem17.ValueFormat = FormatType.Bitmap;
			columnItem17.Visible = true;
			columnItem17.Width = 3;
			columnItem18.Alignment = StringAlignment.Near;
			columnItem18.BackColor = Color.FromArgb(64, 64, 64);
			columnItem18.ColumnAlignment = StringAlignment.Center;
			columnItem18.FontColor = Color.LightGray;
			columnItem18.MyStyle = FontStyle.Regular;
			columnItem18.Name = "order_number";
			columnItem18.Text = "Order No.";
			columnItem18.ValueFormat = FormatType.Text;
			columnItem18.Visible = true;
			columnItem18.Width = 11;
			columnItem19.Alignment = StringAlignment.Center;
			columnItem19.BackColor = Color.FromArgb(64, 64, 64);
			columnItem19.ColumnAlignment = StringAlignment.Center;
			columnItem19.FontColor = Color.LightGray;
			columnItem19.MyStyle = FontStyle.Regular;
			columnItem19.Name = "side";
			columnItem19.Text = "B/S";
			columnItem19.ValueFormat = FormatType.Text;
			columnItem19.Visible = true;
			columnItem19.Width = 4;
			columnItem20.Alignment = StringAlignment.Near;
			columnItem20.BackColor = Color.FromArgb(64, 64, 64);
			columnItem20.ColumnAlignment = StringAlignment.Center;
			columnItem20.FontColor = Color.LightGray;
			columnItem20.MyStyle = FontStyle.Regular;
			columnItem20.Name = "stock";
			columnItem20.Text = "Symbol";
			columnItem20.ValueFormat = FormatType.Text;
			columnItem20.Visible = true;
			columnItem20.Width = 10;
			columnItem21.Alignment = StringAlignment.Center;
			columnItem21.BackColor = Color.FromArgb(64, 64, 64);
			columnItem21.ColumnAlignment = StringAlignment.Center;
			columnItem21.FontColor = Color.LightGray;
			columnItem21.MyStyle = FontStyle.Regular;
			columnItem21.Name = "ttf";
			columnItem21.Text = "TTF";
			columnItem21.ValueFormat = FormatType.Text;
			columnItem21.Visible = true;
			columnItem21.Width = 4;
			columnItem22.Alignment = StringAlignment.Far;
			columnItem22.BackColor = Color.FromArgb(64, 64, 64);
			columnItem22.ColumnAlignment = StringAlignment.Center;
			columnItem22.FontColor = Color.LightGray;
			columnItem22.MyStyle = FontStyle.Regular;
			columnItem22.Name = "volume";
			columnItem22.Text = "Volume";
			columnItem22.ValueFormat = FormatType.Volume;
			columnItem22.Visible = true;
			columnItem22.Width = 10;
			columnItem23.Alignment = StringAlignment.Far;
			columnItem23.BackColor = Color.FromArgb(64, 64, 64);
			columnItem23.ColumnAlignment = StringAlignment.Center;
			columnItem23.FontColor = Color.LightGray;
			columnItem23.MyStyle = FontStyle.Regular;
			columnItem23.Name = "price";
			columnItem23.Text = "Price";
			columnItem23.ValueFormat = FormatType.Text;
			columnItem23.Visible = true;
			columnItem23.Width = 7;
			columnItem24.Alignment = StringAlignment.Far;
			columnItem24.BackColor = Color.FromArgb(64, 64, 64);
			columnItem24.ColumnAlignment = StringAlignment.Center;
			columnItem24.FontColor = Color.LightGray;
			columnItem24.MyStyle = FontStyle.Regular;
			columnItem24.Name = "matched";
			columnItem24.Text = "Matched";
			columnItem24.ValueFormat = FormatType.Volume;
			columnItem24.Visible = true;
			columnItem24.Width = 10;
			columnItem25.Alignment = StringAlignment.Far;
			columnItem25.BackColor = Color.FromArgb(64, 64, 64);
			columnItem25.ColumnAlignment = StringAlignment.Center;
			columnItem25.FontColor = Color.LightGray;
			columnItem25.MyStyle = FontStyle.Regular;
			columnItem25.Name = "published";
			columnItem25.Text = "Publish";
			columnItem25.ValueFormat = FormatType.Volume;
			columnItem25.Visible = true;
			columnItem25.Width = 10;
			columnItem26.Alignment = StringAlignment.Center;
			columnItem26.BackColor = Color.FromArgb(64, 64, 64);
			columnItem26.ColumnAlignment = StringAlignment.Center;
			columnItem26.FontColor = Color.LightGray;
			columnItem26.MyStyle = FontStyle.Regular;
			columnItem26.Name = "status";
			columnItem26.Text = "Status";
			columnItem26.ValueFormat = FormatType.Text;
			columnItem26.Visible = true;
			columnItem26.Width = 14;
			columnItem27.Alignment = StringAlignment.Center;
			columnItem27.BackColor = Color.FromArgb(64, 64, 64);
			columnItem27.ColumnAlignment = StringAlignment.Center;
			columnItem27.FontColor = Color.LightGray;
			columnItem27.MyStyle = FontStyle.Regular;
			columnItem27.Name = "time";
			columnItem27.Text = "Time";
			columnItem27.ValueFormat = FormatType.Text;
			columnItem27.Visible = true;
			columnItem27.Width = 9;
			columnItem28.Alignment = StringAlignment.Center;
			columnItem28.BackColor = Color.FromArgb(64, 64, 64);
			columnItem28.ColumnAlignment = StringAlignment.Center;
			columnItem28.FontColor = Color.LightGray;
			columnItem28.MyStyle = FontStyle.Regular;
			columnItem28.Name = "quote";
			columnItem28.Text = "Quote";
			columnItem28.ValueFormat = FormatType.Text;
			columnItem28.Visible = true;
			columnItem28.Width = 5;
			columnItem29.Alignment = StringAlignment.Near;
			columnItem29.BackColor = Color.FromArgb(64, 64, 64);
			columnItem29.ColumnAlignment = StringAlignment.Center;
			columnItem29.FontColor = Color.LightGray;
			columnItem29.MyStyle = FontStyle.Regular;
			columnItem29.Name = "send_date";
			columnItem29.Text = "None";
			columnItem29.ValueFormat = FormatType.Text;
			columnItem29.Visible = false;
			columnItem29.Width = 7;
			columnItem30.Alignment = StringAlignment.Near;
			columnItem30.BackColor = Color.FromArgb(64, 64, 64);
			columnItem30.ColumnAlignment = StringAlignment.Center;
			columnItem30.FontColor = Color.LightGray;
			columnItem30.MyStyle = FontStyle.Regular;
			columnItem30.Name = "key";
			columnItem30.Text = "None";
			columnItem30.ValueFormat = FormatType.Text;
			columnItem30.Visible = false;
			columnItem30.Width = 10;
			columnItem31.Alignment = StringAlignment.Near;
			columnItem31.BackColor = Color.FromArgb(64, 64, 64);
			columnItem31.ColumnAlignment = StringAlignment.Center;
			columnItem31.FontColor = Color.LightGray;
			columnItem31.MyStyle = FontStyle.Regular;
			columnItem31.Name = "info";
			columnItem31.Text = "";
			columnItem31.ValueFormat = FormatType.Bitmap;
			columnItem31.Visible = true;
			columnItem31.Width = 3;
			columnItem32.Alignment = StringAlignment.Near;
			columnItem32.BackColor = Color.FromArgb(64, 64, 64);
			columnItem32.ColumnAlignment = StringAlignment.Center;
			columnItem32.FontColor = Color.LightGray;
			columnItem32.MyStyle = FontStyle.Regular;
			columnItem32.Name = "offline";
			columnItem32.Text = "Offline";
			columnItem32.ValueFormat = FormatType.Text;
			columnItem32.Visible = false;
			columnItem32.Width = 10;
			this.intzaOrderList.Columns.Add(columnItem17);
			this.intzaOrderList.Columns.Add(columnItem18);
			this.intzaOrderList.Columns.Add(columnItem19);
			this.intzaOrderList.Columns.Add(columnItem20);
			this.intzaOrderList.Columns.Add(columnItem21);
			this.intzaOrderList.Columns.Add(columnItem22);
			this.intzaOrderList.Columns.Add(columnItem23);
			this.intzaOrderList.Columns.Add(columnItem24);
			this.intzaOrderList.Columns.Add(columnItem25);
			this.intzaOrderList.Columns.Add(columnItem26);
			this.intzaOrderList.Columns.Add(columnItem27);
			this.intzaOrderList.Columns.Add(columnItem28);
			this.intzaOrderList.Columns.Add(columnItem29);
			this.intzaOrderList.Columns.Add(columnItem30);
			this.intzaOrderList.Columns.Add(columnItem31);
			this.intzaOrderList.Columns.Add(columnItem32);
			this.intzaOrderList.CurrentScroll = 0;
			this.intzaOrderList.FocusItemIndex = -1;
			this.intzaOrderList.ForeColor = Color.Black;
			this.intzaOrderList.GridColor = Color.FromArgb(45, 45, 45);
			this.intzaOrderList.HeaderPctHeight = 80f;
			this.intzaOrderList.IsAutoRepaint = true;
			this.intzaOrderList.IsDrawFullRow = false;
			this.intzaOrderList.IsDrawGrid = true;
			this.intzaOrderList.IsDrawHeader = true;
			this.intzaOrderList.IsScrollable = true;
			this.intzaOrderList.Location = new Point(3, 30);
			this.intzaOrderList.MainColumn = "";
			this.intzaOrderList.Name = "intzaOrderList";
			this.intzaOrderList.Rows = 0;
			this.intzaOrderList.RowSelectColor = Color.FromArgb(0, 0, 128);
			this.intzaOrderList.RowSelectType = 3;
			this.intzaOrderList.RowsVisible = 0;
			this.intzaOrderList.Size = new Size(815, 55);
			this.intzaOrderList.SortColumnName = "";
			this.intzaOrderList.SortType = SortType.Desc;
			this.intzaOrderList.TabIndex = 64;
			this.intzaOrderList.MouseClick += new MouseEventHandler(this.intzaOrderList_MouseClick);
			this.intzaOrderList.TableMouseClick += new SortGrid.TableMouseClickEventHandler(this.intzaOrderList_TableMouseClick);
			this.intzaOrderList.TableMouseDoubleClick += new SortGrid.TableMouseDoubleClickEventHandler(this.intzaOrderList_TableMouseDoubleClick);
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			this.BackColor = Color.FromArgb(20, 20, 20);
			base.Controls.Add(this.lbLoading);
			base.Controls.Add(this.intzaOrderListTFEX);
			base.Controls.Add(this.intzaOrderList);
			base.Controls.Add(this.tStripMenu);
			base.Margin = new Padding(0);
			base.Name = "ucViewOrder";
			base.Size = new Size(833, 303);
			base.Load += new EventHandler(this.ucViewOrder_Load);
			base.VisibleChanged += new EventHandler(this.ucViewOrder_VisibleChanged);
			base.KeyDown += new KeyEventHandler(this.ucViewOrder_KeyDown);
			this.tStripMenu.ResumeLayout(false);
			this.tStripMenu.PerformLayout();
			this.contextMenuStrip1.ResumeLayout(false);
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		public void DisposeMe()
		{
			try
			{
				if (this._viewOrderInfo != null)
				{
					this._viewOrderInfo.FormClosed -= new FormClosedEventHandler(this.viewOrderInfo_FormClosed);
					this._viewOrderInfo.Close();
					this._viewOrderInfo = null;
				}
				if (this._viewOrderInfoTfex != null)
				{
					this._viewOrderInfoTfex.FormClosed += new FormClosedEventHandler(this.viewOrderInfoTfex_FormClosed);
					this._viewOrderInfoTfex.Close();
					this._viewOrderInfoTfex = null;
				}
				this.bgwViewOrder = null;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		private void ShowSplash(bool visible, string message, bool isAutoClose)
		{
			if (base.InvokeRequired)
			{
				base.Invoke(new ucViewOrder.ShowSplashCallBack(this.ShowSplash), new object[]
				{
					visible,
					message,
					isAutoClose
				});
			}
			else
			{
				try
				{
					if (ApplicationInfo.SuuportSplash == "Y")
					{
						if (this.tmCloseSplash == null)
						{
							this.tmCloseSplash = new System.Windows.Forms.Timer();
							this.tmCloseSplash.Interval = 1000;
							this.tmCloseSplash.Tick += new EventHandler(this.tmCloseSplash_Tick);
						}
						if (visible)
						{
							this.lbLoading.Text = message;
							this.lbLoading.Left = (base.Width - this.lbLoading.Width) / 2;
							this.lbLoading.Top = (base.Height - this.lbLoading.Height) / 2;
							this.lbLoading.Visible = true;
							this.lbLoading.BringToFront();
							this.tmCloseSplash.Enabled = false;
							if (isAutoClose)
							{
								this.tmCloseSplash.Enabled = true;
							}
						}
						else
						{
							this.lbLoading.Visible = false;
						}
					}
				}
				catch (Exception ex)
				{
					this.ShowError("ShowSplash", ex);
				}
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tmCloseSplash_Tick(object sender, EventArgs e)
		{
			this.tmCloseSplash.Enabled = false;
			this.ShowSplash(false, "", false);
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		public ucViewOrder()
		{
			try
			{
				this.InitializeComponent();
				base.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer, true);
				base.UpdateStyles();
				if (!base.DesignMode)
				{
					this.tscbStatus.Items.Clear();
					this.tscbStatus.Items.Add("ALL");
					this.tscbStatus.Items.Add("O");
					this.tscbStatus.Items.Add("PO");
					this.tscbStatus.Items.Add("M");
					this.tscbStatus.Items.Add("C");
					this.tscbStatus.Items.Add("R");
					this.tscbStatus.Items.Add("X");
					if (ApplicationInfo.SupportFreewill)
					{
						this.tscbStatus.Items.Add("D");
					}
					this.tscbSide.Text = "ALL";
					this.tscbStatus.Text = "ALL";
				}
				this.bgwViewOrder = new BackgroundWorker();
				this.bgwViewOrder.WorkerReportsProgress = true;
				this.bgwViewOrder.DoWork += new DoWorkEventHandler(this.bgwViewOrder_DoWork);
				this.bgwViewOrder.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.bgwViewOrder_RunWorkerCompleted);
				this.intzaOrderListTFEX.BackColor = Color.FromArgb(30, 30, 30);
			}
			catch (Exception ex)
			{
				this.ShowError("ViewOrder.Create", ex);
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		private void bgwViewOrder_DoWork(object sender, DoWorkEventArgs e)
		{
			try
			{
				string data = string.Empty;
				if (this._viewType == 1)
				{
					data = ApplicationInfo.WebOrderService.ViewOrderTransaction(this._selAccount, ApplicationInfo.UserLoginMode, 0, this._selAccount, this.selStock, this.selSide, this.selPrice, this.selStatus, 0L, Settings.Default.ViewOrderRows, 0);
				}
				if (this.tdsOrder == null)
				{
					this.tdsOrder = new DataSet();
				}
				else
				{
					this.tdsOrder.Clear();
				}
				MyDataHelper.StringToDataSet(data, this.tdsOrder);
			}
			catch (Exception ex)
			{
				this.ShowError("bgwViewOrder_DoWork", ex);
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		private void bgwViewOrder_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			try
			{
				if (e.Error == null)
				{
					if (this._viewType == 1)
					{
						this.UpdateToControl();
					}
					this.tdsOrder.Clear();
				}
				else
				{
					this.ShowError("bgwViewOrder_RunWorkerCompleted", new Exception(e.Error.Message));
				}
			}
			catch (Exception ex)
			{
				this.ShowError("bgwViewOrder_RunWorkerCompleted", ex);
			}
			this.IsLoadingData = false;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		private void SetViewHeader()
		{
			try
			{
				if (!this._isSetHeaderDone)
				{
					this._isSetHeaderDone = true;
					if (this.showOnMainForm)
					{
						this.tsbtnCancelOrder.Text = "Cancel";
						this.intzaOrderList.GetColumn("order_number").Text = "Order#";
						this.intzaOrderList.GetColumn("status").Text = "ST";
						this.intzaOrderList.GetColumn("quote").Text = "QT";
						this.intzaOrderList.GetColumn("ttf").Text = "T";
						this.intzaOrderList.GetColumn("order_number").Width = 11;
						this.intzaOrderList.GetColumn("side").Width = 4;
						this.intzaOrderList.GetColumn("stock").Width = 11;
						this.intzaOrderList.GetColumn("volume").Width = 12;
						this.intzaOrderList.GetColumn("matched").Width = 12;
						this.intzaOrderList.GetColumn("published").Width = 12;
						this.intzaOrderList.GetColumn("status").Width = 6;
						this.intzaOrderList.GetColumn("time").Width = 10;
						this.intzaOrderList.GetColumn("ttf").Width = 3;
						this.intzaOrderList.GetColumn("quote").Width = 5;
						this.intzaOrderList.GetColumn("checkbox").Width = 3;
						this.intzaOrderList.GetColumn("price").Width = 8;
					}
					else if (ApplicationInfo.SupportFreewill)
					{
						this.tsbtnCancelOrder.Text = "Cancel";
						this.intzaOrderList.GetColumn("order_number").Text = "Order#";
						this.intzaOrderList.GetColumn("quote").Text = "QT";
						this.intzaOrderList.GetColumn("order_number").Width = 10;
						this.intzaOrderList.GetColumn("side").Width = 4;
						this.intzaOrderList.GetColumn("stock").Width = 12;
						this.intzaOrderList.GetColumn("volume").Width = 11;
						this.intzaOrderList.GetColumn("matched").Width = 11;
						this.intzaOrderList.GetColumn("published").Width = 11;
						this.intzaOrderList.GetColumn("status").Width = 9;
						this.intzaOrderList.GetColumn("time").Width = 9;
						this.intzaOrderList.GetColumn("ttf").Width = 4;
						this.intzaOrderList.GetColumn("quote").Width = 5;
						this.intzaOrderList.GetColumn("checkbox").Width = 3;
						this.intzaOrderList.GetColumn("price").Width = 8;
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError("SetViewHeader", ex);
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		private void ucViewOrder_Load(object sender, EventArgs e)
		{
			try
			{
				if (!base.DesignMode)
				{
					this.tstbStock.AutoCompleteMode = AutoCompleteMode.Suggest;
					this.tstbStock.AutoCompleteCustomSource = ApplicationInfo.StockAutoComp;
					this.tStripMenu.Visible = this.isShowToolsBar;
					this.intzaOrderList.IsScrollable = true;
					this.SetHeader();
				}
			}
			catch (Exception ex)
			{
				this.ShowError("ucViewOrder_Load", ex);
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		public void SetResize(bool isChanged)
		{
			try
			{
				if (ApplicationInfo.IsAreadyLogin)
				{
					this.intzaOrderList.Font = Settings.Default.Default_Font;
					this.intzaOrderListTFEX.Font = Settings.Default.Default_Font;
					Font font = new Font(Settings.Default.Default_Font.Name, Settings.Default.Default_Font.Size - 1f, FontStyle.Regular);
					if (this.tStripMenu.Font != font)
					{
						this.tStripMenu.Font = font;
						foreach (ToolStripItem toolStripItem in this.tStripMenu.Items)
						{
							toolStripItem.Font = font;
						}
						this.tStripMenu.Invalidate();
					}
					if (this.tStripMenu.Visible != this.isShowToolsBar)
					{
						this.tStripMenu.Visible = this.isShowToolsBar;
					}
					Rectangle bounds;
					if (this.isShowToolsBar)
					{
						bounds = new Rectangle(0, this.tStripMenu.Height + 1, base.ClientSize.Width, base.ClientSize.Height - this.tStripMenu.Height - 1);
					}
					else
					{
						bounds = new Rectangle(0, 0, base.ClientSize.Width, base.ClientSize.Height);
					}
					if (this._viewType == 1)
					{
						this.SetViewHeader();
						this.intzaOrderList.Bounds = bounds;
					}
					else
					{
						this.intzaOrderListTFEX.Bounds = bounds;
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError("SetResize", ex);
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		public void FormKeyUp(object sender, KeyEventArgs e)
		{
			Keys keyCode = e.KeyCode;
			if (keyCode != Keys.Space)
			{
				if (keyCode == Keys.R)
				{
					if (e.Control)
					{
						this.SelectAllOrderForCancel(!this._isSelectAll);
					}
				}
			}
			else
			{
				this.SetFocus();
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		public void ReceiveMessage(IBroadcastMessage message, StockList.StockInformation realtimeStockInfo)
		{
			if (!this.isLoadingData)
			{
				try
				{
					string messageType = message.MessageType;
					if (messageType != null)
					{
						if (!(messageType == "0I"))
						{
							if (messageType == "0B")
							{
								if (ApplicationInfo.SupportFreewill)
								{
									BroadCastOrderMessageClient broadCastOrderMessageClient = (BroadCastOrderMessageClient)message;
									if (ApplicationInfo.CanReceiveMessage(broadCastOrderMessageClient.EntryID))
									{
										ApplicationInfo.RemoveOrderNoFromAutoRefreshList("", broadCastOrderMessageClient.Reserve2);
									}
								}
							}
						}
						else
						{
							this.ReceiveOrderInfo(message);
						}
					}
				}
				catch (Exception ex)
				{
					this.ShowError("ReceiveMessage", ex);
				}
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		public void ReceiveTfexMessage(IBroadcastMessage message, SeriesList.SeriesInformation realtimeSeriesInfo)
		{
			if (!this.isLoadingData)
			{
				try
				{
					if (message.MessageType == "#T9I")
					{
						this.ReceiveOrderInfoTFEX(message);
					}
				}
				catch (Exception ex)
				{
					this.ShowError("ReceiveMessage", ex);
				}
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		private void ReceiveOrderInfoTFEX(IBroadcastMessage message)
		{
			if (this.intzaOrderListTFEX.InvokeRequired)
			{
				this.intzaOrderListTFEX.Invoke(new ucViewOrder.ReceiveOrderInfoTFEXCallBack(this.ReceiveOrderInfoTFEX), new object[]
				{
					message
				});
			}
			else
			{
				try
				{
					OrderTFEXInfoClient orderTFEXInfoClient = (OrderTFEXInfoClient)message;
					if (ApplicationInfo.CanReceiveMessage(orderTFEXInfoClient.Account))
					{
						ucViewOrder.RecordData recordData = default(ucViewOrder.RecordData);
						recordData.OrderNumber = orderTFEXInfoClient.OrderNumber;
						recordData.Position = orderTFEXInfoClient.Position;
						recordData.Side = orderTFEXInfoClient.Side;
						recordData.Series = orderTFEXInfoClient.Series;
						recordData.Volume = orderTFEXInfoClient.Volume;
						recordData.Price = orderTFEXInfoClient.Price;
						recordData.Matched = orderTFEXInfoClient.MatchedVolume;
						recordData.PubVol = orderTFEXInfoClient.PublicVolume;
						recordData.OrderStatus = orderTFEXInfoClient.Status;
						recordData.Quote = orderTFEXInfoClient.Quote;
						recordData.OrderDate = orderTFEXInfoClient.SendDate;
						recordData.OrdType = orderTFEXInfoClient.OrderType;
						recordData.OrderTimes = orderTFEXInfoClient.OrderTime;
						bool flag = false;
						int num = this.intzaOrderListTFEX.FindIndex("key", recordData.OrderNumber + "_" + recordData.OrderDate);
						if (num > -1)
						{
							flag = true;
						}
						bool flag2 = false;
						if (flag)
						{
							flag2 = true;
						}
						else if (!flag && (orderTFEXInfoClient.OriginalMessageType == "1I" || orderTFEXInfoClient.OriginalMessageType == "2G"))
						{
							flag2 = true;
						}
						if (flag2)
						{
							this.UpdateToGrid_TFEX(num, recordData);
							this.intzaOrderListTFEX.EndUpdate();
						}
						if (!this.Focused)
						{
							if (num == -1 && !ApplicationInfo.IsEquityAccount)
							{
								if (this.intzaOrderListTFEX.Rows > 0)
								{
									this.intzaOrderListTFEX.SetFocusItem(0);
								}
							}
						}
					}
				}
				catch (Exception ex)
				{
					this.ShowError("ReceiveOrderInfo_TFEX", ex);
				}
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		private void ReceiveOrderInfo(IBroadcastMessage message)
		{
			if (this.intzaOrderList.InvokeRequired)
			{
				this.intzaOrderList.Invoke(new ucViewOrder.ReceiveOrderInfoCallBack(this.ReceiveOrderInfo), new object[]
				{
					message
				});
			}
			else
			{
				try
				{
					OrderInfoClient orderInfoClient = (OrderInfoClient)message;
					if (orderInfoClient.Account.Trim() == ApplicationInfo.AccInfo.CurrentAccount)
					{
						ucViewOrder.RecordData recordData = default(ucViewOrder.RecordData);
						recordData.OrderNumber = orderInfoClient.OrderNumber;
						recordData.Side = orderInfoClient.Side;
						recordData.Stock = orderInfoClient.Stock;
						recordData.Volume = orderInfoClient.Volume;
						recordData.Price = Utilities.PriceFormat(orderInfoClient.PriceToSET);
						if (!ApplicationInfo.SupportFreewill)
						{
							if (orderInfoClient.MatchedValue != orderInfoClient.PriceForCal * orderInfoClient.MatchedVolume)
							{
								recordData.Price = "*" + recordData.Price;
							}
						}
						recordData.Matched = orderInfoClient.MatchedVolume;
						recordData.PubVol = orderInfoClient.PublicVolume;
						recordData.OrderTimes = Utilities.GetTime(orderInfoClient.OrderTime.Trim());
						int.TryParse(orderInfoClient.TrusteeID, out recordData.TrusteeID);
						recordData.Quote = orderInfoClient.Quote;
						recordData.OrderStatus = orderInfoClient.Status;
						recordData.ApprUserNo = orderInfoClient.ApproverId;
						recordData.OrderDate = orderInfoClient.OrderDate.Trim();
						recordData.OrderTime = orderInfoClient.OrderTime.Trim();
						recordData.IsAfterCloseOrder = (orderInfoClient.Reserve2 == "OFFLINE");
						bool flag = false;
						int num;
						if (ApplicationInfo.SupportFreewill)
						{
							num = this.intzaOrderList.FindIndex("order_number", recordData.OrderNumber.ToString());
						}
						else
						{
							num = this.intzaOrderList.FindIndex("key", recordData.OrderNumber + "_" + recordData.OrderDate.Trim());
						}
						if (num > -1)
						{
							flag = true;
						}
						else if (orderInfoClient.OriginalMessageType == "1I")
						{
							flag = true;
						}
						if (flag)
						{
							this.intzaOrderList.BeginUpdate();
							this.UpdateToGrid(num, recordData);
							if (num == -1)
							{
								if (this.intzaOrderList.Rows > 0)
								{
									this.intzaOrderList.SetFocusItem(0);
								}
							}
							this.intzaOrderList.Redraw();
						}
					}
				}
				catch (Exception ex)
				{
					this.ShowError("ReceiveOrderInfo", ex);
				}
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		private void UpdateToGrid(int rowIndex, ucViewOrder.RecordData recordData)
		{
			try
			{
				RecordItem recordItem;
				if (rowIndex == -1)
				{
					recordItem = this.intzaOrderList.AddRecord(1, false);
				}
				else
				{
					recordItem = this.intzaOrderList.Records(rowIndex);
				}
				recordItem.Fields("order_number").Text = recordData.OrderNumber;
				recordItem.Fields("side").Text = recordData.Side;
				recordItem.Fields("stock").Text = recordData.Stock;
				recordItem.Fields("volume").Text = recordData.Volume;
				recordItem.Fields("price").Text = recordData.Price;
				recordItem.Fields("matched").Text = recordData.Matched;
				recordItem.Fields("time").Text = recordData.OrderTimes;
				recordItem.Fields("info").Text = "2";
				recordItem.Fields("published").Text = Utilities.GetPublishedVolumeDisplay(recordData.Volume, recordData.PubVol, recordData.Matched, recordData.OrderStatus);
				string text = string.Empty;
				text = recordData.OrderStatus.Trim();
				if (text != "A" && recordData.ApprUserNo.Trim() != string.Empty)
				{
					text = recordData.OrderStatus + "A";
				}
				if (this.showOnMainForm || ApplicationInfo.SupportFreewill)
				{
					recordItem.Fields("status").Text = text;
				}
				else
				{
					recordItem.Fields("status").Text = Utilities.GetDisplayOrderStatus(text) + " (" + text + ")";
				}
				if (recordData.TrusteeID == 0)
				{
					recordItem.Fields("ttf").Text = string.Empty;
				}
				else
				{
					recordItem.Fields("ttf").Text = recordData.TrusteeID;
				}
				recordItem.Fields("quote").Text = recordData.Quote;
				recordItem.Fields("send_date").Text = recordData.OrderDate;
				recordItem.Fields("key").Text = recordData.OrderNumber + "_" + recordData.OrderDate;
				recordItem.Fields("order_number").FontColor = Color.White;
				if (recordData.Side == "B")
				{
					recordItem.Fields("side").FontColor = Color.Lime;
				}
				else if (recordData.Side == "S")
				{
					recordItem.Fields("side").FontColor = Color.Red;
				}
				else if (recordData.Side == "C")
				{
					recordItem.Fields("side").FontColor = Color.Cyan;
				}
				else if (recordData.Side == "H")
				{
					recordItem.Fields("side").FontColor = Color.Pink;
				}
				else
				{
					recordItem.Fields("side").FontColor = Color.Yellow;
				}
				recordItem.Fields("stock").FontColor = Color.White;
				recordItem.Fields("volume").FontColor = Color.White;
				recordItem.Fields("price").FontColor = Color.White;
				recordItem.Fields("matched").FontColor = Color.Cyan;
				recordItem.Fields("published").FontColor = Color.White;
				recordItem.Fields("time").FontColor = Color.White;
				recordItem.Fields("status").FontColor = Color.Cyan;
				recordItem.Fields("ttf").FontColor = Color.Cyan;
				recordItem.Fields("quote").FontColor = Color.Yellow;
				if (this.CanShowCheckBox(recordData.OrderStatus))
				{
					recordItem.Fields("checkbox").Text = "0";
				}
				else
				{
					recordItem.Fields("checkbox").Text = "";
				}
				if (ApplicationInfo.SupportFreewill)
				{
					recordItem.Fields("offline").Text = (recordData.IsAfterCloseOrder ? "Y" : "N");
				}
				recordItem.Changed = true;
			}
			catch (Exception ex)
			{
				this.ShowError("UpdateToGrid", ex);
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		public void SwitchAccount()
		{
			if (ApplicationInfo.IsEquityAccount)
			{
				this.tscbSide.Items.Clear();
				this.tscbSide.Items.Add("ALL");
				this.tscbSide.Items.Add("B");
				this.tscbSide.Items.Add("S");
				if (ApplicationInfo.SuuportSBL == "Y" && ApplicationInfo.AccInfo.CurrentAccountType == "B")
				{
					this.tscbSide.Items.Add("C");
					this.tscbSide.Items.Add("H");
				}
			}
			else
			{
				this.tscbSide.Items.Clear();
				this.tscbSide.Items.Add("ALL");
				this.tscbSide.Items.Add("L");
				this.tscbSide.Items.Add("S");
			}
			this.ReloadData();
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		public void ReloadData()
		{
			if (this._selAccount != ApplicationInfo.AccInfo.CurrentAccount && ApplicationInfo.AccInfo.CurrentAccount != string.Empty)
			{
				if (ApplicationInfo.IsEquityAccount)
				{
					this.SetView(1);
				}
				else
				{
					this.SetView(3);
				}
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		public void SetBoxFocus(bool focusFlag)
		{
			try
			{
				this.focusFlag = focusFlag;
				this.tscbStatus.Focus();
			}
			catch (Exception ex)
			{
				this.ShowError("SetBoxFocus", ex);
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		public void SetGridFocus(bool forceIndex)
		{
			if (base.InvokeRequired)
			{
				base.Invoke(new ucViewOrder.SetGridFocusCallBack(this.SetGridFocus), new object[]
				{
					forceIndex
				});
			}
			else
			{
				try
				{
					if (forceIndex)
					{
						if (this.intzaOrderList.Rows > 0)
						{
							this.intzaOrderList.FocusItemIndex = 0;
						}
					}
					else if (this.intzaOrderList.FocusItemIndex < 0)
					{
						this.intzaOrderList.FocusItemIndex = 0;
					}
					this.intzaOrderList.Focus();
					this.intzaOrderList.Redraw();
				}
				catch (Exception ex)
				{
					this.ShowError("SetStockBoxFocus", ex);
				}
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		public void SetFocus()
		{
			if (this.tStripMenu.Visible)
			{
				if (this.tscbStatus.Focused && this.intzaOrderList.Rows > 0)
				{
					this.SetGridFocus(false);
				}
				else
				{
					this.SetBoxFocus(false);
				}
			}
			else
			{
				this.SetGridFocus(false);
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		private void RequestWebData()
		{
			string empty = string.Empty;
			try
			{
				if (!this.IsLoadingData)
				{
					this._selAccount = ApplicationInfo.AccInfo.CurrentAccount;
					if (!string.IsNullOrEmpty(this._selAccount))
					{
						this.IsLoadingData = true;
						this.selStatus = string.Empty;
						if (this.tscbStatus.Text.Trim() != string.Empty && this.tscbStatus.Text.Trim() != "ALL")
						{
							this.selStatus = this.tscbStatus.Text.Trim();
						}
						this.selStock = this.tstbStock.Text.Trim();
						this.selPrice = this.tstbPrice.Text.Trim();
						if (this.tscbSide.Text == "B")
						{
							this.selSide = "B";
						}
						else if (this.tscbSide.Text == "S")
						{
							this.selSide = "S";
						}
						else if (this.tscbSide.Text == "H")
						{
							this.selSide = "H";
						}
						else if (this.tscbSide.Text == "C")
						{
							this.selSide = "C";
						}
						else
						{
							this.selSide = string.Empty;
						}
						if (this._viewType == 1)
						{
							if (!this.bgwViewOrder.IsBusy)
							{
								this.bgwViewOrder.RunWorkerAsync();
							}
						}
						else if (this._viewType == 3)
						{
							ApplicationInfo.WebServiceTFEX.ViewOrderTransactionCompleted -= new ViewOrderTransactionCompletedEventHandler(this.MyWebTfex_ViewOrderTransactionCompleted);
							ApplicationInfo.WebServiceTFEX.ViewOrderTransactionCompleted += new ViewOrderTransactionCompletedEventHandler(this.MyWebTfex_ViewOrderTransactionCompleted);
							ApplicationInfo.WebServiceTFEX.ViewOrderTransactionAsync(ApplicationInfo.AccInfo.CurrentAccount, ApplicationInfo.UserLoginMode, 0, this._selAccount, this.selStock, this.selSide, this.selPrice, this.selStatus, 0L, Settings.Default.ViewOrderRows, 0);
						}
					}
					else
					{
						this.IsLoadingData = false;
						this.ShowSplash(true, "Current Account is null", true);
						this.ShowError("RequestWebData", new Exception("Current Account is null"));
					}
				}
			}
			catch (Exception ex)
			{
				this.IsLoadingData = false;
				this.ShowError("RequestWebData", ex);
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		private void MyWebTfex_ViewOrderTransactionCompleted(object sender, ViewOrderTransactionCompletedEventArgs e)
		{
			try
			{
				ApplicationInfo.WebServiceTFEX.ViewOrderTransactionCompleted -= new ViewOrderTransactionCompletedEventHandler(this.MyWebTfex_ViewOrderTransactionCompleted);
				if (e.Error == null)
				{
					if (this.tdsOrder == null)
					{
						this.tdsOrder = new DataSet();
					}
					else
					{
						this.tdsOrder.Clear();
					}
					if (!string.IsNullOrEmpty(e.Result))
					{
						MyDataHelper.StringToDataSet(e.Result, this.tdsOrder);
						this.UpdateToControlTFEX();
						this.tdsOrder.Clear();
					}
				}
				else
				{
					this.ShowError("WebService_ViewOrderTFEXTransactionCompleted", new Exception(e.Error.Message));
				}
			}
			catch (Exception ex)
			{
				this.ShowError("MyWebTfex_ViewOrderTransactionCompleted", ex);
			}
			this.IsLoadingData = false;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		private void UpdateToControlTFEX()
		{
			if (base.InvokeRequired)
			{
				base.Invoke(new ucViewOrder.UpdateDataToControlTFEXCallBack(this.UpdateToControlTFEX));
			}
			else
			{
				try
				{
					if (this.tdsOrder.Tables.Contains("ORDERS"))
					{
						this.intzaOrderListTFEX.BeginUpdate();
						this.intzaOrderListTFEX.Rows = this.tdsOrder.Tables["ORDERS"].Rows.Count;
						long num = 0L;
						long num2 = 0L;
						long num3 = 0L;
						long orderNumber = 0L;
						for (int i = 0; i < this.tdsOrder.Tables["ORDERS"].Rows.Count; i++)
						{
							DataRow dataRow = this.tdsOrder.Tables["ORDERS"].Rows[i];
							ucViewOrder.RecordData recordData = default(ucViewOrder.RecordData);
							long.TryParse(dataRow["order_number"].ToString(), out orderNumber);
							recordData.OrderNumber = orderNumber;
							recordData.Position = dataRow["position"].ToString();
							recordData.Side = dataRow["side"].ToString();
							recordData.Series = dataRow["series"].ToString().Trim();
							long.TryParse(dataRow["volume"].ToString(), out num);
							recordData.Volume = num;
							recordData.Price = dataRow["price"].ToString();
							long.TryParse(dataRow["matched_volume"].ToString(), out num2);
							recordData.Matched = num2;
							long.TryParse(dataRow["pub_vol"].ToString(), out num3);
							if (num - num2 > 0L && num3 != 0L)
							{
								recordData.PubVol = (num - num2) % num3;
								if (recordData.PubVol == 0L)
								{
									recordData.PubVol = num3;
								}
							}
							else
							{
								recordData.PubVol = 0L;
							}
							recordData.OrderStatus = dataRow["status"].ToString();
							recordData.OrderTimes = dataRow["order_time"].ToString().Trim();
							recordData.Quote = dataRow["quote"].ToString().Trim();
							recordData.OrdType = dataRow["sOrdType"].ToString();
							recordData.OrderDate = dataRow["sSendDate"].ToString();
							recordData.OrderTime = string.Empty;
							this.UpdateToGrid_TFEX(i, recordData);
						}
						if (this.intzaOrderListTFEX.Rows > 0)
						{
							this.intzaOrderListTFEX.FocusItemIndex = 0;
						}
						this.intzaOrderListTFEX.Redraw();
					}
				}
				catch (Exception ex)
				{
					this.intzaOrderListTFEX.Redraw();
					this.ShowError("UpdateToControlTFEX", ex);
				}
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		private void UpdateToGrid_TFEX(int rowIndex, ucViewOrder.RecordData recordData)
		{
			try
			{
				SeriesList.SeriesInformation seriesInformation = ApplicationInfo.SeriesInfo[recordData.Series];
				RecordItem recordItem;
				if (rowIndex == -1)
				{
					recordItem = this.intzaOrderListTFEX.AddRecord(1, false);
				}
				else
				{
					recordItem = this.intzaOrderListTFEX.Records(rowIndex);
				}
				recordItem.Fields("order_number").Text = recordData.OrderNumber;
				if (recordData.Position == "1")
				{
					recordItem.Fields("position").Text = "Open";
				}
				else if (recordData.Position == "2")
				{
					recordItem.Fields("position").Text = "Close";
				}
				recordItem.Fields("side").Text = recordData.Side;
				recordItem.Fields("stock").Text = recordData.Series;
				recordItem.Fields("volume").Text = recordData.Volume;
				if (recordData.Price == "-1")
				{
					recordItem.Fields("price").Text = "MP";
				}
				else if (seriesInformation != null)
				{
					recordItem.Fields("price").Text = Utilities.PriceFormat(recordData.Price, seriesInformation.NumOfDec, 0);
				}
				else
				{
					recordItem.Fields("price").Text = Utilities.PriceFormat(recordData.Price, 2, 0);
				}
				recordItem.Fields("matched").Text = recordData.Matched;
				if (recordData.OrderTimes == null)
				{
					recordData.OrderTimes = string.Empty;
				}
				recordItem.Fields("time").Text = recordData.OrderTimes;
				string text = string.Empty;
				text = recordData.OrderStatus.Trim();
				if (text == "M" || text == "O")
				{
					if (recordData.Volume - recordData.Matched == 0L)
					{
						recordItem.Fields("published").Text = "0";
					}
					else
					{
						recordItem.Fields("published").Text = recordData.PubVol;
					}
				}
				else if (text == "R")
				{
					recordItem.Fields("published").Text = string.Empty;
				}
				else
				{
					recordItem.Fields("published").Text = recordData.PubVol;
				}
				recordItem.Fields("status").Text = text;
				if (recordData.Quote == null)
				{
					recordData.Quote = string.Empty;
				}
				if (recordData.Quote != "Y")
				{
					recordItem.Fields("quote").Text = string.Empty;
				}
				else
				{
					recordItem.Fields("quote").Text = recordData.Quote;
				}
				recordItem.Fields("send_date").Text = recordData.OrderDate;
				recordItem.Fields("key").Text = recordData.OrderNumber + "_" + recordData.OrderDate;
				recordItem.Fields("ordType").Text = recordData.OrdType;
				Color fontColor = Color.Yellow;
				if (recordData.Side == "L")
				{
					fontColor = MyColor.CeilingColor;
				}
				else if (recordData.Side == "S")
				{
					fontColor = MyColor.FloorColor;
				}
				recordItem.Fields("order_number").FontColor = Color.White;
				recordItem.Fields("position").FontColor = MyColor.OpenColor;
				recordItem.Fields("side").FontColor = fontColor;
				recordItem.Fields("stock").FontColor = Color.White;
				recordItem.Fields("volume").FontColor = Color.White;
				recordItem.Fields("price").FontColor = Color.White;
				recordItem.Fields("matched").FontColor = Color.Cyan;
				recordItem.Fields("published").FontColor = Color.White;
				recordItem.Fields("status").FontColor = Color.Cyan;
				recordItem.Fields("time").FontColor = Color.White;
				recordItem.Fields("quote").FontColor = Color.Yellow;
				recordItem.Fields("valid").FontColor = fontColor;
				if (this.CanShowCheckBox(recordData.OrderStatus))
				{
					recordItem.Fields("checkbox").Text = 0;
				}
				else
				{
					recordItem.Fields("checkbox").Text = -1;
				}
				recordItem.Changed = true;
			}
			catch (Exception ex)
			{
				this.ShowError("UpdateToGrid_TFXE", ex);
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		private void UpdateToControl()
		{
			try
			{
				if (this.tdsOrder.Tables.Contains("ORDERS"))
				{
					this.intzaOrderList.SortColumnName = string.Empty;
					this.intzaOrderList.BeginUpdate();
					this.intzaOrderList.ClearAllText();
					int num = 0;
					if (ApplicationInfo.SupportFreewill)
					{
						if (this.tdsOrder.Tables.Contains("OFFLINE"))
						{
							this.intzaOrderList.Rows = this.tdsOrder.Tables["ORDERS"].Rows.Count + this.tdsOrder.Tables["OFFLINE"].Rows.Count;
							DataRow[] array = this.tdsOrder.Tables["OFFLINE"].Select("", "OrderNo desc");
							DataRow[] array2 = array;
							int i = 0;
							while (i < array2.Length)
							{
								DataRow dataRow = array2[i];
								ucViewOrder.RecordData recordData = default(ucViewOrder.RecordData);
								long.TryParse(dataRow["OrderNo"].ToString(), out recordData.OrderNumber);
								recordData.Side = dataRow["side"].ToString();
								recordData.Stock = dataRow["SecSymbol"].ToString();
								long.TryParse(dataRow["volume"].ToString(), out recordData.Volume);
								long.TryParse(dataRow["matchvolume"].ToString(), out recordData.Matched);
								long.TryParse(dataRow["pubvolume"].ToString(), out recordData.PubVol);
								recordData.OrderStatus = dataRow["orderstatus"].ToString();
								string text = dataRow["conditionprice"].ToString().Trim();
								if (text == null)
								{
									goto IL_2A0;
								}
								if (!(text == "A"))
								{
									if (!(text == "C"))
									{
										if (!(text == "M"))
										{
											if (!(text == "K"))
											{
												if (!(text == "L"))
												{
													goto IL_2A0;
												}
												recordData.Price = "ML";
											}
											else
											{
												recordData.Price = "MO";
											}
										}
										else
										{
											recordData.Price = "MP";
										}
									}
									else
									{
										recordData.Price = "ATC";
									}
								}
								else
								{
									recordData.Price = "ATO";
								}
								IL_2C7:
								recordData.OrderTimes = Utilities.GetTime(dataRow["ordertime"].ToString());
								int.TryParse(dataRow["TrusteeId"].ToString(), out recordData.TrusteeID);
								recordData.Quote = string.Empty;
								recordData.ApprUserNo = "";
								recordData.OrderDate = dataRow["OrderDate"].ToString().Trim();
								recordData.OrderTime = dataRow["OrderTime"].ToString().Trim();
								recordData.IsAfterCloseOrder = true;
								this.UpdateToGrid(num, recordData);
								num++;
								i++;
								continue;
								IL_2A0:
								recordData.Price = Utilities.PriceFormat(dataRow["price"].ToString().Trim());
								goto IL_2C7;
							}
						}
						else
						{
							this.intzaOrderList.Rows = this.tdsOrder.Tables["ORDERS"].Rows.Count;
						}
					}
					else
					{
						this.intzaOrderList.Rows = this.tdsOrder.Tables["ORDERS"].Rows.Count;
					}
					bool flag = this.tdsOrder.Tables["ORDERS"].Columns.Contains("sSendDate");
					bool flag2 = this.tdsOrder.Tables["ORDERS"].Columns.Contains("bitApproval");
					foreach (DataRow dataRow in this.tdsOrder.Tables["ORDERS"].Rows)
					{
						ucViewOrder.RecordData recordData = default(ucViewOrder.RecordData);
						DataRow dataRow;
						long.TryParse(dataRow["order_number"].ToString(), out recordData.OrderNumber);
						if (ApplicationInfo.SupportFreewill)
						{
							if (dataRow["side"].ToString() == "B" && dataRow["ordertype"].ToString() == "S")
							{
								recordData.Side = "C";
							}
							else if (dataRow["side"].ToString() == "S" && dataRow["ordertype"].ToString() == "S")
							{
								recordData.Side = "H";
							}
							else
							{
								recordData.Side = dataRow["side"].ToString();
							}
						}
						else
						{
							recordData.Side = dataRow["side"].ToString();
						}
						recordData.Stock = dataRow["stock"].ToString();
						long.TryParse(dataRow["volume"].ToString(), out recordData.Volume);
						long.TryParse(dataRow["matched_volume"].ToString(), out recordData.Matched);
						long.TryParse(dataRow["pub_vol"].ToString(), out recordData.PubVol);
						recordData.OrderStatus = dataRow["status"].ToString();
						recordData.Price = Utilities.PriceFormat(dataRow["price_to_set"].ToString().Trim());
						decimal d = 0m;
						decimal.TryParse(dataRow["price"].ToString(), out d);
						if (!ApplicationInfo.SupportFreewill)
						{
							decimal d2 = 0m;
							decimal.TryParse(dataRow["matched_value"].ToString(), out d2);
							if (d * recordData.Matched != d2)
							{
								recordData.Price = "*" + recordData.Price;
							}
						}
						recordData.OrderTimes = Utilities.GetTime(dataRow["order_time"].ToString());
						int.TryParse(dataRow["trustee_id"].ToString(), out recordData.TrusteeID);
						recordData.Quote = dataRow["quote"].ToString();
						if (flag2)
						{
							if (dataRow["bitApproval"].ToString().ToUpper() == "TRUE" || dataRow["bitApproval"].ToString().ToUpper() == "1")
							{
								recordData.ApprUserNo = "True";
							}
							else
							{
								recordData.ApprUserNo = string.Empty;
							}
						}
						else
						{
							recordData.ApprUserNo = "";
						}
						if (flag)
						{
							recordData.OrderDate = dataRow["sSendDate"].ToString().Trim();
						}
						else
						{
							recordData.OrderDate = string.Empty;
						}
						recordData.OrderTime = string.Empty;
						this.UpdateToGrid(num, recordData);
						num++;
					}
					if (this.OnDisplaySummaryOrders != null)
					{
						this.UpdateDisplaySummary(this.tdsOrder.Tables["ORDERS"]);
						this.OnDisplaySummaryOrders();
					}
					this.intzaOrderList.Redraw();
					if (!this.showOnMainForm)
					{
						if (this.intzaOrderList.Rows > 0)
						{
							this.intzaOrderList.Focus();
							this.intzaOrderList.SetFocusItem(0);
						}
					}
				}
			}
			catch (Exception ex)
			{
				this.intzaOrderList.Redraw();
				this.ShowError("UpdateToControl", ex);
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tstbStock_KeyDown(object sender, KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
			case Keys.Left:
			case Keys.Up:
			case Keys.Right:
			case Keys.Down:
				e.SuppressKeyPress = true;
				break;
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tstbStock_KeyUp(object sender, KeyEventArgs e)
		{
			Keys keyCode = e.KeyCode;
			if (keyCode != Keys.Return)
			{
				switch (keyCode)
				{
				case Keys.Left:
					this.tscbStatus.Focus();
					break;
				case Keys.Right:
					this.tstbPrice.Focus();
					break;
				case Keys.Down:
					e.SuppressKeyPress = true;
					break;
				}
			}
			else
			{
				string text = this.tstbStock.Text.Trim();
				if (text == string.Empty)
				{
					this.RequestWebData();
				}
				else
				{
					StockList.StockInformation stockInformation = ApplicationInfo.StockInfo[text];
					if (stockInformation.Number > -1)
					{
						this.tstbStock.Text = stockInformation.Symbol;
						this.tstbStock.SelectAll();
						this.RequestWebData();
					}
					if (this.tstbStock.Text != this.selStock)
					{
						this.tstbStock.Text = this.selStock;
						this.tstbStock.Focus();
						this.tstbStock.SelectAll();
					}
				}
				e.SuppressKeyPress = true;
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tscbStatus_KeyUp(object sender, KeyEventArgs e)
		{
			try
			{
				Keys keyCode = e.KeyCode;
				if (keyCode != Keys.Return)
				{
					switch (keyCode)
					{
					case Keys.Left:
						this.tscbStatus.SelectAll();
						e.SuppressKeyPress = true;
						break;
					case Keys.Right:
						this.tstbStock.Focus();
						break;
					}
				}
				else
				{
					this.RequestWebData();
					this.tscbStatus.AutoCompleteSource = AutoCompleteSource.None;
					this.tscbStatus.AutoCompleteSource = AutoCompleteSource.CustomSource;
				}
			}
			catch (Exception ex)
			{
				this.ShowError("tscbStatus_KeyUp", ex);
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tscbSide_KeyUp(object sender, KeyEventArgs e)
		{
			try
			{
				Keys keyCode = e.KeyCode;
				if (keyCode != Keys.Return)
				{
					switch (keyCode)
					{
					case Keys.Left:
						this.tstbPrice.Focus();
						break;
					case Keys.Right:
						this.tscbSide.SelectAll();
						e.SuppressKeyPress = true;
						break;
					}
				}
				else
				{
					this.RequestWebData();
					this.tscbSide.AutoCompleteSource = AutoCompleteSource.None;
					this.tscbSide.AutoCompleteSource = AutoCompleteSource.CustomSource;
				}
			}
			catch (Exception ex)
			{
				this.ShowError("tscbSide_KeyUp", ex);
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		private bool DoSendCancelOrder(string sendDate, string sendTime, long orderNumber, string isAfterCloseFw)
		{
			bool result = false;
			string data = string.Empty;
			try
			{
				this.ShowSplash(true, "Send Cancel Order " + orderNumber + "...", false);
				if (this._viewType == 1)
				{
					if (isAfterCloseFw == "Y")
					{
						data = ApplicationInfo.WebOrderService.SendCancelOrder_AfterCloseFw(sendDate, sendTime.Replace(":", ""), orderNumber, ApplicationInfo.AccInfo.CurrInternetUser, ApplicationInfo.AuthenKey);
						using (DataSet dataSet = new DataSet())
						{
							MyDataHelper.StringToDataSet(data, dataSet);
							if (dataSet.Tables.Contains("Results") && dataSet.Tables["Results"].Rows.Count > 0)
							{
								if (Convert.ToInt32(dataSet.Tables["Results"].Rows[0]["code"]) >= 0)
								{
									this.ShowSplash(true, "Cancel Successful. [" + orderNumber + "]", true);
									ApplicationInfo.AddOrderNoToAutoRefreshList(orderNumber.ToString(), 3);
								}
								else
								{
									this.ShowMessageInFormConfirm("Fail >> " + dataSet.Tables["Results"].Rows[0]["message"].ToString(), frmOrderFormConfirm.OpenStyle.ShowBox);
									RecordItem recordItem = this.intzaOrderList.Find("order_number", orderNumber.ToString());
									if (recordItem != null && recordItem.Fields("checkbox").Text.ToString() == "1")
									{
										recordItem.Fields("checkbox").Text = "0";
										this.intzaOrderList.Redraw();
									}
								}
							}
							dataSet.Clear();
						}
					}
					else
					{
						string message = SendCancelOrderMessage.Pack(sendDate, orderNumber, ApplicationInfo.AccInfo.CurrentAccount, ApplicationInfo.GetSession(), ApplicationInfo.AuthenKey, ApplicationInfo.AccInfo.CurrInternetUser, ApplicationInfo.GetTermicalId());
						data = ApplicationInfo.WebOrderService.SendCancelOrder(message);
						using (DataSet dataSet = new DataSet())
						{
							MyDataHelper.StringToDataSet(data, dataSet);
							if (dataSet.Tables.Contains("Results") && dataSet.Tables["Results"].Rows.Count > 0)
							{
								if (Convert.ToInt32(dataSet.Tables["Results"].Rows[0]["code"]) == 0)
								{
									ApplicationInfo.AddOrderNoToAutoRefreshList(orderNumber.ToString(), 2);
									this.ShowSplash(true, "Cancel Successful. [" + orderNumber + "]", true);
								}
								else
								{
									this.ShowMessageInFormConfirm("Fail >> " + dataSet.Tables["Results"].Rows[0]["message"].ToString(), frmOrderFormConfirm.OpenStyle.ShowBox);
									RecordItem recordItem = this.intzaOrderList.Find("order_number", orderNumber.ToString());
									if (recordItem != null && recordItem.Fields("checkbox").Text.ToString() == "1")
									{
										recordItem.Fields("checkbox").Text = "0";
										this.intzaOrderList.Redraw();
									}
								}
							}
						}
					}
				}
				else if (this._viewType == 3)
				{
					data = ApplicationInfo.WebServiceTFEX.SendTFEXCancelOrder(orderNumber, ApplicationInfo.AccInfo.CurrentAccount, sendDate, ApplicationInfo.AccInfo.InternetUserTFEX, ApplicationInfo.AuthenKey, ApplicationInfo.IP, ApplicationInfo.GetSession(), ApplicationInfo.GetTermicalId());
					using (DataSet dataSet = new DataSet())
					{
						MyDataHelper.StringToDataSet(data, dataSet);
						if (dataSet.Tables.Contains("Results") && dataSet.Tables["Results"].Rows.Count > 0)
						{
							if (Convert.ToInt32(dataSet.Tables["Results"].Rows[0]["code"]) == 0)
							{
								ApplicationInfo.AddOrderNoToAutoRefreshList_TFEX(orderNumber.ToString());
								this.ShowSplash(true, "Cancel Successful. [" + orderNumber + "]", true);
							}
							else
							{
								this.ShowMessageInFormConfirm("Fail >> " + dataSet.Tables["Results"].Rows[0]["message"].ToString(), frmOrderFormConfirm.OpenStyle.ShowBox);
								RecordItem recordItem = this.intzaOrderListTFEX.Find("order_number", orderNumber.ToString());
								if (recordItem != null && recordItem.Fields("checkbox").Text.ToString() == "1")
								{
									recordItem.Fields("checkbox").Text = "0";
									this.intzaOrderListTFEX.Redraw();
								}
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowMessageInFormConfirm(ex.Message, frmOrderFormConfirm.OpenStyle.ShowBox);
			}
			this.ShowSplash(false, "", false);
			return result;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tsbtnCancelOrder_Click(object sender, EventArgs e)
		{
			try
			{
				if (this._viewType == 1)
				{
					if (this.intzaOrderList.FocusItemIndex >= 0)
					{
						if (this.intzaOrderList.Records(this.intzaOrderList.FocusItemIndex).Fields("checkbox").Text.ToString() == "0")
						{
							this.intzaOrderList.Records(this.intzaOrderList.FocusItemIndex).Fields("checkbox").Text = "1";
						}
					}
					this.CallCancelOrder();
				}
				else if (this._viewType == 3)
				{
					if (this.intzaOrderListTFEX.FocusItemIndex >= 0)
					{
						if (this.intzaOrderListTFEX.Records(this.intzaOrderListTFEX.FocusItemIndex).Fields("checkbox").Text.ToString() == "0")
						{
							this.intzaOrderListTFEX.Records(this.intzaOrderListTFEX.FocusItemIndex).Fields("checkbox").Text = "1";
						}
					}
					this.CallCancelOrder();
				}
			}
			catch (Exception ex)
			{
				this.ShowError("tsbtnCancelOrder_Click", ex);
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		private void CallCancelOrder()
		{
			if (base.InvokeRequired)
			{
				base.BeginInvoke(new MethodInvoker(this.CallCancelOrder));
			}
			else
			{
				try
				{
					if (this._listOfOrderCancel == null)
					{
						this._listOfOrderCancel = new List<ucViewOrder.CancelItem>();
					}
					else
					{
						this._listOfOrderCancel.Clear();
					}
					if (this._viewType == 1)
					{
						string text = string.Empty;
						for (int i = 0; i < this.intzaOrderList.Rows; i++)
						{
							if (this.intzaOrderList.Records(i).Fields("checkbox").Text.ToString() == "1")
							{
								text = text + "," + this.intzaOrderList.Records(i).Fields("order_number").Text;
								this._listOfOrderCancel.Add(new ucViewOrder.CancelItem(Convert.ToInt64(this.intzaOrderList.Records(i).Fields("order_number").Text), this.intzaOrderList.Records(i).Fields("send_date").Text.ToString().PadRight(10, ' '), this.intzaOrderList.Records(i).Fields("time").Text.ToString(), this.intzaOrderList.Records(i).Fields("offline").Text.ToString()));
							}
						}
						if (this._listOfOrderCancel.Count > 0)
						{
							if (text != string.Empty)
							{
								text = text.Substring(1);
							}
							this.ShowMessageInFormConfirm("Do you want to cancel order number " + text + " ?", frmOrderFormConfirm.OpenStyle.ConfirmCancel);
						}
						else
						{
							this.ShowMessageInFormConfirm("Can not find the item you want to cancel.", frmOrderFormConfirm.OpenStyle.ShowBox);
						}
					}
					else if (this._viewType == 3)
					{
						string text = string.Empty;
						for (int i = 0; i < this.intzaOrderListTFEX.Rows; i++)
						{
							if (this.intzaOrderListTFEX.Records(i).Fields("checkbox").Text.ToString() == "1")
							{
								text = text + "," + this.intzaOrderListTFEX.Records(i).Fields("order_number").Text;
								this._listOfOrderCancel.Add(new ucViewOrder.CancelItem(Convert.ToInt64(this.intzaOrderListTFEX.Records(i).Fields("order_number").Text), this.intzaOrderListTFEX.Records(i).Fields("send_date").Text.ToString(), "", ""));
							}
						}
						if (this._listOfOrderCancel.Count > 0)
						{
							if (text != string.Empty)
							{
								text = text.Substring(1);
							}
							this.ShowMessageInFormConfirm("Do you want to cancel order number " + text + " ?", frmOrderFormConfirm.OpenStyle.ConfirmCancel);
						}
						else
						{
							this.ShowMessageInFormConfirm("Can not find the item you want to cancel.", frmOrderFormConfirm.OpenStyle.ShowBox);
						}
					}
				}
				catch (Exception ex)
				{
					this.ShowError("CallCancelOrder", ex);
				}
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		private void CallOrderInfo(string sendDate, long orderNumber, string isFwAfterClose)
		{
			try
			{
				if (this._viewType == 1)
				{
					if (this._viewOrderInfo == null)
					{
						if (this.showOnMainForm)
						{
							this._viewOrderInfo = new frmViewOrderInfo(false, sendDate, orderNumber, base.Parent.Top + base.Top + base.Height, isFwAfterClose);
						}
						else
						{
							this._viewOrderInfo = new frmViewOrderInfo(false, sendDate, orderNumber, 0, isFwAfterClose);
						}
						this._viewOrderInfo.FormClosed -= new FormClosedEventHandler(this.viewOrderInfo_FormClosed);
						this._viewOrderInfo.FormClosed += new FormClosedEventHandler(this.viewOrderInfo_FormClosed);
						this._viewOrderInfo.TopLevel = false;
						if (base.Parent.GetType().BaseType == typeof(ClientBaseForm))
						{
							this._viewOrderInfo.Parent = base.Parent;
						}
						else
						{
							this._viewOrderInfo.Parent = base.Parent.Parent;
						}
						this._viewOrderInfo.Font = Settings.Default.Default_Font;
						this._viewOrderInfo.Show();
						this._viewOrderInfo.BringToFront();
						this._viewOrderInfo.Focus();
					}
					else
					{
						this._viewOrderInfo.Initial(sendDate, orderNumber, isFwAfterClose);
						this._viewOrderInfo.Show();
						this._viewOrderInfo.BringToFront();
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError("CallOrderInfo", ex);
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		private void CallOrderInfoTfex(string sendDate, string orderType, long orderNumber)
		{
			try
			{
				if (this._viewOrderInfoTfex == null)
				{
					if (this.showOnMainForm)
					{
						this._viewOrderInfoTfex = new frmViewOrderInfoTFEX(orderNumber, orderType, sendDate, base.Parent.Top + base.Top + base.Height);
					}
					else
					{
						this._viewOrderInfoTfex = new frmViewOrderInfoTFEX(orderNumber, orderType, sendDate, 0);
					}
					this._viewOrderInfoTfex.FormClosed -= new FormClosedEventHandler(this.viewOrderInfoTfex_FormClosed);
					this._viewOrderInfoTfex.FormClosed += new FormClosedEventHandler(this.viewOrderInfoTfex_FormClosed);
					this._viewOrderInfoTfex.TopLevel = false;
					if (base.Parent.GetType().BaseType == typeof(ClientBaseForm))
					{
						this._viewOrderInfoTfex.Parent = base.Parent;
					}
					else
					{
						this._viewOrderInfoTfex.Parent = base.Parent.Parent;
					}
					this._viewOrderInfoTfex.Font = Settings.Default.Default_Font;
					this._viewOrderInfoTfex.Show();
					this._viewOrderInfoTfex.BringToFront();
					this._viewOrderInfoTfex.Focus();
				}
				else
				{
					this._viewOrderInfoTfex.Initial(orderNumber, orderType, sendDate);
					this._viewOrderInfoTfex.Show();
					this._viewOrderInfoTfex.BringToFront();
				}
			}
			catch (Exception ex)
			{
				this.ShowError("CallOrderInfoTfex", ex);
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		protected override void OnFontChanged(EventArgs e)
		{
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		private void viewOrderInfo_FormClosed(object sender, FormClosedEventArgs e)
		{
			try
			{
				this._viewOrderInfo.FormClosed -= new FormClosedEventHandler(this.viewOrderInfo_FormClosed);
				this._viewOrderInfo = null;
				if (this.tmTest == null)
				{
					this.tmTest = new System.Windows.Forms.Timer();
					this.tmTest.Interval = 100;
					this.tmTest.Tick += new EventHandler(this.tmTest_Tick);
				}
				this.tmTest.Stop();
				this.tmTest.Start();
			}
			catch (Exception ex)
			{
				this.ShowError("viewOrderInfo_FormClosed", ex);
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		private void viewOrderInfoTfex_FormClosed(object sender, FormClosedEventArgs e)
		{
			try
			{
				this._viewOrderInfoTfex.FormClosed -= new FormClosedEventHandler(this.viewOrderInfoTfex_FormClosed);
				this._viewOrderInfoTfex = null;
				if (this.tmTest == null)
				{
					this.tmTest = new System.Windows.Forms.Timer();
					this.tmTest.Interval = 100;
					this.tmTest.Tick += new EventHandler(this.tmTest_Tick);
				}
				this.tmTest.Stop();
				this.tmTest.Start();
			}
			catch (Exception ex)
			{
				this.ShowError("viewOrderInfo_FormClosed", ex);
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tmTest_Tick(object sender, EventArgs e)
		{
			this.tmTest.Stop();
			if (this._viewType == 1)
			{
				this.intzaOrderList.Focus();
			}
			else if (this._viewType == 3)
			{
				this.intzaOrderListTFEX.Focus();
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tsbtnSearch_Click(object sender, EventArgs e)
		{
			this.RequestWebData();
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		private void ShowMessageInFormConfirm(string message, frmOrderFormConfirm.OpenStyle openStyle)
		{
			if (base.InvokeRequired)
			{
				base.Invoke(new ucViewOrder.ShowMessageInFormConfirmCallBack(this.ShowMessageInFormConfirm), new object[]
				{
					message,
					openStyle
				});
			}
			else
			{
				try
				{
					if (this.frmConfirm != null)
					{
						if (!this.frmConfirm.IsDisposed)
						{
							this.frmConfirm.Dispose();
						}
						this.frmConfirm = null;
					}
					this.frmConfirm = new frmOrderFormConfirm(message, openStyle);
					this.frmConfirm.FormClosing -= new FormClosingEventHandler(this.frmConfirm_FormClosing);
					this.frmConfirm.FormClosing += new FormClosingEventHandler(this.frmConfirm_FormClosing);
					this.frmConfirm.TopLevel = false;
					if (base.Parent.GetType() == typeof(Panel))
					{
						this.frmConfirm.Parent = base.Parent.Parent;
					}
					else if (base.Parent.GetType().BaseType == typeof(ClientBaseForm))
					{
						this.frmConfirm.Parent = base.Parent;
					}
					else if (base.Parent.Parent.Parent.GetType() == typeof(frmMain))
					{
						this.frmConfirm.Parent = base.Parent.Parent.Parent;
					}
					this.frmConfirm.Location = new Point((this.frmConfirm.Parent.Width - this.frmConfirm.Width) / 2, (this.frmConfirm.Parent.Height - this.frmConfirm.Height) / 2);
					this.frmConfirm.TopMost = true;
					this.frmConfirm.Show();
					this.frmConfirm.BringToFront();
				}
				catch (Exception ex)
				{
					this.ShowError("ShowMessageInFormConfirm", ex);
				}
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		private void LoopCancelOrder()
		{
			try
			{
				if (this._viewType == 1)
				{
					foreach (ucViewOrder.CancelItem current in this._listOfOrderCancel)
					{
						if (current.OrderNo > 0L)
						{
							this.DoSendCancelOrder(current.OrderDate, current.OrderTime, current.OrderNo, current.AfterClose);
							Thread.Sleep(20);
						}
					}
					this.SetGridFocus(false);
				}
				else if (this._viewType == 3)
				{
					foreach (ucViewOrder.CancelItem current in this._listOfOrderCancel)
					{
						if (current.OrderNo > 0L)
						{
							this.DoSendCancelOrder(current.OrderDate, "", current.OrderNo, "N");
							Thread.Sleep(20);
						}
					}
					this.SetGridFocus(false);
				}
			}
			catch (Exception ex)
			{
				this.ShowError("LoopCancelOrder", ex);
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		private void frmConfirm_FormClosing(object sender, FormClosingEventArgs e)
		{
			try
			{
				DialogResult result = ((frmOrderFormConfirm)sender).Result;
				frmOrderFormConfirm.OpenStyle openFormStyle = ((frmOrderFormConfirm)sender).OpenFormStyle;
				if (openFormStyle == frmOrderFormConfirm.OpenStyle.ConfirmCancel)
				{
					if (result == DialogResult.OK)
					{
						Thread thread = new Thread(new ThreadStart(this.LoopCancelOrder));
						thread.Start();
					}
					else if (this._viewType == 1 || this._viewType == 3)
					{
						foreach (ucViewOrder.CancelItem current in this._listOfOrderCancel)
						{
							if (current.OrderNo > 0L)
							{
								if (this._viewType == 1)
								{
									int index = this.intzaOrderList.FindIndex("order_number", current.OrderNo.ToString());
									if (this.intzaOrderList.Records(index).Fields("checkbox").Text.ToString() == "1")
									{
										this.intzaOrderList.Records(index).Fields("checkbox").Text = "0";
									}
								}
								else if (this._viewType == 3)
								{
									int index = this.intzaOrderListTFEX.FindIndex("order_number", current.OrderNo.ToString());
									if (this.intzaOrderListTFEX.Records(index).Fields("checkbox").Text.ToString() == "1")
									{
										this.intzaOrderListTFEX.Records(index).Fields("checkbox").Text = "0";
									}
								}
							}
						}
						this._listOfOrderCancel.Clear();
						if (this._viewType == 1)
						{
							this.intzaOrderList.Redraw();
						}
						else
						{
							this.intzaOrderListTFEX.Redraw();
						}
						this.SetGridFocus(false);
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError("ConfirmForm", ex);
			}
			this._isSelectAll = false;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		private void controlOrder_Enter(object sender, EventArgs e)
		{
			try
			{
				if (sender.GetType() == typeof(ToolStripTextBox))
				{
					((ToolStripTextBox)sender).BackColor = Color.Yellow;
					((ToolStripTextBox)sender).ForeColor = Color.Black;
					((ToolStripTextBox)sender).SelectAll();
				}
				else if (sender.GetType() == typeof(ToolStripComboBox))
				{
					((ToolStripComboBox)sender).BackColor = Color.Yellow;
					((ToolStripComboBox)sender).ForeColor = Color.Black;
				}
			}
			catch (Exception ex)
			{
				this.ShowError("controlOrder_Enter", ex);
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		private void controlOrder_Leave(object sender, EventArgs e)
		{
			try
			{
				if (sender.GetType() == typeof(ToolStripTextBox))
				{
					((ToolStripTextBox)sender).BackColor = Color.FromArgb(45, 45, 45);
					((ToolStripTextBox)sender).ForeColor = Color.Cyan;
				}
				else if (sender.GetType() == typeof(ToolStripComboBox))
				{
					((ToolStripComboBox)sender).BackColor = Color.FromArgb(45, 45, 45);
					((ToolStripComboBox)sender).ForeColor = Color.Cyan;
				}
			}
			catch (Exception ex)
			{
				this.ShowError("controlOrder_Leave", ex);
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tscbStatus_TextChanged(object sender, EventArgs e)
		{
			if (!base.DesignMode)
			{
				if (this.isActive)
				{
					this.tscbStatus.Text = this.tscbStatus.Text.ToUpper();
					this.tscbStatus.SelectionStart = this.tscbStatus.Text.Length;
				}
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tscbSide_TextChanged(object sender, EventArgs e)
		{
			if (!base.DesignMode)
			{
				if (this.isActive)
				{
					this.tscbSide.Text = this.tscbSide.Text.ToUpper();
					this.tscbSide.SelectionStart = this.tscbSide.Text.Length;
				}
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		private void ucViewOrder_KeyDown(object sender, KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
			case Keys.Space:
				if (this.tscbStatus.Focused)
				{
					this.tscbStatus.Text = "ALL";
					this.tscbStatus.SelectAll();
				}
				else if (this.tstbStock.Focused)
				{
					this.tstbStock.Text = string.Empty;
				}
				e.SuppressKeyPress = true;
				break;
			case Keys.Prior:
			case Keys.Next:
				e.SuppressKeyPress = true;
				break;
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		public bool ClearCondition()
		{
			bool result;
			try
			{
				this.tscbStatus.Text = "ALL";
				this.tscbSide.Text = "ALL";
				this.tstbStock.Text = string.Empty;
				this.tstbPrice.Text = string.Empty;
				if (this.selStatus != string.Empty || this.selSide != string.Empty || this.selStock != string.Empty)
				{
					result = true;
					return result;
				}
				result = false;
				return result;
			}
			catch (Exception ex)
			{
				this.ShowError("ClearCondition", ex);
			}
			result = false;
			return result;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tsbtnClearCondition_Click(object sender, EventArgs e)
		{
			this.ClearCondition();
			this.RequestWebData();
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		protected virtual void ShowError(string functionName, Exception ex)
		{
			ExceptionManager.Show(new ErrorItem(DateTime.Now, base.Name, functionName, ex.Message, ex.ToString()));
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		private void UpdateDisplaySummary(DataTable dt)
		{
			try
			{
				this.totalBuyVolume = 0L;
				this.totalBuyMatchedVolume = 0L;
				this.totalBuyMatchedValue = 0m;
				this.totalSellVolume = 0L;
				this.totalSellMatchedVolume = 0L;
				this.totalSellMatchedValue = 0m;
				this.unMatchedBuyVol = 0m;
				this.unMatchedSellVol = 0m;
				if (dt.Rows.Count > 0)
				{
					long num = 0L;
					long num2 = 0L;
					decimal d = 0m;
					for (int i = 0; i < dt.Rows.Count; i++)
					{
						long.TryParse(dt.Rows[i]["volume"].ToString(), out num);
						long.TryParse(dt.Rows[i]["matched_volume"].ToString(), out num2);
						decimal.TryParse(dt.Rows[i]["matched_value"].ToString(), out d);
						if (dt.Rows[i]["side"].ToString().ToLower() == "b")
						{
							this.totalBuyVolume += num;
							this.totalBuyMatchedVolume += num2;
							this.totalBuyMatchedValue += d;
							string text = dt.Rows[i]["status"].ToString().Trim();
							if (text != null)
							{
								if (text == "O" || text == "OA" || text == "OC" || text == "OAC" || text == "PO" || text == "POA")
								{
									this.unMatchedBuyVol += num - num2;
								}
							}
						}
						else if (dt.Rows[i]["side"].ToString().ToLower() == "s")
						{
							this.totalSellVolume += num;
							this.totalSellMatchedVolume += num2;
							this.totalSellMatchedValue += d;
							string text = dt.Rows[i]["status"].ToString().Trim();
							if (text != null)
							{
								if (text == "O" || text == "OA" || text == "OC" || text == "OAC" || text == "PO" || text == "POA")
								{
									this.unMatchedSellVol += num - num2;
								}
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError("UpdateDisplaySummary", ex);
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		private bool CanShowCheckBox(string status)
		{
			string text = status.Trim();
			bool result;
			switch (text)
			{
			case "O":
			case "OA":
			case "OC":
			case "OAC":
			case "PO":
			case "POA":
			case "A":
			case "AM":
				result = true;
				return result;
			}
			result = false;
			return result;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		private void intzaOrderList_TableMouseDoubleClick(object sender, TableMouseEventArgs e)
		{
			try
			{
				if (e.Column.Name == "checkbox" && e.RowIndex == -1)
				{
					this.tsbtnCancelOrder.PerformClick();
				}
				else
				{
					long orderNumber = Convert.ToInt64(this.intzaOrderList.Records(e.RowIndex).Fields("order_number").Text);
					string sendDate = this.intzaOrderList.Records(this.intzaOrderList.FocusItemIndex).Fields("send_date").Text.ToString().Trim();
					string isFwAfterClose = this.intzaOrderList.Records(this.intzaOrderList.FocusItemIndex).Fields("offline").Text.ToString().Trim();
					this.CallOrderInfo(sendDate, orderNumber, isFwAfterClose);
				}
			}
			catch (Exception ex)
			{
				this.ShowError("lsvOrderList_DoubleClick", ex);
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		private void intzaOrderList_TableMouseClick(object sender, TableMouseEventArgs e)
		{
			try
			{
				if (e.Mouse.Button == MouseButtons.Left)
				{
					if (e.RowIndex == -1)
					{
						string name = e.Column.Name;
						switch (name)
						{
						case "checkbox":
							this.SelectAllOrderForCancel(!this._isSelectAll);
							break;
						case "order_number":
						case "side":
						case "stock":
						case "status":
						case "time":
						case "ttf":
						case "quote":
							if (this.intzaOrderList.SortType == SortType.Asc)
							{
								this.intzaOrderList.Sort(e.Column.Name, SortType.Desc);
							}
							else
							{
								this.intzaOrderList.Sort(e.Column.Name, SortType.Asc);
							}
							this.intzaOrderList.Redraw();
							break;
						}
					}
					else
					{
						string name = e.Column.Name;
						if (name != null)
						{
							if (!(name == "checkbox"))
							{
								if (name == "info")
								{
									if (this.intzaOrderList.FocusItemIndex >= 0)
									{
										long orderNumber = Convert.ToInt64(this.intzaOrderList.Records(this.intzaOrderList.FocusItemIndex).Fields("order_number").Text);
										string sendDate = this.intzaOrderList.Records(this.intzaOrderList.FocusItemIndex).Fields("send_date").Text.ToString().Trim();
										string isFwAfterClose = this.intzaOrderList.Records(this.intzaOrderList.FocusItemIndex).Fields("offline").Text.ToString().Trim();
										this.CallOrderInfo(sendDate, orderNumber, isFwAfterClose);
									}
								}
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError("intzaOrderList_TableMouseClick", ex);
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		private void intzaOrderList_TableKeyDown(KeyEventArgs e)
		{
			try
			{
				if (this.isActive)
				{
					Keys keyCode = e.KeyCode;
					if (keyCode != Keys.Return)
					{
						switch (keyCode)
						{
						case Keys.Left:
						case Keys.Right:
							e.SuppressKeyPress = true;
							break;
						case Keys.Up:
							break;
						default:
							if (keyCode == Keys.Insert)
							{
								if (!e.Control)
								{
									this.CallCancelOrder();
								}
							}
							break;
						}
					}
					else if (this.intzaOrderList.Rows > 0 && this.intzaOrderList.FocusItemIndex > -1 && this.intzaOrderList.FocusItemIndex < this.intzaOrderList.Rows)
					{
						long num;
						long.TryParse(this.intzaOrderList.Records(this.intzaOrderList.FocusItemIndex).Fields("order_number").Text.ToString(), out num);
						string sendDate = this.intzaOrderList.Records(this.intzaOrderList.FocusItemIndex).Fields("send_date").Text.ToString().Trim();
						string isFwAfterClose = this.intzaOrderList.Records(this.intzaOrderList.FocusItemIndex).Fields("offline").Text.ToString().Trim();
						if (num > 0L)
						{
							this.CallOrderInfo(sendDate, num, isFwAfterClose);
						}
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError("intzaOrderList_TableKeyDown", ex);
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		private void ucViewOrder_VisibleChanged(object sender, EventArgs e)
		{
			if (!base.DesignMode)
			{
				if (!base.Visible)
				{
					this.CloseViewOrderInfoBox();
				}
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		public void CloseViewOrderInfoBox()
		{
			try
			{
				if (this._viewOrderInfo != null)
				{
					this._viewOrderInfo.Close();
					this._viewOrderInfo = null;
				}
				if (this.frmConfirm != null)
				{
					this.frmConfirm.FormClosing -= new FormClosingEventHandler(this.frmConfirm_FormClosing);
					if (!this.frmConfirm.IsDisposed)
					{
						this.frmConfirm.Dispose();
					}
					this.frmConfirm = null;
				}
			}
			catch (Exception ex)
			{
				this.ShowError("CloseViewOrderInfoBox", ex);
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tstbPrice_KeyUp(object sender, KeyEventArgs e)
		{
			Keys keyCode = e.KeyCode;
			if (keyCode != Keys.Return)
			{
				switch (keyCode)
				{
				case Keys.Left:
					this.tstbStock.Focus();
					break;
				case Keys.Right:
					this.tscbSide.Focus();
					break;
				case Keys.Down:
					e.SuppressKeyPress = true;
					break;
				}
			}
			else
			{
				this.RequestWebData();
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tscbStatus_KeyDown(object sender, KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
			case Keys.Prior:
			case Keys.Next:
				e.SuppressKeyPress = true;
				break;
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		public void SelectAllOrderForCancel(bool setSelectAll)
		{
			try
			{
				if (this.intzaOrderList.InvokeRequired)
				{
					this.intzaOrderList.Invoke(new ucViewOrder.SelectAllOrderForCancelCallBack(this.SelectAllOrderForCancel), new object[]
					{
						setSelectAll
					});
				}
				else
				{
					this._isSelectAll = setSelectAll;
					if (this._viewType == 1)
					{
						for (int i = 0; i < this.intzaOrderList.Rows; i++)
						{
							if (this.intzaOrderList.Records(i).Fields("checkbox").Text.ToString() == "1" || this.intzaOrderList.Records(i).Fields("checkbox").Text.ToString() == "0")
							{
								if (this._isSelectAll)
								{
									this.intzaOrderList.Records(i).Fields("checkbox").Text = "1";
								}
								else
								{
									this.intzaOrderList.Records(i).Fields("checkbox").Text = "0";
								}
								this.intzaOrderList.Records(i).Changed = true;
							}
						}
						this.intzaOrderList.EndUpdate();
					}
					else if (this._viewType == 3)
					{
						for (int i = 0; i < this.intzaOrderListTFEX.Rows; i++)
						{
							if (this.intzaOrderListTFEX.Records(i).Fields("checkbox").Text.ToString() == "1" || this.intzaOrderListTFEX.Records(i).Fields("checkbox").Text.ToString() == "0")
							{
								if (this._isSelectAll)
								{
									this.intzaOrderListTFEX.Records(i).Fields("checkbox").Text = "1";
								}
								else
								{
									this.intzaOrderListTFEX.Records(i).Fields("checkbox").Text = "0";
								}
								this.intzaOrderListTFEX.Records(i).Changed = true;
							}
						}
						this.intzaOrderListTFEX.EndUpdate();
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError("SelectAllOrderForCancel", ex);
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tsmRefresh_Click(object sender, EventArgs e)
		{
			this.RequestWebData();
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tsmCancelOrder_Click(object sender, EventArgs e)
		{
			this.CallCancelOrder();
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		public void SetHeader()
		{
			try
			{
				foreach (ColumnItem current in this.intzaOrderList.Columns)
				{
					current.BackColor = Settings.Default.HeaderBackGColor;
					current.FontColor = Settings.Default.HeaderFontColor;
				}
				this.intzaOrderList.GridColor = Settings.Default.GridColor;
				this.intzaOrderList.Redraw();
			}
			catch (Exception ex)
			{
				this.ShowError("SetHeader", ex);
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		public void SetHeaderHeightPct(int pct)
		{
			try
			{
				this.intzaOrderList.HeaderPctHeight = (float)pct;
			}
			catch (Exception ex)
			{
				this.ShowError("SetHeaderHeightPct", ex);
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		private void intzaOrderList_MouseClick(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
			{
				this.contextMenuStrip1.Show(this.intzaOrderList, e.X, e.Y);
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tsbtnEditOrder_Click(object sender, EventArgs e)
		{
			this.ShowEditForm();
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		private void ShowEditForm()
		{
			if (base.InvokeRequired)
			{
				base.Invoke(new MethodInvoker(this.ShowEditForm));
			}
			else
			{
				try
				{
					RecordItem recordItem = this.intzaOrderList.Records(this.intzaOrderList.FocusItemIndex);
					string text = recordItem.Fields("status").Text.ToString();
					long num = 0L;
					long.TryParse(recordItem.Fields("matched").Text.ToString().Replace(",", ""), out num);
					if (text == "O" || text == "OC" || text == "OA" || text == "OAC" || text == "M" || text == "MA" || text == "MC" || text == "MAC" || (ApplicationInfo.SupportFreewill && (text == "PO" || text == "POC")) || text.IndexOf("(O)") > 0 || text.IndexOf("(OC)") > 0 || text.IndexOf("(OA)") > 0 || text.IndexOf("(OAC)") > 0 || text.IndexOf("(M)") > 0 || text.IndexOf("(MA)") > 0 || text.IndexOf("(MC)") > 0 || text.IndexOf("(MAC)") > 0 || num > 0L)
					{
						if (this._editOrderBox != null)
						{
							if (!this._editOrderBox.IsDisposed)
							{
								this._editOrderBox.Dispose();
							}
							this._editOrderBox = null;
						}
						if (ApplicationInfo.SupportFreewill)
						{
							if (recordItem.Fields("offline").Text.ToString() == "Y")
							{
								this.ShowMessageInFormConfirm("AfterClose order cannot edit order.", frmOrderFormConfirm.OpenStyle.ShowBox);
								return;
							}
						}
						frmEditOrder.OrderEditRecord recordData;
						recordData.OrderNumber = Convert.ToInt64(recordItem.Fields("order_number").Text.ToString());
						recordData.Side = recordItem.Fields("side").Text.ToString();
						recordData.Stock = recordItem.Fields("stock").Text.ToString();
						int trusteeID = 0;
						int.TryParse(recordItem.Fields("ttf").Text.ToString(), out trusteeID);
						recordData.TrusteeID = trusteeID;
						long.TryParse(recordItem.Fields("volume").Text.ToString().Replace(",", ""), out recordData.Volume);
						long.TryParse(recordItem.Fields("matched").Text.ToString().Replace(",", ""), out recordData.MatVolume);
						recordData.Price = recordItem.Fields("price").Text.ToString();
						recordData.EntryDate = recordItem.Fields("send_date").Text.ToString();
						long.TryParse(recordItem.Fields("published").Text.ToString().Replace(",", ""), out recordData.PubVol);
						this._editOrderBox = new frmEditOrder(recordData);
						this._editOrderBox.TopLevel = false;
						this._editOrderBox.Parent = this;
						Rectangle position = this.intzaOrderList.GetPosition(this.intzaOrderList.FocusItemIndex, "order_number");
						this._editOrderBox.Location = new Point(0, this.intzaOrderList.Top + position.Top + position.Height);
						this._editOrderBox.TopMost = true;
						this._editOrderBox.Show();
						this._editOrderBox.BringToFront();
					}
					else
					{
						this.ShowMessageInFormConfirm("Invalid order status.", frmOrderFormConfirm.OpenStyle.ShowBox);
					}
				}
				catch (Exception ex)
				{
					this.ShowError("ShowEditForm", ex);
				}
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tsbtnViewOrder_Click(object sender, EventArgs e)
		{
			this.SetView(1);
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tsbtnStopOrder_Click(object sender, EventArgs e)
		{
			this.SetView(2);
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		public void SetView(int viewType)
		{
			try
			{
				this._viewType = viewType;
				this._isSelectAll = false;
				if (this._viewType == 1)
				{
					this.intzaOrderList.Visible = true;
					this.intzaOrderListTFEX.Visible = false;
					this.tslbStatus.Visible = true;
					this.tscbStatus.Visible = true;
					this.tslbStock.Visible = true;
					this.tstbStock.Visible = true;
					this.tslbPrice.Visible = true;
					this.tstbPrice.Visible = true;
					this.tslbSide.Visible = true;
					this.tscbSide.Visible = true;
					if (ApplicationInfo.PCCanEditorder == "Y")
					{
						this.tsbtnEditOrder.Visible = true;
					}
					else
					{
						this.tsbtnEditOrder.Visible = false;
					}
					this.tsbtnClearCondition.Visible = !this.showOnMainForm;
					this.tsbtnSearch.Visible = true;
					this.tsbtnReloadReorder.Visible = false;
					this.tsbtnCancelOrder.Visible = true;
				}
				else
				{
					if (this._viewType != 3)
					{
						return;
					}
					this.intzaOrderListTFEX.Visible = true;
					this.intzaOrderList.Visible = false;
					this.tsbtnSearch.Visible = true;
					this.tsbtnReloadReorder.Visible = false;
					this.tsbtnCancelOrder.Visible = true;
					this.tsbtnEditOrder.Visible = false;
				}
				this.SetResize(true);
				this.RequestWebData();
			}
			catch (Exception ex)
			{
				this.ShowError("SetView", ex);
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		private void intzaOrderListTFEX_TableMouseClick(object sender, TableMouseEventArgs e)
		{
			try
			{
				if (e.Mouse.Button == MouseButtons.Left)
				{
					if (e.RowIndex == -1)
					{
						string name = e.Column.Name;
						if (name != null)
						{
							if (name == "checkbox")
							{
								this.SelectAllOrderForCancel(!this._isSelectAll);
							}
						}
					}
					else
					{
						string name = e.Column.Name;
						if (name != null)
						{
							if (!(name == "checkbox"))
							{
								if (name == "info")
								{
									if (this.intzaOrderListTFEX.FocusItemIndex >= 0)
									{
										long orderNumber = Convert.ToInt64(this.intzaOrderListTFEX.Records(e.RowIndex).Fields("order_number").Text);
										string sendDate = this.intzaOrderListTFEX.Records(this.intzaOrderListTFEX.FocusItemIndex).Fields("send_date").Text.ToString().Trim();
										string orderType = this.intzaOrderListTFEX.Records(this.intzaOrderListTFEX.FocusItemIndex).Fields("ordType").Text.ToString().Trim();
										this.CallOrderInfoTfex(sendDate, orderType, orderNumber);
									}
								}
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError("intzaOrderList_TableMouseClick", ex);
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		private void intzaOrderListTFEX_TableMouseDoubleClick(object sender, TableMouseEventArgs e)
		{
			try
			{
				if (!(e.Column.Name == "checkbox") || e.RowIndex != -1)
				{
					long orderNumber = Convert.ToInt64(this.intzaOrderListTFEX.Records(e.RowIndex).Fields("order_number").Text);
					string sendDate = this.intzaOrderListTFEX.Records(this.intzaOrderListTFEX.FocusItemIndex).Fields("send_date").Text.ToString().Trim();
					string orderType = this.intzaOrderListTFEX.Records(this.intzaOrderListTFEX.FocusItemIndex).Fields("ordType").Text.ToString().Trim();
					this.CallOrderInfoTfex(sendDate, orderType, orderNumber);
				}
			}
			catch (Exception ex)
			{
				this.ShowError("lsvOrderList_DoubleClick", ex);
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		private void intzaOrderListTFEX_MouseClick(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
			{
				this.contextMenuStrip1.Show(this.intzaOrderListTFEX, e.X, e.Y);
			}
		}
	}
}
