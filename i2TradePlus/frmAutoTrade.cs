using i2TradePlus.Information;
using i2TradePlus.ITSNetBusinessWS;
using i2TradePlus.Properties;
using ITSNet.Common.BIZ;
using ITSNet.Common.BIZ.AutoTradeMessage;
using STIControl;
using STIControl.ExpandTableGrid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace i2TradePlus
{
    public class frmAutoTrade : ClientBaseForm, IRealtimeMessage
    {
        private delegate void ShowMessageBoxCallBack(string message, frmOrderFormConfirm.OpenStyle openStyle, Control lastFocusOjb);

        private delegate void ShowOrderFormConfirmCallBack(string message, string orderParam, frmOrderFormConfirm.OpenStyle openStyle);

        private delegate void ShowSplashCallBack(bool visible, string message);

        private delegate void OnStartAutoTradeCallback(string message);

        private IContainer components = null;

        private Panel panType1;

        private Label lb1Value;

        private ComboBox cb1Value;

        private Label lbStopOrderField;

        private ComboBox cb1Stock;

        private Label lb1Stock;

        private Label lbPin;

        private TextBox tbPin;

        private Label lbPrice;

        private Label lbVolume;

        private TextBox tb1Volume;

        private Label lbPattern;

        private Panel panelTop;

        private ToolStrip tStripMenu;

        private ToolStripLabel tslbStatus;

        private ToolStripComboBox tscbStatus;

        private ToolStripLabel tslbStock;

        private ToolStripTextBox tstbStock;

        private ToolStripLabel tslbSide;

        private ToolStripComboBox tscbSide;

        private ToolStripButton tsbtnClearCondition;

        private ToolStripButton tsbtnCancelOrder;

        private ToolStripButton tsbtnSearch;

        private ComboBox cb1Price;

        private Button btnClear;

        private Button btnSendOrder;

        private Button btnSendLocalOrder;

        private Button btnType2;

        private Button btnType1;

        private Button btnSell;

        private Button btnBuy;

        private Panel panType2;

        private ComboBox cb2OperCutloss;

        private ComboBox cb2OperTrailingStop;

        private ComboBox cb2OperTakeProfit;

        private CheckBox chb2CutLossCond;

        private CheckBox chb2TrailingStopCond;

        private CheckBox chb2TakeProfitCond;

        private Label lb2ValueTakeProfit;

        private ComboBox cb2ValueTakeProfit;

        private ComboBox cb2PriceLast;

        private CheckBox chbGroupCancel;

        private Label label3;

        private TextBox tb2Volume;

        private Label label4;

        private Label lb2ValueCutloss;

        private ComboBox cb2ValueCutloss;

        private ComboBox cb2PriceBreak;

        private Label label7;

        private Label lb2ValueTrailingStop;

        private ComboBox cb2ValueTrailingStop;

        private ComboBox cb2PriceSMA;

        private Label label5;

        private Label label8;

        private ComboBox cb1Condition;

        private Button btnTypeMM;

        private Panel panel1;

        private ExpandGrid intzaOrder;

        private Panel panelDCA;

        private ExpandGrid gridDcaMain;

        private Button btn3Cancel;

        private Button btnDcaReload;

        private Button btnTypeDCA;

        private ToolTip toolTip1;

        private Button btnDcaCreate;

        private Panel panelPZ;

        private Button btnPzCreateNew;

        private Button btnPzCancel;

        private Button btnPzReload;

        private ExpandGrid gridPzMain;

        private Button btnTypePZ;

        private ComboBox cbExpire;

        private Label lbExpire;

        private Label lbAutoTradeLoading;

        private string _typeId = "ALG1";

        private string _ordSide = string.Empty;

        private BackgroundWorker bgwSendOrder = null;

        private frmOrderFormConfirm _frmConfirm = null;

        private List<string> _holidays = null;

        private bool _isReloadData = false;

        private AutoTradeMain _commandMain = null;

        private LocalAutoTradeItem _localAutoTradeItem = null;

        private frmDcaItemsInfo _frmDcaInfo = null;

        private frmDcaCreateNew _frmDcaCreateNew = null;

        private frmPzItemsInfo _frmPzInfo = null;

        private frmPzCreateNew _frmPzCreateNew = null;

        private Control _lastFocusOjb;

        private frmSplash splashForm = null;

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
            ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(frmAutoTrade));
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
            ColumnItem columnItem33 = new ColumnItem();
            this.btnClear = new Button();
            this.btnSendOrder = new Button();
            this.btnSendLocalOrder = new Button();
            this.tbPin = new TextBox();
            this.lbPin = new Label();
            this.cb1Price = new ComboBox();
            this.cb1Stock = new ComboBox();
            this.lb1Stock = new Label();
            this.lbPrice = new Label();
            this.lbVolume = new Label();
            this.tb1Volume = new TextBox();
            this.panelTop = new Panel();
            this.lbExpire = new Label();
            this.cbExpire = new ComboBox();
            this.panelPZ = new Panel();
            this.gridPzMain = new ExpandGrid();
            this.btnPzCreateNew = new Button();
            this.btnPzCancel = new Button();
            this.btnPzReload = new Button();
            this.panelDCA = new Panel();
            this.gridDcaMain = new ExpandGrid();
            this.btnDcaCreate = new Button();
            this.btn3Cancel = new Button();
            this.btnDcaReload = new Button();
            this.panType2 = new Panel();
            this.label8 = new Label();
            this.lb2ValueCutloss = new Label();
            this.cb2ValueCutloss = new ComboBox();
            this.cb2PriceBreak = new ComboBox();
            this.label7 = new Label();
            this.lb2ValueTrailingStop = new Label();
            this.cb2ValueTrailingStop = new ComboBox();
            this.cb2PriceSMA = new ComboBox();
            this.label5 = new Label();
            this.cb2OperCutloss = new ComboBox();
            this.cb2OperTrailingStop = new ComboBox();
            this.cb2OperTakeProfit = new ComboBox();
            this.chb2CutLossCond = new CheckBox();
            this.chb2TrailingStopCond = new CheckBox();
            this.chb2TakeProfitCond = new CheckBox();
            this.lb2ValueTakeProfit = new Label();
            this.cb2ValueTakeProfit = new ComboBox();
            this.cb2PriceLast = new ComboBox();
            this.chbGroupCancel = new CheckBox();
            this.label3 = new Label();
            this.tb2Volume = new TextBox();
            this.label4 = new Label();
            this.btnTypePZ = new Button();
            this.panType1 = new Panel();
            this.cb1Condition = new ComboBox();
            this.lb1Value = new Label();
            this.cb1Value = new ComboBox();
            this.lbStopOrderField = new Label();
            this.btnTypeDCA = new Button();
            this.btnTypeMM = new Button();
            this.btnSell = new Button();
            this.btnBuy = new Button();
            this.btnType2 = new Button();
            this.btnType1 = new Button();
            this.lbPattern = new Label();
            this.tStripMenu = new ToolStrip();
            this.tslbStatus = new ToolStripLabel();
            this.tscbStatus = new ToolStripComboBox();
            this.tslbStock = new ToolStripLabel();
            this.tstbStock = new ToolStripTextBox();
            this.tslbSide = new ToolStripLabel();
            this.tscbSide = new ToolStripComboBox();
            this.tsbtnClearCondition = new ToolStripButton();
            this.tsbtnCancelOrder = new ToolStripButton();
            this.tsbtnSearch = new ToolStripButton();
            this.panel1 = new Panel();
            this.intzaOrder = new ExpandGrid();
            this.lbAutoTradeLoading = new Label();
            this.toolTip1 = new ToolTip(this.components);
            this.panelTop.SuspendLayout();
            this.panelPZ.SuspendLayout();
            this.panelDCA.SuspendLayout();
            this.panType2.SuspendLayout();
            this.panType1.SuspendLayout();
            this.tStripMenu.SuspendLayout();
            this.panel1.SuspendLayout();
            base.SuspendLayout();
            this.btnClear.AutoEllipsis = true;
            this.btnClear.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            this.btnClear.BackColor = Color.Transparent;
            this.btnClear.Cursor = Cursors.Hand;
            this.btnClear.FlatAppearance.BorderColor = Color.DimGray;
            this.btnClear.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 128, 0);
            this.btnClear.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 192, 192);
            this.btnClear.FlatStyle = FlatStyle.Flat;
            this.btnClear.ForeColor = Color.WhiteSmoke;
            this.btnClear.Location = new Point(620, 394);
            this.btnClear.MaximumSize = new Size(58, 23);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new Size(54, 22);
            this.btnClear.TabIndex = 103;
            this.btnClear.TabStop = false;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new EventHandler(this.btnClear_Click);
            this.btnSendOrder.AutoEllipsis = true;
            this.btnSendOrder.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            this.btnSendOrder.BackColor = Color.Transparent;
            this.btnSendOrder.Cursor = Cursors.Hand;
            this.btnSendOrder.FlatAppearance.BorderColor = Color.DimGray;
            this.btnSendOrder.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 128, 0);
            this.btnSendOrder.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 192, 192);
            this.btnSendOrder.FlatStyle = FlatStyle.Flat;
            this.btnSendOrder.ForeColor = Color.WhiteSmoke;
            this.btnSendOrder.Location = new Point(560, 394);
            this.btnSendOrder.MaximumSize = new Size(58, 23);
            this.btnSendOrder.Name = "btnSendOrder";
            this.btnSendOrder.Size = new Size(54, 22);
            this.btnSendOrder.TabIndex = 102;
            this.btnSendOrder.TabStop = false;
            this.btnSendOrder.Text = "Send";
            this.btnSendOrder.UseVisualStyleBackColor = false;
            this.btnSendOrder.Click += new EventHandler(this.btnSendOrder_Click);

            this.btnSendLocalOrder.AutoEllipsis = true;
            this.btnSendLocalOrder.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            this.btnSendLocalOrder.BackColor = Color.Transparent;
            this.btnSendLocalOrder.Cursor = Cursors.Hand;
            this.btnSendLocalOrder.FlatAppearance.BorderColor = Color.DimGray;
            this.btnSendLocalOrder.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 128, 0);
            this.btnSendLocalOrder.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 192, 192);
            this.btnSendLocalOrder.FlatStyle = FlatStyle.Flat;
            this.btnSendLocalOrder.ForeColor = Color.WhiteSmoke;
            this.btnSendLocalOrder.Location = new Point(560, 394);
            this.btnSendLocalOrder.MaximumSize = new Size(58, 23);
            this.btnSendLocalOrder.Name = "btnSendLocalOrder";
            this.btnSendLocalOrder.Size = new Size(54, 22);
            this.btnSendLocalOrder.TabIndex = 102;
            this.btnSendLocalOrder.TabStop = false;
            this.btnSendLocalOrder.Text = "Send";
            this.btnSendLocalOrder.UseVisualStyleBackColor = false;
            this.btnSendLocalOrder.Click += new EventHandler(this.btnSendLocalOrder_Click);

            this.lbAutoTradeLoading.AutoSize = true;
            this.lbAutoTradeLoading.BackColor = Color.FromArgb(64, 64, 64);
            this.lbAutoTradeLoading.BorderStyle = BorderStyle.FixedSingle;
            this.lbAutoTradeLoading.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 222);
            this.lbAutoTradeLoading.ForeColor = Color.Yellow;
            this.lbAutoTradeLoading.Location = new Point(602, 166);
            this.lbAutoTradeLoading.Name = "lbAutoTradeLoading";
            this.lbAutoTradeLoading.Padding = new Padding(5, 3, 5, 3);
            this.lbAutoTradeLoading.Size = new Size(69, 21);
            this.lbAutoTradeLoading.TabIndex = 73;
            this.lbAutoTradeLoading.Text = "Loading ...";
            this.lbAutoTradeLoading.TextAlign = ContentAlignment.MiddleCenter;
            this.lbAutoTradeLoading.Visible = false;

            this.tbPin.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            this.tbPin.AutoCompleteSource = AutoCompleteSource.CustomSource;
            this.tbPin.BackColor = Color.FromArgb(224, 224, 224);
            this.tbPin.BorderStyle = BorderStyle.FixedSingle;
            this.tbPin.CharacterCasing = CharacterCasing.Upper;
            this.tbPin.Location = new Point(500, 395);
            this.tbPin.Margin = new Padding(2, 3, 2, 3);
            this.tbPin.MaxLength = 10;
            this.tbPin.Name = "tbPin";
            this.tbPin.PasswordChar = '*';
            this.tbPin.Size = new Size(55, 20);
            this.tbPin.TabIndex = 7;
            this.tbPin.KeyDown += new KeyEventHandler(this.tbPin_KeyDown);
            this.tbPin.Leave += new EventHandler(this.controlOrder_Leave);
            this.tbPin.Enter += new EventHandler(this.controlOrder_Enter);
            this.lbPin.AutoSize = true;
            this.lbPin.ForeColor = Color.LightGray;
            this.lbPin.Location = new Point(473, 399);
            this.lbPin.Margin = new Padding(2, 0, 2, 0);
            this.lbPin.Name = "lbPin";
            this.lbPin.Size = new Size(25, 13);
            this.lbPin.TabIndex = 90;
            this.lbPin.Text = "PIN";
            this.lbPin.TextAlign = ContentAlignment.MiddleLeft;
            this.cb1Price.AllowDrop = true;
            this.cb1Price.AutoCompleteCustomSource.AddRange(new string[]
			{
				"",
				"ATO",
				"ATC",
				"MP",
				"MO",
				"ML"
			});
            this.cb1Price.AutoCompleteMode = AutoCompleteMode.Append;
            this.cb1Price.AutoCompleteSource = AutoCompleteSource.CustomSource;
            this.cb1Price.BackColor = Color.FromArgb(224, 224, 224);
            this.cb1Price.FlatStyle = FlatStyle.Popup;
            this.cb1Price.ForeColor = Color.Black;
            this.cb1Price.FormattingEnabled = true;
            this.cb1Price.Location = new Point(311, 30);
            this.cb1Price.Name = "cb1Price";
            this.cb1Price.Size = new Size(57, 21);
            this.cb1Price.TabIndex = 118;
            this.cb1Price.Leave += new EventHandler(this.controlOrder_Leave);
            this.cb1Price.Enter += new EventHandler(this.controlOrder_Enter);
            this.cb1Price.KeyPress += new KeyPressEventHandler(this.cbPrice_KeyPress);
            this.cb1Price.KeyDown += new KeyEventHandler(this.cb1Price_KeyDown);
            this.cb1Stock.AutoCompleteMode = AutoCompleteMode.Suggest;
            this.cb1Stock.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.cb1Stock.BackColor = Color.FromArgb(224, 224, 224);
            this.cb1Stock.FlatStyle = FlatStyle.Popup;
            this.cb1Stock.ForeColor = Color.Black;
            this.cb1Stock.FormattingEnabled = true;
            this.cb1Stock.Location = new Point(180, 6);
            this.cb1Stock.MaxLength = 20;
            this.cb1Stock.Name = "cb1Stock";
            this.cb1Stock.Size = new Size(100, 21);
            this.cb1Stock.TabIndex = 0;
            this.cb1Stock.Leave += new EventHandler(this.controlOrder_Leave);
            this.cb1Stock.Enter += new EventHandler(this.controlOrder_Enter);
            this.cb1Stock.KeyPress += new KeyPressEventHandler(this.cbPrice_KeyPress);
            this.cb1Stock.KeyDown += new KeyEventHandler(this.cbStock_KeyDown);
            this.lb1Stock.AutoSize = true;
            this.lb1Stock.ForeColor = Color.LightGray;
            this.lb1Stock.Location = new Point(134, 10);
            this.lb1Stock.Margin = new Padding(2, 0, 2, 0);
            this.lb1Stock.Name = "lb1Stock";
            this.lb1Stock.Size = new Size(41, 13);
            this.lb1Stock.TabIndex = 100;
            this.lb1Stock.Text = "Symbol";
            this.lb1Stock.TextAlign = ContentAlignment.MiddleLeft;
            this.lbPrice.AutoSize = true;
            this.lbPrice.ForeColor = Color.LightGray;
            this.lbPrice.Location = new Point(276, 34);
            this.lbPrice.Margin = new Padding(2, 0, 2, 0);
            this.lbPrice.Name = "lbPrice";
            this.lbPrice.Size = new Size(31, 13);
            this.lbPrice.TabIndex = 13;
            this.lbPrice.Text = "Price";
            this.lbPrice.TextAlign = ContentAlignment.MiddleLeft;
            this.lbVolume.AutoSize = true;
            this.lbVolume.ForeColor = Color.LightGray;
            this.lbVolume.Location = new Point(383, 34);
            this.lbVolume.Margin = new Padding(2, 0, 2, 0);
            this.lbVolume.Name = "lbVolume";
            this.lbVolume.Size = new Size(42, 13);
            this.lbVolume.TabIndex = 11;
            this.lbVolume.Text = "Volume";
            this.lbVolume.TextAlign = ContentAlignment.MiddleLeft;
            this.tb1Volume.BackColor = Color.FromArgb(224, 224, 224);
            this.tb1Volume.BorderStyle = BorderStyle.FixedSingle;
            this.tb1Volume.Location = new Point(428, 30);
            this.tb1Volume.Margin = new Padding(2, 3, 2, 3);
            this.tb1Volume.MaxLength = 10;
            this.tb1Volume.Name = "tb1Volume";
            this.tb1Volume.Size = new Size(59, 20);
            this.tb1Volume.TabIndex = 119;
            this.tb1Volume.TextChanged += new EventHandler(this.tb1Volume_TextChanged);
            this.tb1Volume.KeyDown += new KeyEventHandler(this.tb1Volume_KeyDown);
            this.tb1Volume.Leave += new EventHandler(this.controlOrder_Leave);
            this.tb1Volume.Enter += new EventHandler(this.controlOrder_Enter);
            this.panelTop.BackColor = Color.FromArgb(20, 20, 20);
            this.panelTop.Controls.Add(this.lbExpire);
            this.panelTop.Controls.Add(this.cbExpire);
            this.panelTop.Controls.Add(this.panelPZ);
            this.panelTop.Controls.Add(this.panelDCA);
            this.panelTop.Controls.Add(this.panType2);
            this.panelTop.Controls.Add(this.btnTypePZ);
            this.panelTop.Controls.Add(this.panType1);
            this.panelTop.Controls.Add(this.btnTypeDCA);
            this.panelTop.Controls.Add(this.btnTypeMM);
            this.panelTop.Controls.Add(this.btnClear);
            this.panelTop.Controls.Add(this.lb1Stock);
            this.panelTop.Controls.Add(this.btnSendOrder);
            this.panelTop.Controls.Add(this.btnSendLocalOrder);
            this.panelTop.Controls.Add(this.cb1Stock);
            this.panelTop.Controls.Add(this.tbPin);
            this.panelTop.Controls.Add(this.lbPin);
            this.panelTop.Controls.Add(this.btnSell);
            this.panelTop.Controls.Add(this.btnBuy);
            this.panelTop.Controls.Add(this.btnType2);
            this.panelTop.Controls.Add(this.btnType1);
            this.panelTop.Controls.Add(this.lbPattern);
            this.panelTop.Controls.Add(this.lbAutoTradeLoading);
            this.panelTop.Dock = DockStyle.Top;
            this.panelTop.Location = new Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Padding = new Padding(2);
            this.panelTop.Size = new Size(697, 424);
            this.panelTop.TabIndex = 113;
            this.lbExpire.AutoSize = true;
            this.lbExpire.ForeColor = Color.LightGray;
            this.lbExpire.Location = new Point(334, 400);
            this.lbExpire.Margin = new Padding(2, 0, 2, 0);
            this.lbExpire.Name = "lbExpire";
            this.lbExpire.Size = new Size(36, 13);
            this.lbExpire.TabIndex = 127;
            this.lbExpire.Text = "Expire";
            this.lbExpire.TextAlign = ContentAlignment.MiddleLeft;
            this.cbExpire.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cbExpire.FlatStyle = FlatStyle.Flat;
            this.cbExpire.ForeColor = Color.FromArgb(192, 0, 0);
            this.cbExpire.FormattingEnabled = true;
            this.cbExpire.Items.AddRange(new object[]
			{
				"End Day",
				"30 Days",
				"60 Days",
				"90 Days",
				"180 Days"
			});
            this.cbExpire.Location = new Point(375, 396);
            this.cbExpire.Margin = new Padding(2);
            this.cbExpire.Name = "cbExpire";
            this.cbExpire.Size = new Size(70, 21);
            this.cbExpire.TabIndex = 126;
            this.panelPZ.BackColor = Color.FromArgb(30, 30, 30);
            this.panelPZ.Controls.Add(this.gridPzMain);
            this.panelPZ.Controls.Add(this.btnPzCreateNew);
            this.panelPZ.Controls.Add(this.btnPzCancel);
            this.panelPZ.Controls.Add(this.btnPzReload);
            this.panelPZ.Location = new Point(7, 193);
            this.panelPZ.Name = "panelPZ";
            this.panelPZ.Size = new Size(673, 105);
            this.panelPZ.TabIndex = 124;
            this.panelPZ.Visible = false;
            this.gridPzMain.AllowDrop = true;
            this.gridPzMain.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
            this.gridPzMain.BackColor = Color.FromArgb(20, 20, 20);
            this.gridPzMain.CanBlink = true;
            this.gridPzMain.CanDrag = false;
            this.gridPzMain.CanGetMouseMove = false;
            columnItem.Alignment = StringAlignment.Center;
            columnItem.BackColor = Color.FromArgb(64, 64, 64);
            columnItem.FontColor = Color.LightGray;
            columnItem.IsExpand = false;
            columnItem.MyStyle = FontStyle.Regular;
            columnItem.Name = "refno";
            columnItem.Text = "Ref No.";
            columnItem.ValueFormat = FormatType.Text;
            columnItem.Visible = true;
            columnItem.Width = 7;
            columnItem2.Alignment = StringAlignment.Near;
            columnItem2.BackColor = Color.FromArgb(64, 64, 64);
            columnItem2.FontColor = Color.LightGray;
            columnItem2.IsExpand = false;
            columnItem2.MyStyle = FontStyle.Regular;
            columnItem2.Name = "stock";
            columnItem2.Text = "Symbol";
            columnItem2.ValueFormat = FormatType.Text;
            columnItem2.Visible = true;
            columnItem2.Width = 14;
            columnItem3.Alignment = StringAlignment.Far;
            columnItem3.BackColor = Color.FromArgb(64, 64, 64);
            columnItem3.FontColor = Color.LightGray;
            columnItem3.IsExpand = false;
            columnItem3.MyStyle = FontStyle.Regular;
            columnItem3.Name = "budget";
            columnItem3.Text = "Budget";
            columnItem3.ValueFormat = FormatType.Volume;
            columnItem3.Visible = true;
            columnItem3.Width = 12;
            columnItem4.Alignment = StringAlignment.Center;
            columnItem4.BackColor = Color.FromArgb(64, 64, 64);
            columnItem4.FontColor = Color.LightGray;
            columnItem4.IsExpand = false;
            columnItem4.MyStyle = FontStyle.Regular;
            columnItem4.Name = "start_price";
            columnItem4.Text = "Start Price";
            columnItem4.ValueFormat = FormatType.Text;
            columnItem4.Visible = true;
            columnItem4.Width = 8;
            columnItem5.Alignment = StringAlignment.Center;
            columnItem5.BackColor = Color.FromArgb(64, 64, 64);
            columnItem5.FontColor = Color.LightGray;
            columnItem5.IsExpand = false;
            columnItem5.MyStyle = FontStyle.Regular;
            columnItem5.Name = "no_steps";
            columnItem5.Text = "Segment";
            columnItem5.ValueFormat = FormatType.Text;
            columnItem5.Visible = true;
            columnItem5.Width = 9;
            columnItem6.Alignment = StringAlignment.Center;
            columnItem6.BackColor = Color.FromArgb(64, 64, 64);
            columnItem6.FontColor = Color.LightGray;
            columnItem6.IsExpand = false;
            columnItem6.MyStyle = FontStyle.Regular;
            columnItem6.Name = "depth_pct";
            columnItem6.Text = "%Chg";
            columnItem6.ValueFormat = FormatType.Text;
            columnItem6.Visible = true;
            columnItem6.Width = 8;
            columnItem7.Alignment = StringAlignment.Far;
            columnItem7.BackColor = Color.FromArgb(64, 64, 64);
            columnItem7.FontColor = Color.LightGray;
            columnItem7.IsExpand = false;
            columnItem7.MyStyle = FontStyle.Regular;
            columnItem7.Name = "matvol";
            columnItem7.Text = "M-Volume";
            columnItem7.ValueFormat = FormatType.Volume;
            columnItem7.Visible = true;
            columnItem7.Width = 11;
            columnItem8.Alignment = StringAlignment.Far;
            columnItem8.BackColor = Color.FromArgb(64, 64, 64);
            columnItem8.FontColor = Color.LightGray;
            columnItem8.IsExpand = false;
            columnItem8.MyStyle = FontStyle.Regular;
            columnItem8.Name = "matval";
            columnItem8.Text = "Cost";
            columnItem8.ValueFormat = FormatType.Volume;
            columnItem8.Visible = true;
            columnItem8.Width = 11;
            columnItem9.Alignment = StringAlignment.Far;
            columnItem9.BackColor = Color.FromArgb(64, 64, 64);
            columnItem9.FontColor = Color.LightGray;
            columnItem9.IsExpand = false;
            columnItem9.MyStyle = FontStyle.Regular;
            columnItem9.Name = "avg";
            columnItem9.Text = "Avg";
            columnItem9.ValueFormat = FormatType.Price;
            columnItem9.Visible = true;
            columnItem9.Width = 8;
            columnItem10.Alignment = StringAlignment.Center;
            columnItem10.BackColor = Color.FromArgb(64, 64, 64);
            columnItem10.FontColor = Color.LightGray;
            columnItem10.IsExpand = false;
            columnItem10.MyStyle = FontStyle.Regular;
            columnItem10.Name = "status";
            columnItem10.Text = "Status";
            columnItem10.ValueFormat = FormatType.Text;
            columnItem10.Visible = true;
            columnItem10.Width = 11;
            this.gridPzMain.Columns.Add(columnItem);
            this.gridPzMain.Columns.Add(columnItem2);
            this.gridPzMain.Columns.Add(columnItem3);
            this.gridPzMain.Columns.Add(columnItem4);
            this.gridPzMain.Columns.Add(columnItem5);
            this.gridPzMain.Columns.Add(columnItem6);
            this.gridPzMain.Columns.Add(columnItem7);
            this.gridPzMain.Columns.Add(columnItem8);
            this.gridPzMain.Columns.Add(columnItem9);
            this.gridPzMain.Columns.Add(columnItem10);
            this.gridPzMain.CurrentScroll = 0;
            this.gridPzMain.FocusItemIndex = -1;
            this.gridPzMain.ForeColor = Color.Black;
            this.gridPzMain.GridColor = Color.FromArgb(50, 50, 50);
            this.gridPzMain.HeaderPctHeight = 100f;
            this.gridPzMain.IsAutoRepaint = true;
            this.gridPzMain.IsDrawGrid = true;
            this.gridPzMain.IsDrawHeader = true;
            this.gridPzMain.IsScrollable = true;
            this.gridPzMain.Location = new Point(8, 35);
            this.gridPzMain.MainColumn = "";
            this.gridPzMain.Name = "gridPzMain";
            this.gridPzMain.Rows = 0;
            this.gridPzMain.RowSelectColor = Color.FromArgb(50, 50, 50);
            this.gridPzMain.RowSelectType = 3;
            this.gridPzMain.Size = new Size(657, 61);
            this.gridPzMain.SortColumnName = "";
            this.gridPzMain.SortType = SortType.Desc;
            this.gridPzMain.TabIndex = 130;
            this.gridPzMain.TableMouseDoubleClick += new ExpandGrid.TableMouseDoubleClickEventHandler(this.gridPzMain_TableMouseDoubleClick);
            this.btnPzCreateNew.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            this.btnPzCreateNew.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            this.btnPzCreateNew.BackColor = Color.Transparent;
            this.btnPzCreateNew.Cursor = Cursors.Hand;
            this.btnPzCreateNew.FlatAppearance.BorderColor = Color.DimGray;
            this.btnPzCreateNew.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 128, 0);
            this.btnPzCreateNew.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 192, 192);
            this.btnPzCreateNew.FlatStyle = FlatStyle.Flat;
            this.btnPzCreateNew.ForeColor = Color.WhiteSmoke;
            this.btnPzCreateNew.Image = Resources.blue_tab;
            this.btnPzCreateNew.Location = new Point(547, 7);
            this.btnPzCreateNew.MaximumSize = new Size(58, 23);
            this.btnPzCreateNew.Name = "btnPzCreateNew";
            this.btnPzCreateNew.Size = new Size(54, 22);
            this.btnPzCreateNew.TabIndex = 139;
            this.btnPzCreateNew.TabStop = false;
            this.btnPzCreateNew.Text = "Create";
            this.btnPzCreateNew.UseVisualStyleBackColor = false;
            this.btnPzCreateNew.Click += new EventHandler(this.btnPzCreateNew_Click);
            this.btnPzCancel.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            this.btnPzCancel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            this.btnPzCancel.BackColor = Color.Transparent;
            this.btnPzCancel.Cursor = Cursors.Hand;
            this.btnPzCancel.FlatAppearance.BorderColor = Color.DimGray;
            this.btnPzCancel.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 128, 0);
            this.btnPzCancel.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 192, 192);
            this.btnPzCancel.FlatStyle = FlatStyle.Flat;
            this.btnPzCancel.ForeColor = Color.WhiteSmoke;
            this.btnPzCancel.Image = Resources.sell_button;
            this.btnPzCancel.Location = new Point(607, 7);
            this.btnPzCancel.MaximumSize = new Size(58, 23);
            this.btnPzCancel.Name = "btnPzCancel";
            this.btnPzCancel.Size = new Size(54, 22);
            this.btnPzCancel.TabIndex = 132;
            this.btnPzCancel.TabStop = false;
            this.btnPzCancel.Text = "Cancel";
            this.btnPzCancel.UseVisualStyleBackColor = false;
            this.btnPzCancel.Click += new EventHandler(this.btnPzCancel_Click);
            this.btnPzReload.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            this.btnPzReload.BackColor = Color.Transparent;
            this.btnPzReload.Cursor = Cursors.Hand;
            this.btnPzReload.FlatAppearance.BorderColor = Color.DimGray;
            this.btnPzReload.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 128, 0);
            this.btnPzReload.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 192, 192);
            this.btnPzReload.FlatStyle = FlatStyle.Flat;
            this.btnPzReload.ForeColor = Color.LightGray;
            this.btnPzReload.Image = Resources._1_4type_bt;
            this.btnPzReload.Location = new Point(8, 7);
            this.btnPzReload.MaximumSize = new Size(58, 23);
            this.btnPzReload.Name = "btnPzReload";
            this.btnPzReload.Size = new Size(54, 22);
            this.btnPzReload.TabIndex = 131;
            this.btnPzReload.TabStop = false;
            this.btnPzReload.Text = "Reload";
            this.btnPzReload.UseVisualStyleBackColor = false;
            this.btnPzReload.Click += new EventHandler(this.btnPzReload_Click);
            this.panelDCA.BackColor = Color.FromArgb(30, 30, 30);
            this.panelDCA.Controls.Add(this.gridDcaMain);
            this.panelDCA.Controls.Add(this.btnDcaCreate);
            this.panelDCA.Controls.Add(this.btn3Cancel);
            this.panelDCA.Controls.Add(this.btnDcaReload);
            this.panelDCA.Location = new Point(6, 303);
            this.panelDCA.Name = "panelDCA";
            this.panelDCA.Size = new Size(673, 88);
            this.panelDCA.TabIndex = 122;
            this.panelDCA.Visible = false;
            this.gridDcaMain.AllowDrop = true;
            this.gridDcaMain.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
            this.gridDcaMain.BackColor = Color.FromArgb(20, 20, 20);
            this.gridDcaMain.CanBlink = true;
            this.gridDcaMain.CanDrag = false;
            this.gridDcaMain.CanGetMouseMove = false;
            columnItem11.Alignment = StringAlignment.Center;
            columnItem11.BackColor = Color.FromArgb(64, 64, 64);
            columnItem11.FontColor = Color.LightGray;
            columnItem11.IsExpand = false;
            columnItem11.MyStyle = FontStyle.Regular;
            columnItem11.Name = "refno";
            columnItem11.Text = "Ref No.";
            columnItem11.ValueFormat = FormatType.Text;
            columnItem11.Visible = true;
            columnItem11.Width = 10;
            columnItem12.Alignment = StringAlignment.Near;
            columnItem12.BackColor = Color.FromArgb(64, 64, 64);
            columnItem12.FontColor = Color.LightGray;
            columnItem12.IsExpand = false;
            columnItem12.MyStyle = FontStyle.Regular;
            columnItem12.Name = "stock";
            columnItem12.Text = "Symbol";
            columnItem12.ValueFormat = FormatType.Text;
            columnItem12.Visible = true;
            columnItem12.Width = 15;
            columnItem13.Alignment = StringAlignment.Far;
            columnItem13.BackColor = Color.FromArgb(64, 64, 64);
            columnItem13.FontColor = Color.LightGray;
            columnItem13.IsExpand = false;
            columnItem13.MyStyle = FontStyle.Regular;
            columnItem13.Name = "budget";
            columnItem13.Text = "Budget";
            columnItem13.ValueFormat = FormatType.Volume;
            columnItem13.Visible = true;
            columnItem13.Width = 13;
            columnItem14.Alignment = StringAlignment.Center;
            columnItem14.BackColor = Color.FromArgb(64, 64, 64);
            columnItem14.FontColor = Color.LightGray;
            columnItem14.IsExpand = false;
            columnItem14.MyStyle = FontStyle.Regular;
            columnItem14.Name = "period";
            columnItem14.Text = "Period";
            columnItem14.ValueFormat = FormatType.Text;
            columnItem14.Visible = true;
            columnItem14.Width = 12;
            columnItem15.Alignment = StringAlignment.Center;
            columnItem15.BackColor = Color.FromArgb(64, 64, 64);
            columnItem15.FontColor = Color.LightGray;
            columnItem15.IsExpand = false;
            columnItem15.MyStyle = FontStyle.Regular;
            columnItem15.Name = "startdate";
            columnItem15.Text = "Start Date";
            columnItem15.ValueFormat = FormatType.Text;
            columnItem15.Visible = true;
            columnItem15.Width = 12;
            columnItem16.Alignment = StringAlignment.Center;
            columnItem16.BackColor = Color.FromArgb(64, 64, 64);
            columnItem16.FontColor = Color.LightGray;
            columnItem16.IsExpand = false;
            columnItem16.MyStyle = FontStyle.Regular;
            columnItem16.Name = "enddate";
            columnItem16.Text = "End Date";
            columnItem16.ValueFormat = FormatType.Text;
            columnItem16.Visible = true;
            columnItem16.Width = 12;
            columnItem17.Alignment = StringAlignment.Far;
            columnItem17.BackColor = Color.FromArgb(64, 64, 64);
            columnItem17.FontColor = Color.LightGray;
            columnItem17.IsExpand = false;
            columnItem17.MyStyle = FontStyle.Regular;
            columnItem17.Name = "matvol";
            columnItem17.Text = "Matched";
            columnItem17.ValueFormat = FormatType.Volume;
            columnItem17.Visible = true;
            columnItem17.Width = 12;
            columnItem18.Alignment = StringAlignment.Center;
            columnItem18.BackColor = Color.FromArgb(64, 64, 64);
            columnItem18.FontColor = Color.LightGray;
            columnItem18.IsExpand = false;
            columnItem18.MyStyle = FontStyle.Regular;
            columnItem18.Name = "status";
            columnItem18.Text = "Status";
            columnItem18.ValueFormat = FormatType.Text;
            columnItem18.Visible = true;
            columnItem18.Width = 14;
            this.gridDcaMain.Columns.Add(columnItem11);
            this.gridDcaMain.Columns.Add(columnItem12);
            this.gridDcaMain.Columns.Add(columnItem13);
            this.gridDcaMain.Columns.Add(columnItem14);
            this.gridDcaMain.Columns.Add(columnItem15);
            this.gridDcaMain.Columns.Add(columnItem16);
            this.gridDcaMain.Columns.Add(columnItem17);
            this.gridDcaMain.Columns.Add(columnItem18);
            this.gridDcaMain.CurrentScroll = 0;
            this.gridDcaMain.FocusItemIndex = -1;
            this.gridDcaMain.ForeColor = Color.Black;
            this.gridDcaMain.GridColor = Color.FromArgb(30, 30, 30);
            this.gridDcaMain.HeaderPctHeight = 100f;
            this.gridDcaMain.IsAutoRepaint = true;
            this.gridDcaMain.IsDrawGrid = true;
            this.gridDcaMain.IsDrawHeader = true;
            this.gridDcaMain.IsScrollable = true;
            this.gridDcaMain.Location = new Point(8, 35);
            this.gridDcaMain.MainColumn = "";
            this.gridDcaMain.Name = "gridDcaMain";
            this.gridDcaMain.Rows = 0;
            this.gridDcaMain.RowSelectColor = Color.FromArgb(50, 50, 50);
            this.gridDcaMain.RowSelectType = 3;
            this.gridDcaMain.Size = new Size(660, 44);
            this.gridDcaMain.SortColumnName = "";
            this.gridDcaMain.SortType = SortType.Desc;
            this.gridDcaMain.TabIndex = 130;
            this.gridDcaMain.TableMouseDoubleClick += new ExpandGrid.TableMouseDoubleClickEventHandler(this.grid3_TableMouseDoubleClick);
            this.btnDcaCreate.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            this.btnDcaCreate.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            this.btnDcaCreate.BackColor = Color.Transparent;
            this.btnDcaCreate.Cursor = Cursors.Hand;
            this.btnDcaCreate.FlatAppearance.BorderColor = Color.DimGray;
            this.btnDcaCreate.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 128, 0);
            this.btnDcaCreate.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 192, 192);
            this.btnDcaCreate.FlatStyle = FlatStyle.Flat;
            this.btnDcaCreate.ForeColor = Color.WhiteSmoke;
            this.btnDcaCreate.Image = Resources.blue_tab;
            this.btnDcaCreate.Location = new Point(544, 7);
            this.btnDcaCreate.MaximumSize = new Size(58, 23);
            this.btnDcaCreate.Name = "btnDcaCreate";
            this.btnDcaCreate.Size = new Size(54, 22);
            this.btnDcaCreate.TabIndex = 139;
            this.btnDcaCreate.TabStop = false;
            this.btnDcaCreate.Text = "Create";
            this.btnDcaCreate.UseVisualStyleBackColor = false;
            this.btnDcaCreate.Click += new EventHandler(this.btnDcaCreate_Click);
            this.btn3Cancel.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            this.btn3Cancel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            this.btn3Cancel.BackColor = Color.Transparent;
            this.btn3Cancel.Cursor = Cursors.Hand;
            this.btn3Cancel.FlatAppearance.BorderColor = Color.DimGray;
            this.btn3Cancel.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 128, 0);
            this.btn3Cancel.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 192, 192);
            this.btn3Cancel.FlatStyle = FlatStyle.Flat;
            this.btn3Cancel.ForeColor = Color.WhiteSmoke;
            this.btn3Cancel.Image = Resources.sell_button;
            this.btn3Cancel.Location = new Point(607, 7);
            this.btn3Cancel.MaximumSize = new Size(58, 23);
            this.btn3Cancel.Name = "btn3Cancel";
            this.btn3Cancel.Size = new Size(54, 22);
            this.btn3Cancel.TabIndex = 132;
            this.btn3Cancel.TabStop = false;
            this.btn3Cancel.Text = "Cancel";
            this.btn3Cancel.UseVisualStyleBackColor = false;
            this.btn3Cancel.Click += new EventHandler(this.btnDcaCancel_Click);
            this.btnDcaReload.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            this.btnDcaReload.BackColor = Color.Transparent;
            this.btnDcaReload.Cursor = Cursors.Hand;
            this.btnDcaReload.FlatAppearance.BorderColor = Color.DimGray;
            this.btnDcaReload.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 128, 0);
            this.btnDcaReload.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 192, 192);
            this.btnDcaReload.FlatStyle = FlatStyle.Flat;
            this.btnDcaReload.ForeColor = Color.LightGray;
            this.btnDcaReload.Image = Resources._1_4type_bt;
            this.btnDcaReload.Location = new Point(8, 7);
            this.btnDcaReload.MaximumSize = new Size(58, 23);
            this.btnDcaReload.Name = "btnDcaReload";
            this.btnDcaReload.Size = new Size(54, 22);
            this.btnDcaReload.TabIndex = 131;
            this.btnDcaReload.TabStop = false;
            this.btnDcaReload.Text = "Reload";
            this.btnDcaReload.UseVisualStyleBackColor = false;
            this.btnDcaReload.Click += new EventHandler(this.btnDcaReload_Click);
            this.panType2.BackColor = Color.FromArgb(30, 30, 30);
            this.panType2.Controls.Add(this.label8);
            this.panType2.Controls.Add(this.lb2ValueCutloss);
            this.panType2.Controls.Add(this.cb2ValueCutloss);
            this.panType2.Controls.Add(this.cb2PriceBreak);
            this.panType2.Controls.Add(this.label7);
            this.panType2.Controls.Add(this.lb2ValueTrailingStop);
            this.panType2.Controls.Add(this.cb2ValueTrailingStop);
            this.panType2.Controls.Add(this.cb2PriceSMA);
            this.panType2.Controls.Add(this.label5);
            this.panType2.Controls.Add(this.cb2OperCutloss);
            this.panType2.Controls.Add(this.cb2OperTrailingStop);
            this.panType2.Controls.Add(this.cb2OperTakeProfit);
            this.panType2.Controls.Add(this.chb2CutLossCond);
            this.panType2.Controls.Add(this.chb2TrailingStopCond);
            this.panType2.Controls.Add(this.chb2TakeProfitCond);
            this.panType2.Controls.Add(this.lb2ValueTakeProfit);
            this.panType2.Controls.Add(this.cb2ValueTakeProfit);
            this.panType2.Controls.Add(this.cb2PriceLast);
            this.panType2.Controls.Add(this.chbGroupCancel);
            this.panType2.Controls.Add(this.label3);
            this.panType2.Controls.Add(this.tb2Volume);
            this.panType2.Controls.Add(this.label4);
            this.panType2.Location = new Point(7, 93);
            this.panType2.Name = "panType2";
            this.panType2.Size = new Size(673, 99);
            this.panType2.TabIndex = 118;
            this.panType2.Visible = false;
            this.label8.AutoSize = true;
            this.label8.ForeColor = Color.LightGray;
            this.label8.Location = new Point(11, 5);
            this.label8.Margin = new Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new Size(76, 13);
            this.label8.TabIndex = 134;
            this.label8.Text = "Sell Conditions";
            this.label8.TextAlign = ContentAlignment.MiddleLeft;
            this.lb2ValueCutloss.AutoSize = true;
            this.lb2ValueCutloss.ForeColor = Color.LightGray;
            this.lb2ValueCutloss.Location = new Point(190, 74);
            this.lb2ValueCutloss.Margin = new Padding(2, 0, 2, 0);
            this.lb2ValueCutloss.Name = "lb2ValueCutloss";
            this.lb2ValueCutloss.Size = new Size(37, 13);
            this.lb2ValueCutloss.TabIndex = 133;
            this.lb2ValueCutloss.Text = "Period";
            this.lb2ValueCutloss.TextAlign = ContentAlignment.MiddleLeft;
            this.cb2ValueCutloss.AutoCompleteSource = AutoCompleteSource.CustomSource;
            this.cb2ValueCutloss.BackColor = Color.FromArgb(224, 224, 224);
            this.cb2ValueCutloss.FlatStyle = FlatStyle.Popup;
            this.cb2ValueCutloss.ForeColor = Color.Black;
            this.cb2ValueCutloss.FormattingEnabled = true;
            this.cb2ValueCutloss.Location = new Point(231, 70);
            this.cb2ValueCutloss.Name = "cb2ValueCutloss";
            this.cb2ValueCutloss.Size = new Size(70, 21);
            this.cb2ValueCutloss.TabIndex = 126;
            this.cb2ValueCutloss.Leave += new EventHandler(this.controlOrder_Leave);
            this.cb2ValueCutloss.Enter += new EventHandler(this.controlOrder_Enter);
            this.cb2ValueCutloss.KeyDown += new KeyEventHandler(this.cb2ValueCutloss_KeyDown);
            this.cb2PriceBreak.AllowDrop = true;
            this.cb2PriceBreak.AutoCompleteCustomSource.AddRange(new string[]
			{
				"",
				"ATO",
				"ATC",
				"MP",
				"MO",
				"ML"
			});
            this.cb2PriceBreak.AutoCompleteMode = AutoCompleteMode.Append;
            this.cb2PriceBreak.AutoCompleteSource = AutoCompleteSource.CustomSource;
            this.cb2PriceBreak.BackColor = Color.FromArgb(224, 224, 224);
            this.cb2PriceBreak.Enabled = false;
            this.cb2PriceBreak.FlatStyle = FlatStyle.Popup;
            this.cb2PriceBreak.ForeColor = Color.Black;
            this.cb2PriceBreak.FormattingEnabled = true;
            this.cb2PriceBreak.Location = new Point(346, 70);
            this.cb2PriceBreak.Name = "cb2PriceBreak";
            this.cb2PriceBreak.Size = new Size(57, 21);
            this.cb2PriceBreak.TabIndex = 127;
            this.cb2PriceBreak.Text = "MP";
            this.cb2PriceBreak.Leave += new EventHandler(this.controlOrder_Leave);
            this.cb2PriceBreak.Enter += new EventHandler(this.controlOrder_Enter);
            this.cb2PriceBreak.KeyPress += new KeyPressEventHandler(this.cbPrice_KeyPress);
            this.label7.AutoSize = true;
            this.label7.ForeColor = Color.LightGray;
            this.label7.Location = new Point(311, 74);
            this.label7.Margin = new Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new Size(31, 13);
            this.label7.TabIndex = 128;
            this.label7.Text = "Price";
            this.label7.TextAlign = ContentAlignment.MiddleLeft;
            this.lb2ValueTrailingStop.AutoSize = true;
            this.lb2ValueTrailingStop.ForeColor = Color.LightGray;
            this.lb2ValueTrailingStop.Location = new Point(190, 51);
            this.lb2ValueTrailingStop.Margin = new Padding(2, 0, 2, 0);
            this.lb2ValueTrailingStop.Name = "lb2ValueTrailingStop";
            this.lb2ValueTrailingStop.Size = new Size(37, 13);
            this.lb2ValueTrailingStop.TabIndex = 127;
            this.lb2ValueTrailingStop.Text = "Period";
            this.lb2ValueTrailingStop.TextAlign = ContentAlignment.MiddleLeft;
            this.cb2ValueTrailingStop.AutoCompleteSource = AutoCompleteSource.CustomSource;
            this.cb2ValueTrailingStop.BackColor = Color.FromArgb(224, 224, 224);
            this.cb2ValueTrailingStop.FlatStyle = FlatStyle.Popup;
            this.cb2ValueTrailingStop.ForeColor = Color.Black;
            this.cb2ValueTrailingStop.FormattingEnabled = true;
            this.cb2ValueTrailingStop.Location = new Point(231, 47);
            this.cb2ValueTrailingStop.Name = "cb2ValueTrailingStop";
            this.cb2ValueTrailingStop.Size = new Size(70, 21);
            this.cb2ValueTrailingStop.TabIndex = 123;
            this.cb2ValueTrailingStop.Leave += new EventHandler(this.controlOrder_Leave);
            this.cb2ValueTrailingStop.Enter += new EventHandler(this.controlOrder_Enter);
            this.cb2ValueTrailingStop.KeyDown += new KeyEventHandler(this.cb2ValueTrailingStop_KeyDown);
            this.cb2PriceSMA.AllowDrop = true;
            this.cb2PriceSMA.AutoCompleteCustomSource.AddRange(new string[]
			{
				"",
				"ATO",
				"ATC",
				"MP",
				"MO",
				"ML"
			});
            this.cb2PriceSMA.AutoCompleteMode = AutoCompleteMode.Append;
            this.cb2PriceSMA.AutoCompleteSource = AutoCompleteSource.CustomSource;
            this.cb2PriceSMA.BackColor = Color.FromArgb(224, 224, 224);
            this.cb2PriceSMA.Enabled = false;
            this.cb2PriceSMA.FlatStyle = FlatStyle.Popup;
            this.cb2PriceSMA.ForeColor = Color.Black;
            this.cb2PriceSMA.FormattingEnabled = true;
            this.cb2PriceSMA.Location = new Point(346, 47);
            this.cb2PriceSMA.Name = "cb2PriceSMA";
            this.cb2PriceSMA.Size = new Size(57, 21);
            this.cb2PriceSMA.TabIndex = 124;
            this.cb2PriceSMA.Text = "MP";
            this.cb2PriceSMA.Leave += new EventHandler(this.controlOrder_Leave);
            this.cb2PriceSMA.Enter += new EventHandler(this.controlOrder_Enter);
            this.cb2PriceSMA.KeyPress += new KeyPressEventHandler(this.cbPrice_KeyPress);
            this.label5.AutoSize = true;
            this.label5.ForeColor = Color.LightGray;
            this.label5.Location = new Point(311, 51);
            this.label5.Margin = new Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new Size(31, 13);
            this.label5.TabIndex = 122;
            this.label5.Text = "Price";
            this.label5.TextAlign = ContentAlignment.MiddleLeft;
            this.cb2OperCutloss.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cb2OperCutloss.FlatStyle = FlatStyle.Flat;
            this.cb2OperCutloss.FormattingEnabled = true;
            this.cb2OperCutloss.Items.AddRange(new object[]
			{
				"Last <",
				"Break Low <"
			});
            this.cb2OperCutloss.Location = new Point(98, 70);
            this.cb2OperCutloss.Margin = new Padding(2);
            this.cb2OperCutloss.Name = "cb2OperCutloss";
            this.cb2OperCutloss.Size = new Size(85, 21);
            this.cb2OperCutloss.TabIndex = 121;
            this.cb2OperCutloss.SelectedIndexChanged += new EventHandler(this.cb2OperCutloss_SelectedIndexChanged);
            this.cb2OperCutloss.Leave += new EventHandler(this.controlOrder_Leave);
            this.cb2OperCutloss.Enter += new EventHandler(this.controlOrder_Enter);
            this.cb2OperCutloss.KeyDown += new KeyEventHandler(this.cb2OperCutloss_KeyDown);
            this.cb2OperTrailingStop.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cb2OperTrailingStop.FlatStyle = FlatStyle.Flat;
            this.cb2OperTrailingStop.FormattingEnabled = true;
            this.cb2OperTrailingStop.Items.AddRange(new object[]
			{
				"SMA <",
				"Break Low <"
			});
            this.cb2OperTrailingStop.Location = new Point(98, 47);
            this.cb2OperTrailingStop.Margin = new Padding(2);
            this.cb2OperTrailingStop.Name = "cb2OperTrailingStop";
            this.cb2OperTrailingStop.Size = new Size(85, 21);
            this.cb2OperTrailingStop.TabIndex = 122;
            this.cb2OperTrailingStop.SelectedIndexChanged += new EventHandler(this.cb2OperTrailingStop_SelectedIndexChanged);
            this.cb2OperTrailingStop.Leave += new EventHandler(this.controlOrder_Leave);
            this.cb2OperTrailingStop.Enter += new EventHandler(this.controlOrder_Enter);
            this.cb2OperTrailingStop.KeyDown += new KeyEventHandler(this.cb2OperTrailingStop_KeyDown);
            this.cb2OperTakeProfit.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cb2OperTakeProfit.FlatStyle = FlatStyle.Flat;
            this.cb2OperTakeProfit.FormattingEnabled = true;
            this.cb2OperTakeProfit.Items.AddRange(new object[]
			{
				"Last >="
			});
            this.cb2OperTakeProfit.Location = new Point(98, 24);
            this.cb2OperTakeProfit.Margin = new Padding(2);
            this.cb2OperTakeProfit.Name = "cb2OperTakeProfit";
            this.cb2OperTakeProfit.Size = new Size(85, 21);
            this.cb2OperTakeProfit.TabIndex = 117;
            this.cb2OperTakeProfit.SelectedIndexChanged += new EventHandler(this.cb2OperTakeProfit_SelectedIndexChanged);
            this.cb2OperTakeProfit.Leave += new EventHandler(this.controlOrder_Leave);
            this.cb2OperTakeProfit.Enter += new EventHandler(this.controlOrder_Enter);
            this.cb2OperTakeProfit.KeyDown += new KeyEventHandler(this.cb2OperTakeProfit_KeyDown);
            this.chb2CutLossCond.AutoSize = true;
            this.chb2CutLossCond.ForeColor = Color.LightGray;
            this.chb2CutLossCond.Location = new Point(14, 72);
            this.chb2CutLossCond.Margin = new Padding(2);
            this.chb2CutLossCond.Name = "chb2CutLossCond";
            this.chb2CutLossCond.Size = new Size(67, 17);
            this.chb2CutLossCond.TabIndex = 125;
            this.chb2CutLossCond.Text = "Cut Loss";
            this.chb2CutLossCond.UseVisualStyleBackColor = true;
            this.chb2TrailingStopCond.AutoSize = true;
            this.chb2TrailingStopCond.ForeColor = Color.LightGray;
            this.chb2TrailingStopCond.Location = new Point(14, 49);
            this.chb2TrailingStopCond.Margin = new Padding(2);
            this.chb2TrailingStopCond.Name = "chb2TrailingStopCond";
            this.chb2TrailingStopCond.Size = new Size(85, 17);
            this.chb2TrailingStopCond.TabIndex = 121;
            this.chb2TrailingStopCond.Text = "Trailing Stop";
            this.chb2TrailingStopCond.UseVisualStyleBackColor = true;
            this.chb2TakeProfitCond.AutoSize = true;
            this.chb2TakeProfitCond.ForeColor = Color.LightGray;
            this.chb2TakeProfitCond.Location = new Point(14, 27);
            this.chb2TakeProfitCond.Margin = new Padding(2);
            this.chb2TakeProfitCond.Name = "chb2TakeProfitCond";
            this.chb2TakeProfitCond.Size = new Size(78, 17);
            this.chb2TakeProfitCond.TabIndex = 116;
            this.chb2TakeProfitCond.Text = "Take Profit";
            this.chb2TakeProfitCond.UseVisualStyleBackColor = true;
            this.lb2ValueTakeProfit.AutoSize = true;
            this.lb2ValueTakeProfit.ForeColor = Color.LightGray;
            this.lb2ValueTakeProfit.Location = new Point(193, 28);
            this.lb2ValueTakeProfit.Margin = new Padding(2, 0, 2, 0);
            this.lb2ValueTakeProfit.Name = "lb2ValueTakeProfit";
            this.lb2ValueTakeProfit.Size = new Size(34, 13);
            this.lb2ValueTakeProfit.TabIndex = 115;
            this.lb2ValueTakeProfit.Text = "Value";
            this.lb2ValueTakeProfit.TextAlign = ContentAlignment.MiddleLeft;
            this.cb2ValueTakeProfit.AutoCompleteSource = AutoCompleteSource.CustomSource;
            this.cb2ValueTakeProfit.BackColor = Color.FromArgb(224, 224, 224);
            this.cb2ValueTakeProfit.FlatStyle = FlatStyle.Popup;
            this.cb2ValueTakeProfit.ForeColor = Color.Black;
            this.cb2ValueTakeProfit.FormattingEnabled = true;
            this.cb2ValueTakeProfit.Location = new Point(231, 24);
            this.cb2ValueTakeProfit.Name = "cb2ValueTakeProfit";
            this.cb2ValueTakeProfit.Size = new Size(70, 21);
            this.cb2ValueTakeProfit.TabIndex = 118;
            this.cb2ValueTakeProfit.Leave += new EventHandler(this.controlOrder_Leave);
            this.cb2ValueTakeProfit.Enter += new EventHandler(this.controlOrder_Enter);
            this.cb2ValueTakeProfit.KeyDown += new KeyEventHandler(this.cb2ValueTakeProfit_KeyDown);
            this.cb2PriceLast.AllowDrop = true;
            this.cb2PriceLast.AutoCompleteCustomSource.AddRange(new string[]
			{
				"",
				"ATO",
				"ATC",
				"MP",
				"MO",
				"ML"
			});
            this.cb2PriceLast.AutoCompleteMode = AutoCompleteMode.Append;
            this.cb2PriceLast.AutoCompleteSource = AutoCompleteSource.CustomSource;
            this.cb2PriceLast.BackColor = Color.FromArgb(224, 224, 224);
            this.cb2PriceLast.FlatStyle = FlatStyle.Popup;
            this.cb2PriceLast.ForeColor = Color.Black;
            this.cb2PriceLast.FormattingEnabled = true;
            this.cb2PriceLast.Location = new Point(346, 24);
            this.cb2PriceLast.Name = "cb2PriceLast";
            this.cb2PriceLast.Size = new Size(57, 21);
            this.cb2PriceLast.TabIndex = 119;
            this.cb2PriceLast.Leave += new EventHandler(this.controlOrder_Leave);
            this.cb2PriceLast.Enter += new EventHandler(this.controlOrder_Enter);
            this.cb2PriceLast.KeyPress += new KeyPressEventHandler(this.cbPrice_KeyPress);
            this.cb2PriceLast.KeyDown += new KeyEventHandler(this.cb2PriceLast_KeyDown);
            this.chbGroupCancel.AutoSize = true;
            this.chbGroupCancel.ForeColor = Color.FromArgb(255, 192, 128);
            this.chbGroupCancel.Location = new Point(422, 72);
            this.chbGroupCancel.Margin = new Padding(2, 3, 0, 3);
            this.chbGroupCancel.Name = "chbGroupCancel";
            this.chbGroupCancel.Size = new Size(91, 17);
            this.chbGroupCancel.TabIndex = 111;
            this.chbGroupCancel.Text = "Group Cancel";
            this.chbGroupCancel.UseVisualStyleBackColor = false;
            this.label3.AutoSize = true;
            this.label3.ForeColor = Color.LightGray;
            this.label3.Location = new Point(418, 28);
            this.label3.Margin = new Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new Size(42, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Volume";
            this.label3.TextAlign = ContentAlignment.MiddleLeft;
            this.tb2Volume.BackColor = Color.FromArgb(224, 224, 224);
            this.tb2Volume.BorderStyle = BorderStyle.FixedSingle;
            this.tb2Volume.Location = new Point(463, 24);
            this.tb2Volume.Margin = new Padding(2, 3, 2, 3);
            this.tb2Volume.MaxLength = 10;
            this.tb2Volume.Name = "tb2Volume";
            this.tb2Volume.Size = new Size(59, 20);
            this.tb2Volume.TabIndex = 120;
            this.tb2Volume.TextChanged += new EventHandler(this.tb2Volume_TextChanged);
            this.tb2Volume.KeyDown += new KeyEventHandler(this.tb2Volume_KeyDown);
            this.tb2Volume.Leave += new EventHandler(this.controlOrder_Leave);
            this.tb2Volume.Enter += new EventHandler(this.controlOrder_Enter);
            this.label4.AutoSize = true;
            this.label4.ForeColor = Color.LightGray;
            this.label4.Location = new Point(311, 28);
            this.label4.Margin = new Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new Size(31, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "Price";
            this.label4.TextAlign = ContentAlignment.MiddleLeft;
            this.btnTypePZ.AutoEllipsis = true;
            this.btnTypePZ.Cursor = Cursors.Hand;
            this.btnTypePZ.FlatAppearance.BorderColor = Color.DimGray;
            this.btnTypePZ.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 128, 0);
            this.btnTypePZ.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 192, 192);
            this.btnTypePZ.FlatStyle = FlatStyle.Flat;
            this.btnTypePZ.ForeColor = Color.WhiteSmoke;
            this.btnTypePZ.Location = new Point(647, 6);
            this.btnTypePZ.MaximumSize = new Size(58, 23);
            this.btnTypePZ.Name = "btnTypePZ";
            this.btnTypePZ.Size = new Size(31, 22);
            this.btnTypePZ.TabIndex = 125;
            this.btnTypePZ.TabStop = false;
            this.btnTypePZ.Text = "PZ";
            this.toolTip1.SetToolTip(this.btnTypePZ, "Pricing Zone");
            this.btnTypePZ.UseVisualStyleBackColor = false;
            this.btnTypePZ.Click += new EventHandler(this.btnTypePZ_Click);
            this.panType1.BackColor = Color.FromArgb(30, 30, 30);
            this.panType1.Controls.Add(this.cb1Condition);
            this.panType1.Controls.Add(this.lb1Value);
            this.panType1.Controls.Add(this.cb1Value);
            this.panType1.Controls.Add(this.cb1Price);
            this.panType1.Controls.Add(this.lbStopOrderField);
            this.panType1.Controls.Add(this.lbVolume);
            this.panType1.Controls.Add(this.tb1Volume);
            this.panType1.Controls.Add(this.lbPrice);
            this.panType1.Location = new Point(6, 31);
            this.panType1.Name = "panType1";
            this.panType1.Size = new Size(674, 60);
            this.panType1.TabIndex = 110;
            this.cb1Condition.AllowDrop = true;
            this.cb1Condition.AutoCompleteSource = AutoCompleteSource.CustomSource;
            this.cb1Condition.BackColor = Color.FromArgb(224, 224, 224);
            this.cb1Condition.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cb1Condition.FlatStyle = FlatStyle.Popup;
            this.cb1Condition.ForeColor = Color.Black;
            this.cb1Condition.FormattingEnabled = true;
            this.cb1Condition.Items.AddRange(new object[]
			{
				"Last <=",
                "Follow Biglot",
                "Last <= (sma)"
			});
            this.cb1Condition.Location = new Point(15, 30);
            this.cb1Condition.Name = "cb1Condition";
            this.cb1Condition.Size = new Size(138, 21);
            this.cb1Condition.TabIndex = 116;
            this.cb1Condition.SelectedIndexChanged += new EventHandler(this.cb1Field_SelectedIndexChanged);
            this.cb1Condition.Leave += new EventHandler(this.controlOrder_Leave);
            this.cb1Condition.Enter += new EventHandler(this.controlOrder_Enter);
            this.cb1Condition.KeyDown += new KeyEventHandler(this.cb1Condition_KeyDown);
            this.lb1Value.AutoSize = true;
            this.lb1Value.ForeColor = Color.LightGray;
            this.lb1Value.Location = new Point(161, 34);
            this.lb1Value.Margin = new Padding(2, 0, 2, 0);
            this.lb1Value.Name = "lb1Value";
            this.lb1Value.Size = new Size(34, 13);
            this.lb1Value.TabIndex = 115;
            this.lb1Value.Text = "Value";
            this.lb1Value.TextAlign = ContentAlignment.MiddleLeft;
            this.cb1Value.AutoCompleteSource = AutoCompleteSource.CustomSource;
            this.cb1Value.BackColor = Color.FromArgb(224, 224, 224);
            this.cb1Value.FlatStyle = FlatStyle.Popup;
            this.cb1Value.ForeColor = Color.Black;
            this.cb1Value.FormattingEnabled = true;
            this.cb1Value.Location = new Point(199, 30);
            this.cb1Value.Name = "cb1Value";
            this.cb1Value.Size = new Size(64, 21);
            this.cb1Value.TabIndex = 117;
            this.cb1Value.Leave += new EventHandler(this.controlOrder_Leave);
            this.cb1Value.Enter += new EventHandler(this.controlOrder_Enter);
            this.cb1Value.KeyDown += new KeyEventHandler(this.cb1Value_KeyDown);
            this.lbStopOrderField.AutoSize = true;
            this.lbStopOrderField.ForeColor = Color.LightGray;
            this.lbStopOrderField.Location = new Point(12, 8);
            this.lbStopOrderField.Margin = new Padding(2, 0, 2, 0);
            this.lbStopOrderField.Name = "lbStopOrderField";
            this.lbStopOrderField.Size = new Size(85, 13);
            this.lbStopOrderField.TabIndex = 109;
            this.lbStopOrderField.Text = "Order Conditions";
            this.lbStopOrderField.TextAlign = ContentAlignment.MiddleLeft;
            this.btnTypeDCA.AutoEllipsis = true;
            this.btnTypeDCA.Cursor = Cursors.Hand;
            this.btnTypeDCA.FlatAppearance.BorderColor = Color.DimGray;
            this.btnTypeDCA.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 128, 0);
            this.btnTypeDCA.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 192, 192);
            this.btnTypeDCA.FlatStyle = FlatStyle.Flat;
            this.btnTypeDCA.ForeColor = Color.WhiteSmoke;
            this.btnTypeDCA.Location = new Point(604, 6);
            this.btnTypeDCA.MaximumSize = new Size(58, 23);
            this.btnTypeDCA.Name = "btnTypeDCA";
            this.btnTypeDCA.Size = new Size(39, 22);
            this.btnTypeDCA.TabIndex = 123;
            this.btnTypeDCA.TabStop = false;
            this.btnTypeDCA.Text = "DCA";
            this.toolTip1.SetToolTip(this.btnTypeDCA, "Dolla Cost Average");
            this.btnTypeDCA.UseVisualStyleBackColor = false;
            this.btnTypeDCA.Click += new EventHandler(this.btnType3_Click);
            this.btnTypeMM.AutoEllipsis = true;
            this.btnTypeMM.Cursor = Cursors.Hand;
            this.btnTypeMM.FlatAppearance.BorderColor = Color.DimGray;
            this.btnTypeMM.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 128, 0);
            this.btnTypeMM.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 192, 192);
            this.btnTypeMM.FlatStyle = FlatStyle.Flat;
            this.btnTypeMM.ForeColor = Color.WhiteSmoke;
            this.btnTypeMM.Location = new Point(564, 6);
            this.btnTypeMM.MaximumSize = new Size(58, 23);
            this.btnTypeMM.Name = "btnTypeMM";
            this.btnTypeMM.Size = new Size(35, 22);
            this.btnTypeMM.TabIndex = 119;
            this.btnTypeMM.TabStop = false;
            this.btnTypeMM.Text = "MM";
            this.toolTip1.SetToolTip(this.btnTypeMM, "Money Management");
            this.btnTypeMM.UseVisualStyleBackColor = false;
            this.btnTypeMM.Click += new EventHandler(this.btnTypeMM_Click);
            this.btnSell.AutoEllipsis = true;
            this.btnSell.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            this.btnSell.BackColor = Color.Transparent;
            this.btnSell.Cursor = Cursors.Hand;
            this.btnSell.FlatAppearance.BorderColor = Color.DimGray;
            this.btnSell.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 128, 0);
            this.btnSell.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 192, 192);
            this.btnSell.FlatStyle = FlatStyle.Flat;
            this.btnSell.ForeColor = Color.WhiteSmoke;
            this.btnSell.Image = Resources._1_4type_bt;
            this.btnSell.Location = new Point(75, 5);
            this.btnSell.MaximumSize = new Size(58, 23);
            this.btnSell.Name = "btnSell";
            this.btnSell.Size = new Size(54, 22);
            this.btnSell.TabIndex = 117;
            this.btnSell.TabStop = false;
            this.btnSell.Text = "Sell";
            this.btnSell.UseVisualStyleBackColor = false;
            this.btnSell.Click += new EventHandler(this.btnSell_Click);
            this.btnBuy.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            this.btnBuy.BackColor = Color.Transparent;
            this.btnBuy.Cursor = Cursors.Hand;
            this.btnBuy.FlatAppearance.BorderColor = Color.DimGray;
            this.btnBuy.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 128, 0);
            this.btnBuy.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 192, 192);
            this.btnBuy.FlatStyle = FlatStyle.Flat;
            this.btnBuy.ForeColor = Color.WhiteSmoke;
            this.btnBuy.Image = Resources._1_4type_bt;
            this.btnBuy.Location = new Point(15, 5);
            this.btnBuy.MaximumSize = new Size(58, 23);
            this.btnBuy.Name = "btnBuy";
            this.btnBuy.Size = new Size(54, 22);
            this.btnBuy.TabIndex = 116;
            this.btnBuy.TabStop = false;
            this.btnBuy.Text = "Buy";
            this.btnBuy.UseVisualStyleBackColor = false;
            this.btnBuy.Click += new EventHandler(this.btnBuy_Click);
            this.btnType2.AutoEllipsis = true;
            this.btnType2.Cursor = Cursors.Hand;
            this.btnType2.FlatAppearance.BorderColor = Color.DimGray;
            this.btnType2.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 128, 0);
            this.btnType2.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 192, 192);
            this.btnType2.FlatStyle = FlatStyle.Flat;
            this.btnType2.ForeColor = Color.WhiteSmoke;
            this.btnType2.Location = new Point(537, 6);
            this.btnType2.MaximumSize = new Size(58, 23);
            this.btnType2.Name = "btnType2";
            this.btnType2.Size = new Size(22, 22);
            this.btnType2.TabIndex = 113;
            this.btnType2.TabStop = false;
            this.btnType2.Text = "2";
            this.btnType2.UseVisualStyleBackColor = false;
            this.btnType2.Click += new EventHandler(this.btnType2_Click);
            this.btnType1.AutoEllipsis = true;
            this.btnType1.BackColor = Color.DodgerBlue;
            this.btnType1.Cursor = Cursors.Hand;
            this.btnType1.FlatAppearance.BorderColor = Color.DimGray;
            this.btnType1.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 128, 0);
            this.btnType1.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 192, 192);
            this.btnType1.FlatStyle = FlatStyle.Flat;
            this.btnType1.ForeColor = Color.WhiteSmoke;
            this.btnType1.Location = new Point(507, 6);
            this.btnType1.MaximumSize = new Size(58, 23);
            this.btnType1.Name = "btnType1";
            this.btnType1.Size = new Size(22, 22);
            this.btnType1.TabIndex = 112;
            this.btnType1.TabStop = false;
            this.btnType1.Text = "1";
            this.btnType1.UseVisualStyleBackColor = false;
            this.btnType1.Click += new EventHandler(this.btnType1_Click);
            this.lbPattern.AutoSize = true;
            this.lbPattern.ForeColor = Color.LightGray;
            this.lbPattern.Location = new Point(466, 10);
            this.lbPattern.Margin = new Padding(2, 0, 2, 0);
            this.lbPattern.Name = "lbPattern";
            this.lbPattern.Size = new Size(31, 13);
            this.lbPattern.TabIndex = 111;
            this.lbPattern.Text = "Type";
            this.lbPattern.TextAlign = ContentAlignment.MiddleLeft;
            this.tStripMenu.AllowMerge = false;
            this.tStripMenu.BackColor = Color.FromArgb(40, 40, 40);
            this.tStripMenu.CanOverflow = false;
            this.tStripMenu.GripMargin = new Padding(0);
            this.tStripMenu.GripStyle = ToolStripGripStyle.Hidden;
            this.tStripMenu.Items.AddRange(new ToolStripItem[]
			{
				this.tslbStatus,
				this.tscbStatus,
				this.tslbStock,
				this.tstbStock,
				this.tslbSide,
				this.tscbSide,
				this.tsbtnClearCondition,
				this.tsbtnCancelOrder,
				this.tsbtnSearch
			});
            this.tStripMenu.LayoutStyle = ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.tStripMenu.Location = new Point(0, 0);
            this.tStripMenu.Name = "tStripMenu";
            this.tStripMenu.Padding = new Padding(1, 2, 1, 1);
            this.tStripMenu.RenderMode = ToolStripRenderMode.System;
            this.tStripMenu.Size = new Size(697, 28);
            this.tStripMenu.TabIndex = 114;
            this.tslbStatus.BackColor = Color.Transparent;
            this.tslbStatus.ForeColor = Color.Gainsboro;
            this.tslbStatus.Margin = new Padding(5, 1, 1, 1);
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
            this.tscbStatus.AutoCompleteSource = AutoCompleteSource.CustomSource;
            this.tscbStatus.BackColor = Color.FromArgb(45, 45, 45);
            this.tscbStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            this.tscbStatus.ForeColor = Color.LightGray;
            this.tscbStatus.Margin = new Padding(1, 0, 1, 2);
            this.tscbStatus.MaxLength = 3;
            this.tscbStatus.Name = "tscbStatus";
            this.tscbStatus.Size = new Size(75, 23);
            this.tslbStock.BackColor = Color.Transparent;
            this.tslbStock.ForeColor = Color.Gainsboro;
            this.tslbStock.Margin = new Padding(5, 1, 1, 1);
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
            this.tstbStock.KeyDown += new KeyEventHandler(this.tstbStock_KeyDown);
            this.tslbSide.BackColor = Color.Transparent;
            this.tslbSide.ForeColor = Color.Gainsboro;
            this.tslbSide.Margin = new Padding(5, 1, 1, 1);
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
            this.tscbSide.AutoCompleteSource = AutoCompleteSource.CustomSource;
            this.tscbSide.BackColor = Color.FromArgb(45, 45, 45);
            this.tscbSide.DropDownStyle = ComboBoxStyle.DropDownList;
            this.tscbSide.ForeColor = Color.LightGray;
            this.tscbSide.Items.AddRange(new object[]
			{
				"ALL",
				"B",
				"S"
			});
            this.tscbSide.Margin = new Padding(1, 0, 1, 2);
            this.tscbSide.MaxLength = 3;
            this.tscbSide.Name = "tscbSide";
            this.tscbSide.Size = new Size(75, 23);
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
            this.panel1.BackColor = Color.FromArgb(50, 50, 50);
            this.panel1.Controls.Add(this.intzaOrder);
            this.panel1.Controls.Add(this.tStripMenu);
            this.panel1.Dock = DockStyle.Fill;
            this.panel1.Location = new Point(0, 424);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(697, 105);
            this.panel1.TabIndex = 116;
            this.intzaOrder.AllowDrop = true;
            this.intzaOrder.BackColor = Color.FromArgb(30, 30, 30);
            this.intzaOrder.CanBlink = true;
            this.intzaOrder.CanDrag = false;
            this.intzaOrder.CanGetMouseMove = false;
            columnItem19.Alignment = StringAlignment.Near;
            columnItem19.BackColor = Color.FromArgb(64, 64, 64);
            columnItem19.FontColor = Color.LightGray;
            columnItem19.IsExpand = false;
            columnItem19.MyStyle = FontStyle.Regular;
            columnItem19.Name = "checkbox";
            columnItem19.Text = "";
            columnItem19.ValueFormat = FormatType.Bitmap;
            columnItem19.Visible = true;
            columnItem19.Width = 3;
            columnItem20.Alignment = StringAlignment.Center;
            columnItem20.BackColor = Color.FromArgb(64, 64, 64);
            columnItem20.FontColor = Color.LightGray;
            columnItem20.IsExpand = false;
            columnItem20.MyStyle = FontStyle.Regular;
            columnItem20.Name = "side";
            columnItem20.Text = "B/S";
            columnItem20.ValueFormat = FormatType.Text;
            columnItem20.Visible = true;
            columnItem20.Width = 5;
            columnItem21.Alignment = StringAlignment.Near;
            columnItem21.BackColor = Color.FromArgb(64, 64, 64);
            columnItem21.FontColor = Color.LightGray;
            columnItem21.IsExpand = false;
            columnItem21.MyStyle = FontStyle.Regular;
            columnItem21.Name = "stock";
            columnItem21.Text = "Symbol";
            columnItem21.ValueFormat = FormatType.Text;
            columnItem21.Visible = true;
            columnItem21.Width = 12;
            columnItem22.Alignment = StringAlignment.Far;
            columnItem22.BackColor = Color.FromArgb(64, 64, 64);
            columnItem22.FontColor = Color.LightGray;
            columnItem22.IsExpand = false;
            columnItem22.MyStyle = FontStyle.Regular;
            columnItem22.Name = "volume";
            columnItem22.Text = "Volume";
            columnItem22.ValueFormat = FormatType.Volume;
            columnItem22.Visible = true;
            columnItem22.Width = 13;
            columnItem23.Alignment = StringAlignment.Far;
            columnItem23.BackColor = Color.FromArgb(64, 64, 64);
            columnItem23.FontColor = Color.LightGray;
            columnItem23.IsExpand = false;
            columnItem23.MyStyle = FontStyle.Regular;
            columnItem23.Name = "price";
            columnItem23.Text = "Price";
            columnItem23.ValueFormat = FormatType.Text;
            columnItem23.Visible = true;
            columnItem23.Width = 10;
            columnItem24.Alignment = StringAlignment.Near;
            columnItem24.BackColor = Color.FromArgb(64, 64, 64);
            columnItem24.FontColor = Color.LightGray;
            columnItem24.IsExpand = false;
            columnItem24.MyStyle = FontStyle.Regular;
            columnItem24.Name = "condition";
            columnItem24.Text = "Condition";
            columnItem24.ValueFormat = FormatType.Text;
            columnItem24.Visible = true;
            columnItem24.Width = 32;
            columnItem25.Alignment = StringAlignment.Center;
            columnItem25.BackColor = Color.FromArgb(64, 64, 64);
            columnItem25.FontColor = Color.LightGray;
            columnItem25.IsExpand = false;
            columnItem25.MyStyle = FontStyle.Regular;
            columnItem25.Name = "status";
            columnItem25.Text = "Status";
            columnItem25.ValueFormat = FormatType.Text;
            columnItem25.Visible = true;
            columnItem25.Width = 13;
            columnItem26.Alignment = StringAlignment.Center;
            columnItem26.BackColor = Color.FromArgb(64, 64, 64);
            columnItem26.FontColor = Color.LightGray;
            columnItem26.IsExpand = false;
            columnItem26.MyStyle = FontStyle.Regular;
            columnItem26.Name = "sent_time";
            columnItem26.Text = "Time";
            columnItem26.ValueFormat = FormatType.Text;
            columnItem26.Visible = true;
            columnItem26.Width = 12;
            columnItem27.Alignment = StringAlignment.Center;
            columnItem27.BackColor = Color.FromArgb(64, 64, 64);
            columnItem27.FontColor = Color.LightGray;
            columnItem27.IsExpand = false;
            columnItem27.MyStyle = FontStyle.Regular;
            columnItem27.Name = "limit";
            columnItem27.Text = "Expire Date";
            columnItem27.ValueFormat = FormatType.Text;
            columnItem27.Visible = true;
            columnItem27.Width = 13;
            columnItem28.Alignment = StringAlignment.Center;
            columnItem28.BackColor = Color.FromArgb(64, 64, 64);
            columnItem28.FontColor = Color.LightGray;
            columnItem28.IsExpand = false;
            columnItem28.MyStyle = FontStyle.Regular;
            columnItem28.Name = "group_cancel";
            columnItem28.Text = "Group Cancel";
            columnItem28.ValueFormat = FormatType.Text;
            columnItem28.Visible = true;
            columnItem28.Width = 13;
            columnItem29.Alignment = StringAlignment.Center;
            columnItem29.BackColor = Color.FromArgb(64, 64, 64);
            columnItem29.FontColor = Color.LightGray;
            columnItem29.IsExpand = false;
            columnItem29.MyStyle = FontStyle.Regular;
            columnItem29.Name = "ref_no";
            columnItem29.Text = "Ref No.";
            columnItem29.ValueFormat = FormatType.Text;
            columnItem29.Visible = true;
            columnItem29.Width = 10;
            columnItem30.Alignment = StringAlignment.Center;
            columnItem30.BackColor = Color.FromArgb(64, 64, 64);
            columnItem30.FontColor = Color.LightGray;
            columnItem30.IsExpand = false;
            columnItem30.MyStyle = FontStyle.Regular;
            columnItem30.Name = "mm_src_ordno";
            columnItem30.Text = "Source No";
            columnItem30.ValueFormat = FormatType.Text;
            columnItem30.Visible = true;
            columnItem30.Width = 12;
            columnItem31.Alignment = StringAlignment.Center;
            columnItem31.BackColor = Color.FromArgb(64, 64, 64);
            columnItem31.FontColor = Color.LightGray;
            columnItem31.IsExpand = false;
            columnItem31.MyStyle = FontStyle.Regular;
            columnItem31.Name = "matched_time";
            columnItem31.Text = "S-Time";
            columnItem31.ValueFormat = FormatType.Text;
            columnItem31.Visible = true;
            columnItem31.Width = 10;
            columnItem32.Alignment = StringAlignment.Center;
            columnItem32.BackColor = Color.FromArgb(64, 64, 64);
            columnItem32.FontColor = Color.LightGray;
            columnItem32.IsExpand = false;
            columnItem32.MyStyle = FontStyle.Regular;
            columnItem32.Name = "order_no";
            columnItem32.Text = "Order No";
            columnItem32.ValueFormat = FormatType.Text;
            columnItem32.Visible = true;
            columnItem32.Width = 12;
            columnItem33.Alignment = StringAlignment.Near;
            columnItem33.BackColor = Color.FromArgb(64, 64, 64);
            columnItem33.FontColor = Color.LightGray;
            columnItem33.IsExpand = false;
            columnItem33.MyStyle = FontStyle.Regular;
            columnItem33.Name = "message";
            columnItem33.Text = "Message";
            columnItem33.ValueFormat = FormatType.Text;
            columnItem33.Visible = true;
            columnItem33.Width = 70;
            this.intzaOrder.Columns.Add(columnItem19);
            this.intzaOrder.Columns.Add(columnItem20);
            this.intzaOrder.Columns.Add(columnItem21);
            this.intzaOrder.Columns.Add(columnItem22);
            this.intzaOrder.Columns.Add(columnItem23);
            this.intzaOrder.Columns.Add(columnItem24);
            this.intzaOrder.Columns.Add(columnItem25);
            this.intzaOrder.Columns.Add(columnItem26);
            this.intzaOrder.Columns.Add(columnItem27);
            this.intzaOrder.Columns.Add(columnItem28);
            this.intzaOrder.Columns.Add(columnItem29);
            this.intzaOrder.Columns.Add(columnItem30);
            this.intzaOrder.Columns.Add(columnItem31);
            this.intzaOrder.Columns.Add(columnItem32);
            this.intzaOrder.Columns.Add(columnItem33);
            this.intzaOrder.CurrentScroll = 0;
            this.intzaOrder.Dock = DockStyle.Fill;
            this.intzaOrder.FocusItemIndex = -1;
            this.intzaOrder.ForeColor = Color.Black;
            this.intzaOrder.GridColor = Color.FromArgb(45, 45, 45);
            this.intzaOrder.HeaderPctHeight = 100f;
            this.intzaOrder.IsAutoRepaint = true;
            this.intzaOrder.IsDrawGrid = true;
            this.intzaOrder.IsDrawHeader = true;
            this.intzaOrder.IsScrollable = true;
            this.intzaOrder.Location = new Point(0, 28);
            this.intzaOrder.MainColumn = "";
            this.intzaOrder.Name = "intzaOrder";
            this.intzaOrder.Rows = 0;
            this.intzaOrder.RowSelectColor = Color.FromArgb(50, 50, 50);
            this.intzaOrder.RowSelectType = 3;
            this.intzaOrder.Size = new Size(697, 77);
            this.intzaOrder.SortColumnName = "";
            this.intzaOrder.SortType = SortType.Desc;
            this.intzaOrder.TabIndex = 116;
            this.intzaOrder.TableMouseClick += new ExpandGrid.TableMouseClickEventHandler(this.intzaOrder_TableMouseClick);
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(697, 529);
            base.Controls.Add(this.panel1);
            base.Controls.Add(this.panelTop);
            base.Name = "frmAutoTrade";
            this.Text = "frmAutoTrade";
            base.IDoShownDelay += new ClientBaseForm.OnShownDelayEventHandler(this.frmAutoTrade_IDoShownDelay);
            base.IDoLoadData += new ClientBaseForm.OnIDoLoadDataEventHandler(this.frmAutoTrade_IDoLoadData);
            base.IDoFontChanged += new ClientBaseForm.OnFontChangedEventHandler(this.frmAutoTrade_IDoFontChanged);
            base.IDoCustomSizeChanged += new ClientBaseForm.CustomSizeChangedEventHandler(this.frmAutoTrade_IDoCustomSizeChanged);
            base.IDoSymbolLinked += new ClientBaseForm.OnSymbolLinkEventHandler(this.frmAutoTrade_IDoSymbolLinked);
            base.IDoMainFormKeyUp += new ClientBaseForm.OnFormKeyUpEventHandler(this.frmAutoTrade_IDoMainFormKeyUp);
            base.IDoReActivated += new ClientBaseForm.OnReActiveEventHandler(this.frmAutoTrade_IDoReActivated);
            base.Controls.SetChildIndex(this.panelTop, 0);
            base.Controls.SetChildIndex(this.panel1, 0);
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.panelPZ.ResumeLayout(false);
            this.panelDCA.ResumeLayout(false);
            this.panType2.ResumeLayout(false);
            this.panType2.PerformLayout();
            this.panType1.ResumeLayout(false);
            this.panType1.PerformLayout();
            this.tStripMenu.ResumeLayout(false);
            this.tStripMenu.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            base.ResumeLayout(false);
            base.PerformLayout();

            AutoTradeManager.Instance.Init();
            AutoTradeManager.Instance.OnStartAutoTrade -= new AutoTradeManager.OnStartAutoTradeHandler(this.OnStartAutoTrade);
            AutoTradeManager.Instance.OnStartAutoTrade += new AutoTradeManager.OnStartAutoTradeHandler(this.OnStartAutoTrade);
            AutoTradeManager.Instance.OnEndAutoTrade -= new AutoTradeManager.OnEndAutoTradeHandler(this.OnEndAutoTrade);
            AutoTradeManager.Instance.OnEndAutoTrade += new AutoTradeManager.OnEndAutoTradeHandler(this.OnEndAutoTrade);

        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public frmAutoTrade()
        {
            this.InitializeComponent();
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public frmAutoTrade(Dictionary<string, object> propertiesValue)
            : base(propertiesValue)
        {
            try
            {
                this.InitializeComponent();
                if (this.cb1Stock.AutoCompleteCustomSource != null)
                {
                    if (this.cb1Stock.AutoCompleteCustomSource.Count == 0 && ApplicationInfo.StockAutoComp != null)
                    {
                        this.cb1Stock.AutoCompleteMode = AutoCompleteMode.Suggest;
                        this.cb1Stock.AutoCompleteSource = AutoCompleteSource.CustomSource;
                        this.cb1Stock.AutoCompleteCustomSource = ApplicationInfo.StockAutoComp;
                    }
                }
                this.tscbStatus.Items.Clear();
                this.tscbStatus.Items.Add("ALL");
                this.tscbStatus.Items.Add(AutoTradeConstant.GetSatusString("P"));
                this.tscbStatus.Items.Add(AutoTradeConstant.GetSatusString("F"));
                this.tscbStatus.Items.Add(AutoTradeConstant.GetSatusString("S"));
                this.tscbStatus.Items.Add(AutoTradeConstant.GetSatusString("X"));
                this.tscbStatus.Items.Add(AutoTradeConstant.GetSatusString("XP"));
                this.bgwSendOrder = new BackgroundWorker();
                this.bgwSendOrder.WorkerReportsProgress = true;
                this.bgwSendOrder.DoWork += new DoWorkEventHandler(this.bgwSendOrder_DoWork);
                this.bgwSendOrder.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.bgwSendOrder_RunWorkerCompleted);
                this.btnType1.Visible = ApplicationInfo.IsAutoTradeSupport(1);
                this.btnType2.Visible = ApplicationInfo.IsAutoTradeSupport(2);
                this.btnTypeMM.Visible = ApplicationInfo.IsAutoTradeSupport(16);
                this.btnTypeDCA.Visible = ApplicationInfo.IsAutoTradeSupport(4);
                this.btnTypePZ.Visible = ApplicationInfo.IsAutoTradeSupport(8);
                this.cb1Price.Text = "MP";
                this.cb2PriceLast.Text = "MP";
                this.cb2PriceSMA.Text = "MP";
                this.cb2PriceBreak.Text = "MP";
                this.cbExpire.Text = "180 Days";
                this.cb1Price.Items.Clear();
                this.cb1Price.Items.Add("");
                this.cb1Price.Items.Add("MP");
                this.cb2PriceLast.Items.Clear();
                this.cb2PriceLast.Items.Add("");
                this.cb2PriceLast.Items.Add("MP");

            }
            catch (Exception ex)
            {
                this.ShowError("frmAutoTrade", ex);
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void frmAutoTrade_IDoLoadData()
        {
            try
            {
                string holiday = ApplicationInfo.WebAlertService.GetHoliday();
                this._holidays = new List<string>();
                using (DataSet dataSet = new DataSet())
                {
                    MyDataHelper.StringToDataSet(holiday, dataSet);
                    if (dataSet != null && dataSet.Tables.Contains("TAB"))
                    {
                        foreach (DataRow dataRow in dataSet.Tables["TAB"].Rows)
                        {
                            this._holidays.Add(dataRow["sDate"].ToString());
                        }
                    }
                    dataSet.Clear();
                }
                this.SetAutoTradeType("ALG1");
            }
            catch (Exception ex)
            {
                this.ShowError("frmAutoTrade_IDoLoadData", ex);
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void frmAutoTrade_IDoShownDelay()
        {
            try
            {
                this.SetAutoTradeType("ALG1");
                this.SetValueItems(this.cb2ValueTrailingStop, this.lb2ValueTrailingStop, "sma");
                this.SetValueItems(this.cb2ValueCutloss, this.lb2ValueCutloss, "break");
                this.tbPin.Text = ApplicationInfo.UserPincodeLastEntry;
                ApplicationInfo.OnPincodeChanged -= new ApplicationInfo.OnPincodeChangedCompleteHandler(this.ApplicationInfo_OnPincodeChanged);
                ApplicationInfo.OnPincodeChanged += new ApplicationInfo.OnPincodeChangedCompleteHandler(this.ApplicationInfo_OnPincodeChanged);
                base.Show();
                base.IsLoadingData = false;
                base.OpenedForm();
            }
            catch (Exception ex)
            {
                this.ShowError("frmAutoTrade_IDoShownDelay", ex);
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void ApplicationInfo_OnPincodeChanged()
        {
            try
            {
                if (base.InvokeRequired)
                {
                    base.BeginInvoke(new MethodInvoker(this.ApplicationInfo_OnPincodeChanged));
                }
                else
                {
                    this.tbPin.Text = ApplicationInfo.UserPincodeLastEntry;
                }
            }
            catch (Exception ex)
            {
                this.ShowError("OnPincodeChanged", ex);
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void frmAutoTrade_IDoReActivated()
        {
            if (!base.IsLoadingData)
            {
                this.SetResize(this.IsWidthChanged, this.IsHeightChanged);
                base.Show();
                this.ReloadData();
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void frmAutoTrade_IDoMainFormKeyUp(KeyEventArgs e)
        {
            Keys keyCode = e.KeyCode;
            if (keyCode == Keys.Space)
            {
                this.cb1Stock.Focus();
                this.cb1Stock.SelectAll();
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void frmAutoTrade_IDoCustomSizeChanged()
        {
            if (!base.IsLoadingData)
            {
                this.SetResize(this.IsWidthChanged, this.IsHeightChanged);
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void frmAutoTrade_IDoFontChanged()
        {
            if (!base.IsLoadingData)
            {
                this.SetResize(true, true);
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void ReceiveMessage(IBroadcastMessage message, StockList.StockInformation realtimeStockInfo)
        {
            AutoTradeManager.Instance.ReceiveMessage(message, realtimeStockInfo);
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void ReceiveTfexMessage(IBroadcastMessage message, SeriesList.SeriesInformation realtimeSeriesInfo)
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void SetResize(bool isWidthChanged, bool isHeightChanged)
        {
            try
            {
                if (this._typeId == "ALGDC")
                {
                    this.panelTop.Height = base.Height;
                    this.panelDCA.SetBounds(5, this.btnBuy.Bottom + 5, this.panelTop.Width - 10, this.panelTop.Height - (this.btnTypeDCA.Bottom + 5) - 5);
                    if (this._frmDcaCreateNew != null && this._frmDcaCreateNew.Visible)
                    {
                        this._frmDcaCreateNew.Bounds = new Rectangle(0, 0, this.panelDCA.Width, this.panelDCA.Height);
                    }
                    if (this._frmDcaInfo != null && this._frmDcaInfo.Visible)
                    {
                        this._frmDcaInfo.Bounds = new Rectangle(0, 0, this.panelDCA.Width, this.panelDCA.Height);
                    }
                }
                else if (this._typeId == "ALG4")
                {
                    this.panelTop.Height = base.Height;
                    this.panelPZ.SetBounds(5, this.btnTypePZ.Bottom + 5, this.panelTop.Width - 10, this.panelTop.Bottom - (this.btnTypePZ.Bottom + 5) - 5);
                    if (this._frmPzCreateNew != null && this._frmPzCreateNew.Visible)
                    {
                        this._frmPzCreateNew.Bounds = new Rectangle(0, 0, this.panelPZ.Width, this.panelPZ.Height);
                    }
                    if (this._frmPzInfo != null && this._frmPzInfo.Visible)
                    {
                        this._frmPzInfo.Bounds = new Rectangle(0, 0, this.panelPZ.Width, this.panelPZ.Height);
                    }
                }
                else if (this._typeId == "ALG1")
                {
                    this.panType1.SetBounds(5, this.btnBuy.Bottom + 5, this.panelTop.Width - 10, this.cb1Condition.Bottom + 10);
                    this.panelTop.Height = this.panType1.Bottom + 50;
                    this.lb1Stock.Left = this.btnSell.Right + 10;
                    this.cb1Stock.Left = this.lb1Stock.Right + 5;
                }
                else if (this._typeId == "ALG2")
                {
                    this.panType2.SetBounds(5, this.btnBuy.Bottom + 5, this.panelTop.Width - 10, this.cb2OperCutloss.Bottom + 10);
                    this.panelTop.Height = this.panType2.Bottom + 50;
                    this.lb1Stock.Left = 10;
                    this.cb1Stock.Left = this.lb1Stock.Right + 5;
                }
                else if (this._typeId == "ALGMM")
                {
                    this.panelTop.Height = this.btnSell.Bottom + 10;
                }
                if (this._typeId == "ALG1" || this._typeId == "ALG2")
                {
                    this.btnClear.Location = new Point(this.panelTop.Width - this.btnClear.Width - 10, this.panelTop.Bottom - this.btnClear.Height - 10);
                    this.btnSendOrder.Location = new Point(this.btnClear.Left - this.btnSendOrder.Width - 10, this.btnClear.Top);
                    this.btnSendLocalOrder.Location = new Point(this.btnClear.Left - this.btnSendLocalOrder.Width - 10, this.btnClear.Top);
                    this.tbPin.Location = new Point(this.btnSendOrder.Left - this.tbPin.Width - 15, this.btnClear.Top + 2);
                    this.lbPin.Location = new Point(this.tbPin.Left - this.lbPin.Width - 5, this.btnClear.Top + 4);
                    this.cbExpire.Location = new Point(this.lbPin.Left - this.cbExpire.Width - 10, this.btnClear.Top + 1);
                    this.lbExpire.Location = new Point(this.cbExpire.Left - this.lbExpire.Width - 5, this.btnClear.Top + 4);
                }
                int num = this.panelTop.Width;
                if (ApplicationInfo.IsAutoTradeSupport(8))
                {
                    this.btnTypePZ.Location = new Point(this.panelTop.Width - this.btnTypePZ.Width - 5, 5);
                    num = this.btnTypePZ.Left;
                }
                if (ApplicationInfo.IsAutoTradeSupport(4))
                {
                    this.btnTypeDCA.Location = new Point(num - this.btnTypeDCA.Width - 5, 5);
                    num = this.btnTypeDCA.Left;
                }
                if (ApplicationInfo.IsAutoTradeSupport(16))
                {
                    this.btnTypeMM.Location = new Point(num - this.btnTypeMM.Width - 5, 5);
                    num = this.btnTypeMM.Left;
                }
                if (ApplicationInfo.IsAutoTradeSupport(2))
                {
                    this.btnType2.Location = new Point(num - this.btnType2.Width - 5, 5);
                    num = this.btnType2.Left;
                }
                if (ApplicationInfo.IsAutoTradeSupport(1))
                {
                    this.btnType1.Location = new Point(num - this.btnType1.Width - 5, 5);
                    num = this.btnType1.Left;
                }
                this.lbPattern.Location = new Point(num - this.lbPattern.Width - 5, this.lb1Stock.Top);
            }
            catch
            {
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void ReloadData()
        {
            try
            {
                if (!this._isReloadData)
                {
                    base.IsLoadingData = true;
                    this._isReloadData = true;
                    if (this._typeId == "ALG1")
                    {
                        UpdateLocalStopOrderToGrid(true);
                    }
                    else
                    {

                        ApplicationInfo.WebAlertService.ViewAutoTradeTransCompleted -= new ViewAutoTradeTransCompletedEventHandler(this.WebAlertService_ViewAutoTradeTransCompleted);
                        ApplicationInfo.WebAlertService.ViewAutoTradeTransCompleted += new ViewAutoTradeTransCompletedEventHandler(this.WebAlertService_ViewAutoTradeTransCompleted);
                        ApplicationInfo.WebAlertService.ViewAutoTradeTransAsync(ApplicationInfo.UserLoginID, ApplicationInfo.AccInfo.CurrentAccount, this._typeId, this.tstbStock.Text, (this.tscbSide.Text.ToLower() == "all") ? string.Empty : this.tscbSide.Text, AutoTradeConstant.GetStatusByName(this.tscbStatus.Text, this._typeId));
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowError("RequestWeb", ex);
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void WebAlertService_ViewAutoTradeTransCompleted(object sender, ViewAutoTradeTransCompletedEventArgs e)
        {
            try
            {
                ApplicationInfo.WebAlertService.ViewAutoTradeTransCompleted -= new ViewAutoTradeTransCompletedEventHandler(this.WebAlertService_ViewAutoTradeTransCompleted);
                if (e.Error == null)
                {
                    using (DataSet dataSet = new DataSet())
                    {
                        MyDataHelper.StringToDataSet(e.Result.ToString(), dataSet);
                        if (this._typeId == "ALG1" || this._typeId == "ALG2" || this._typeId == "ALGMM")
                        {
                            if (dataSet.Tables.Contains("AUTO"))
                            {
                                this.intzaOrder.SortColumnName = string.Empty;
                                this.intzaOrder.BeginUpdate();
                                if (this._typeId == "ALG1" || this._typeId == "ALGMM")
                                {
                                    this.intzaOrder.Rows = dataSet.Tables["AUTO"].Rows.Count;
                                    int num = 0;
                                    foreach (DataRow dataRow in dataSet.Tables["AUTO"].Rows)
                                    {
                                        this.UpdateStopOrderToGrid(dataRow, num, false);
                                        num++;
                                    }
                                }
                                else if (this._typeId == "ALG2")
                                {
                                    this.intzaOrder.Rows = 0;
                                    long num2 = 0L;
                                    long num3 = 0L;
                                    List<DataRow> list = new List<DataRow>();
                                    foreach (DataRow dataRow in dataSet.Tables["AUTO"].Rows)
                                    {
                                        long.TryParse(dataRow["ref_no"].ToString(), out num2);
                                        if (num3 == 0L)
                                        {
                                            num3 = num2;
                                        }
                                        if (num3 != num2)
                                        {
                                            this.UpdateStopOrderToGrid2(list, false);
                                            list.Clear();
                                            num3 = num2;
                                        }
                                        list.Add(dataRow);
                                    }
                                    if (list.Count > 0)
                                    {
                                        this.UpdateStopOrderToGrid2(list, false);
                                    }
                                }
                                this.intzaOrder.Redraw();
                                if (this.intzaOrder.Rows > 0)
                                {
                                    this.intzaOrder.Focus();
                                    this.intzaOrder.SetFocusItem(0);
                                }
                            }
                        }
                        else if (this._typeId == "ALGDC")
                        {
                            if (dataSet.Tables.Contains("AUTO"))
                            {
                                this.gridDcaMain.SortColumnName = string.Empty;
                                this.gridDcaMain.BeginUpdate();
                                this.gridDcaMain.Rows = dataSet.Tables["AUTO"].Rows.Count;
                                int num = 0;
                                foreach (DataRow dataRow in dataSet.Tables["AUTO"].Rows)
                                {
                                    RecordItem recordItem = this.gridDcaMain.Records(num);
                                    recordItem.Fields("refno").Text = dataRow["iRefNo"].ToString();
                                    recordItem.Fields("stock").Text = dataRow["sSymbol"].ToString();
                                    recordItem.Fields("budget").Text = dataRow["nmrBudget"].ToString();
                                    DateTime dateTime = DateTime.ParseExact(dataRow["sStartDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture);
                                    recordItem.Fields("startdate").Text = dateTime.ToString("dd MMM yy");
                                    dateTime = DateTime.ParseExact(dataRow["sEndDate"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture);
                                    recordItem.Fields("enddate").Text = dateTime.ToString("dd MMM yy");
                                    recordItem.Fields("matvol").Text = dataRow["iTotMat"].ToString();
                                    string text = dataRow["sPeriod"].ToString();
                                    if (text != null)
                                    {
                                        if (!(text == "D"))
                                        {
                                            if (!(text == "W"))
                                            {
                                                if (text == "M")
                                                {
                                                    recordItem.Fields("period").Text = "Every Month";
                                                }
                                            }
                                            else
                                            {
                                                recordItem.Fields("period").Text = "Every Week";
                                            }
                                        }
                                        else
                                        {
                                            recordItem.Fields("period").Text = "Every Day";
                                        }
                                    }
                                    recordItem.Fields("period").FontColor = Color.LightGray;
                                    recordItem.Fields("status").Text = AutoTradeConstant.GetSatusString(dataRow["sStatus"].ToString());
                                    recordItem.Fields("refno").FontColor = Color.LightGray;
                                    recordItem.Fields("stock").FontColor = Color.LightGray;
                                    recordItem.Fields("budget").FontColor = Color.Yellow;
                                    recordItem.Fields("startdate").FontColor = Color.LightGray;
                                    recordItem.Fields("enddate").FontColor = Color.LightGray;
                                    recordItem.Fields("status").FontColor = Color.Cyan;
                                    recordItem.Fields("matvol").FontColor = Color.Yellow;
                                    num++;
                                }
                                this.gridDcaMain.Redraw();
                                if (this.gridDcaMain.Rows > 0)
                                {
                                    this.gridDcaMain.Focus();
                                    this.gridDcaMain.SetFocusItem(0);
                                }
                            }
                        }
                        else if (this._typeId == "ALG4")
                        {
                            if (dataSet.Tables.Contains("AUTO"))
                            {
                                this.gridPzMain.SortColumnName = string.Empty;
                                this.gridPzMain.BeginUpdate();
                                this.gridPzMain.Rows = dataSet.Tables["AUTO"].Rows.Count;
                                int num = 0;
                                foreach (DataRow dataRow in dataSet.Tables["AUTO"].Rows)
                                {
                                    RecordItem recordItem = this.gridPzMain.Records(num);
                                    recordItem.Fields("refno").Text = dataRow["iRefNo"].ToString();
                                    recordItem.Fields("stock").Text = dataRow["sSymbol"].ToString();
                                    recordItem.Fields("budget").Text = dataRow["nmrBudget"].ToString();
                                    recordItem.Fields("start_price").Text = dataRow["nmrStartPrice"].ToString();
                                    recordItem.Fields("depth_pct").Text = dataRow["nmrChgPct"].ToString();
                                    recordItem.Fields("no_steps").Text = dataRow["iSegment"].ToString();
                                    long num4;
                                    long.TryParse(dataRow["iTotMatVol"].ToString(), out num4);
                                    decimal num5;
                                    decimal.TryParse(dataRow["iTotMatVal"].ToString(), out num5);
                                    recordItem.Fields("matvol").Text = num4;
                                    recordItem.Fields("matval").Text = num5;
                                    decimal num6 = 0m;
                                    if (num4 > 0L)
                                    {
                                        num6 = num5 / num4;
                                    }
                                    recordItem.Fields("avg").Text = num6;
                                    recordItem.Fields("status").Text = AutoTradeConstant.GetSatusString(dataRow["sStatus"].ToString());
                                    recordItem.Fields("refno").FontColor = Color.LightGray;
                                    recordItem.Fields("stock").FontColor = Color.LightGray;
                                    recordItem.Fields("budget").FontColor = MyColor.UnChgColor;
                                    recordItem.Fields("start_price").FontColor = MyColor.UnChgColor;
                                    recordItem.Fields("depth_pct").FontColor = Color.LightGray;
                                    recordItem.Fields("no_steps").FontColor = Color.LightGray;
                                    recordItem.Fields("status").FontColor = Color.LightGray;
                                    recordItem.Fields("matvol").FontColor = Color.Cyan;
                                    recordItem.Fields("matval").FontColor = Color.Cyan;
                                    recordItem.Fields("avg").FontColor = Color.Cyan;
                                    num++;
                                }
                                this.gridPzMain.Redraw();
                                if (this.gridDcaMain.Rows > 0)
                                {
                                    this.gridPzMain.Focus();
                                    this.gridPzMain.SetFocusItem(0);
                                }
                            }
                        }
                        dataSet.Clear();
                    }
                }
            }
            catch (Exception ex)
            {
                this.intzaOrder.Redraw();
                this.ShowError("ViewAutoTradeTransCompleted", ex);
            }
            this._isReloadData = false;
            base.IsLoadingData = false;
        }


        [MethodImpl(MethodImplOptions.NoInlining)]
        public void UpdateStopOrderToGrid(DataRow dr, int i, bool isRedraw)
        {
            try
            {
                long num = 0L;
                long num2 = 0L;
                string text = string.Empty;
                string empty = string.Empty;
                string text2 = string.Empty;
                long.TryParse(dr["ref_no"].ToString(), out num2);
                text = dr["ord_side"].ToString();
                long.TryParse(dr["ord_volume"].ToString(), out num);
                text2 = dr["status"].ToString().Trim();
                int operType = 0;
                int.TryParse(dr["operator_type"].ToString(), out operType);
                decimal checkPrice = 0m;
                decimal.TryParse(dr["check_price"].ToString(), out checkPrice);
                decimal value = 0m;
                decimal.TryParse(dr["value"].ToString(), out value);
                RecordItem recordItem;
                if (i == -1)
                {
                    recordItem = this.intzaOrder.Find("ref_no", num2.ToString());
                    if (recordItem == null)
                    {
                        recordItem = this.intzaOrder.AddRecord(1, false);
                    }
                }
                else
                {
                    recordItem = this.intzaOrder.Records(i);
                }
                StockList.StockInformation stockInformation = ApplicationInfo.StockInfo[dr["stock"].ToString().Trim()];
                recordItem.Fields("ref_no").Text = num2;
                recordItem.Fields("side").Text = text;
                recordItem.Fields("stock").Text = stockInformation.Symbol;
                recordItem.Fields("volume").Text = num;
                if (dr["expire_date"].ToString() != string.Empty)
                {
                    DateTime dateTime = DateTime.ParseExact(dr["expire_date"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture);
                    if (dateTime.ToString("yyyyMMdd") == DateTime.Now.ToString("yyyyMMdd"))
                    {
                        recordItem.Fields("limit").Text = "End Day";
                    }
                    else
                    {
                        recordItem.Fields("limit").Text = dateTime.ToString("dd MMM yy");
                    }
                }
                recordItem.Fields("status").Text = AutoTradeConstant.GetSatusString(text2);
                recordItem.Fields("price").Text = Utilities.PriceFormat(dr["ord_price"].ToString());
                if (dr["time"].ToString().Length == 6)
                {
                    recordItem.Fields("sent_time").Text = Utilities.GetTime(dr["time"].ToString());
                }
                else
                {
                    DateTime dateTime = DateTime.ParseExact(dr["time"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture);
                    recordItem.Fields("sent_time").Text = dateTime.ToString("dd MMM yy");
                }
                recordItem.Fields("matched_time").Text = Utilities.GetTime(dr["mtime"].ToString());
                recordItem.Fields("condition").Text = this.GetConditionString(dr["field_type"].ToString().Trim(), operType, value, checkPrice);
                recordItem.Fields("order_no").Text = ((dr["order_number"].ToString() == "0") ? "" : dr["order_number"].ToString());
                recordItem.Fields("mm_src_ordno").Text = dr["mm_src_ordno"].ToString();
                recordItem.Fields("message").Text = dr["message"].ToString().Trim();
                recordItem.Fields("ref_no").FontColor = Color.White;
                if (text == "B")
                {
                    recordItem.Fields("side").FontColor = Color.Lime;
                }
                else if (text == "S")
                {
                    recordItem.Fields("side").FontColor = Color.Red;
                }
                else if (text == "C")
                {
                    recordItem.Fields("side").FontColor = Color.Cyan;
                }
                else if (text == "H")
                {
                    recordItem.Fields("side").FontColor = Color.Pink;
                }
                else
                {
                    recordItem.Fields("side").FontColor = MyColor.UnChgColor;
                }
                recordItem.Fields("stock").FontColor = Color.LightGray;
                recordItem.Fields("volume").FontColor = Color.LightGray;
                recordItem.Fields("price").FontColor = Color.LightGray;
                recordItem.Fields("limit").FontColor = Color.LightGray;
                recordItem.Fields("sent_time").FontColor = Color.LightGray;
                recordItem.Fields("matched_time").FontColor = Color.LightGray;
                recordItem.Fields("status").FontColor = Color.Cyan;
                recordItem.Fields("order_no").FontColor = MyColor.UnChgColor;
                recordItem.Fields("mm_src_ordno").FontColor = MyColor.UnChgColor;
                recordItem.Fields("message").FontColor = Color.Red;
                recordItem.Fields("condition").FontColor = MyColor.UnChgColor;
                if (text2 == "W" || text2 == "P" || text2 == "PM")
                {
                    recordItem.Fields("checkbox").Text = "0";
                }
                else
                {
                    recordItem.Fields("checkbox").Text = "";
                }
                recordItem.Changed = true;
                if (isRedraw)
                {
                    this.intzaOrder.Redraw();
                }
            }
            catch (Exception ex)
            {
                this.intzaOrder.Redraw();
                this.ShowError("UpdateToControl", ex);
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void UpdateStopOrderToGrid2(List<DataRow> arDr, bool isRedraw)
        {
            try
            {
                long num = 0L;
                long num2 = 0L;
                string text = string.Empty;
                string empty = string.Empty;
                string text2 = string.Empty;
                RecordItem recordItem = this.intzaOrder.Find("ref_no", num2.ToString());
                if (recordItem == null)
                {
                    recordItem = this.intzaOrder.AddRecord(-1, false);
                    this.intzaOrder.ExpandRows(this.intzaOrder.Rows - 1, arDr.Count, "", false, false);
                }
                int num3 = 1;
                bool flag = false;
                foreach (DataRow current in arDr)
                {
                    long.TryParse(current["ref_no"].ToString(), out num2);
                    text = current["ord_side"].ToString();
                    long.TryParse(current["ord_volume"].ToString(), out num);
                    text2 = current["status"].ToString().Trim();
                    int operType = 0;
                    int.TryParse(current["operator_type"].ToString(), out operType);
                    decimal checkPrice = 0m;
                    decimal.TryParse(current["check_price"].ToString(), out checkPrice);
                    decimal value = 0m;
                    decimal.TryParse(current["value"].ToString(), out value);
                    if (num3 == 1)
                    {
                        StockList.StockInformation stockInformation = ApplicationInfo.StockInfo[current["stock"].ToString().Trim()];
                        recordItem.Fields("ref_no").Text = num2;
                        recordItem.Fields("side").Text = text;
                        recordItem.Fields("stock").Text = stockInformation.Symbol;
                        recordItem.Fields("volume").Text = num;
                        if (current["expire_date"].ToString() != string.Empty)
                        {
                            DateTime dateTime = DateTime.ParseExact(current["expire_date"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture);
                            if (dateTime.ToString("yyyyMMdd") == DateTime.Now.ToString("yyyyMMdd"))
                            {
                                recordItem.Fields("limit").Text = "ToDay";
                            }
                            else
                            {
                                recordItem.Fields("limit").Text = dateTime.ToString("dd MMM yy");
                            }
                        }
                        recordItem.Fields("group_cancel").Text = current["group_cancel"].ToString();
                        recordItem.Fields("status").Text = AutoTradeConstant.GetSatusString(text2);
                        recordItem.Fields("price").Text = Utilities.PriceFormat(current["ord_price"].ToString());
                        if (current["time"].ToString().Length == 6)
                        {
                            recordItem.Fields("sent_time").Text = Utilities.GetTime(current["time"].ToString());
                        }
                        else
                        {
                            DateTime dateTime = DateTime.ParseExact(current["time"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture);
                            recordItem.Fields("sent_time").Text = dateTime.ToString("dd MMM yy");
                        }
                        if (current["mtime"].ToString().Length == 6)
                        {
                            recordItem.Fields("matched_time").Text = Utilities.GetTime(current["mtime"].ToString());
                        }
                        else if (current["mtime"].ToString().Length == 8)
                        {
                            DateTime dateTime = DateTime.ParseExact(current["mtime"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture);
                            recordItem.Fields("matched_time").Text = dateTime.ToString("dd MMM yy");
                        }
                        recordItem.Fields("condition").Text = this.GetConditionString(current["field_type"].ToString().Trim(), operType, value, checkPrice);
                        recordItem.Fields("order_no").Text = ((current["order_number"].ToString() == "0") ? "" : current["order_number"].ToString());
                        recordItem.Fields("message").Text = current["message"].ToString().Trim();
                        recordItem.Fields("ref_no").FontColor = Color.White;
                        if (text == "B")
                        {
                            recordItem.Fields("side").FontColor = Color.Lime;
                        }
                        else if (text == "S")
                        {
                            recordItem.Fields("side").FontColor = Color.Red;
                        }
                        else if (text == "C")
                        {
                            recordItem.Fields("side").FontColor = Color.Cyan;
                        }
                        else if (text == "H")
                        {
                            recordItem.Fields("side").FontColor = Color.Pink;
                        }
                        else
                        {
                            recordItem.Fields("side").FontColor = Color.Yellow;
                        }
                        recordItem.Fields("stock").FontColor = Color.LightGray;
                        recordItem.Fields("volume").FontColor = Color.LightGray;
                        recordItem.Fields("price").FontColor = Color.LightGray;
                        recordItem.Fields("limit").FontColor = Color.LightGray;
                        recordItem.Fields("sent_time").FontColor = Color.LightGray;
                        recordItem.Fields("matched_time").FontColor = Color.LightGray;
                        recordItem.Fields("status").FontColor = Color.Cyan;
                        recordItem.Fields("order_no").FontColor = Color.Yellow;
                        recordItem.Fields("message").FontColor = Color.Red;
                        recordItem.Fields("condition").FontColor = MyColor.UnChgColor;
                        recordItem.Changed = true;
                    }
                    else
                    {
                        SubRecordItem subRecordItem = this.intzaOrder.Records(this.intzaOrder.Rows - 1).SubRecord[num3 - 2];
                        subRecordItem.Fields("price").Text = Utilities.PriceFormat(current["ord_price"].ToString());
                        subRecordItem.Fields("status").Text = AutoTradeConstant.GetSatusString(text2);
                        if (current["mtime"].ToString().Length == 6)
                        {
                            subRecordItem.Fields("matched_time").Text = Utilities.GetTime(current["mtime"].ToString());
                        }
                        else if (current["mtime"].ToString().Length == 8)
                        {
                            DateTime dateTime = DateTime.ParseExact(current["mtime"].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture);
                            subRecordItem.Fields("matched_time").Text = dateTime.ToString("dd MMM yy");
                        }
                        subRecordItem.Fields("condition").Text = this.GetConditionString(current["field_type"].ToString().Trim(), operType, value, checkPrice);
                        subRecordItem.Fields("order_no").Text = ((current["order_number"].ToString() == "0") ? "" : current["order_number"].ToString());
                        subRecordItem.Fields("message").Text = current["message"].ToString().Trim();
                        subRecordItem.Fields("sent_time").FontColor = Color.LightGray;
                        subRecordItem.Fields("matched_time").FontColor = Color.LightGray;
                        subRecordItem.Fields("status").FontColor = Color.Cyan;
                        subRecordItem.Fields("order_no").FontColor = Color.Yellow;
                        subRecordItem.Fields("message").FontColor = Color.Red;
                        subRecordItem.Fields("condition").FontColor = MyColor.UnChgColor;
                    }
                    if (text2 == "W" || text2 == "P")
                    {
                        flag = true;
                    }
                    num3++;
                }
                if (flag)
                {
                    recordItem.Fields("checkbox").Text = "0";
                }
                else
                {
                    recordItem.Fields("checkbox").Text = "";
                }
                if (isRedraw)
                {
                    this.intzaOrder.Redraw();
                }
            }
            catch (Exception ex)
            {
                this.intzaOrder.Redraw();
                this.ShowError("UpdateToControl", ex);
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private string GetConditionString(string fieldType, int operType, decimal value, decimal checkPrice)
        {
            string text = "  ";
            if (fieldType == "LAST" || fieldType == "SMA" || fieldType == "HHV" || fieldType == "LLV")
            {
                text += "Last";
            }
            else
            {
                text += "Unknow";
            }
            if (operType == 1)
            {
                text += " >= ";
            }
            else if (operType == 2)
            {
                text += " <= ";
            }
            else if (operType == 3)
            {
                text += " > ";
            }
            else if (operType == 4)
            {
                text += " < ";
            }
            if (fieldType == "SMA" || fieldType == "HHV" || fieldType == "LLV")
            {
                if (fieldType == "SMA")
                {
                    text += "SMA ";
                }
                else if (fieldType == "HHV")
                {
                    text += "Break High ";
                }
                else if (fieldType == "LLV")
                {
                    text += "Break Low ";
                }
                text += Utilities.PriceFormat(value);
                if (checkPrice > 0m)
                {
                    text = text + " (@" + Utilities.PriceFormat(checkPrice, 4) + " )";
                }
                else
                {
                    text += " (...)";
                }
            }
            else
            {
                text += Utilities.PriceFormat(checkPrice);
            }
            return text;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void controlOrder_Enter(object sender, EventArgs e)
        {
            try
            {
                ((Control)sender).BackColor = MyColor.UnChgColor;
                ((Control)sender).ForeColor = Color.Black;
                if (sender.GetType() == typeof(TextBox))
                {
                    ((TextBox)sender).SelectAll();
                }
                if (sender.Equals(this.cb1Price))
                {
                    if (this.tbPin.Text == string.Empty && ApplicationInfo.UserPincodeLastEntry != string.Empty)
                    {
                        this.tbPin.Text = ApplicationInfo.UserPincodeLastEntry;
                    }
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
                if (sender.GetType() == typeof(CheckBox))
                {
                    ((Control)sender).BackColor = Color.Transparent;
                    ((Control)sender).ForeColor = Color.LightGray;
                }
                else if (sender.GetType() == typeof(ComboBox))
                {
                    ((Control)sender).BackColor = Color.FromArgb(224, 224, 224);
                    ((Control)sender).ForeColor = Color.Black;
                }
                else
                {
                    ((Control)sender).BackColor = Color.FromArgb(224, 224, 224);
                    ((Control)sender).ForeColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
                this.ShowError("controlOrder_Leave", ex);
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void btnType1_Click(object sender, EventArgs e)
        {
            this.SetAutoTradeType("ALG1");
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void btnType2_Click(object sender, EventArgs e)
        {
            this.SetAutoTradeType("ALG2");
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void btnType3_Click(object sender, EventArgs e)
        {
            this.SetAutoTradeType("ALGDC");
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void btnType4_Click(object sender, EventArgs e)
        {
            this.SetAutoTradeType("ALG4");
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void btnTypeMM_Click(object sender, EventArgs e)
        {
            this.SetAutoTradeType("ALGMM");
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void btnTypePZ_Click(object sender, EventArgs e)
        {
            this.SetAutoTradeType("ALG4");
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void SetAutoTradeType(string newTypeId)
        {
            try
            {
                this._typeId = newTypeId;
                this.btnType1.BackColor = Color.Transparent;
                this.btnType2.BackColor = Color.Transparent;
                this.btnTypeDCA.BackColor = Color.Transparent;
                this.btnTypeMM.BackColor = Color.Transparent;
                this.btnTypePZ.BackColor = Color.Transparent;
                this.panType1.Visible = (this._typeId == "ALG1");
                this.panType2.Visible = (this._typeId == "ALG2");
                this.panelDCA.Visible = (this._typeId == "ALGDC");
                this.panelPZ.Visible = (this._typeId == "ALG4");
                this.lbExpire.Hide();
                this.cbExpire.Hide();
                this.tbPin.Hide();
                this.lbPin.Hide();
                this.btnClear.Hide();
                this.btnSendOrder.Hide();
                this.btnSendLocalOrder.Hide();
                this.btnBuy.Hide();
                this.btnSell.Hide();
                this.lb1Stock.Hide();
                this.cb1Stock.Hide();
                if (this._typeId == "ALG1")
                {
                    this.btnType1.BackColor = Color.DodgerBlue;
                    this.intzaOrder.GetColumn("mm_src_ordno").Visible = false;
                    this.panel1.Show();
                    this.btnBuy.Show();
                    this.btnSell.Show();
                    this.lb1Stock.Show();
                    this.cb1Stock.Show();
                    this.btnClear.Show();
                    this.btnSendLocalOrder.Show();
                    this.tbPin.Show();
                    this.lbPin.Show();
                    this.lbExpire.Show();
                    this.cbExpire.Show();
                    this.SetSide(string.Empty);
                }
                else if (this._typeId == "ALG2")
                {
                    this._ordSide = "S";
                    this.btnType2.BackColor = Color.DodgerBlue;
                    this.panType2.BackColor = Color.DarkRed;
                    this.intzaOrder.GetColumn("mm_src_ordno").Visible = false;
                    this.panel1.Show();
                    this.btnBuy.Hide();
                    this.btnSell.Hide();
                    this.lb1Stock.Show();
                    this.cb1Stock.Show();
                    this.btnClear.Show();
                    this.btnSendOrder.Show();
                    this.tbPin.Show();
                    this.lbPin.Show();
                    this.lbExpire.Show();
                    this.cbExpire.Show();
                }
                else if (this._typeId == "ALGDC")
                {
                    this.btnTypeDCA.BackColor = Color.DodgerBlue;
                    this.panel1.Hide();
                }
                else if (this._typeId == "ALG4")
                {
                    this.btnTypePZ.BackColor = Color.DodgerBlue;
                    this.panel1.Hide();
                }
                else if (this._typeId == "ALGMM")
                {
                    this.btnTypeMM.BackColor = Color.DodgerBlue;
                    this.intzaOrder.GetColumn("mm_src_ordno").Visible = true;
                    this.panel1.Show();
                }
                this.SetResize(true, true);
                this.ReloadData();
            }
            catch (Exception ex)
            {
                this.ShowError("SetType", ex);
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void btnSendOrder_Click(object sender, EventArgs e)
        {
            try
            {
                if (this._typeId == "ALG1")
                {

                    AutoTradeT1Command autoTradeT1Command = new AutoTradeT1Command();
                    autoTradeT1Command.UserId = ApplicationInfo.UserLoginID;
                    if (this.cbExpire.Text.ToLower().IndexOf("end") > -1)
                    {
                        autoTradeT1Command.ExpireDays = 1;
                    }
                    else if (this.cbExpire.Text.ToLower().IndexOf("30") > -1)
                    {
                        autoTradeT1Command.ExpireDays = 30;
                    }
                    else if (this.cbExpire.Text.ToLower().IndexOf("60") > -1)
                    {
                        autoTradeT1Command.ExpireDays = 60;
                    }
                    else if (this.cbExpire.Text.ToLower().IndexOf("90") > -1)
                    {
                        autoTradeT1Command.ExpireDays = 90;
                    }
                    else if (this.cbExpire.Text.ToLower().IndexOf("180") > -1)
                    {
                        autoTradeT1Command.ExpireDays = 180;
                    }
                    string text = string.Empty;
                    if (this.cb1Condition.Text.ToLower().IndexOf("sma") > 0)
                    {
                        text = "SMA";
                    }
                    else if (this.cb1Condition.Text.ToLower().IndexOf("break high") > 0)
                    {
                        text = "HHV";
                    }
                    else if (this.cb1Condition.Text.ToLower().IndexOf("break low") > 0)
                    {
                        text = "LLV";
                    }
                    else if (this.cb1Condition.Text.ToLower().IndexOf("last") > -1)
                    {
                        text = "LAST";
                    }
                    string text2 = this.cb1Stock.Text.Trim();
                    string text3 = this.cb1Price.Text.Trim();
                    long num = 0L;
                    long.TryParse(this.tb1Volume.Text.Replace(",", ""), out num);
                    int num2 = 0;
                    if (this.cb1Condition.Text.IndexOf(">=") > 0)
                    {
                        num2 = 1;
                    }
                    else if (this.cb1Condition.Text.IndexOf("<=") > 0)
                    {
                        num2 = 2;
                    }
                    else if (this.cb1Condition.Text.IndexOf(">") > 0)
                    {
                        num2 = 3;
                    }
                    else if (this.cb1Condition.Text.IndexOf("<") > 0)
                    {
                        num2 = 4;
                    }
                    decimal num3 = 0m;
                    decimal.TryParse(this.cb1Value.Text, out num3);
                    if (this._ordSide == string.Empty)
                    {
                        this.ShowMessageBox("Invalid order side", frmOrderFormConfirm.OpenStyle.Error, null);
                    }
                    else if (text2 == string.Empty)
                    {
                        this.ShowMessageBox("Invalid order symbol", frmOrderFormConfirm.OpenStyle.Error, this.cb1Stock);
                    }
                    else
                    {
                        StockList.StockInformation stockInformation = ApplicationInfo.StockInfo[text2];
                        if (stockInformation.Number <= 0)
                        {
                            this.ShowMessageBox("Invalid order symbol", frmOrderFormConfirm.OpenStyle.Error, this.cb1Stock);
                        }
                        else if (text == string.Empty)
                        {
                            this.ShowMessageBox("Invalid order condition", frmOrderFormConfirm.OpenStyle.Error, this.cb1Condition);
                        }
                        else if (num3 <= 0m)
                        {
                            this.ShowMessageBox("Invalid price condition", frmOrderFormConfirm.OpenStyle.Error, this.cb1Value);
                        }
                        else
                        {
                            decimal checkPrice;
                            if (text == "SMA" || text == "HHV" || text == "LLV")
                            {
                                if (!this.cb1Value.Items.Contains(num3.ToString()))
                                {
                                    this.ShowMessageBox("Invalid period condition", frmOrderFormConfirm.OpenStyle.Error, this.cb1Value);
                                    return;
                                }
                                checkPrice = -1m;
                            }
                            else
                            {
                                if (!this.IsValidPrice(this.cb1Value.Text, true, this.cb1Value))
                                {
                                    return;
                                }
                                checkPrice = num3;
                            }
                            string text4 = text3;
                            if (text4 != null)
                            {
                                if (text4 == "MP")
                                {
                                    goto IL_529;
                                }
                            }
                            if (!this.IsValidPrice(text3, true, this.cb1Price))
                            {
                                return;
                            }
                        IL_529:
                            if (num <= 0L)
                            {
                                this.ShowMessageBox("Invalid Volume [More than Zero]!", frmOrderFormConfirm.OpenStyle.Error, this.tb1Volume);
                            }
                            else if (autoTradeT1Command.ExpireDays < 1)
                            {
                                this.ShowMessageBox("Invalid Expire!", frmOrderFormConfirm.OpenStyle.Error, this.cbExpire);
                            }
                            else
                            {
                                autoTradeT1Command.Item = new AutoTradeItem(0L, 0, "ALG1", ApplicationInfo.UserLoginID, text2, text, num2, num3, checkPrice, ApplicationInfo.AccInfo.CurrentAccount, this._ordSide, num, text3, string.Empty, ApplicationInfo.AccInfo.Items[ApplicationInfo.AccInfo.CurrentAccount].PcFlag, "N");
                                this._commandMain = new AutoTradeMain();
                                this._commandMain.Pack("T1", autoTradeT1Command);
                                string text5 = "Last " + AutoTradeConstant.GetOperatorSymbol(num2) + " ";
                                if (text == "SMA")
                                {
                                    object obj = text5;
                                    text5 = string.Concat(new object[]
									    {
										    obj,
										    "SMA (",
										    num3,
										    ")"
									    });
                                }
                                else if (text == "HHV")
                                {
                                    object obj = text5;
                                    text5 = string.Concat(new object[]
									    {
										    obj,
										    "Break High (",
										    num3,
										    ")"
									    });
                                }
                                else if (text == "LLV")
                                {
                                    object obj = text5;
                                    text5 = string.Concat(new object[]
									    {
										    obj,
										    "Break Low (",
										    num3,
										    ")"
									    });
                                }
                                else
                                {
                                    text5 += num3;
                                }
                                string orderParam = string.Concat(new string[]
								    {
									    "Auto Trade :",
									    " Account : ",
									    ApplicationInfo.AccInfo.CurrentAccount,
									    "\n",
									    Utilities.GetOrderSideName(this._ordSide),
									    "  ‘",
									    text2,
									    "’",
									    "  Volume ",
									    FormatUtil.VolumeFormat(num, true),
									    "  Price ",
									    text3,
									    "\nCondition : ",
									    text5
								    });
                                this.ShowOrderFormConfirm("Confirm to send?", orderParam, frmOrderFormConfirm.OpenStyle.ConfirmSendNew);
                            }
                        }
                    }
                }
                else if (this._typeId == "ALG2")
                {
                    string text2 = this.cb1Stock.Text.Trim();
                    string isGroupRemove = this.chbGroupCancel.Checked ? "Y" : "N";
                    long num;
                    long.TryParse(this.tb2Volume.Text.Trim().Replace(",", ""), out num);
                    List<AutoTradeItem> list = new List<AutoTradeItem>();
                    AutoTradeT2Command autoTradeT2Command = new AutoTradeT2Command();
                    autoTradeT2Command.UserId = ApplicationInfo.UserLoginID;
                    autoTradeT2Command.Stock = text2;
                    autoTradeT2Command.Side = this._ordSide;
                    if (this.cbExpire.Text.ToLower().IndexOf("end") > -1)
                    {
                        autoTradeT2Command.ExpireDays = 1;
                    }
                    else if (this.cbExpire.Text.ToLower().IndexOf("30") > -1)
                    {
                        autoTradeT2Command.ExpireDays = 30;
                    }
                    else if (this.cbExpire.Text.ToLower().IndexOf("60") > -1)
                    {
                        autoTradeT2Command.ExpireDays = 60;
                    }
                    else if (this.cbExpire.Text.ToLower().IndexOf("90") > -1)
                    {
                        autoTradeT2Command.ExpireDays = 90;
                    }
                    else if (this.cbExpire.Text.ToLower().IndexOf("180") > -1)
                    {
                        autoTradeT2Command.ExpireDays = 180;
                    }
                    string text6 = "";
                    if (this._ordSide == string.Empty)
                    {
                        this.ShowMessageBox("Invalid order side", frmOrderFormConfirm.OpenStyle.Error, null);
                    }
                    else if (text2 == string.Empty)
                    {
                        this.ShowMessageBox("Invalid order symbol", frmOrderFormConfirm.OpenStyle.Error, this.cb1Stock);
                    }
                    else
                    {
                        StockList.StockInformation stockInformation = ApplicationInfo.StockInfo[text2];
                        if (stockInformation.Number <= 0)
                        {
                            this.ShowMessageBox("Invalid order symbol", frmOrderFormConfirm.OpenStyle.Error, this.cb1Stock);
                        }
                        else
                        {
                            if (this.chb2TakeProfitCond.Checked)
                            {
                                int num2 = 1;
                                decimal num3 = 0m;
                                decimal.TryParse(this.cb2ValueTakeProfit.Text, out num3);
                                string text3 = this.cb2PriceLast.Text.Trim();
                                if (num2 == -1)
                                {
                                    this.ShowMessageBox("Invalid operator condition", frmOrderFormConfirm.OpenStyle.Error, null);
                                    return;
                                }
                                if (num3 <= 0m)
                                {
                                    this.ShowMessageBox("Invalid price condition", frmOrderFormConfirm.OpenStyle.Error, this.cb2ValueTakeProfit);
                                    return;
                                }
                                string text4 = text3;
                                if (text4 != null)
                                {
                                    if (text4 == "MP")
                                    {
                                        goto IL_B1B;
                                    }
                                }
                                if (!this.IsValidPrice(text3, true, this.cb2PriceLast))
                                {
                                    return;
                                }
                            IL_B1B:
                                AutoTradeItem item = new AutoTradeItem(0L, 1, "ALG2", ApplicationInfo.UserLoginID, text2, "LAST", num2, num3, num3, ApplicationInfo.AccInfo.CurrentAccount, this._ordSide, num, text3, string.Empty, ApplicationInfo.AccInfo.Items[ApplicationInfo.AccInfo.CurrentAccount].PcFlag, isGroupRemove);
                                list.Add(item);
                                text6 = string.Concat(new object[]
								{
									"Take Profit   : Last ",
									AutoTradeConstant.GetOperatorSymbol(num2),
									" ",
									num3
								});
                            }
                            if (this.chb2TrailingStopCond.Checked)
                            {
                                int num2 = 4;
                                string text = string.Empty;
                                if (this.cb2OperTrailingStop.Text.IndexOf("SMA") > -1)
                                {
                                    text = "SMA";
                                }
                                else if (this.cb2OperTrailingStop.Text.IndexOf("Break") > -1)
                                {
                                    text = "LLV";
                                }
                                decimal num3 = 0m;
                                decimal.TryParse(this.cb2ValueTrailingStop.Text, out num3);
                                string text3 = this.cb2PriceSMA.Text.Trim();
                                if (num2 == -1)
                                {
                                    this.ShowMessageBox("Invalid operator condition", frmOrderFormConfirm.OpenStyle.Error, null);
                                    return;
                                }
                                if (num3 <= 0m)
                                {
                                    this.ShowMessageBox("Invalid price condition", frmOrderFormConfirm.OpenStyle.Error, this.cb2ValueTrailingStop);
                                    return;
                                }
                                if (!this.cb2ValueTrailingStop.Items.Contains(num3.ToString()))
                                {
                                    this.ShowMessageBox("Invalid preriod condition", frmOrderFormConfirm.OpenStyle.Error, this.cb2ValueTrailingStop);
                                    return;
                                }
                                string text4 = text3;
                                if (text4 != null)
                                {
                                    if (text4 == "MP")
                                    {
                                        goto IL_D36;
                                    }
                                }
                                if (!this.IsValidPrice(text3, true, this.cb2PriceSMA))
                                {
                                    return;
                                }
                            IL_D36:
                                AutoTradeItem item2 = new AutoTradeItem(0L, 2, "ALG2", ApplicationInfo.UserLoginID, text2, text, num2, num3, -1m, ApplicationInfo.AccInfo.CurrentAccount, this._ordSide, num, text3, string.Empty, ApplicationInfo.AccInfo.Items[ApplicationInfo.AccInfo.CurrentAccount].PcFlag, isGroupRemove);
                                list.Add(item2);
                                text6 = string.Concat(new object[]
								{
									text6,
									(text6 != string.Empty) ? "\n" : "",
									"Trailing Stop : ",
									this.cb2OperTrailingStop.Text,
									" Period(",
									num3,
									")"
								});
                            }
                            if (this.chb2CutLossCond.Checked)
                            {
                                int num2 = 4;
                                decimal num3 = 0m;
                                decimal.TryParse(this.cb2ValueCutloss.Text, out num3);
                                string text3 = this.cb2PriceBreak.Text.Trim();
                                string text = string.Empty;
                                if (this.cb2OperCutloss.Text.ToLower().IndexOf("low") > -1)
                                {
                                    text = "LLV";
                                }
                                else if (this.cb2OperCutloss.Text.ToLower().IndexOf("last") > -1)
                                {
                                    text = "LAST";
                                }
                                if (num2 == -1)
                                {
                                    this.ShowMessageBox("Invalid operator condition", frmOrderFormConfirm.OpenStyle.Error, null);
                                    return;
                                }
                                if (num3 <= 0m)
                                {
                                    this.ShowMessageBox("Invalid price condition", frmOrderFormConfirm.OpenStyle.Error, this.cb2ValueCutloss);
                                    return;
                                }
                                if (text == "LLV")
                                {
                                    if (!this.cb2ValueCutloss.Items.Contains(num3.ToString()))
                                    {
                                        this.ShowMessageBox("Invalid preriod condition", frmOrderFormConfirm.OpenStyle.Error, this.cb2ValueCutloss);
                                        return;
                                    }
                                }
                                string text4 = text3;
                                if (text4 != null)
                                {
                                    if (text4 == "MP")
                                    {
                                        goto IL_FAB;
                                    }
                                }
                                if (!this.IsValidPrice(text3, true, this.cb2PriceBreak))
                                {
                                    return;
                                }
                            IL_FAB:
                                AutoTradeItem item3 = new AutoTradeItem(0L, 3, "ALG2", ApplicationInfo.UserLoginID, text2, text, num2, num3, -1m, ApplicationInfo.AccInfo.CurrentAccount, this._ordSide, num, text3, string.Empty, ApplicationInfo.AccInfo.Items[ApplicationInfo.AccInfo.CurrentAccount].PcFlag, isGroupRemove);
                                list.Add(item3);
                                text6 = text6 + ((text6 != string.Empty) ? "\n" : "") + ("Cut loss       : " + this.cb2OperCutloss.Text) + ((text == "LAST") ? (" " + num3) : (" Period(" + num3 + ")"));
                            }
                            if (autoTradeT2Command.ExpireDays < 1)
                            {
                                this.ShowMessageBox("Invalid Expire!", frmOrderFormConfirm.OpenStyle.Error, this.cbExpire);
                            }
                            else if (list.Count > 0)
                            {
                                if (num <= 0L)
                                {
                                    this.ShowMessageBox("Invalid Volume [More than Zero]!", frmOrderFormConfirm.OpenStyle.Error, this.tb2Volume);
                                }
                                else
                                {
                                    autoTradeT2Command.Items = list;
                                    this._commandMain = new AutoTradeMain();
                                    this._commandMain.Pack("T2", autoTradeT2Command);
                                    string orderParam = string.Concat(new string[]
									{
										"Auto Trade :",
										" Account : ",
										ApplicationInfo.AccInfo.CurrentAccount,
										"  ",
										Utilities.GetOrderSideName(this._ordSide),
										" : ‘",
										text2,
										"’",
										"  Volume : ",
										FormatUtil.VolumeFormat(num, true),
										"\nCondition : \n",
										text6
									});
                                    this.ShowOrderFormConfirm("Confirm to send?", orderParam, frmOrderFormConfirm.OpenStyle.ConfirmSendNew);
                                }
                            }
                            else
                            {
                                this.ShowMessageBox("Can not create command Please check the terms!", frmOrderFormConfirm.OpenStyle.Error, null);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowError("btnSendOrder_Click", ex);
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void cb1Field_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.lbPrice.Show();
            this.cb1Price.Show();
            this.SetValueItems(this.cb1Value, this.lb1Value, this.cb1Condition.Text);
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void SetValueItems(ComboBox control, Label label, string indicatorName)
        {
            try
            {
                if (indicatorName.ToLower().IndexOf("sma") > -1)
                {
                    label.Text = "Period";
                    control.Items.Clear();
                    control.Text = string.Empty;
                    for (int i = 2; i <= 200; i++)
                    {
                        if ((i >= 2 && i <= 50) || i == 75 || i == 200)
                        {
                            control.Items.Add(i.ToString());
                        }
                    }
                }
                else if (indicatorName.ToLower().IndexOf("break") > -1 || indicatorName.ToLower().IndexOf("hhv") > -1 || indicatorName.ToLower().IndexOf("llv") > -1)
                {
                    label.Text = "Period";
                    control.Items.Clear();
                    control.Text = string.Empty;
                    for (int j = 2; j <= 60; j++)
                    {
                        control.Items.Add(j.ToString());
                    }
                }
                else if(indicatorName.ToLower().IndexOf("biglot") > -1)
                {
                    label.Text = "Lot";
                    this.lbPrice.Hide();
                    this.cb1Price.Hide();
                }
                else
                {
                    label.Text = "Value";
                    control.Items.Clear();
                }
            }
            catch (Exception ex)
            {
                this.ShowError("SetValueItems", ex);
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void btnBuy_Click(object sender, EventArgs e)
        {
            this.SetSide("B");
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void btnSell_Click(object sender, EventArgs e)
        {
            this.SetSide("S");
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void SetSide(string side)
        {
            try
            {
                if (this._typeId == "ALG1")
                {
                    if (side == "B")
                    {
                        this._ordSide = "B";
                        this.panType1.BackColor = Color.Green;
                        this.btnBuy.Image = Resources.buy_button;
                        this.btnSell.Image = Resources.no_side_button;
                        this.cb1Stock.Focus();
                    }
                    else if (side == "S")
                    {
                        this._ordSide = "S";
                        this.panType1.BackColor = Color.DarkRed;
                        this.btnBuy.Image = Resources.no_side_button;
                        this.btnSell.Image = Resources.sell_button;
                        this.cb1Stock.Focus();
                    }
                    else
                    {
                        this._ordSide = side;
                        this.panType1.BackColor = Color.FromArgb(30, 30, 30);
                        this.btnBuy.Image = Resources.no_side_button;
                        this.btnSell.Image = Resources.no_side_button;
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowError("SetSide", ex);
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void bgwSendOrder_DoWork(object sender, DoWorkEventArgs e)
        {
            base.IsLoadingData = true;
            string result = string.Empty;
            try
            {
                result = ApplicationInfo.WebAlertService.SendAutoTrade(this._commandMain.ToMessage());
            }
            catch (Exception ex)
            {
                this.ShowError("bgwSendOrder_DoWork", ex);
                result = ex.Message;
            }
            e.Result = result;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void bgwSendOrder_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (this._typeId == "ALG1")
                {
                    this.ReloadData();
                    this.DoClear();
                }
                else
                {
                    if (e.Error == null)
                    {
                        string data = e.Result.ToString();
                        using (DataSet dataSet = new DataSet())
                        {
                            MyDataHelper.StringToDataSet(data, dataSet);
                            if (dataSet.Tables.Contains("Results") && dataSet.Tables["Results"].Rows.Count > 0)
                            {
                                if (dataSet.Tables["Results"].Rows[0]["message"].ToString() == "ok")
                                {
                                    this.ReloadData();
                                    this.DoClear();
                                }
                                else
                                {
                                    this.ReloadData();
                                    this.ShowMessageBox(dataSet.Tables["Results"].Rows[0]["message"].ToString(), frmOrderFormConfirm.OpenStyle.Error, null);
                                }
                            }
                            dataSet.Clear();
                        }
                    }
                    else
                    {
                        this.ShowMessageBox(e.Result.ToString(), frmOrderFormConfirm.OpenStyle.Error, null);
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowError("bgwSendOrder_RunWorkerCompleted", ex);
            }
            base.IsLoadingData = false;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void ShowMessageBox(string message, frmOrderFormConfirm.OpenStyle openStyle, Control lastFocusOjb)
        {
            if (base.InvokeRequired)
            {
                base.Invoke(new frmAutoTrade.ShowMessageBoxCallBack(this.ShowMessageBox), new object[]
				{
					message,
					openStyle,
					lastFocusOjb
				});
            }
            else
            {
                try
                {
                    this._lastFocusOjb = lastFocusOjb;
                    if (this._frmConfirm != null)
                    {
                        if (!this._frmConfirm.IsDisposed)
                        {
                            this._frmConfirm.FormClosing -= new FormClosingEventHandler(this.frmConfirm_FormClosing);
                            this._frmConfirm.Dispose();
                        }
                        this._frmConfirm = null;
                    }
                    this._frmConfirm = new frmOrderFormConfirm(message, openStyle);
                    this._frmConfirm.FormClosing -= new FormClosingEventHandler(this.frmConfirm_FormClosing);
                    this._frmConfirm.FormClosing += new FormClosingEventHandler(this.frmConfirm_FormClosing);
                    this._frmConfirm.TopLevel = false;
                    this._frmConfirm.Parent = this;
                    this._frmConfirm.Location = new Point((this._frmConfirm.Parent.Width - this._frmConfirm.Width) / 2, (this._frmConfirm.Parent.Height - this._frmConfirm.Height) / 2);
                    this._frmConfirm.TopMost = true;
                    this._frmConfirm.Show();
                    this._frmConfirm.BringToFront();
                }
                catch (Exception ex)
                {
                    this.ShowError("ShowMessageInFormConfirm", ex);
                }
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void ShowOrderFormConfirm(string message, string orderParam, frmOrderFormConfirm.OpenStyle openStyle)
        {
            if (base.InvokeRequired)
            {
                base.Invoke(new frmAutoTrade.ShowOrderFormConfirmCallBack(this.ShowOrderFormConfirm), new object[]
				{
					message,
					orderParam,
					openStyle
				});
            }
            else
            {
                try
                {
                    if (this._frmConfirm != null)
                    {
                        if (!this._frmConfirm.IsDisposed)
                        {
                            this._frmConfirm.FormClosing -= new FormClosingEventHandler(this.frmConfirm_FormClosing);
                            this._frmConfirm.Dispose();
                        }
                        this._frmConfirm = null;
                    }
                    this._frmConfirm = new frmOrderFormConfirm(message, openStyle, this.tbPin.Text.Trim());
                    this._frmConfirm.FormClosing -= new FormClosingEventHandler(this.frmConfirm_FormClosing);
                    this._frmConfirm.FormClosing += new FormClosingEventHandler(this.frmConfirm_FormClosing);
                    this._frmConfirm.TopLevel = false;
                    this._frmConfirm.OssMessage = string.Empty;
                    this._frmConfirm.StockThreshold = string.Empty;
                    this._frmConfirm.OrderParam = orderParam;
                    this._frmConfirm.Parent = this;
                    this._frmConfirm.Location = new Point((this._frmConfirm.Parent.Width - this._frmConfirm.Width) / 2, (this._frmConfirm.Parent.Height - this._frmConfirm.Height) / 2);
                    this._frmConfirm.TopMost = true;
                    this._frmConfirm.Show();
                    this._frmConfirm.BringToFront();
                }
                catch (Exception ex)
                {
                    this.ShowError("ShowMessageInFormConfirm", ex);
                }
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void frmConfirm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                base.Focus();
                if (ApplicationInfo.IsEquityAccount)
                {
                    frmOrderFormConfirm.OpenStyle openFormStyle = ((frmOrderFormConfirm)sender).OpenFormStyle;
                    if (openFormStyle == frmOrderFormConfirm.OpenStyle.ConfirmSendNew || openFormStyle == frmOrderFormConfirm.OpenStyle.ConfirmCancel)
                    {
                        DialogResult result = ((frmOrderFormConfirm)sender).Result;
                        if (result == DialogResult.OK)
                        {
                            if (this._typeId == "ALG1")
                            {
                                if (openFormStyle == frmOrderFormConfirm.OpenStyle.ConfirmSendNew)
                                {
                                    AutoTradeManager.Instance.UpdateItemList(_localAutoTradeItem);
                                }
                                else if (openFormStyle == frmOrderFormConfirm.OpenStyle.ConfirmCancel)
                                {
                                    this.CancelLocalItem();
                                }
                                else
                                {

                                }
                                this.ReloadData();


                            }
                            else
                            {
                                if (!this.bgwSendOrder.IsBusy)
                                {
                                    this.bgwSendOrder.RunWorkerAsync();
                                }
                                else
                                {
                                    this.ShowMessageBox("The system is not ready yet.", frmOrderFormConfirm.OpenStyle.Error, null);
                                }
                            }
                        }
                        else
                        {
                            this.btnSendOrder.Enabled = true;
                        }
                    }
                    else if (this._lastFocusOjb != null)
                    {
                        this._lastFocusOjb.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowError("ConfirmForm", ex);
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void tsbtnSearch_Click(object sender, EventArgs e)
        {
            this.ReloadData();
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void cbStock_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Keys keyCode = e.KeyCode;
                if (keyCode != Keys.Return)
                {
                    switch (keyCode)
                    {
                        case Keys.End:
                        case Keys.Home:
                        case Keys.Left:
                        case Keys.Down:
                            e.SuppressKeyPress = true;
                            goto IL_11E;
                        case Keys.Up:
                            goto IL_11E;
                        case Keys.Right:
                            break;
                        default:
                            goto IL_11E;
                    }
                }
                if (this.cb1Stock.Text.Trim() != string.Empty)
                {
                    StockList.StockInformation stockInformation = ApplicationInfo.StockInfo[this.cb1Stock.Text.Trim()];
                    if (stockInformation.Number > 0)
                    {
                        if (this._typeId == "ALG1")
                        {
                            this.cb1Condition.Focus();
                        }
                        else if (this._typeId == "ALG2")
                        {
                            this.cb2OperTakeProfit.Focus();
                        }
                    }
                    else
                    {
                        this.cb1Stock.Focus();
                        this.cb1Stock.SelectAll();
                    }
                }
                e.SuppressKeyPress = true;
            IL_11E: ;
            }
            catch (Exception ex)
            {
                this.ShowError("cbStock_KeyDown", ex);
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void cb1Condition_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Keys keyCode = e.KeyCode;
                if (keyCode != Keys.Return)
                {
                    switch (keyCode)
                    {
                        case Keys.End:
                        case Keys.Home:
                            e.SuppressKeyPress = true;
                            goto IL_79;
                        case Keys.Left:
                            this.cb1Stock.Focus();
                            e.SuppressKeyPress = true;
                            goto IL_79;
                        case Keys.Up:
                            goto IL_79;
                        case Keys.Right:
                            break;
                        default:
                            goto IL_79;
                    }
                }
                this.cb1Value.Focus();
                e.SuppressKeyPress = true;
            IL_79: ;
            }
            catch (Exception ex)
            {
                this.ShowError("cb1Condition_KeyDown", ex);
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void cb1Value_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Keys keyCode = e.KeyCode;
                if (keyCode != Keys.Return)
                {
                    switch (keyCode)
                    {
                        case Keys.Left:
                            this.cb1Condition.Focus();
                            e.SuppressKeyPress = true;
                            goto IL_8E;
                        case Keys.Up:
                        case Keys.Down:
                            e.SuppressKeyPress = true;
                            goto IL_8E;
                        case Keys.Right:
                            break;
                        default:
                            goto IL_8E;
                    }
                }
                this.IsValidPrice(this.cb1Value.Text, true, this.cb1Price);
                this.cb1Price.Focus();
                e.SuppressKeyPress = true;
            IL_8E: ;
            }
            catch (Exception ex)
            {
                this.ShowError("cbPrice_KeyDown", ex);
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void cb1Price_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Keys keyCode = e.KeyCode;
                if (keyCode != Keys.Return)
                {
                    switch (keyCode)
                    {
                        case Keys.Left:
                            this.cb1Value.Focus();
                            e.SuppressKeyPress = true;
                            goto IL_103;
                        case Keys.Up:
                            this.cb1Price.Text = Utilities.PriceFormat(this.GetPrice(true, this.cb1Price.Text));
                            this.cb1Price.SelectAll();
                            e.SuppressKeyPress = true;
                            goto IL_103;
                        case Keys.Right:
                            break;
                        case Keys.Down:
                            this.cb1Price.Text = Utilities.PriceFormat(this.GetPrice(false, this.cb1Price.Text));
                            this.cb1Price.SelectAll();
                            e.SuppressKeyPress = true;
                            goto IL_103;
                        default:
                            goto IL_103;
                    }
                }
                this.IsValidPrice(this.cb1Price.Text, true, this.cb1Price);
                this.tb1Volume.Focus();
                e.SuppressKeyPress = true;
            IL_103: ;
            }
            catch (Exception ex)
            {
                this.ShowError("cb1Price_KeyDown", ex);
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void cbPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                e.KeyChar = Convert.ToChar(e.KeyChar.ToString().ToUpper());
            }
            catch (Exception ex)
            {
                this.ShowError("cbStock_KeyPress", ex);
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private decimal GetPrice(bool isIncrease, string orgPrice)
        {
            decimal num = 0m;
            try
            {
                string stockSymbol = this.cb1Stock.Text.Trim().ToUpper();
                StockList.StockInformation stockInformation = ApplicationInfo.StockInfo[stockSymbol];
                if (stockInformation != null && stockInformation.Number > 0)
                {
                    if (decimal.TryParse(orgPrice, out num))
                    {
                        if (isIncrease)
                        {
                            num += Utilities.GetNextSpreadUp(stockInformation, num);
                            if (num > stockInformation.Ceiling)
                            {
                                num = stockInformation.Ceiling;
                            }
                        }
                        else
                        {
                            num -= Utilities.GetNextSpreadDown(stockInformation, num);
                            if (num < stockInformation.Floor)
                            {
                                num = stockInformation.Floor;
                            }
                        }
                    }
                    else if (num == 0m && stockInformation != null)
                    {
                        num = stockInformation.PriorPrice;
                    }
                }
            }
            catch
            {
            }
            return num;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private bool IsValidPrice(string price, bool IsShowMessage, Control control)
        {
            bool result;
            try
            {
                if (price != null)
                {
                    if (price == "MP")
                    {
                        result = true;
                        return result;
                    }
                }
                if (!FormatUtil.Isnumeric(price))
                {
                    if (IsShowMessage)
                    {
                        this.ShowMessageBox("Invalid price.", frmOrderFormConfirm.OpenStyle.ShowBox, control);
                    }
                    result = false;
                    return result;
                }
                int num = price.IndexOf('.');
                string text = string.Empty;
                if (num > -1)
                {
                    text = price.Substring(num + 1);
                    if (text.Length < 2)
                    {
                        if (ApplicationInfo.BrokerId != 11)
                        {
                            if (IsShowMessage)
                            {
                                this.ShowMessageBox("Invalid price format [2 digits]!.", frmOrderFormConfirm.OpenStyle.ShowBox, control);
                            }
                            result = false;
                            return result;
                        }
                    }
                    else if (text.Length > 2)
                    {
                        if (IsShowMessage)
                        {
                            this.ShowMessageBox("Invalid price format [2 digits]!.", frmOrderFormConfirm.OpenStyle.ShowBox, control);
                        }
                        result = false;
                        return result;
                    }
                }
                if (Convert.ToDecimal(price) <= 0m)
                {
                    if (IsShowMessage)
                    {
                        this.ShowMessageBox("Invalid price [More than 0]!.", frmOrderFormConfirm.OpenStyle.ShowBox, control);
                    }
                    result = false;
                    return result;
                }
            }
            catch (Exception ex)
            {
                this.ShowError("IsValidPrice", ex);
            }
            result = true;
            return result;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void tb1Volume_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.tb1Volume.Text.Trim() != string.Empty)
                {
                    if (FormatUtil.Isnumeric(this.tb1Volume.Text))
                    {
                        try
                        {
                            decimal num = Convert.ToInt64(this.tb1Volume.Text.Replace(",", ""));
                            this.tb1Volume.Text = num.ToString("#,###");
                            this.tb1Volume.Select(this.tb1Volume.Text.Length, 0);
                        }
                        catch
                        {
                            this.tb1Volume.Text = this.tb1Volume.Text.Substring(0, this.tb1Volume.Text.Length - 1);
                        }
                    }
                    else
                    {
                        this.tb1Volume.Text = this.tb1Volume.Text.Substring(0, this.tb1Volume.Text.Length - 1);
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowError("tb1Volume_TextChanged", ex);
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void tb1Volume_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Keys keyCode = e.KeyCode;
                if (keyCode != Keys.Return)
                {
                    switch (keyCode)
                    {
                        case Keys.Left:
                            this.cb1Price.Focus();
                            e.SuppressKeyPress = true;
                            break;
                        case Keys.Up:
                            {
                                StockList.StockInformation stockInformation = ApplicationInfo.StockInfo[this.cb1Stock.Text.Trim()];
                                if (stockInformation.Number > 0)
                                {
                                    long num = 0L;
                                    long.TryParse(this.tb1Volume.Text.Replace(",", ""), out num);
                                    num += (long)stockInformation.BoardLot;
                                    this.tb1Volume.Text = Utilities.VolumeFormat(num, true);
                                    this.tb1Volume.SelectAll();
                                }
                                e.SuppressKeyPress = true;
                                break;
                            }
                        case Keys.Right:
                            if (this.IsValidPrice(this.cb1Price.Text, false, null))
                            {
                                this.tb1Volume.Focus();
                            }
                            e.SuppressKeyPress = true;
                            break;
                        case Keys.Down:
                            {
                                StockList.StockInformation stockInformation2 = ApplicationInfo.StockInfo[this.cb1Stock.Text.Trim()];
                                if (stockInformation2.Number > 0)
                                {
                                    long num = 0L;
                                    long.TryParse(this.tb1Volume.Text.Replace(",", ""), out num);
                                    num -= (long)stockInformation2.BoardLot;
                                    if (num > 0L)
                                    {
                                        this.tb1Volume.Text = Utilities.VolumeFormat(num, true);
                                        this.tb1Volume.SelectAll();
                                    }
                                }
                                e.SuppressKeyPress = true;
                                break;
                            }
                    }
                }
                else
                {
                    if (this.IsValidPrice(this.cb1Price.Text, true, null))
                    {
                        if (this.tbPin.Text.Trim() == string.Empty)
                        {
                            this.tbPin.Focus();
                        }
                        else
                        {
                            this.btnSendOrder.PerformClick();
                        }
                    }
                    e.SuppressKeyPress = true;
                }
            }
            catch (Exception ex)
            {
                this.ShowError("tb1Volume_KeyDown", ex);
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void tb2Volume_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.tb2Volume.Text.Trim() != string.Empty)
                {
                    if (FormatUtil.Isnumeric(this.tb2Volume.Text))
                    {
                        try
                        {
                            decimal num = Convert.ToInt64(this.tb2Volume.Text.Replace(",", ""));
                            this.tb2Volume.Text = num.ToString("#,###");
                            this.tb2Volume.Select(this.tb2Volume.Text.Length, 0);
                        }
                        catch
                        {
                            this.tb2Volume.Text = this.tb2Volume.Text.Substring(0, this.tb2Volume.Text.Length - 1);
                        }
                    }
                    else
                    {
                        this.tb2Volume.Text = this.tb2Volume.Text.Substring(0, this.tb2Volume.Text.Length - 1);
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowError("tb2Volume_TextChanged", ex);
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void tb2Volume_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Keys keyCode = e.KeyCode;
                if (keyCode != Keys.Return)
                {
                    switch (keyCode)
                    {
                        case Keys.Left:
                            this.cb2PriceLast.Focus();
                            e.SuppressKeyPress = true;
                            break;
                        case Keys.Up:
                            {
                                StockList.StockInformation stockInformation = ApplicationInfo.StockInfo[this.cb1Stock.Text.Trim()];
                                if (stockInformation.Number > 0)
                                {
                                    long num = 0L;
                                    long.TryParse(this.tb2Volume.Text.Replace(",", ""), out num);
                                    num += (long)stockInformation.BoardLot;
                                    this.tb2Volume.Text = Utilities.VolumeFormat(num, true);
                                    this.tb2Volume.SelectAll();
                                }
                                e.SuppressKeyPress = true;
                                break;
                            }
                        case Keys.Right:
                            if (this.IsValidPrice(this.cb2PriceLast.Text, false, null))
                            {
                                this.cb2OperTrailingStop.Focus();
                            }
                            e.SuppressKeyPress = true;
                            break;
                        case Keys.Down:
                            {
                                StockList.StockInformation stockInformation2 = ApplicationInfo.StockInfo[this.cb1Stock.Text.Trim()];
                                if (stockInformation2.Number > 0)
                                {
                                    long num = 0L;
                                    long.TryParse(this.tb2Volume.Text.Replace(",", ""), out num);
                                    num -= (long)stockInformation2.BoardLot;
                                    if (num > 0L)
                                    {
                                        this.tb2Volume.Text = Utilities.VolumeFormat(num, true);
                                        this.tb2Volume.SelectAll();
                                    }
                                }
                                e.SuppressKeyPress = true;
                                break;
                            }
                    }
                }
                else
                {
                    if (this.tbPin.Text.Trim() == string.Empty)
                    {
                        this.tbPin.Focus();
                    }
                    else
                    {
                        this.btnSendOrder.PerformClick();
                    }
                    e.SuppressKeyPress = true;
                }
            }
            catch (Exception ex)
            {
                this.ShowError("tb2Volume_KeyDown", ex);
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void cb2PriceLast_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Keys keyCode = e.KeyCode;
                if (keyCode != Keys.Return)
                {
                    switch (keyCode)
                    {
                        case Keys.Left:
                            this.cb2ValueTakeProfit.Focus();
                            e.SuppressKeyPress = true;
                            goto IL_103;
                        case Keys.Up:
                            this.cb2PriceLast.Text = Utilities.PriceFormat(this.GetPrice(true, this.cb2PriceLast.Text));
                            this.cb2PriceLast.SelectAll();
                            e.SuppressKeyPress = true;
                            goto IL_103;
                        case Keys.Right:
                            break;
                        case Keys.Down:
                            this.cb2PriceLast.Text = Utilities.PriceFormat(this.GetPrice(false, this.cb2PriceLast.Text));
                            this.cb2PriceLast.SelectAll();
                            e.SuppressKeyPress = true;
                            goto IL_103;
                        default:
                            goto IL_103;
                    }
                }
                this.IsValidPrice(this.cb2PriceLast.Text, true, this.cb2PriceLast);
                this.cb2OperTrailingStop.Focus();
                e.SuppressKeyPress = true;
            IL_103: ;
            }
            catch (Exception ex)
            {
                this.ShowError("cb2PriceLast_KeyDown", ex);
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void tsbtnCancelOrder_Click(object sender, EventArgs e)
        {
            if (this.intzaOrder.FocusItemIndex >= 0)
            {
                this.CallCancelOrder();
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
                    AutoTradeCancelCommand autoTradeCancelCommand = new AutoTradeCancelCommand();
                    autoTradeCancelCommand.UserId = ApplicationInfo.UserLoginID;
                    autoTradeCancelCommand.CommandType = this._typeId;
                    for (int i = 0; i < this.intzaOrder.Rows; i++)
                    {
                        if (this.intzaOrder.Records(i).Fields("checkbox").Text.ToString() == "1")
                        {
                            autoTradeCancelCommand.Items.Add(new AutoTradeCancelCommand.CancelItem(Convert.ToInt64(this.intzaOrder.Records(i).Fields("ref_no").Text), this.intzaOrder.Records(i).Fields("stock").Text.ToString()));
                        }
                    }
                    if (autoTradeCancelCommand.Items.Count == 0 && this.intzaOrder.FocusItemIndex > -1)
                    {
                        RecordItem recordItem = this.intzaOrder.Records(this.intzaOrder.FocusItemIndex);
                        if (recordItem.Fields("checkbox").Text.ToString() == "0")
                        {
                            autoTradeCancelCommand.Items.Add(new AutoTradeCancelCommand.CancelItem(Convert.ToInt64(recordItem.Fields("ref_no").Text), recordItem.Fields("stock").Text.ToString()));
                        }
                    }
                    if (autoTradeCancelCommand.Items.Count > 0)
                    {
                        this._commandMain = new AutoTradeMain();
                        this._commandMain.Pack("AX", autoTradeCancelCommand);
                        this.ShowOrderFormConfirm("Do you want to cancel?", "", frmOrderFormConfirm.OpenStyle.ConfirmCancel);
                    }
                    else
                    {
                        this.ShowMessageBox("Can not find the item you want to cancel.", frmOrderFormConfirm.OpenStyle.ShowBox, null);
                    }
                }
                catch (Exception ex)
                {
                    this.ShowError("CallCancelOrder", ex);
                }
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void CancelLocalItem()
        {
            for (int i = 0; i < this.intzaOrder.Rows; i++)
            {
                if (this.intzaOrder.Records(i).Fields("checkbox").Text.ToString() == "1")
                {
                    int refNo;
                    int.TryParse(this.intzaOrder.Records(i).Fields("ref_no").Text.ToString(), out refNo);
                    AutoTradeManager.Instance.CancelOrder(refNo);
                }
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void btnClear_Click(object sender, EventArgs e)
        {
            this.DoClear();
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void DoClear()
        {
            if (base.InvokeRequired)
            {
                base.BeginInvoke(new MethodInvoker(this.DoClear));
            }
            else
            {
                try
                {
                    this.btnSendOrder.Enabled = true;
                    if (this._typeId == "ALG1")
                    {
                        this.cb1Condition.SelectedIndex = -1;
                        this.cb1Value.Text = string.Empty;
                        this.cb1Price.Text = "MP";
                        this.tb1Volume.Text = string.Empty;
                        this.cbExpire.Text = "180 Days";
                    }
                    else if (this._typeId == "ALG2")
                    {
                        this.chb2CutLossCond.Checked = false;
                        this.chb2TakeProfitCond.Checked = false;
                        this.chb2TrailingStopCond.Checked = false;
                        this.cb2OperTakeProfit.SelectedIndex = -1;
                        this.cb2OperTrailingStop.SelectedIndex = -1;
                        this.cb2OperCutloss.SelectedIndex = -1;
                        this.cb2ValueTakeProfit.Text = string.Empty;
                        this.cb2ValueTrailingStop.Text = string.Empty;
                        this.cb2ValueCutloss.Text = string.Empty;
                        this.cb2PriceLast.Text = "MP";
                        this.tb2Volume.Text = string.Empty;
                        this.chbGroupCancel.Checked = false;
                        this.cbExpire.Text = "180 Days";
                    }
                }
                catch (Exception ex)
                {
                    this.ShowError("DoClear", ex);
                }
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void intzaOrder_TableMouseClick(object sender, TableMouseEventArgs e)
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
                            if (!(name == "checkbox"))
                            {
                                if (name == "ref_no" || name == "side" || name == "stock")
                                {
                                    if (this.intzaOrder.SortType == SortType.Asc)
                                    {
                                        this.intzaOrder.Sort(e.Column.Name, SortType.Desc);
                                    }
                                    else
                                    {
                                        this.intzaOrder.Sort(e.Column.Name, SortType.Asc);
                                    }
                                    this.intzaOrder.Redraw();
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
        private void grid3_TableMouseDoubleClick(object sender, TableMouseEventArgs e)
        {
            try
            {
                if (e.RowIndex > -1)
                {
                    long refNo;
                    long.TryParse(this.gridDcaMain.Records(e.RowIndex).Fields("refno").Text.ToString(), out refNo);
                    if (this._frmDcaInfo != null)
                    {
                        if (!this._frmDcaInfo.IsDisposed)
                        {
                            this._frmDcaInfo.FormClosed -= new FormClosedEventHandler(this.frmDcaInfo_FormClosed);
                            this._frmDcaInfo.Dispose();
                        }
                        this._frmPzInfo = null;
                    }
                    this._frmDcaInfo = new frmDcaItemsInfo(refNo);
                    this._frmDcaInfo.FormClosed -= new FormClosedEventHandler(this.frmDcaInfo_FormClosed);
                    this._frmDcaInfo.FormClosed += new FormClosedEventHandler(this.frmDcaInfo_FormClosed);
                    this._frmDcaInfo.TopLevel = false;
                    this._frmDcaInfo.Parent = this.panelDCA;
                    this._frmDcaInfo.Bounds = new Rectangle(0, 0, this.panelDCA.Width, this.panelDCA.Height);
                    this._frmDcaInfo.TopMost = true;
                    this._frmDcaInfo.Show();
                    this._frmDcaInfo.BringToFront();
                }
            }
            catch (Exception ex)
            {
                this.ShowError("grid3_TableMouseDoubleClick", ex);
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void frmDcaInfo_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (this._frmDcaInfo.DialogResult == DialogResult.OK)
            {
                this.ReloadData();
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void btnDcaCancel_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.gridDcaMain.FocusItemIndex > -1 && AutoTradeConstant.GetStatusByName(this.gridDcaMain.Records(this.gridDcaMain.FocusItemIndex).Fields("status").Text.ToString(), this._typeId) == "ST")
                {
                    RecordItem recordItem = this.gridDcaMain.Records(this.gridDcaMain.FocusItemIndex);
                    long num;
                    long.TryParse(recordItem.Fields("refno").Text.ToString(), out num);
                    string text = recordItem.Fields("stock").Text.ToString();
                    AutoTradeDCACommand autoTradeDCACommand = new AutoTradeDCACommand();
                    autoTradeDCACommand.RefNo = num;
                    autoTradeDCACommand.Symbol = text;
                    autoTradeDCACommand.CommandType = "CANCEL";
                    autoTradeDCACommand.Items = new List<AutoTradeDCAItem>();
                    this._commandMain = new AutoTradeMain();
                    this._commandMain.Pack("DCA", autoTradeDCACommand);
                    string orderParam = string.Concat(new object[]
					{
						"Dolla Cost Average :",
						"Cancel DCA , Reference no ",
						num,
						"\nStock : ‘",
						text,
						"’"
					});
                    this.ShowOrderFormConfirm("Do you want to cancel?\r\n", orderParam, frmOrderFormConfirm.OpenStyle.ConfirmCancel);
                }
                else
                {
                    this.ShowMessageBox("Can not find the item you want to cancel.", frmOrderFormConfirm.OpenStyle.ShowBox, null);
                }
            }
            catch (Exception ex)
            {
                this.ShowError("btn3Cancel_Click", ex);
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void tbPin_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Keys keyCode = e.KeyCode;
                if (keyCode != Keys.Return)
                {
                    switch (keyCode)
                    {
                        case Keys.Left:
                            if (this._typeId == "ALG1")
                            {
                                this.tb1Volume.Focus();
                            }
                            else if (this._typeId == "ALG2")
                            {
                                this.tb2Volume.Focus();
                            }
                            e.SuppressKeyPress = true;
                            break;
                        case Keys.Right:
                            e.SuppressKeyPress = true;
                            break;
                    }
                }
                else
                {
                    this.btnSendOrder.PerformClick();
                    e.SuppressKeyPress = true;
                }
            }
            catch (Exception ex)
            {
                this.ShowError("tbPin_KeyDown", ex);
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void frmAutoTrade_IDoSymbolLinked(object sender, SymbolLinkSource source, string newStock)
        {
            if (source == SymbolLinkSource.SwitchAccount)
            {
                this.SetAutoTradeType(this._typeId);
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void btnDcaCreate_Click(object sender, EventArgs e)
        {
            try
            {
                if (this._frmDcaCreateNew != null)
                {
                    if (!this._frmDcaCreateNew.IsDisposed)
                    {
                        this._frmDcaCreateNew.FormClosing -= new FormClosingEventHandler(this.frmDcaCreateNew_FormClosing);
                        this._frmDcaCreateNew.Dispose();
                    }
                    this._frmDcaCreateNew = null;
                }
                this._frmDcaCreateNew = new frmDcaCreateNew(this._holidays);
                this._frmDcaCreateNew.FormClosing -= new FormClosingEventHandler(this.frmDcaCreateNew_FormClosing);
                this._frmDcaCreateNew.FormClosing += new FormClosingEventHandler(this.frmDcaCreateNew_FormClosing);
                this._frmDcaCreateNew.TopLevel = false;
                this._frmDcaCreateNew.Parent = this.panelDCA;
                this._frmDcaCreateNew.Bounds = new Rectangle(0, 0, this.panelDCA.Width, this.panelDCA.Height);
                this._frmDcaCreateNew.TopMost = true;
                this._frmDcaCreateNew.Show();
                this._frmDcaCreateNew.BringToFront();
            }
            catch (Exception ex)
            {
                this.ShowError("btnDcaCreate_Click", ex);
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void frmDcaCreateNew_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (this._frmDcaCreateNew.DialogResult == DialogResult.OK)
                {
                    this.ReloadData();
                }
            }
            catch (Exception ex)
            {
                this.ShowError("frmPzCreateNew_FormClosing", ex);
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void btnPzCancel_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.gridPzMain.FocusItemIndex > -1 && AutoTradeConstant.GetStatusByName(this.gridPzMain.Records(this.gridPzMain.FocusItemIndex).Fields("status").Text.ToString(), this._typeId) == "ST")
                {
                    RecordItem recordItem = this.gridPzMain.Records(this.gridPzMain.FocusItemIndex);
                    long num;
                    long.TryParse(recordItem.Fields("refno").Text.ToString(), out num);
                    string text = recordItem.Fields("stock").Text.ToString();
                    AutoTradePzCommand autoTradePzCommand = new AutoTradePzCommand();
                    autoTradePzCommand.RefNo = num;
                    autoTradePzCommand.Symbol = text;
                    autoTradePzCommand.CommandType = "CANCEL";
                    autoTradePzCommand.Items = new List<AutoTradePzItem>();
                    this._commandMain = new AutoTradeMain();
                    this._commandMain.Pack("PZ", autoTradePzCommand);
                    string orderParam = string.Concat(new object[]
					{
						"Pricing Zone :",
						"Cancel Pricing Zone , Reference no ",
						num,
						"\nStock : ‘",
						text,
						"’"
					});
                    this.ShowOrderFormConfirm("Do you want to cancel?\r\n", orderParam, frmOrderFormConfirm.OpenStyle.ConfirmCancel);
                }
                else
                {
                    this.ShowMessageBox("Can not find the item you want to cancel.", frmOrderFormConfirm.OpenStyle.ShowBox, null);
                }
            }
            catch (Exception ex)
            {
                this.ShowError("btn3Cancel_Click", ex);
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void btnPzReload_Click(object sender, EventArgs e)
        {
            this.ReloadData();
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void gridPzMain_TableMouseDoubleClick(object sender, TableMouseEventArgs e)
        {
            try
            {
                if (e.RowIndex > -1)
                {
                    long refNo;
                    long.TryParse(this.gridPzMain.Records(e.RowIndex).Fields("refno").Text.ToString(), out refNo);
                    if (this._frmPzInfo != null)
                    {
                        if (!this._frmPzInfo.IsDisposed)
                        {
                            this._frmPzInfo.FormClosed -= new FormClosedEventHandler(this.frmPzInfo_FormClosed);
                            this._frmPzInfo.Dispose();
                        }
                        this._frmPzInfo = null;
                    }
                    this._frmPzInfo = new frmPzItemsInfo(refNo, this.gridPzMain.Records(e.RowIndex).Fields("stock").Text.ToString());
                    this._frmPzInfo.FormClosed -= new FormClosedEventHandler(this.frmPzInfo_FormClosed);
                    this._frmPzInfo.FormClosed += new FormClosedEventHandler(this.frmPzInfo_FormClosed);
                    this._frmPzInfo.TopLevel = false;
                    this._frmPzInfo.Parent = this.panelPZ;
                    this._frmPzInfo.Bounds = new Rectangle(0, 0, this.panelPZ.Width, this.panelPZ.Height);
                    this._frmPzInfo.TopMost = true;
                    this._frmPzInfo.Show();
                    this._frmPzInfo.BringToFront();
                }
            }
            catch (Exception ex)
            {
                this.ShowError("grid3_TableMouseDoubleClick", ex);
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void frmPzInfo_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.ReloadData();
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void cb2OperTrailingStop_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SetValueItems(this.cb2ValueTrailingStop, this.lb2ValueTrailingStop, this.cb2OperTrailingStop.Text);
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void cb2OperTakeProfit_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SetValueItems(this.cb2ValueTakeProfit, this.lb2ValueTakeProfit, this.cb2OperTakeProfit.Text);
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void cb2OperCutloss_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SetValueItems(this.cb2ValueCutloss, this.lb2ValueCutloss, this.cb2OperCutloss.Text);
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void btnPzCreateNew_Click(object sender, EventArgs e)
        {
            try
            {
                if (this._frmPzCreateNew != null)
                {
                    if (!this._frmPzCreateNew.IsDisposed)
                    {
                        this._frmPzCreateNew.FormClosing -= new FormClosingEventHandler(this.frmPzCreateNew_FormClosing);
                        this._frmPzCreateNew.Dispose();
                    }
                    this._frmPzCreateNew = null;
                }
                this._frmPzCreateNew = new frmPzCreateNew();
                this._frmPzCreateNew.FormClosing -= new FormClosingEventHandler(this.frmPzCreateNew_FormClosing);
                this._frmPzCreateNew.FormClosing += new FormClosingEventHandler(this.frmPzCreateNew_FormClosing);
                this._frmPzCreateNew.TopLevel = false;
                this._frmPzCreateNew.Parent = this.panelPZ;
                this._frmPzCreateNew.Bounds = new Rectangle(0, 0, this.panelPZ.Width, this.panelPZ.Height);
                this._frmPzCreateNew.TopMost = true;
                this._frmPzCreateNew.Show();
                this._frmPzCreateNew.BringToFront();
            }
            catch (Exception ex)
            {
                this.ShowError("btnPzCreateNew_Click", ex);
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void frmPzCreateNew_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (this._frmPzCreateNew.DialogResult == DialogResult.OK)
                {
                    this.ReloadData();
                }
            }
            catch (Exception ex)
            {
                this.ShowError("frmPzCreateNew_FormClosing", ex);
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void btnDcaReload_Click(object sender, EventArgs e)
        {
            this.ReloadData();
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void tsbtnClearCondition_Click(object sender, EventArgs e)
        {
            try
            {
                this.tscbStatus.Text = "ALL";
                this.tscbSide.Text = "ALL";
                this.tstbStock.Clear();
                this.ReloadData();
            }
            catch (Exception ex)
            {
                this.ShowError("tsbtnClearCondition_Click", ex);
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void tstbStock_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                this.ReloadData();
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void cb2ValueTakeProfit_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Keys keyCode = e.KeyCode;
                if (keyCode != Keys.Return)
                {
                    switch (keyCode)
                    {
                        case Keys.Left:
                            this.cb2OperTakeProfit.Focus();
                            e.SuppressKeyPress = true;
                            goto IL_C1;
                        case Keys.Up:
                        case Keys.Down:
                            e.SuppressKeyPress = true;
                            goto IL_C1;
                        case Keys.Right:
                            break;
                        default:
                            goto IL_C1;
                    }
                }
                if (this.cb2ValueTakeProfit.Text != string.Empty && this.cb2OperTakeProfit.Text != string.Empty)
                {
                    this.chb2TakeProfitCond.Checked = true;
                }
                this.cb2PriceLast.Focus();
                e.SuppressKeyPress = true;
            IL_C1: ;
            }
            catch (Exception ex)
            {
                this.ShowError("cb2ValueTakeProfit_KeyDown", ex);
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void cb2ValueTrailingStop_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Keys keyCode = e.KeyCode;
                if (keyCode != Keys.Return)
                {
                    switch (keyCode)
                    {
                        case Keys.Left:
                            this.cb2OperTrailingStop.Focus();
                            e.SuppressKeyPress = true;
                            goto IL_C1;
                        case Keys.Up:
                        case Keys.Down:
                            e.SuppressKeyPress = true;
                            goto IL_C1;
                        case Keys.Right:
                            break;
                        default:
                            goto IL_C1;
                    }
                }
                if (this.cb2ValueTrailingStop.Text != string.Empty && this.cb2OperTrailingStop.Text != string.Empty)
                {
                    this.chb2TrailingStopCond.Checked = true;
                }
                this.cb2OperCutloss.Focus();
                e.SuppressKeyPress = true;
            IL_C1: ;
            }
            catch (Exception ex)
            {
                this.ShowError("cb2ValueTrailingStop_KeyDown", ex);
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void cb2ValueCutloss_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Keys keyCode = e.KeyCode;
                if (keyCode != Keys.Return)
                {
                    switch (keyCode)
                    {
                        case Keys.Left:
                            this.cb2OperCutloss.Focus();
                            e.SuppressKeyPress = true;
                            goto IL_C1;
                        case Keys.Up:
                        case Keys.Down:
                            e.SuppressKeyPress = true;
                            goto IL_C1;
                        case Keys.Right:
                            break;
                        default:
                            goto IL_C1;
                    }
                }
                if (this.cb2ValueCutloss.Text != string.Empty && this.cb2OperCutloss.Text != string.Empty)
                {
                    this.chb2CutLossCond.Checked = true;
                }
                this.tb2Volume.Focus();
                e.SuppressKeyPress = true;
            IL_C1: ;
            }
            catch (Exception ex)
            {
                this.ShowError("cb2ValueCutloss_KeyDown", ex);
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void cb2OperTakeProfit_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Keys keyCode = e.KeyCode;
                if (keyCode != Keys.Return)
                {
                    switch (keyCode)
                    {
                        case Keys.Left:
                            this.cb1Stock.Focus();
                            e.SuppressKeyPress = true;
                            goto IL_6D;
                        case Keys.Up:
                        case Keys.Down:
                            goto IL_6D;
                        case Keys.Right:
                            break;
                        default:
                            goto IL_6D;
                    }
                }
                this.cb2ValueTakeProfit.Focus();
                e.SuppressKeyPress = true;
            IL_6D: ;
            }
            catch (Exception ex)
            {
                this.ShowError("cb2OperTakeProfit_KeyDown", ex);
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void cb2OperTrailingStop_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Keys keyCode = e.KeyCode;
                if (keyCode != Keys.Return)
                {
                    switch (keyCode)
                    {
                        case Keys.Left:
                            this.tb2Volume.Focus();
                            e.SuppressKeyPress = true;
                            goto IL_6D;
                        case Keys.Up:
                        case Keys.Down:
                            goto IL_6D;
                        case Keys.Right:
                            break;
                        default:
                            goto IL_6D;
                    }
                }
                this.cb2ValueTrailingStop.Focus();
                e.SuppressKeyPress = true;
            IL_6D: ;
            }
            catch (Exception ex)
            {
                this.ShowError("cb2OperTrailingStop_KeyDown", ex);
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void cb2OperCutloss_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Keys keyCode = e.KeyCode;
                if (keyCode != Keys.Return)
                {
                    switch (keyCode)
                    {
                        case Keys.Left:
                            this.cb2ValueTrailingStop.Focus();
                            e.SuppressKeyPress = true;
                            goto IL_6D;
                        case Keys.Up:
                        case Keys.Down:
                            goto IL_6D;
                        case Keys.Right:
                            break;
                        default:
                            goto IL_6D;
                    }
                }
                this.cb2ValueCutloss.Focus();
                e.SuppressKeyPress = true;
            IL_6D: ;
            }
            catch (Exception ex)
            {
                this.ShowError("cb2OperCutloss_KeyDown", ex);
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void btnSendLocalOrder_Click(object sender, EventArgs e)
        {
            _localAutoTradeItem = new LocalAutoTradeItem();
            _localAutoTradeItem.StockName = this.cb1Stock.Text.Trim();

            if (this.cb1Condition.Text.ToLower().IndexOf("last") > -1)
            {
                _localAutoTradeItem.FieldType = "LAST";
            }

            if (this.cb1Condition.Text.IndexOf(">=") > 0)
            {
                _localAutoTradeItem.OperatorType = AutoTradeConstant.OPERATOR_GREATER_EQUAL;
            }
            else if (this.cb1Condition.Text.IndexOf("<=") > 0)
            {
                _localAutoTradeItem.OperatorType = AutoTradeConstant.OPERATOR_LESSER_EQUAL;
            }
            else if (this.cb1Condition.Text.IndexOf(">") > 0)
            {
                _localAutoTradeItem.OperatorType = AutoTradeConstant.OPERATOR_GREATER;
            }
            else if (this.cb1Condition.Text.IndexOf("<") > 0)
            {
                _localAutoTradeItem.OperatorType = AutoTradeConstant.OPERATOR_LESSER;
            }

            if (this.cb1Condition.Text.ToLower().IndexOf("sma") > 0)
            {
                _localAutoTradeItem.ConditionType = LocalAutoTradeItem.AutoTradeCondition.SMA;
            }
            else if (this.cb1Condition.Text.ToLower().IndexOf("biglot") > 0)
            {
                _localAutoTradeItem.ConditionType = LocalAutoTradeItem.AutoTradeCondition.FOLLOW_BIGLOT;
            }
            else
            {
                _localAutoTradeItem.ConditionType = LocalAutoTradeItem.AutoTradeCondition.COMMON;
            }


            decimal comparePrice;
            decimal.TryParse(this.cb1Value.Text, out comparePrice);
            _localAutoTradeItem.Value = comparePrice;

            _localAutoTradeItem.OrdPrice = this.cb1Price.Text.Trim();
            if (!this.IsValidPrice(_localAutoTradeItem.OrdPrice, true, this.cb1Price))
            {
                return;
            }

            long volumn = 0L;
            long.TryParse(this.tb1Volume.Text.Replace(",", ""), out volumn);
            _localAutoTradeItem.OrdVolume = volumn;

            _localAutoTradeItem.OrdSide = this._ordSide;

            _localAutoTradeItem.Time = DateTime.Now;

            string orderParam = string.Empty ;

            if (this.cb1Condition.Text.ToLower().IndexOf("biglot") > 0)
            {
                orderParam = string.Concat(new string[]
                                        {
                                            "Auto Trade :",
                                            " Account : ",
                                            ApplicationInfo.AccInfo.CurrentAccount,
                                            "\n",
                                            Utilities.GetOrderSideName(this._ordSide),
                                            "  ‘",
                                            _localAutoTradeItem.StockName,
                                            "’",
                                            "  Volume ",
                                            FormatUtil.VolumeFormat(_localAutoTradeItem.OrdVolume, true),
                                           "\n",
                                           "Follow Biglot with volume ",
                                            this.cb1Value.Text
                                        });
            }
            else
            {
                orderParam = string.Concat(new string[]
                                        {
                                            "Auto Trade :",
                                            " Account : ",
                                            ApplicationInfo.AccInfo.CurrentAccount,
                                            "\n",
                                            Utilities.GetOrderSideName(this._ordSide),
                                            "  ‘",
                                            _localAutoTradeItem.StockName,
                                            "’",
                                            "  Volume ",
                                            FormatUtil.VolumeFormat(_localAutoTradeItem.OrdVolume, true),
                                            "  Price ",
                                            _localAutoTradeItem.OrdPrice,
                                            "\nCondition : ",
                                            _localAutoTradeItem.FieldType + " " + AutoTradeConstant.GetOperatorSymbol(_localAutoTradeItem.OperatorType) + " " + _localAutoTradeItem.Value
                                        });
            }
            this.ShowOrderFormConfirm("Confirm to send?", orderParam, frmOrderFormConfirm.OpenStyle.ConfirmSendNew);
        }


        [MethodImpl(MethodImplOptions.NoInlining)]
        public void UpdateLocalStopOrderToGrid(bool isRedraw)
        {
            try
            {
                List<LocalAutoTradeItem> itemList = AutoTradeManager.Instance.ItemList;
                this.intzaOrder.SortColumnName = string.Empty;
                this.intzaOrder.BeginUpdate();
                this.intzaOrder.Rows = itemList.Count;

                for (int listIndex = 0; listIndex < itemList.Count; ++listIndex)
                {
                    RecordItem recordItem = this.intzaOrder.Records(listIndex);
                    recordItem.Fields("ref_no").Text = itemList[listIndex].RefNo;
                    recordItem.Fields("side").Text = itemList[listIndex].OrdSide;
                    recordItem.Fields("stock").Text = itemList[listIndex].StockName;
                    recordItem.Fields("volume").Text = itemList[listIndex].OrdVolume;

                    recordItem.Fields("status").Text = AutoTradeConstant.GetSatusString(itemList[listIndex].Status);
                    if (itemList[listIndex].ConditionType != LocalAutoTradeItem.AutoTradeCondition.FOLLOW_BIGLOT)
                    {
                        recordItem.Fields("price").Text = itemList[listIndex].Value;
                    }
                    recordItem.Fields("sent_time").Text = itemList[listIndex].Time.ToString("h:mm:ss tt");
                    recordItem.Fields("matched_time").Text = itemList[listIndex].Mtime.ToString("h:mm:ss tt");
                    recordItem.Fields("condition").Text = this.GetLocalConditionString(itemList[listIndex].ConditionType, itemList[listIndex].FieldType, (int)itemList[listIndex].OperatorType, itemList[listIndex].Value);
                    recordItem.Fields("message").Text = itemList[listIndex].Message;

                    string text = itemList[listIndex].OrdSide;
                    recordItem.Fields("ref_no").FontColor = Color.White;
                    if (text == "B")
                    {
                        recordItem.Fields("side").FontColor = Color.Lime;
                    }
                    else if (text == "S")
                    {
                        recordItem.Fields("side").FontColor = Color.Red;
                    }
                    else if (text == "C")
                    {
                        recordItem.Fields("side").FontColor = Color.Cyan;
                    }
                    else if (text == "H")
                    {
                        recordItem.Fields("side").FontColor = Color.Pink;
                    }
                    else
                    {
                        recordItem.Fields("side").FontColor = MyColor.UnChgColor;
                    }
                    recordItem.Fields("stock").FontColor = Color.LightGray;
                    recordItem.Fields("volume").FontColor = Color.LightGray;
                    recordItem.Fields("price").FontColor = Color.LightGray;
                    recordItem.Fields("limit").FontColor = Color.LightGray;
                    recordItem.Fields("sent_time").FontColor = Color.LightGray;
                    recordItem.Fields("matched_time").FontColor = Color.LightGray;
                    recordItem.Fields("status").FontColor = Color.Cyan;
                    recordItem.Fields("condition").FontColor = MyColor.UnChgColor;

                    string statusTmp = itemList[listIndex].Status;
                    if (statusTmp == "W")
                    {
                        recordItem.Fields("checkbox").Text = "0";
                    }
                    else
                    {
                        recordItem.Fields("checkbox").Text = "";
                    }
                    recordItem.Changed = true;
                }
                this.intzaOrder.Redraw();
                this.intzaOrder.Focus();
                this.intzaOrder.SetFocusItem(0);
            }
            catch (Exception ex)
            {
                this.intzaOrder.Redraw();
                this.ShowError("UpdateToControl", ex);
            }

            this._isReloadData = false;
            base.IsLoadingData = false;

        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void ShowSplash(bool visible, string message)
        {
            if (base.InvokeRequired)
            {
                base.Invoke(new frmAutoTrade.ShowSplashCallBack(this.ShowSplash), new object[]
				{
					visible,
                    message
				});
            }
            else
            {
                if (this.splashForm == null)
                {
                    this.splashForm = new frmSplash();
                    this.splashForm.TopLevel = false;
                    this.splashForm.Parent = this;
                    this.splashForm.TopMost = true;
                }
                if (base.WindowState != FormWindowState.Minimized)
                {
                    this.splashForm.Left = (base.Width - this.splashForm.Width) / 2;
                    this.splashForm.Top = (base.Height - this.splashForm.Height) / 2;
                }
                this.splashForm.SetCurrentTask(message);
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void CloseSpash()
        {
            if (this.splashForm != null)
            {
                if (this.splashForm.InvokeRequired)
                {
                    this.splashForm.Invoke(new MethodInvoker(this.CloseSpash));
                }
                else
                {
                    this.splashForm.Hide();
                    this.splashForm.DisposeMe();
                    this.splashForm = null;
                }
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void OnStartAutoTrade(string message)
        {
            if (base.InvokeRequired)
            {
                base.Invoke(new frmAutoTrade.OnStartAutoTradeCallback(this.OnStartAutoTrade), new object[]
				{
					message
				});
            }
            else
            {
                this.ShowSplash(true, message);
                this.ReloadData();
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void OnEndAutoTrade()
        {
            if (base.InvokeRequired)
            {
                base.Invoke(new MethodInvoker(this.OnEndAutoTrade));
            }
            else
            {
                this.CloseSpash();
                this.ReloadData();
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private string GetLocalConditionString(LocalAutoTradeItem.AutoTradeCondition conditionType, string fieldType, int operType, decimal value)
        {
            if (LocalAutoTradeItem.AutoTradeCondition.COMMON == conditionType)
            {
                return fieldType + " " + AutoTradeConstant.GetOperatorSymbol(operType) + " " + value;
            }
            else if (LocalAutoTradeItem.AutoTradeCondition.FOLLOW_BIGLOT == conditionType)
            {
                return "Volume " + AutoTradeConstant.GetOperatorSymbol(operType) + " " + value + "(Follow Biglot)";
            }
            return null;
        }
    }
}
