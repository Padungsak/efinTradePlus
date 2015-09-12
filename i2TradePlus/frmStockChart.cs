using EO.WebBrowser;
using EO.WebBrowser.WinForm;
using i2TradePlus.Information;
using i2TradePlus.Properties;
using ITSNet.Common.BIZ;
using ITSNet.Common.BIZ.RealtimeMessage;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace i2TradePlus
{
	public class frmStockChart : ClientBaseForm, IRealtimeMessage
	{
		private delegate void SetNewStockInfoCallBack(string symbol);

		private const string CHART_PAGE = "CHART";

		private const string FINANCE_PAGE = "FINANCE";

		private const string NEWS_PAGE = "NEWS";

		private const string MY_PORT_SUBPAGE = "MyPort";

		private const string MY_FAV1_SUBPAGE = "FAV1";

		private const string MY_FAV2_SUBPAGE = "FAV2";

		private const string MY_FAV3_SUBPAGE = "FAV3";

		private const string MY_FAV4_SUBPAGE = "FAV4";

		private const string MY_FAV5_SUBPAGE = "FAV5";

		private IContainer components = null;

		private ToolStripLabel tslbStock;

		private ToolStripComboBox tstbStock;

		private ToolStrip tStripMenu;

		private ToolStripSeparator toolStripSeparator2;

		private ToolStripButton tsbtnFinance;

		private ToolStripButton tsbtnNews;

		private ToolStripButton tsbtnMyPort;

		private ToolStripButton tsbtnFav1;

		private ToolStripButton tsbtnFav2;

		private ToolStripButton tsbtnFav3;

		private ToolStripButton tsbtnFav4;

		private ToolStripButton tsbtnFav5;

		private ToolStripLabel tslbFav;

		private ToolStripSeparator toolStripSeparator3;

		private ToolStripButton tsbtnChart;

		private ToolStripSeparator toolStripSeparator1;

		private ToolStripButton tsbtnPeriod;

		private ToolStripButton tsbtnDraw;

		private ToolStripButton tsbtbPS;

		private ToolStripButton tsbtnIndicator;

		private WebControl webControl1;

		private WebView webView1;

		private BackgroundWorker bgwReloadData = null;

		private string _currSymbol = string.Empty;

		private bool _firstLoad = true;

		private string _currPage = string.Empty;

		private string _currSubPage = string.Empty;

		private string _myPort = string.Empty;

		private string _myFav = string.Empty;

		private bool _isLoading = false;

		private bool _canRecv = false;

		private bool _isSetChartParam = false;

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
			this.tslbStock = new ToolStripLabel();
			this.tstbStock = new ToolStripComboBox();
			this.tStripMenu = new ToolStrip();
			this.tsbtnChart = new ToolStripButton();
			this.tsbtnFinance = new ToolStripButton();
			this.tsbtnNews = new ToolStripButton();
			this.toolStripSeparator2 = new ToolStripSeparator();
			this.tsbtnMyPort = new ToolStripButton();
			this.toolStripSeparator1 = new ToolStripSeparator();
			this.tslbFav = new ToolStripLabel();
			this.tsbtnFav1 = new ToolStripButton();
			this.tsbtnFav2 = new ToolStripButton();
			this.tsbtnFav3 = new ToolStripButton();
			this.tsbtnFav4 = new ToolStripButton();
			this.tsbtnFav5 = new ToolStripButton();
			this.tsbtnPeriod = new ToolStripButton();
			this.tsbtnDraw = new ToolStripButton();
			this.tsbtbPS = new ToolStripButton();
			this.tsbtnIndicator = new ToolStripButton();
			this.toolStripSeparator3 = new ToolStripSeparator();
			this.webControl1 = new WebControl();
			this.webView1 = new WebView();
			this.tStripMenu.SuspendLayout();
			base.SuspendLayout();
			this.tslbStock.Alignment = ToolStripItemAlignment.Right;
			this.tslbStock.BackColor = Color.Transparent;
			this.tslbStock.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.tslbStock.Font = new Font("Microsoft Sans Serif", 8.25f);
			this.tslbStock.ForeColor = Color.Gainsboro;
			this.tslbStock.ImageTransparentColor = Color.Magenta;
			this.tslbStock.Margin = new Padding(5, 1, 0, 2);
			this.tslbStock.Name = "tslbStock";
			this.tslbStock.Padding = new Padding(1, 0, 2, 0);
			this.tslbStock.Size = new Size(44, 20);
			this.tslbStock.Text = "Symbol";
			this.tstbStock.Alignment = ToolStripItemAlignment.Right;
			this.tstbStock.BackColor = Color.FromArgb(45, 45, 45);
			this.tstbStock.Font = new Font("Microsoft Sans Serif", 8.25f);
			this.tstbStock.ForeColor = Color.Yellow;
			this.tstbStock.Name = "tstbStock";
			this.tstbStock.Size = new Size(100, 23);
			this.tstbStock.SelectedIndexChanged += new EventHandler(this.tstbStock_SelectedIndexChanged);
			this.tstbStock.KeyUp += new KeyEventHandler(this.tstbStock_KeyUp);
			this.tstbStock.KeyDown += new KeyEventHandler(this.tstbStock_KeyDown);
			this.tstbStock.KeyPress += new KeyPressEventHandler(this.tstbStock_KeyPress);
			this.tStripMenu.BackColor = Color.FromArgb(30, 30, 30);
			this.tStripMenu.BackgroundImageLayout = ImageLayout.None;
			this.tStripMenu.GripStyle = ToolStripGripStyle.Hidden;
			this.tStripMenu.Items.AddRange(new ToolStripItem[]
			{
				this.tsbtnChart,
				this.tsbtnFinance,
				this.tsbtnNews,
				this.toolStripSeparator2,
				this.tsbtnMyPort,
				this.toolStripSeparator1,
				this.tslbFav,
				this.tsbtnFav1,
				this.tsbtnFav2,
				this.tsbtnFav3,
				this.tsbtnFav4,
				this.tsbtnFav5,
				this.tsbtnPeriod,
				this.tsbtnDraw,
				this.tsbtbPS,
				this.tsbtnIndicator,
				this.toolStripSeparator3,
				this.tstbStock,
				this.tslbStock
			});
			this.tStripMenu.Location = new Point(0, 0);
			this.tStripMenu.Name = "tStripMenu";
			this.tStripMenu.Padding = new Padding(0, 2, 1, 3);
			this.tStripMenu.RenderMode = ToolStripRenderMode.System;
			this.tStripMenu.Size = new Size(779, 28);
			this.tStripMenu.TabIndex = 51;
			this.tStripMenu.KeyDown += new KeyEventHandler(this.tstbStock_KeyDown);
			this.tsbtnChart.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.tsbtnChart.ForeColor = Color.Gainsboro;
			this.tsbtnChart.ImageTransparentColor = Color.Magenta;
			this.tsbtnChart.Margin = new Padding(5, 1, 0, 2);
			this.tsbtnChart.Name = "tsbtnChart";
			this.tsbtnChart.Size = new Size(40, 20);
			this.tsbtnChart.Text = "Chart";
			this.tsbtnChart.Click += new EventHandler(this.tsbtnChart_Click);
			this.tsbtnFinance.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.tsbtnFinance.ForeColor = Color.LightGray;
			this.tsbtnFinance.ImageTransparentColor = Color.Magenta;
			this.tsbtnFinance.Margin = new Padding(5, 1, 0, 2);
			this.tsbtnFinance.Name = "tsbtnFinance";
			this.tsbtnFinance.Size = new Size(52, 20);
			this.tsbtnFinance.Text = "Finance";
			this.tsbtnFinance.Click += new EventHandler(this.tsbtnFinance_Click);
			this.tsbtnNews.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.tsbtnNews.ForeColor = Color.LightGray;
			this.tsbtnNews.ImageTransparentColor = Color.Magenta;
			this.tsbtnNews.Margin = new Padding(5, 1, 0, 2);
			this.tsbtnNews.Name = "tsbtnNews";
			this.tsbtnNews.Size = new Size(40, 20);
			this.tsbtnNews.Text = "News";
			this.tsbtnNews.Click += new EventHandler(this.tsbtnNews_Click);
			this.toolStripSeparator2.ForeColor = SystemColors.ControlText;
			this.toolStripSeparator2.Margin = new Padding(5, 0, 5, 0);
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new Size(6, 23);
			this.tsbtnMyPort.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.tsbtnMyPort.ForeColor = Color.LightGray;
			this.tsbtnMyPort.ImageTransparentColor = Color.Magenta;
			this.tsbtnMyPort.Name = "tsbtnMyPort";
			this.tsbtnMyPort.Size = new Size(53, 20);
			this.tsbtnMyPort.Text = "My Port";
			this.tsbtnMyPort.Click += new EventHandler(this.tsbtnMyPort_Click);
			this.toolStripSeparator1.Margin = new Padding(5, 0, 0, 0);
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new Size(6, 23);
			this.tslbFav.ForeColor = Color.LightGray;
			this.tslbFav.Margin = new Padding(5, 1, 0, 2);
			this.tslbFav.Name = "tslbFav";
			this.tslbFav.Size = new Size(49, 20);
			this.tslbFav.Text = "Favorite";
			this.tsbtnFav1.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.tsbtnFav1.ForeColor = Color.LightGray;
			this.tsbtnFav1.ImageTransparentColor = Color.Magenta;
			this.tsbtnFav1.Name = "tsbtnFav1";
			this.tsbtnFav1.Size = new Size(23, 20);
			this.tsbtnFav1.Text = "1";
			this.tsbtnFav1.Click += new EventHandler(this.tsbtnMyPort_Click);
			this.tsbtnFav2.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.tsbtnFav2.ForeColor = Color.LightGray;
			this.tsbtnFav2.ImageTransparentColor = Color.Magenta;
			this.tsbtnFav2.Name = "tsbtnFav2";
			this.tsbtnFav2.Size = new Size(23, 20);
			this.tsbtnFav2.Text = "2";
			this.tsbtnFav2.Click += new EventHandler(this.tsbtnMyPort_Click);
			this.tsbtnFav3.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.tsbtnFav3.ForeColor = Color.LightGray;
			this.tsbtnFav3.ImageTransparentColor = Color.Magenta;
			this.tsbtnFav3.Name = "tsbtnFav3";
			this.tsbtnFav3.Size = new Size(23, 20);
			this.tsbtnFav3.Text = "3";
			this.tsbtnFav3.Click += new EventHandler(this.tsbtnMyPort_Click);
			this.tsbtnFav4.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.tsbtnFav4.ForeColor = Color.LightGray;
			this.tsbtnFav4.ImageTransparentColor = Color.Magenta;
			this.tsbtnFav4.Name = "tsbtnFav4";
			this.tsbtnFav4.Size = new Size(23, 20);
			this.tsbtnFav4.Text = "4";
			this.tsbtnFav4.Click += new EventHandler(this.tsbtnMyPort_Click);
			this.tsbtnFav5.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.tsbtnFav5.ForeColor = Color.LightGray;
			this.tsbtnFav5.ImageTransparentColor = Color.Magenta;
			this.tsbtnFav5.Name = "tsbtnFav5";
			this.tsbtnFav5.Size = new Size(23, 20);
			this.tsbtnFav5.Text = "5";
			this.tsbtnFav5.Click += new EventHandler(this.tsbtnMyPort_Click);
			this.tsbtnPeriod.Alignment = ToolStripItemAlignment.Right;
			this.tsbtnPeriod.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.tsbtnPeriod.ForeColor = Color.LightGray;
			this.tsbtnPeriod.ImageTransparentColor = Color.Magenta;
			this.tsbtnPeriod.Name = "tsbtnPeriod";
			this.tsbtnPeriod.Size = new Size(45, 20);
			this.tsbtnPeriod.Text = "Period";
			this.tsbtnPeriod.Click += new EventHandler(this.tsbtnPeriod_Click);
			this.tsbtnDraw.Alignment = ToolStripItemAlignment.Right;
			this.tsbtnDraw.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.tsbtnDraw.ForeColor = Color.LightGray;
			this.tsbtnDraw.ImageTransparentColor = Color.Magenta;
			this.tsbtnDraw.Name = "tsbtnDraw";
			this.tsbtnDraw.Size = new Size(38, 20);
			this.tsbtnDraw.Text = "Draw";
			this.tsbtnDraw.Click += new EventHandler(this.tsbtnDraw_Click);
			this.tsbtbPS.Alignment = ToolStripItemAlignment.Right;
			this.tsbtbPS.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.tsbtbPS.ForeColor = Color.LightGray;
			this.tsbtbPS.ImageTransparentColor = Color.Magenta;
			this.tsbtbPS.Name = "tsbtbPS";
			this.tsbtbPS.Size = new Size(24, 20);
			this.tsbtbPS.Text = "PS";
			this.tsbtbPS.Click += new EventHandler(this.tsbtbPS_Click);
			this.tsbtnIndicator.Alignment = ToolStripItemAlignment.Right;
			this.tsbtnIndicator.DisplayStyle = ToolStripItemDisplayStyle.Text;
			this.tsbtnIndicator.ForeColor = Color.LightGray;
			this.tsbtnIndicator.ImageTransparentColor = Color.Magenta;
			this.tsbtnIndicator.Margin = new Padding(5, 1, 0, 2);
			this.tsbtnIndicator.Name = "tsbtnIndicator";
			this.tsbtnIndicator.Size = new Size(58, 20);
			this.tsbtnIndicator.Text = "Indicator";
			this.tsbtnIndicator.Click += new EventHandler(this.tsbtnIndicator_Click);
			this.toolStripSeparator3.Alignment = ToolStripItemAlignment.Right;
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new Size(6, 23);
			this.webControl1.BackColor = Color.White;
			this.webControl1.Dock = DockStyle.Fill;
			this.webControl1.ForeColor = Color.DimGray;
			this.webControl1.Location = new Point(0, 28);
			this.webControl1.Name = "webControl1";
			this.webControl1.Size = new Size(779, 355);
			this.webControl1.TabIndex = 52;
			this.webControl1.Text = "webControl1";
			this.webControl1.WebView = this.webView1;
			this.webView1.AllowDropLoad = true;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			this.BackColor = Color.FromArgb(25, 25, 25);
			base.ClientSize = new Size(779, 383);
			base.Controls.Add(this.webControl1);
			base.Controls.Add(this.tStripMenu);
			base.FormBorderStyle = FormBorderStyle.Sizable;
			base.Margin = new Padding(3, 4, 3, 4);
			base.Name = "frmStockChart";
			this.Text = "eFin Tools";
			base.IDoShownDelay += new ClientBaseForm.OnShownDelayEventHandler(this.frmStockHistory_IDoShownDelay);
			base.IDoLoadData += new ClientBaseForm.OnIDoLoadDataEventHandler(this.frmStockHistory_IDoLoadData);
			base.IDoFontChanged += new ClientBaseForm.OnFontChangedEventHandler(this.frmStockHistory_IDoFontChanged);
			base.IDoCustomSizeChanged += new ClientBaseForm.CustomSizeChangedEventHandler(this.frmStockHistory_IDoCustomSizeChanged);
			base.IDoSymbolLinked += new ClientBaseForm.OnSymbolLinkEventHandler(this.frmStockChart_IDoSymbolLinked);
			base.IDoMainFormKeyUp += new ClientBaseForm.OnFormKeyUpEventHandler(this.frmStockHistory_IDoMainFormKeyUp);
			base.IDoReActivated += new ClientBaseForm.OnReActiveEventHandler(this.frmStockHistory_IDoReActivated);
			base.Controls.SetChildIndex(this.tStripMenu, 0);
			base.Controls.SetChildIndex(this.webControl1, 0);
			this.tStripMenu.ResumeLayout(false);
			this.tStripMenu.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		public frmStockChart()
		{
			this.InitializeComponent();
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		public frmStockChart(Dictionary<string, object> propertiesValue) : base(propertiesValue)
		{
			this.InitializeComponent();
			try
			{
				if (!base.DesignMode)
				{
					this.webControl1.WebView.LoadCompleted -= new NavigationTaskEventHandler(this.WebView_LoadCompleted);
					this.webControl1.WebView.LoadCompleted += new NavigationTaskEventHandler(this.WebView_LoadCompleted);
					this.webControl1.WebView.NewWindow -= new NewWindowHandler(this.WebView_NewWindow);
					this.webControl1.WebView.NewWindow += new NewWindowHandler(this.WebView_NewWindow);
					if (this.bgwReloadData == null)
					{
						this.bgwReloadData = new BackgroundWorker();
						this.bgwReloadData.WorkerReportsProgress = true;
						this.bgwReloadData.DoWork += new DoWorkEventHandler(this.bgwReloadData_DoWork);
						this.bgwReloadData.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.bgwReloadData_RunWorkerCompleted);
					}
				}
				Runtime.AddLicense("jbHnrprj8AAivUaBpLHLn3Xm9vUQ8YLl6gDL45rr6c7NtGiovcDdr2qsprEh5Kvq7QAZvFupprHavUaBpLHLn3Xq7fgZ4K3s9vbp0pzjvd/WsoXbttj59Xrk3tztxW2ixc7ou2jq7fgZ4K3s9vbpjEOzs/0U4p7l9/bpjEN14+30EO2s3MKetZ9Zl6TNF+ic3PIEEMidtbvC5K9pr7nD4bB1pvD6DuSn6unaD71GgaSxy5914+30EO2s3OnP566l4Of2GfKe3MKetZ9Zl6TNDOul5vvPuIlZl6Sxy59Zl8DyD+NZ6/0BELxbvNO/7uer5vH2zZ+v3PYEFO6ntKbD2a9bl7PPuIlZl6Q=");
				if (ApplicationInfo.IsSupportEfinChart)
				{
					this.webControl1.Visible = true;
				}
				if (!base.DesignMode)
				{
					this.tstbStock.Items.AddRange(ApplicationInfo.StockAutoCompStringArr);
					this.tstbStock.Sorted = true;
					this.tstbStock.AutoCompleteMode = AutoCompleteMode.Suggest;
					this.tstbStock.AutoCompleteSource = AutoCompleteSource.ListItems;
				}
				this.tsbtnMyPort.Visible = false;
				this.tslbFav.Visible = false;
				this.toolStripSeparator2.Visible = false;
				this.toolStripSeparator3.Visible = false;
				this.tsbtnMyPort.Visible = false;
				this.tsbtnFav1.Visible = false;
				this.tsbtnFav2.Visible = false;
				this.tsbtnFav3.Visible = false;
				this.tsbtnFav4.Visible = false;
				this.tsbtnFav5.Visible = false;
			}
			catch (Exception ex)
			{
				this.ShowError("frmStockChart", ex);
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		private void WebView_NewWindow(object sender, NewWindowEventArgs e)
		{
			try
			{
				Process.Start(e.TargetUrl);
			}
			catch (Exception ex)
			{
				this.ShowError("WebView_NewWindow", ex);
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		protected override Dictionary<string, object> DoPackProperties()
		{
			this.SaveTemplate();
			base.PropertiesValue.Clear();
			return base.PropertiesValue;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		protected override void DoUnpackProperties()
		{
			if (base.PropertiesValue != null)
			{
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		private void frmStockHistory_IDoShownDelay()
		{
			this.SetResize(true);
			base.Show();
			base.OpenedForm();
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		private void frmStockHistory_IDoLoadData()
		{
			try
			{
				if (ApplicationInfo.IsSupportEfinChart)
				{
					if (this._firstLoad)
					{
						ApplicationInfo.WebService.ClearEfinSession(ApplicationInfo.UserEfin);
					}
					if (!string.IsNullOrEmpty(ApplicationInfo.UserEfin))
					{
						if (ApplicationInfo.CurrentSymbol != string.Empty)
						{
							StockList.StockInformation stockInformation = ApplicationInfo.StockInfo[ApplicationInfo.CurrentSymbol];
							this.tstbStock.Text = stockInformation.Symbol;
						}
						this.SetPage("CHART", true);
					}
					else
					{
						this.ShowError("StockChart", new Exception("eFin user is empty!!!"));
						this.webControl1.Visible = false;
						MessageBox.Show("eFin user is empty!!!");
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError("frmStockHistory_IDoLoadData", ex);
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		private void frmStockHistory_IDoCustomSizeChanged()
		{
			if (!this._isLoading)
			{
				this.SetResize(this.IsWidthChanged | this.IsHeightChanged);
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		private void frmStockHistory_IDoFontChanged()
		{
			if (!this._isLoading)
			{
				this.SetResize(true);
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		private void frmStockHistory_IDoReActivated()
		{
			if (!this._isLoading)
			{
				if (ApplicationInfo.IsSupportEfinChart)
				{
					this.SetResize(true);
					base.Show();
					if (this._currPage == "CHART")
					{
						this.SetNewStock(ApplicationInfo.CurrentSymbol);
					}
				}
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		private void frmStockHistory_IDoMainFormKeyUp(KeyEventArgs e)
		{
			Keys keyCode = e.KeyCode;
			if (keyCode != Keys.Space)
			{
				switch (keyCode)
				{
				}
			}
			else
			{
				this.tstbStock.Focus();
				this.tstbStock.SelectAll();
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		private void frmStockChart_IDoSymbolLinked(object sender, SymbolLinkSource source, string newStock)
		{
			if (source == SymbolLinkSource.SwitchAccount)
			{
				if (this._currPage == "NEWS")
				{
					this.ReloadData();
				}
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		private void SetResize(bool isChanged)
		{
			if (isChanged)
			{
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		private void bgwReloadData_DoWork(object sender, DoWorkEventArgs e)
		{
			try
			{
				this._isLoading = true;
				if (ApplicationInfo.IsSupportEfinChart)
				{
					if (this._currPage == "NEWS")
					{
						if (this._currSubPage == "MyPort")
						{
							this._myPort = ApplicationInfo.WebOrderService.GetMyPortSymbolList(ApplicationInfo.AccInfo.CurrentAccount);
							this._myPort = this._myPort.Replace("'", "");
						}
						else if (this._currSubPage == "FAV1" || this._currSubPage == "FAV2" || this._currSubPage == "FAV3" || this._currSubPage == "FAV4" || this._currSubPage == "FAV5")
						{
							int favPage = 0;
							if (this._currSubPage == "FAV1")
							{
								favPage = 1;
							}
							if (this._currSubPage == "FAV2")
							{
								favPage = 2;
							}
							if (this._currSubPage == "FAV3")
							{
								favPage = 3;
							}
							if (this._currSubPage == "FAV4")
							{
								favPage = 4;
							}
							if (this._currSubPage == "FAV5")
							{
								favPage = 5;
							}
							this._myFav = ApplicationInfo.GetFavSymbolListByPage(favPage, ApplicationInfo.FavStockPerPage);
						}
					}
				}
			}
			catch (Exception ex)
			{
				this._isLoading = false;
				this.ShowError("bgwReloadData_DoWork", ex);
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		private void bgwReloadData_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			try
			{
				if (ApplicationInfo.IsSupportEfinChart)
				{
					if (this._firstLoad)
					{
						string text = string.Empty;
						if (this._currPage == "CHART")
						{
							this._canRecv = false;
							text = string.Concat(new string[]
							{
								ApplicationInfo.UrlEfinChart,
								"?userid=",
								ApplicationInfo.UserEfin,
								"&symbol=",
								this._currSymbol.Replace("&", "and"),
								"&tp=1"
							});
						}
						else if (this._currPage == "FINANCE")
						{
							text = string.Concat(new string[]
							{
								ApplicationInfo.UrlEfinFinance,
								"?userid=",
								ApplicationInfo.UserEfin,
								"&websessionid=",
								ApplicationInfo.UrlEfinSession,
								"&symbol=",
								this._currSymbol.Replace("&", "and")
							});
						}
						else if (this._currPage == "NEWS")
						{
							text = string.Concat(new string[]
							{
								ApplicationInfo.UrlEfinNews,
								"?userid=",
								ApplicationInfo.UserEfin,
								"&websessionid=",
								ApplicationInfo.UrlEfinSession,
								"&symbol=",
								this._currSymbol.Replace("&", "and")
							});
						}
						if (text != null && this.webControl1 != null)
						{
							this.webControl1.WebView.LoadUrl(text);
						}
					}
					else if (this._currPage == "CHART")
					{
						this._canRecv = false;
						JSObject dOMWindow = this.webControl1.WebView.GetDOMWindow();
						JSFunction jSFunction = (JSFunction)this.webControl1.WebView.EvalScript("changesymbol");
						object obj = jSFunction.Invoke(dOMWindow, new object[]
						{
							this._currSymbol.Replace("&", "and")
						});
						this._canRecv = true;
					}
					else if (this._currPage == "FINANCE")
					{
						JSObject dOMWindow = this.webControl1.WebView.GetDOMWindow();
						JSFunction jSFunction = (JSFunction)this.webControl1.WebView.EvalScript("setsymbols");
						object obj = jSFunction.Invoke(dOMWindow, new object[]
						{
							this._currSymbol.Replace("&", "and")
						});
					}
					else if (this._currPage == "NEWS")
					{
						if (this._currSubPage == "MyPort")
						{
							JSObject dOMWindow = this.webControl1.WebView.GetDOMWindow();
							JSFunction jSFunction = (JSFunction)this.webControl1.WebView.EvalScript("setsymbols");
							object obj = jSFunction.Invoke(dOMWindow, new object[]
							{
								this._myPort.Replace("&", "and")
							});
						}
						else if (this._currSubPage == "FAV1" || this._currSubPage == "FAV2" || this._currSubPage == "FAV3" || this._currSubPage == "FAV4" || this._currSubPage == "FAV5")
						{
							JSObject dOMWindow = this.webControl1.WebView.GetDOMWindow();
							JSFunction jSFunction = (JSFunction)this.webControl1.WebView.EvalScript("setsymbols");
							object obj = jSFunction.Invoke(dOMWindow, new object[]
							{
								this._myFav.Replace("&", "and")
							});
						}
						else
						{
							JSObject dOMWindow = this.webControl1.WebView.GetDOMWindow();
							JSFunction jSFunction = (JSFunction)this.webControl1.WebView.EvalScript("setsymbols");
							object obj = jSFunction.Invoke(dOMWindow, new object[]
							{
								this._currSymbol.Replace("&", "and")
							});
						}
					}
					this._firstLoad = false;
				}
			}
			catch (Exception ex)
			{
				this.ShowError("bgwReloadData_RunWorkerCompleted", ex);
			}
			this._isLoading = false;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		public void ReloadData()
		{
			if (!this.bgwReloadData.IsBusy)
			{
				this.bgwReloadData.RunWorkerAsync();
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		public void ReceiveMessage(IBroadcastMessage message, StockList.StockInformation realtimeStockInfo)
		{
			if (this.FormState != ClientBaseForm.ClientBaseFormState.Closing)
			{
				if (!this._isLoading)
				{
					try
					{
						if (this._canRecv && this._currPage == "CHART")
						{
							string messageType = message.MessageType;
							if (messageType != null)
							{
								if (messageType == "L+")
								{
									if (realtimeStockInfo != null && realtimeStockInfo.Symbol == this._currSymbol && !realtimeStockInfo.IsOddLot)
									{
										LSAccumulate lSAccumulate = (LSAccumulate)message;
										string text = string.Concat(new object[]
										{
											this._currSymbol,
											";",
											lSAccumulate.Side,
											";",
											lSAccumulate.LastPrice,
											";",
											lSAccumulate.Volume,
											";",
											lSAccumulate.LastTime.Replace(":", "")
										});
										JSObject dOMWindow = this.webControl1.WebView.GetDOMWindow();
										JSFunction jSFunction = (JSFunction)this.webControl1.WebView.EvalScript("dataInSTI");
										object obj = jSFunction.Invoke(dOMWindow, new object[]
										{
											text
										});
									}
								}
							}
						}
					}
					catch (Exception ex)
					{
						this.ShowError("ReceiveMessage", ex);
					}
				}
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		public void ReceiveTfexMessage(IBroadcastMessage message, SeriesList.SeriesInformation realtimeSeriesInfo)
		{
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		private void controlOrder_Enter(object sender, EventArgs e)
		{
			((Control)sender).BackColor = Color.Yellow;
			((Control)sender).ForeColor = Color.Black;
			if (sender.GetType() == typeof(TextBox))
			{
				((TextBox)sender).SelectAll();
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		private void controlOrder_Leave(object sender, EventArgs e)
		{
			if (sender.GetType() == typeof(CheckBox))
			{
				((Control)sender).BackColor = Color.Transparent;
				if (this.BackColor == Color.DarkGreen)
				{
					((Control)sender).ForeColor = Color.White;
				}
				else
				{
					((Control)sender).ForeColor = Color.Black;
				}
			}
			else
			{
				((Control)sender).BackColor = Color.White;
				((Control)sender).ForeColor = Color.Black;
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tstbStock_KeyPress(object sender, KeyPressEventArgs e)
		{
			try
			{
				e.KeyChar = Convert.ToChar(e.KeyChar.ToString().ToUpper());
			}
			catch (Exception ex)
			{
				this.ShowError("tstbStock_KeyPress", ex);
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tstbStock_KeyDown(object sender, KeyEventArgs e)
		{
			try
			{
				Keys keyCode = e.KeyCode;
				if (keyCode != Keys.Return)
				{
					switch (keyCode)
					{
					case Keys.Prior:
					case Keys.Next:
						break;
					default:
						switch (keyCode)
						{
						case Keys.Up:
						case Keys.Down:
							break;
						case Keys.Right:
							goto IL_5D;
						default:
							goto IL_5D;
						}
						break;
					}
					e.SuppressKeyPress = true;
				}
				else
				{
					e.SuppressKeyPress = true;
				}
				IL_5D:;
			}
			catch (Exception ex)
			{
				this.ShowError("tstbStock_KeyDown", ex);
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tstbStock_KeyUp(object sender, KeyEventArgs e)
		{
			try
			{
				Keys keyCode = e.KeyCode;
				if (keyCode == Keys.Return)
				{
					this.SetNewStock(this.tstbStock.Text.Trim());
				}
			}
			catch (Exception ex)
			{
				this.ShowError("tstbStock_KeyUp", ex);
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tstbStock_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!this._isLoading)
			{
				this.SetNewStock(this.tstbStock.Text.Trim());
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		private void SetNewStock(string symbol)
		{
			if (this.tStripMenu.InvokeRequired)
			{
				this.tStripMenu.Invoke(new frmStockChart.SetNewStockInfoCallBack(this.SetNewStock), new object[]
				{
					symbol
				});
			}
			else
			{
				try
				{
					if (!this._isLoading && this._currSymbol != symbol)
					{
						StockList.StockInformation stockInformation = ApplicationInfo.StockInfo[symbol];
						if (stockInformation.Number > 0)
						{
							this._currSymbol = stockInformation.Symbol;
							this._currSubPage = string.Empty;
							if (!this._firstLoad)
							{
								this.SetPage(this._currPage, false);
							}
						}
						else
						{
							this._currSymbol = symbol;
							this._currSubPage = string.Empty;
							if (!this._firstLoad)
							{
								this.SetPage(this._currPage, false);
							}
						}
						if (this.tstbStock.Text != this._currSymbol)
						{
							this.tstbStock.Text = this._currSymbol;
						}
						this.tstbStock.Focus();
						this.tstbStock.SelectAll();
					}
				}
				catch (Exception ex)
				{
					this.ShowError("SetNewStockInfo", ex);
				}
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tsbtnFinance_Click(object sender, EventArgs e)
		{
			if (this._currPage == "CHART")
			{
				this.SaveTemplate();
			}
			this.SetPage("FINANCE", true);
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tsbtnChart_Click(object sender, EventArgs e)
		{
			this.SetPage("CHART", true);
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tsbtnNews_Click(object sender, EventArgs e)
		{
			if (this._currPage == "CHART")
			{
				this.SaveTemplate();
			}
			this.SetPage("NEWS", true);
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		private void SetPage(string newPage, bool firstLoad)
		{
			try
			{
				this._isSetChartParam = false;
				this._currPage = newPage;
				this._firstLoad = firstLoad;
				this.tStripMenu.SuspendLayout();
				this.tsbtnFav1.Visible = (this._currPage == "NEWS");
				this.tsbtnFav2.Visible = (this._currPage == "NEWS");
				this.tsbtnFav3.Visible = (this._currPage == "NEWS");
				this.tsbtnFav4.Visible = (this._currPage == "NEWS");
				this.tsbtnFav5.Visible = (this._currPage == "NEWS");
				this.tsbtnMyPort.Visible = (this._currPage == "NEWS");
				this.tslbFav.Visible = (this._currPage == "NEWS");
				this.toolStripSeparator2.Visible = (this._currPage == "NEWS");
				this.toolStripSeparator3.Visible = (this._currPage == "NEWS");
				this.tsbtnIndicator.Visible = (this._currPage == "CHART");
				this.tsbtbPS.Visible = (this._currPage == "CHART");
				this.tsbtnDraw.Visible = (this._currPage == "CHART");
				this.tsbtnPeriod.Visible = (this._currPage == "CHART");
				this.toolStripSeparator3.Visible = (this._currPage == "CHART");
				this.tsbtnChart.BackColor = ((this._currPage == "CHART") ? Color.DimGray : this.tStripMenu.BackColor);
				this.tsbtnFinance.BackColor = ((this._currPage == "FINANCE") ? Color.DimGray : this.tStripMenu.BackColor);
				this.tsbtnNews.BackColor = ((this._currPage == "NEWS") ? Color.DimGray : this.tStripMenu.BackColor);
				if (this._currPage == "NEWS")
				{
					this.tsbtnMyPort.ForeColor = ((this._currSubPage == "MyPort") ? Color.Cyan : Color.LightGray);
					this.tsbtnFav1.ForeColor = ((this._currSubPage == "FAV1") ? Color.Cyan : Color.LightGray);
					this.tsbtnFav2.ForeColor = ((this._currSubPage == "FAV2") ? Color.Cyan : Color.LightGray);
					this.tsbtnFav3.ForeColor = ((this._currSubPage == "FAV3") ? Color.Cyan : Color.LightGray);
					this.tsbtnFav4.ForeColor = ((this._currSubPage == "FAV4") ? Color.Cyan : Color.LightGray);
					this.tsbtnFav5.ForeColor = ((this._currSubPage == "FAV5") ? Color.Cyan : Color.LightGray);
					this.tstbStock.Text = ((this._currSubPage == string.Empty) ? this._currSymbol : string.Empty);
				}
				else if (this._currPage == "FINANCE")
				{
					this._isLoading = true;
					this.tstbStock.Text = this._currSymbol;
					this._isLoading = false;
				}
				this.tStripMenu.ResumeLayout();
				this.ReloadData();
			}
			catch (Exception ex)
			{
				this.ShowError("SetPage", ex);
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tsbtnMyPort_Click(object sender, EventArgs e)
		{
			if (sender == this.tsbtnMyPort)
			{
				this._currSubPage = "MyPort";
			}
			else if (sender == this.tsbtnFav1)
			{
				this._currSubPage = "FAV1";
			}
			else if (sender == this.tsbtnFav2)
			{
				this._currSubPage = "FAV2";
			}
			else if (sender == this.tsbtnFav3)
			{
				this._currSubPage = "FAV3";
			}
			else if (sender == this.tsbtnFav4)
			{
				this._currSubPage = "FAV4";
			}
			else if (sender == this.tsbtnFav5)
			{
				this._currSubPage = "FAV5";
			}
			this.SetPage("NEWS", false);
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tsbtnIndicator_Click(object sender, EventArgs e)
		{
			try
			{
				if (this._currPage == "CHART")
				{
					JSObject dOMWindow = this.webControl1.WebView.GetDOMWindow();
					JSFunction jSFunction = (JSFunction)this.webControl1.WebView.EvalScript("jsinfToggleMenu");
					object obj = jSFunction.Invoke(dOMWindow, new object[]
					{
						"studies"
					});
				}
			}
			catch (Exception ex)
			{
				this.ShowError("tsbtnIndicator_Click", ex);
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tsbtbPS_Click(object sender, EventArgs e)
		{
			try
			{
				if (this._currPage == "CHART")
				{
					JSObject dOMWindow = this.webControl1.WebView.GetDOMWindow();
					JSFunction jSFunction = (JSFunction)this.webControl1.WebView.EvalScript("jsinfToggleMenu");
					object obj = jSFunction.Invoke(dOMWindow, new object[]
					{
						"menu_ps"
					});
				}
			}
			catch (Exception ex)
			{
				this.ShowError("tsbtbPS_Click", ex);
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tsbtnDraw_Click(object sender, EventArgs e)
		{
			try
			{
				JSObject dOMWindow = this.webControl1.WebView.GetDOMWindow();
				JSFunction jSFunction = (JSFunction)this.webControl1.WebView.EvalScript("jsinfToggleMenu");
				object obj = jSFunction.Invoke(dOMWindow, new object[]
				{
					"draw"
				});
			}
			catch (Exception ex)
			{
				this.ShowError("tsbtnDraw_Click", ex);
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tsbtnPeriod_Click(object sender, EventArgs e)
		{
			try
			{
				JSObject dOMWindow = this.webControl1.WebView.GetDOMWindow();
				JSFunction jSFunction = (JSFunction)this.webControl1.WebView.EvalScript("jsinfToggleMenu");
				object obj = jSFunction.Invoke(dOMWindow, new object[]
				{
					"periodicity"
				});
			}
			catch (Exception ex)
			{
				this.ShowError("tsbtnPeriod_Click", ex);
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		private void WebView_LoadCompleted(object sender, NavigationTaskEventArgs e)
		{
			try
			{
				if (this._currPage == "CHART" && !this._isSetChartParam)
				{
					JSObject dOMWindow = this.webControl1.WebView.GetDOMWindow();
					JSFunction jSFunction = (JSFunction)this.webControl1.WebView.EvalScript("setParametersSave");
					object obj = jSFunction.Invoke(dOMWindow, new object[]
					{
						Settings.Default.Template
					});
					this._isSetChartParam = true;
				}
			}
			catch (Exception ex)
			{
				this.ShowError("WebView_LoadCompleted", ex);
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		private void SaveTemplate()
		{
			try
			{
				if (this._currPage == "CHART")
				{
					JSObject dOMWindow = this.webControl1.WebView.GetDOMWindow();
					JSFunction jSFunction = (JSFunction)this.webControl1.WebView.EvalScript("getParametersSave");
					object obj = jSFunction.Invoke(dOMWindow, null);
					if (obj != null)
					{
						Settings.Default.Template = obj.ToString();
						Settings.Default.Save();
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError("SaveTemplate", ex);
			}
		}
	}
}
