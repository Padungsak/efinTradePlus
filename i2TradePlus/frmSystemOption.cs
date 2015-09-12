using i2TradePlus.FixForm;
using i2TradePlus.Properties;
using i2TradePlus.Templates;
using Microsoft.Win32;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace i2TradePlus
{
	public class frmSystemOption : Form
	{
		private IContainer components = null;

		private Button btnCancel;

		private Button btnOk;

		private GroupBox gbGeneral;

		private TabControl tabControl1;

		private TabPage tpGeneral;

		private ToolTip toolTipSystemOption;

		private GroupBox gbFont;

		private ColumnHeader colShortcut;

		private Label lbFontSize;

		private Label lbFontStyle;

		private Label lbNameFont;

		private CheckBox checkBoxShowSector;

		private Label lbSpeedSecound;

		private Label lbSpeedTime;

		private CheckBox checkBoxShowTop5Loser;

		private CheckBox checkBoxShowTop5Gainer;

		private CheckBox checkBoxShowTop5MostActive;

		private CheckBox checkBoxShowIndustryInfo;

		private TabPage tabConnection;

		private TabPage tabPagePincode;

		private GroupBox gbProtocal;

		private TabPage tabPageError;

		private GroupBox gbException;

		private CheckBox chkIsWriteLog;

		private TabPage tabPagePassword;

		private Label lblPwdDescription;

		private Label lblPincodeDescription;

		private GroupBox groupBox1;

		private Panel pnlProxy;

		private Label label4;

		private Label label5;

		private Label label3;

		private Label label2;

		private TextBox txtPort;

		private TextBox txtAddress;

		private TextBox txtPassword;

		private TextBox txtUserName;

		private ComboBox cbDefaultFontName;

		private ComboBox cbDefaultFontSize;

		private ComboBox cbDefaultFontStyle;

		private PasswordEntryUC pwdEntryChangePassword;

		private PasswordEntryUC pwdEntryChangePincode;

		private GroupBox groupBox2;

		private Label lbQuoteCancel;

		private Label lbOffer;

		private Label lbBid;

		private Label label7;

		private Label label6;

		private Label label1;

		private TabPage tabViewOrders;

		private Label lbMaxViewOrderRows;

		private Label lbPullBuySell;

		private Label lbAutoGetOrderInfoInterval;

		private ComboBox cbMaxViewOrderRows;

		private RadioButton rdoLangThai;

		private RadioButton rdoLangEnglish;

		private CheckBox cbBidOfferColorBlink;

		private ListView lsvError;

		private ColumnHeader colTime;

		private ColumnHeader colDescription;

		private ColumnHeader colModule;

		private ColumnHeader colFunction;

		private Button btnClearErrorList;

		private GroupBox gbDescription;

		private LinkLabel lnkClose;

		private TextBox txtError;

		private Button btnRefreshError;

		private ListBox lsbTunnelServer;

		private RadioButton rdoPoll;

		private RadioButton rdoPush;

		private Button btnHeader;

		private ColorDialog colorDialog1;

		private GroupBox gbTable;

		private Label label8;

		private Label lbHeaderColor;

		private Label label9;

		private Label lbGridColor;

		private Button btnGridColor;

		private Label label10;

		private Label lbFontHeaderColor;

		private Button btnFontHeaderColor;

		private Button btnResetColor;

		private TabPage tabHotkey;

		private TemplateTreeMenus templateTreeMenus1;

		private Label label11;

		private RadioButton rdbFontColorOriginalStyle;

		private RadioButton rdbFontColorSoftStyle;

		private Label label12;

		private Font defFont = null;

		private string passwordDescription = "   รูปแบบการกำหนด Password\r\n\r\n1. รหัสต้องมีความยาวไม่น้อยกว่า {0} ตัว\r\n2. รหัสต้องมีตัวอักษร A-Z หรือ a-z , 0-9 \r\n3. รหัสต้องมีอักขระพิเศษ เช่น @ , ! ,# ,* , |\r\n  ตัวอย่าง เช่น   Sti@123";

		private string pincodeDescription = "   รูปแบบการกำหนด Password\r\n\r\n1. รหัสต้องมีความยาวไม่น้อยกว่า {0} ตัว";

		private static Form[] formSmartClientCollection = null;

		public static Form[] FormSmartClientCollection
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return frmSystemOption.formSmartClientCollection;
			}
			[MethodImpl(MethodImplOptions.NoInlining)]
			set
			{
				frmSystemOption.formSmartClientCollection = value;
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
			this.btnCancel = new Button();
			this.btnOk = new Button();
			this.gbGeneral = new GroupBox();
			this.gbTable = new GroupBox();
			this.label12 = new Label();
			this.rdbFontColorOriginalStyle = new RadioButton();
			this.rdbFontColorSoftStyle = new RadioButton();
			this.label11 = new Label();
			this.btnResetColor = new Button();
			this.label10 = new Label();
			this.lbFontHeaderColor = new Label();
			this.btnFontHeaderColor = new Button();
			this.label9 = new Label();
			this.lbGridColor = new Label();
			this.btnGridColor = new Button();
			this.label8 = new Label();
			this.lbHeaderColor = new Label();
			this.btnHeader = new Button();
			this.groupBox2 = new GroupBox();
			this.cbBidOfferColorBlink = new CheckBox();
			this.lbQuoteCancel = new Label();
			this.lbOffer = new Label();
			this.lbBid = new Label();
			this.label7 = new Label();
			this.label6 = new Label();
			this.label1 = new Label();
			this.gbFont = new GroupBox();
			this.cbDefaultFontStyle = new ComboBox();
			this.cbDefaultFontSize = new ComboBox();
			this.cbDefaultFontName = new ComboBox();
			this.lbFontSize = new Label();
			this.lbFontStyle = new Label();
			this.lbNameFont = new Label();
			this.tabControl1 = new TabControl();
			this.tpGeneral = new TabPage();
			this.tabViewOrders = new TabPage();
			this.cbMaxViewOrderRows = new ComboBox();
			this.lbAutoGetOrderInfoInterval = new Label();
			this.lbPullBuySell = new Label();
			this.lbMaxViewOrderRows = new Label();
			this.tabHotkey = new TabPage();
			this.tabConnection = new TabPage();
			this.groupBox1 = new GroupBox();
			this.pnlProxy = new Panel();
			this.txtPassword = new TextBox();
			this.txtPort = new TextBox();
			this.txtUserName = new TextBox();
			this.label4 = new Label();
			this.txtAddress = new TextBox();
			this.label5 = new Label();
			this.label3 = new Label();
			this.label2 = new Label();
			this.gbProtocal = new GroupBox();
			this.lsbTunnelServer = new ListBox();
			this.rdoPoll = new RadioButton();
			this.rdoPush = new RadioButton();
			this.tabPagePassword = new TabPage();
			this.lblPwdDescription = new Label();
			this.tabPagePincode = new TabPage();
			this.lblPincodeDescription = new Label();
			this.tabPageError = new TabPage();
			this.btnRefreshError = new Button();
			this.btnClearErrorList = new Button();
			this.gbDescription = new GroupBox();
			this.lnkClose = new LinkLabel();
			this.txtError = new TextBox();
			this.lsvError = new ListView();
			this.colTime = new ColumnHeader();
			this.colDescription = new ColumnHeader();
			this.colModule = new ColumnHeader();
			this.colFunction = new ColumnHeader();
			this.gbException = new GroupBox();
			this.chkIsWriteLog = new CheckBox();
			this.toolTipSystemOption = new ToolTip(this.components);
			this.colShortcut = new ColumnHeader();
			this.checkBoxShowSector = new CheckBox();
			this.lbSpeedSecound = new Label();
			this.lbSpeedTime = new Label();
			this.checkBoxShowTop5Loser = new CheckBox();
			this.checkBoxShowTop5Gainer = new CheckBox();
			this.checkBoxShowTop5MostActive = new CheckBox();
			this.checkBoxShowIndustryInfo = new CheckBox();
			this.rdoLangThai = new RadioButton();
			this.rdoLangEnglish = new RadioButton();
			this.colorDialog1 = new ColorDialog();
			this.templateTreeMenus1 = new TemplateTreeMenus();
			this.pwdEntryChangePassword = new PasswordEntryUC();
			this.pwdEntryChangePincode = new PasswordEntryUC();
			this.gbGeneral.SuspendLayout();
			this.gbTable.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.gbFont.SuspendLayout();
			this.tabControl1.SuspendLayout();
			this.tpGeneral.SuspendLayout();
			this.tabViewOrders.SuspendLayout();
			this.tabHotkey.SuspendLayout();
			this.tabConnection.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.pnlProxy.SuspendLayout();
			this.gbProtocal.SuspendLayout();
			this.tabPagePassword.SuspendLayout();
			this.tabPagePincode.SuspendLayout();
			this.tabPageError.SuspendLayout();
			this.gbDescription.SuspendLayout();
			this.gbException.SuspendLayout();
			base.SuspendLayout();
			this.btnCancel.AutoSize = true;
			this.btnCancel.FlatAppearance.BorderColor = Color.Gray;
			this.btnCancel.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 128, 0);
			this.btnCancel.FlatAppearance.MouseOverBackColor = Color.Teal;
			this.btnCancel.FlatStyle = FlatStyle.Flat;
			this.btnCancel.Location = new Point(494, 390);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new Size(87, 30);
			this.btnCancel.TabIndex = 7;
			this.btnCancel.Text = "&Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
			this.btnOk.AutoSize = true;
			this.btnOk.FlatAppearance.BorderColor = Color.Gray;
			this.btnOk.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 128, 0);
			this.btnOk.FlatAppearance.MouseOverBackColor = Color.Teal;
			this.btnOk.FlatStyle = FlatStyle.Flat;
			this.btnOk.Location = new Point(402, 390);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new Size(87, 30);
			this.btnOk.TabIndex = 6;
			this.btnOk.Text = "&OK";
			this.btnOk.UseVisualStyleBackColor = true;
			this.btnOk.Click += new EventHandler(this.btnOk_Click);
			this.gbGeneral.Controls.Add(this.gbTable);
			this.gbGeneral.Controls.Add(this.groupBox2);
			this.gbGeneral.Controls.Add(this.gbFont);
			this.gbGeneral.Dock = DockStyle.Fill;
			this.gbGeneral.Location = new Point(3, 3);
			this.gbGeneral.Name = "gbGeneral";
			this.gbGeneral.Size = new Size(572, 349);
			this.gbGeneral.TabIndex = 3;
			this.gbGeneral.TabStop = false;
			this.gbGeneral.Text = "General";
			this.gbTable.Controls.Add(this.label12);
			this.gbTable.Controls.Add(this.rdbFontColorOriginalStyle);
			this.gbTable.Controls.Add(this.rdbFontColorSoftStyle);
			this.gbTable.Controls.Add(this.label11);
			this.gbTable.Controls.Add(this.btnResetColor);
			this.gbTable.Controls.Add(this.label10);
			this.gbTable.Controls.Add(this.lbFontHeaderColor);
			this.gbTable.Controls.Add(this.btnFontHeaderColor);
			this.gbTable.Controls.Add(this.label9);
			this.gbTable.Controls.Add(this.lbGridColor);
			this.gbTable.Controls.Add(this.btnGridColor);
			this.gbTable.Controls.Add(this.label8);
			this.gbTable.Controls.Add(this.lbHeaderColor);
			this.gbTable.Controls.Add(this.btnHeader);
			this.gbTable.Location = new Point(14, 21);
			this.gbTable.Name = "gbTable";
			this.gbTable.Size = new Size(553, 152);
			this.gbTable.TabIndex = 20;
			this.gbTable.TabStop = false;
			this.gbTable.Text = "ตาราง";
			this.label12.AutoSize = true;
			this.label12.Location = new Point(19, 126);
			this.label12.Name = "label12";
			this.label12.Size = new Size(336, 14);
			this.label12.TabIndex = 32;
			this.label12.Text = "This function will result in the opening of the next program.";
			this.rdbFontColorOriginalStyle.AutoSize = true;
			this.rdbFontColorOriginalStyle.Location = new Point(110, 30);
			this.rdbFontColorOriginalStyle.Name = "rdbFontColorOriginalStyle";
			this.rdbFontColorOriginalStyle.Size = new Size(64, 18);
			this.rdbFontColorOriginalStyle.TabIndex = 31;
			this.rdbFontColorOriginalStyle.TabStop = true;
			this.rdbFontColorOriginalStyle.Text = "Original";
			this.rdbFontColorOriginalStyle.UseVisualStyleBackColor = true;
			this.rdbFontColorSoftStyle.AutoSize = true;
			this.rdbFontColorSoftStyle.Location = new Point(188, 30);
			this.rdbFontColorSoftStyle.Name = "rdbFontColorSoftStyle";
			this.rdbFontColorSoftStyle.Size = new Size(48, 18);
			this.rdbFontColorSoftStyle.TabIndex = 30;
			this.rdbFontColorSoftStyle.TabStop = true;
			this.rdbFontColorSoftStyle.Text = "Soft";
			this.rdbFontColorSoftStyle.UseVisualStyleBackColor = true;
			this.label11.AutoSize = true;
			this.label11.Location = new Point(29, 32);
			this.label11.Name = "label11";
			this.label11.Size = new Size(62, 14);
			this.label11.TabIndex = 29;
			this.label11.Text = "Font color";
			this.btnResetColor.FlatStyle = FlatStyle.Flat;
			this.btnResetColor.Location = new Point(440, 120);
			this.btnResetColor.Name = "btnResetColor";
			this.btnResetColor.Size = new Size(107, 26);
			this.btnResetColor.TabIndex = 28;
			this.btnResetColor.Text = "Use Default";
			this.btnResetColor.UseVisualStyleBackColor = true;
			this.btnResetColor.Visible = false;
			this.btnResetColor.Click += new EventHandler(this.btnResetColor_Click);
			this.label10.AutoSize = true;
			this.label10.Location = new Point(284, 66);
			this.label10.Name = "label10";
			this.label10.Size = new Size(105, 14);
			this.label10.TabIndex = 27;
			this.label10.Text = "Header Font color";
			this.label10.Visible = false;
			this.lbFontHeaderColor.BackColor = Color.Gray;
			this.lbFontHeaderColor.BorderStyle = BorderStyle.FixedSingle;
			this.lbFontHeaderColor.Location = new Point(395, 62);
			this.lbFontHeaderColor.Name = "lbFontHeaderColor";
			this.lbFontHeaderColor.Size = new Size(35, 23);
			this.lbFontHeaderColor.TabIndex = 26;
			this.lbFontHeaderColor.Visible = false;
			this.btnFontHeaderColor.Location = new Point(439, 62);
			this.btnFontHeaderColor.Name = "btnFontHeaderColor";
			this.btnFontHeaderColor.Size = new Size(29, 23);
			this.btnFontHeaderColor.TabIndex = 25;
			this.btnFontHeaderColor.Text = "...";
			this.btnFontHeaderColor.UseVisualStyleBackColor = true;
			this.btnFontHeaderColor.Visible = false;
			this.btnFontHeaderColor.Click += new EventHandler(this.btnFontHeaderColor_Click);
			this.label9.AutoSize = true;
			this.label9.Location = new Point(328, 29);
			this.label9.Name = "label9";
			this.label9.Size = new Size(58, 14);
			this.label9.TabIndex = 24;
			this.label9.Text = "Grid color";
			this.label9.Visible = false;
			this.lbGridColor.BackColor = Color.Gray;
			this.lbGridColor.BorderStyle = BorderStyle.FixedSingle;
			this.lbGridColor.Location = new Point(396, 25);
			this.lbGridColor.Name = "lbGridColor";
			this.lbGridColor.Size = new Size(35, 23);
			this.lbGridColor.TabIndex = 23;
			this.lbGridColor.Visible = false;
			this.btnGridColor.Location = new Point(439, 25);
			this.btnGridColor.Name = "btnGridColor";
			this.btnGridColor.Size = new Size(29, 23);
			this.btnGridColor.TabIndex = 22;
			this.btnGridColor.Text = "...";
			this.btnGridColor.UseVisualStyleBackColor = true;
			this.btnGridColor.Visible = false;
			this.btnGridColor.Click += new EventHandler(this.btnGridColor_Click);
			this.label8.AutoSize = true;
			this.label8.Location = new Point(278, 94);
			this.label8.Name = "label8";
			this.label8.Size = new Size(144, 14);
			this.label8.TabIndex = 21;
			this.label8.Text = "Header Background color";
			this.label8.Visible = false;
			this.lbHeaderColor.BackColor = Color.Gray;
			this.lbHeaderColor.BorderStyle = BorderStyle.FixedSingle;
			this.lbHeaderColor.Location = new Point(434, 90);
			this.lbHeaderColor.Name = "lbHeaderColor";
			this.lbHeaderColor.Size = new Size(35, 23);
			this.lbHeaderColor.TabIndex = 20;
			this.lbHeaderColor.Visible = false;
			this.btnHeader.Location = new Point(478, 90);
			this.btnHeader.Name = "btnHeader";
			this.btnHeader.Size = new Size(29, 23);
			this.btnHeader.TabIndex = 19;
			this.btnHeader.Text = "...";
			this.btnHeader.UseVisualStyleBackColor = true;
			this.btnHeader.Visible = false;
			this.btnHeader.Click += new EventHandler(this.btnHeader_Click);
			this.groupBox2.Controls.Add(this.cbBidOfferColorBlink);
			this.groupBox2.Controls.Add(this.lbQuoteCancel);
			this.groupBox2.Controls.Add(this.lbOffer);
			this.groupBox2.Controls.Add(this.lbBid);
			this.groupBox2.Controls.Add(this.label7);
			this.groupBox2.Controls.Add(this.label6);
			this.groupBox2.Controls.Add(this.label1);
			this.groupBox2.Location = new Point(13, 243);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new Size(554, 60);
			this.groupBox2.TabIndex = 11;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "สีกระพริบบอกการเสนอซื้อ ยกเลิก หรือจับคู่";
			this.cbBidOfferColorBlink.AutoSize = true;
			this.cbBidOfferColorBlink.Location = new Point(23, 29);
			this.cbBidOfferColorBlink.Name = "cbBidOfferColorBlink";
			this.cbBidOfferColorBlink.Size = new Size(97, 18);
			this.cbBidOfferColorBlink.TabIndex = 19;
			this.cbBidOfferColorBlink.Text = "เปิดการทำงาน";
			this.cbBidOfferColorBlink.UseVisualStyleBackColor = true;
			this.lbQuoteCancel.AutoSize = true;
			this.lbQuoteCancel.Location = new Point(341, 31);
			this.lbQuoteCancel.Name = "lbQuoteCancel";
			this.lbQuoteCancel.Size = new Size(28, 14);
			this.lbQuoteCancel.TabIndex = 5;
			this.lbQuoteCancel.Text = "ถอน";
			this.lbOffer.AutoSize = true;
			this.lbOffer.Location = new Point(466, 31);
			this.lbOffer.Name = "lbOffer";
			this.lbOffer.Size = new Size(29, 14);
			this.lbOffer.TabIndex = 4;
			this.lbOffer.Text = "จับคู่";
			this.lbBid.AutoSize = true;
			this.lbBid.Location = new Point(218, 31);
			this.lbBid.Name = "lbBid";
			this.lbBid.Size = new Size(19, 14);
			this.lbBid.TabIndex = 3;
			this.lbBid.Text = "ใส่";
			this.label7.AutoSize = true;
			this.label7.BackColor = Color.Black;
			this.label7.ForeColor = Color.White;
			this.label7.Location = new Point(301, 31);
			this.label7.Name = "label7";
			this.label7.Padding = new Padding(0, 0, 1, 1);
			this.label7.Size = new Size(34, 15);
			this.label7.TabIndex = 2;
			this.label7.Text = "Text";
			this.label6.AutoSize = true;
			this.label6.BackColor = Color.Red;
			this.label6.ForeColor = Color.White;
			this.label6.Location = new Point(428, 31);
			this.label6.Name = "label6";
			this.label6.Padding = new Padding(0, 0, 1, 1);
			this.label6.Size = new Size(34, 15);
			this.label6.TabIndex = 1;
			this.label6.Text = "Text";
			this.label1.AutoSize = true;
			this.label1.BackColor = Color.FromArgb(0, 192, 0);
			this.label1.ForeColor = Color.White;
			this.label1.Location = new Point(178, 31);
			this.label1.Name = "label1";
			this.label1.Padding = new Padding(0, 0, 1, 1);
			this.label1.Size = new Size(34, 15);
			this.label1.TabIndex = 0;
			this.label1.Text = "Text";
			this.gbFont.Controls.Add(this.cbDefaultFontStyle);
			this.gbFont.Controls.Add(this.cbDefaultFontSize);
			this.gbFont.Controls.Add(this.cbDefaultFontName);
			this.gbFont.Controls.Add(this.lbFontSize);
			this.gbFont.Controls.Add(this.lbFontStyle);
			this.gbFont.Controls.Add(this.lbNameFont);
			this.gbFont.Location = new Point(13, 179);
			this.gbFont.Name = "gbFont";
			this.gbFont.Size = new Size(554, 60);
			this.gbFont.TabIndex = 5;
			this.gbFont.TabStop = false;
			this.gbFont.Text = "ตัวอักษร";
			this.cbDefaultFontStyle.DropDownStyle = ComboBoxStyle.DropDownList;
			this.cbDefaultFontStyle.FormattingEnabled = true;
			this.cbDefaultFontStyle.Items.AddRange(new object[]
			{
				"Regular",
				"Bold"
			});
			this.cbDefaultFontStyle.Location = new Point(282, 25);
			this.cbDefaultFontStyle.Name = "cbDefaultFontStyle";
			this.cbDefaultFontStyle.Size = new Size(110, 22);
			this.cbDefaultFontStyle.TabIndex = 13;
			this.cbDefaultFontSize.DropDownStyle = ComboBoxStyle.DropDownList;
			this.cbDefaultFontSize.FormattingEnabled = true;
			this.cbDefaultFontSize.Items.AddRange(new object[]
			{
				"7",
				"8",
				"9",
				"10",
				"11",
				"12",
				"13",
				"14",
				"15"
			});
			this.cbDefaultFontSize.Location = new Point(449, 25);
			this.cbDefaultFontSize.Name = "cbDefaultFontSize";
			this.cbDefaultFontSize.Size = new Size(44, 22);
			this.cbDefaultFontSize.TabIndex = 12;
			this.cbDefaultFontName.DropDownStyle = ComboBoxStyle.DropDownList;
			this.cbDefaultFontName.FormattingEnabled = true;
			this.cbDefaultFontName.Items.AddRange(new object[]
			{
				"Microsoft Sans Serif",
				"Tahoma",
				"Arial Narrow",
				"Segoe UI"
			});
			this.cbDefaultFontName.Location = new Point(51, 25);
			this.cbDefaultFontName.Name = "cbDefaultFontName";
			this.cbDefaultFontName.Size = new Size(169, 22);
			this.cbDefaultFontName.TabIndex = 11;
			this.lbFontSize.AutoSize = true;
			this.lbFontSize.Location = new Point(404, 28);
			this.lbFontSize.Name = "lbFontSize";
			this.lbFontSize.Size = new Size(34, 14);
			this.lbFontSize.TabIndex = 8;
			this.lbFontSize.Text = "ขนาด";
			this.lbFontStyle.AutoSize = true;
			this.lbFontStyle.Location = new Point(230, 29);
			this.lbFontStyle.Name = "lbFontStyle";
			this.lbFontStyle.Size = new Size(43, 14);
			this.lbFontStyle.TabIndex = 7;
			this.lbFontStyle.Text = "รุปแบบ";
			this.lbNameFont.AutoSize = true;
			this.lbNameFont.Location = new Point(10, 28);
			this.lbNameFont.Name = "lbNameFont";
			this.lbNameFont.Size = new Size(22, 14);
			this.lbNameFont.TabIndex = 6;
			this.lbNameFont.Text = "ชื่อ";
			this.tabControl1.Controls.Add(this.tpGeneral);
			this.tabControl1.Controls.Add(this.tabViewOrders);
			this.tabControl1.Controls.Add(this.tabHotkey);
			this.tabControl1.Controls.Add(this.tabConnection);
			this.tabControl1.Controls.Add(this.tabPagePassword);
			this.tabControl1.Controls.Add(this.tabPagePincode);
			this.tabControl1.Controls.Add(this.tabPageError);
			this.tabControl1.Location = new Point(0, 1);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new Size(586, 382);
			this.tabControl1.TabIndex = 4;
			this.tabControl1.SelectedIndexChanged += new EventHandler(this.tabControl1_SelectedIndexChanged);
			this.tpGeneral.Controls.Add(this.gbGeneral);
			this.tpGeneral.Location = new Point(4, 23);
			this.tpGeneral.Name = "tpGeneral";
			this.tpGeneral.Padding = new Padding(3);
			this.tpGeneral.Size = new Size(578, 355);
			this.tpGeneral.TabIndex = 0;
			this.tpGeneral.Text = "General";
			this.tpGeneral.UseVisualStyleBackColor = true;
			this.tabViewOrders.Controls.Add(this.cbMaxViewOrderRows);
			this.tabViewOrders.Controls.Add(this.lbAutoGetOrderInfoInterval);
			this.tabViewOrders.Controls.Add(this.lbPullBuySell);
			this.tabViewOrders.Controls.Add(this.lbMaxViewOrderRows);
			this.tabViewOrders.Location = new Point(4, 23);
			this.tabViewOrders.Name = "tabViewOrders";
			this.tabViewOrders.Padding = new Padding(3);
			this.tabViewOrders.Size = new Size(578, 355);
			this.tabViewOrders.TabIndex = 10;
			this.tabViewOrders.Text = "View Orders";
			this.tabViewOrders.UseVisualStyleBackColor = true;
			this.cbMaxViewOrderRows.DropDownStyle = ComboBoxStyle.DropDownList;
			this.cbMaxViewOrderRows.FormattingEnabled = true;
			this.cbMaxViewOrderRows.Items.AddRange(new object[]
			{
				"20",
				"30",
				"50",
				"100",
				"200",
				"300",
				"400",
				"500",
				"1000",
				"2000",
				"5000",
				"9999"
			});
			this.cbMaxViewOrderRows.Location = new Point(450, 30);
			this.cbMaxViewOrderRows.Name = "cbMaxViewOrderRows";
			this.cbMaxViewOrderRows.Size = new Size(59, 22);
			this.cbMaxViewOrderRows.TabIndex = 15;
			this.lbAutoGetOrderInfoInterval.AutoSize = true;
			this.lbAutoGetOrderInfoInterval.Font = new Font("Tahoma", 9f, FontStyle.Bold, GraphicsUnit.Point, 177);
			this.lbAutoGetOrderInfoInterval.ForeColor = Color.FromArgb(0, 0, 192);
			this.lbAutoGetOrderInfoInterval.Location = new Point(453, 76);
			this.lbAutoGetOrderInfoInterval.Name = "lbAutoGetOrderInfoInterval";
			this.lbAutoGetOrderInfoInterval.Size = new Size(15, 14);
			this.lbAutoGetOrderInfoInterval.TabIndex = 13;
			this.lbAutoGetOrderInfoInterval.Text = "0";
			this.lbAutoGetOrderInfoInterval.TextAlign = ContentAlignment.MiddleCenter;
			this.lbPullBuySell.AutoSize = true;
			this.lbPullBuySell.Location = new Point(44, 76);
			this.lbPullBuySell.Name = "lbPullBuySell";
			this.lbPullBuySell.Size = new Size(314, 14);
			this.lbPullBuySell.TabIndex = 5;
			this.lbPullBuySell.Text = "ดึงข้อมูลรายซื้อ/ขายแบบอัตโนมัติหลังส่งคำสั่งเป็นเวลา (วินาที)";
			this.lbMaxViewOrderRows.AutoSize = true;
			this.lbMaxViewOrderRows.Location = new Point(44, 30);
			this.lbMaxViewOrderRows.Name = "lbMaxViewOrderRows";
			this.lbMaxViewOrderRows.Size = new Size(138, 14);
			this.lbMaxViewOrderRows.TabIndex = 0;
			this.lbMaxViewOrderRows.Text = "จำนวนรายการสูงสุดต่อหน้า";
			this.tabHotkey.Controls.Add(this.templateTreeMenus1);
			this.tabHotkey.Location = new Point(4, 23);
			this.tabHotkey.Name = "tabHotkey";
			this.tabHotkey.Padding = new Padding(3);
			this.tabHotkey.Size = new Size(578, 355);
			this.tabHotkey.TabIndex = 11;
			this.tabHotkey.Text = "HotKey";
			this.tabHotkey.UseVisualStyleBackColor = true;
			this.tabConnection.Controls.Add(this.groupBox1);
			this.tabConnection.Controls.Add(this.gbProtocal);
			this.tabConnection.Location = new Point(4, 23);
			this.tabConnection.Name = "tabConnection";
			this.tabConnection.Padding = new Padding(3);
			this.tabConnection.Size = new Size(578, 355);
			this.tabConnection.TabIndex = 6;
			this.tabConnection.Text = "Connection";
			this.tabConnection.UseVisualStyleBackColor = true;
			this.groupBox1.Controls.Add(this.pnlProxy);
			this.groupBox1.Location = new Point(27, 138);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new Size(537, 208);
			this.groupBox1.TabIndex = 1;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Proxy";
			this.pnlProxy.Controls.Add(this.txtPassword);
			this.pnlProxy.Controls.Add(this.txtPort);
			this.pnlProxy.Controls.Add(this.txtUserName);
			this.pnlProxy.Controls.Add(this.label4);
			this.pnlProxy.Controls.Add(this.txtAddress);
			this.pnlProxy.Controls.Add(this.label5);
			this.pnlProxy.Controls.Add(this.label3);
			this.pnlProxy.Controls.Add(this.label2);
			this.pnlProxy.Location = new Point(17, 19);
			this.pnlProxy.Name = "pnlProxy";
			this.pnlProxy.Size = new Size(418, 181);
			this.pnlProxy.TabIndex = 2;
			this.txtPassword.BorderStyle = BorderStyle.FixedSingle;
			this.txtPassword.Location = new Point(130, 98);
			this.txtPassword.Name = "txtPassword";
			this.txtPassword.PasswordChar = '*';
			this.txtPassword.Size = new Size(178, 22);
			this.txtPassword.TabIndex = 9;
			this.txtPort.BorderStyle = BorderStyle.FixedSingle;
			this.txtPort.Location = new Point(130, 40);
			this.txtPort.Name = "txtPort";
			this.txtPort.Size = new Size(178, 22);
			this.txtPort.TabIndex = 7;
			this.txtUserName.BorderStyle = BorderStyle.FixedSingle;
			this.txtUserName.Location = new Point(130, 70);
			this.txtUserName.Name = "txtUserName";
			this.txtUserName.Size = new Size(178, 22);
			this.txtUserName.TabIndex = 8;
			this.label4.AutoSize = true;
			this.label4.Location = new Point(60, 102);
			this.label4.Name = "label4";
			this.label4.Size = new Size(53, 14);
			this.label4.TabIndex = 3;
			this.label4.Text = "Pasword";
			this.txtAddress.BorderStyle = BorderStyle.FixedSingle;
			this.txtAddress.Location = new Point(130, 12);
			this.txtAddress.Name = "txtAddress";
			this.txtAddress.Size = new Size(178, 22);
			this.txtAddress.TabIndex = 6;
			this.label5.AutoSize = true;
			this.label5.Location = new Point(40, 74);
			this.label5.Name = "label5";
			this.label5.Size = new Size(66, 14);
			this.label5.TabIndex = 2;
			this.label5.Text = "User Name";
			this.label3.AutoSize = true;
			this.label3.Location = new Point(88, 46);
			this.label3.Name = "label3";
			this.label3.Size = new Size(30, 14);
			this.label3.TabIndex = 2;
			this.label3.Text = "Port";
			this.label2.AutoSize = true;
			this.label2.Location = new Point(62, 18);
			this.label2.Name = "label2";
			this.label2.Size = new Size(50, 14);
			this.label2.TabIndex = 1;
			this.label2.Text = "Address";
			this.gbProtocal.Controls.Add(this.lsbTunnelServer);
			this.gbProtocal.Controls.Add(this.rdoPoll);
			this.gbProtocal.Controls.Add(this.rdoPush);
			this.gbProtocal.Location = new Point(28, 13);
			this.gbProtocal.Name = "gbProtocal";
			this.gbProtocal.Size = new Size(536, 118);
			this.gbProtocal.TabIndex = 3;
			this.gbProtocal.TabStop = false;
			this.gbProtocal.Text = "Connection Protocal";
			this.lsbTunnelServer.FormattingEnabled = true;
			this.lsbTunnelServer.ItemHeight = 14;
			this.lsbTunnelServer.Location = new Point(135, 27);
			this.lsbTunnelServer.Name = "lsbTunnelServer";
			this.lsbTunnelServer.Size = new Size(299, 46);
			this.lsbTunnelServer.TabIndex = 8;
			this.rdoPoll.AutoSize = true;
			this.rdoPoll.Enabled = false;
			this.rdoPoll.Location = new Point(55, 55);
			this.rdoPoll.Name = "rdoPoll";
			this.rdoPoll.Size = new Size(52, 18);
			this.rdoPoll.TabIndex = 7;
			this.rdoPoll.Text = "PULL";
			this.rdoPoll.UseVisualStyleBackColor = true;
			this.rdoPush.AutoSize = true;
			this.rdoPush.Checked = true;
			this.rdoPush.Enabled = false;
			this.rdoPush.Location = new Point(55, 28);
			this.rdoPush.Name = "rdoPush";
			this.rdoPush.Size = new Size(55, 18);
			this.rdoPush.TabIndex = 6;
			this.rdoPush.TabStop = true;
			this.rdoPush.Text = "PUSH";
			this.rdoPush.UseVisualStyleBackColor = true;
			this.tabPagePassword.Controls.Add(this.pwdEntryChangePassword);
			this.tabPagePassword.Controls.Add(this.lblPwdDescription);
			this.tabPagePassword.Location = new Point(4, 23);
			this.tabPagePassword.Name = "tabPagePassword";
			this.tabPagePassword.Padding = new Padding(3);
			this.tabPagePassword.Size = new Size(578, 355);
			this.tabPagePassword.TabIndex = 7;
			this.tabPagePassword.Text = "Password";
			this.tabPagePassword.UseVisualStyleBackColor = true;
			this.lblPwdDescription.ForeColor = Color.Blue;
			this.lblPwdDescription.Location = new Point(13, 128);
			this.lblPwdDescription.Name = "lblPwdDescription";
			this.lblPwdDescription.Size = new Size(558, 103);
			this.lblPwdDescription.TabIndex = 7;
			this.lblPwdDescription.Text = "    รูปแบบการกำหนด Password\r\n\r\n1. รหัสต้องมีความยาวไม่น้อยกว่า 6 ตัว\r\n2. รหัสต้องมีตัวอักษร A-Z หรือ a-z , 0-9 \r\n3. รหัสต้องมีอักขระพิเศษ เช่น @ , ! ,# ,* , |\r\n \r\n     ตัวอย่าง เช่น   Sti@123\r\n   \r\n ";
			this.tabPagePincode.Controls.Add(this.pwdEntryChangePincode);
			this.tabPagePincode.Controls.Add(this.lblPincodeDescription);
			this.tabPagePincode.Location = new Point(4, 23);
			this.tabPagePincode.Name = "tabPagePincode";
			this.tabPagePincode.Padding = new Padding(3);
			this.tabPagePincode.Size = new Size(578, 355);
			this.tabPagePincode.TabIndex = 8;
			this.tabPagePincode.Text = "Pincode";
			this.tabPagePincode.UseVisualStyleBackColor = true;
			this.lblPincodeDescription.ForeColor = Color.Blue;
			this.lblPincodeDescription.Location = new Point(10, 136);
			this.lblPincodeDescription.Name = "lblPincodeDescription";
			this.lblPincodeDescription.Size = new Size(557, 103);
			this.lblPincodeDescription.TabIndex = 8;
			this.lblPincodeDescription.Text = "    รูปแบบการกำหนด Password\r\n\r\n1. รหัสต้องมีความยาวไม่น้อยกว่า 4 ตัว\r\n \r\n     ตัวอย่าง เช่น   Sti@123\r\n   \r\n ";
			this.tabPageError.Controls.Add(this.btnRefreshError);
			this.tabPageError.Controls.Add(this.btnClearErrorList);
			this.tabPageError.Controls.Add(this.gbDescription);
			this.tabPageError.Controls.Add(this.lsvError);
			this.tabPageError.Controls.Add(this.gbException);
			this.tabPageError.Location = new Point(4, 23);
			this.tabPageError.Name = "tabPageError";
			this.tabPageError.Padding = new Padding(3);
			this.tabPageError.Size = new Size(578, 355);
			this.tabPageError.TabIndex = 9;
			this.tabPageError.Text = "Error";
			this.tabPageError.UseVisualStyleBackColor = true;
			this.btnRefreshError.AutoSize = true;
			this.btnRefreshError.Location = new Point(410, 79);
			this.btnRefreshError.Name = "btnRefreshError";
			this.btnRefreshError.Size = new Size(75, 28);
			this.btnRefreshError.TabIndex = 14;
			this.btnRefreshError.Text = "Refresh";
			this.btnRefreshError.UseVisualStyleBackColor = true;
			this.btnRefreshError.Click += new EventHandler(this.btnRefreshError_Click);
			this.btnClearErrorList.AutoSize = true;
			this.btnClearErrorList.Location = new Point(491, 79);
			this.btnClearErrorList.Name = "btnClearErrorList";
			this.btnClearErrorList.Size = new Size(80, 28);
			this.btnClearErrorList.TabIndex = 13;
			this.btnClearErrorList.Text = "Clear";
			this.btnClearErrorList.UseVisualStyleBackColor = true;
			this.btnClearErrorList.Click += new EventHandler(this.btnClearErrorList_Click);
			this.gbDescription.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.gbDescription.Controls.Add(this.lnkClose);
			this.gbDescription.Controls.Add(this.txtError);
			this.gbDescription.Location = new Point(7, 116);
			this.gbDescription.Name = "gbDescription";
			this.gbDescription.Size = new Size(572, 235);
			this.gbDescription.TabIndex = 12;
			this.gbDescription.TabStop = false;
			this.gbDescription.Visible = false;
			this.lnkClose.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
			this.lnkClose.AutoSize = true;
			this.lnkClose.Font = new Font("Tahoma", 9.75f, FontStyle.Bold, GraphicsUnit.Point, 222);
			this.lnkClose.LinkColor = Color.Red;
			this.lnkClose.Location = new Point(518, -1);
			this.lnkClose.Name = "lnkClose";
			this.lnkClose.Size = new Size(42, 16);
			this.lnkClose.TabIndex = 1;
			this.lnkClose.TabStop = true;
			this.lnkClose.Text = "Close";
			this.lnkClose.LinkClicked += new LinkLabelLinkClickedEventHandler(this.lnkClose_LinkClicked);
			this.txtError.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.txtError.BackColor = Color.White;
			this.txtError.BorderStyle = BorderStyle.FixedSingle;
			this.txtError.Font = new Font("Tahoma", 9f, FontStyle.Regular, GraphicsUnit.Point, 222);
			this.txtError.Location = new Point(7, 18);
			this.txtError.Multiline = true;
			this.txtError.Name = "txtError";
			this.txtError.ReadOnly = true;
			this.txtError.ScrollBars = ScrollBars.Vertical;
			this.txtError.Size = new Size(557, 207);
			this.txtError.TabIndex = 0;
			this.lsvError.BackColor = Color.Black;
			this.lsvError.BorderStyle = BorderStyle.FixedSingle;
			this.lsvError.Columns.AddRange(new ColumnHeader[]
			{
				this.colTime,
				this.colDescription,
				this.colModule,
				this.colFunction
			});
			this.lsvError.Font = new Font("Tahoma", 9f, FontStyle.Regular, GraphicsUnit.Point, 222);
			this.lsvError.ForeColor = Color.White;
			this.lsvError.FullRowSelect = true;
			this.lsvError.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.lsvError.Location = new Point(6, 112);
			this.lsvError.Name = "lsvError";
			this.lsvError.Size = new Size(574, 229);
			this.lsvError.TabIndex = 11;
			this.lsvError.UseCompatibleStateImageBehavior = false;
			this.lsvError.View = View.Details;
			this.lsvError.MouseDoubleClick += new MouseEventHandler(this.lsvError_MouseDoubleClick);
			this.colTime.Text = "Times";
			this.colTime.Width = 70;
			this.colDescription.Text = "Description";
			this.colDescription.Width = 327;
			this.colModule.Text = "Module";
			this.colModule.Width = 80;
			this.colFunction.Text = "Function";
			this.colFunction.Width = 70;
			this.gbException.Controls.Add(this.chkIsWriteLog);
			this.gbException.Location = new Point(6, 11);
			this.gbException.Name = "gbException";
			this.gbException.Size = new Size(569, 62);
			this.gbException.TabIndex = 10;
			this.gbException.TabStop = false;
			this.gbException.Text = "การจัดการข้อผิดพลาด";
			this.chkIsWriteLog.AutoSize = true;
			this.chkIsWriteLog.Location = new Point(30, 30);
			this.chkIsWriteLog.Name = "chkIsWriteLog";
			this.chkIsWriteLog.Size = new Size(231, 18);
			this.chkIsWriteLog.TabIndex = 9;
			this.chkIsWriteLog.Text = "เก็บบันทึกรายการข้อผิดพลาดแบบอัตโนมัติ";
			this.chkIsWriteLog.UseVisualStyleBackColor = true;
			this.colShortcut.Text = "Shortcut";
			this.colShortcut.Width = 147;
			this.checkBoxShowSector.AutoSize = true;
			this.checkBoxShowSector.Enabled = false;
			this.checkBoxShowSector.Location = new Point(66, 133);
			this.checkBoxShowSector.Name = "checkBoxShowSector";
			this.checkBoxShowSector.Size = new Size(144, 17);
			this.checkBoxShowSector.TabIndex = 7;
			this.checkBoxShowSector.Text = "Show Sector information.";
			this.checkBoxShowSector.UseVisualStyleBackColor = true;
			this.checkBoxShowSector.Visible = false;
			this.lbSpeedSecound.AutoSize = true;
			this.lbSpeedSecound.Location = new Point(132, 49);
			this.lbSpeedSecound.Name = "lbSpeedSecound";
			this.lbSpeedSecound.Size = new Size(34, 13);
			this.lbSpeedSecound.TabIndex = 9;
			this.lbSpeedSecound.Text = "Secs.";
			this.lbSpeedTime.AutoSize = true;
			this.lbSpeedTime.Location = new Point(18, 47);
			this.lbSpeedTime.Name = "lbSpeedTime";
			this.lbSpeedTime.Size = new Size(67, 13);
			this.lbSpeedTime.TabIndex = 7;
			this.lbSpeedTime.Text = "Speed Time:";
			this.checkBoxShowTop5Loser.AutoSize = true;
			this.checkBoxShowTop5Loser.Enabled = false;
			this.checkBoxShowTop5Loser.Location = new Point(100, 133);
			this.checkBoxShowTop5Loser.Name = "checkBoxShowTop5Loser";
			this.checkBoxShowTop5Loser.Size = new Size(124, 17);
			this.checkBoxShowTop5Loser.TabIndex = 4;
			this.checkBoxShowTop5Loser.Text = "Show top 5 of Loser.";
			this.checkBoxShowTop5Loser.UseVisualStyleBackColor = true;
			this.checkBoxShowTop5Loser.Visible = false;
			this.checkBoxShowTop5Gainer.AutoSize = true;
			this.checkBoxShowTop5Gainer.Enabled = false;
			this.checkBoxShowTop5Gainer.Location = new Point(100, 112);
			this.checkBoxShowTop5Gainer.Name = "checkBoxShowTop5Gainer";
			this.checkBoxShowTop5Gainer.Size = new Size(129, 17);
			this.checkBoxShowTop5Gainer.TabIndex = 3;
			this.checkBoxShowTop5Gainer.Text = "Show top 5 of Gainer.";
			this.checkBoxShowTop5Gainer.UseVisualStyleBackColor = true;
			this.checkBoxShowTop5Gainer.Visible = false;
			this.checkBoxShowTop5MostActive.AutoSize = true;
			this.checkBoxShowTop5MostActive.Enabled = false;
			this.checkBoxShowTop5MostActive.Location = new Point(43, 133);
			this.checkBoxShowTop5MostActive.Name = "checkBoxShowTop5MostActive";
			this.checkBoxShowTop5MostActive.Size = new Size(151, 17);
			this.checkBoxShowTop5MostActive.TabIndex = 2;
			this.checkBoxShowTop5MostActive.Text = "Show top 5 of MostActive.";
			this.checkBoxShowTop5MostActive.UseVisualStyleBackColor = true;
			this.checkBoxShowTop5MostActive.Visible = false;
			this.checkBoxShowIndustryInfo.AutoSize = true;
			this.checkBoxShowIndustryInfo.Enabled = false;
			this.checkBoxShowIndustryInfo.Location = new Point(43, 114);
			this.checkBoxShowIndustryInfo.Name = "checkBoxShowIndustryInfo";
			this.checkBoxShowIndustryInfo.Size = new Size(150, 17);
			this.checkBoxShowIndustryInfo.TabIndex = 1;
			this.checkBoxShowIndustryInfo.Text = "Show Industry information.";
			this.checkBoxShowIndustryInfo.UseVisualStyleBackColor = true;
			this.checkBoxShowIndustryInfo.Visible = false;
			this.rdoLangThai.AutoSize = true;
			this.rdoLangThai.Checked = true;
			this.rdoLangThai.Location = new Point(31, 397);
			this.rdoLangThai.Name = "rdoLangThai";
			this.rdoLangThai.Size = new Size(47, 18);
			this.rdoLangThai.TabIndex = 10;
			this.rdoLangThai.TabStop = true;
			this.rdoLangThai.Text = "ไทย";
			this.rdoLangThai.UseVisualStyleBackColor = true;
			this.rdoLangThai.CheckedChanged += new EventHandler(this.rdoLangThai_CheckedChanged);
			this.rdoLangEnglish.AutoSize = true;
			this.rdoLangEnglish.Location = new Point(96, 397);
			this.rdoLangEnglish.Name = "rdoLangEnglish";
			this.rdoLangEnglish.Size = new Size(62, 18);
			this.rdoLangEnglish.TabIndex = 11;
			this.rdoLangEnglish.Text = "English";
			this.rdoLangEnglish.UseVisualStyleBackColor = true;
			this.rdoLangEnglish.CheckedChanged += new EventHandler(this.rdoLangThai_CheckedChanged);
			this.colorDialog1.FullOpen = true;
			this.templateTreeMenus1.Font = new Font("Microsoft Sans Serif", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 222);
			this.templateTreeMenus1.Location = new Point(4, 4);
			this.templateTreeMenus1.Margin = new Padding(4);
			this.templateTreeMenus1.Name = "templateTreeMenus1";
			this.templateTreeMenus1.Size = new Size(313, 347);
			this.templateTreeMenus1.TabIndex = 0;
			this.templateTreeMenus1.ToolbarDocking = DockStyle.Left;
			this.pwdEntryChangePassword.AutoSize = true;
			this.pwdEntryChangePassword.ConfirmPasswordLable = "Confirm password : ";
			this.pwdEntryChangePassword.Dock = DockStyle.Top;
			this.pwdEntryChangePassword.Enabled = false;
			this.pwdEntryChangePassword.IsCustomer = true;
			this.pwdEntryChangePassword.IsPincode = false;
			this.pwdEntryChangePassword.Location = new Point(3, 3);
			this.pwdEntryChangePassword.Name = "pwdEntryChangePassword";
			this.pwdEntryChangePassword.PasswordLable = "New password : ";
			this.pwdEntryChangePassword.Size = new Size(572, 116);
			this.pwdEntryChangePassword.TabIndex = 8;
			this.toolTipSystemOption.SetToolTip(this.pwdEntryChangePassword, "เปลี่ยนรหัสผ่านของผู้ใช้งาน Internet trader.");
			this.pwdEntryChangePincode.AutoSize = true;
			this.pwdEntryChangePincode.ConfirmPasswordLable = "Confirm Pincode : ";
			this.pwdEntryChangePincode.Dock = DockStyle.Top;
			this.pwdEntryChangePincode.IsCustomer = true;
			this.pwdEntryChangePincode.IsPincode = true;
			this.pwdEntryChangePincode.Location = new Point(3, 3);
			this.pwdEntryChangePincode.Name = "pwdEntryChangePincode";
			this.pwdEntryChangePincode.OldPasswordLable = "Old Pincode : ";
			this.pwdEntryChangePincode.PasswordLable = "New Pincode : ";
			this.pwdEntryChangePincode.Size = new Size(572, 116);
			this.pwdEntryChangePincode.TabIndex = 9;
			base.AutoScaleDimensions = new SizeF(7f, 14f);
			base.AutoScaleMode = AutoScaleMode.Font;
			this.AutoSize = true;
			base.ClientSize = new Size(588, 428);
			base.ControlBox = false;
			base.Controls.Add(this.rdoLangEnglish);
			base.Controls.Add(this.rdoLangThai);
			base.Controls.Add(this.btnCancel);
			base.Controls.Add(this.btnOk);
			base.Controls.Add(this.tabControl1);
			this.Font = new Font("Tahoma", 9f, FontStyle.Regular, GraphicsUnit.Point, 177);
			base.FormBorderStyle = FormBorderStyle.FixedDialog;
			base.HelpButton = true;
			base.KeyPreview = true;
			base.MaximizeBox = false;
			base.Name = "frmSystemOption";
			base.StartPosition = FormStartPosition.Manual;
			this.Text = "System Option";
			base.TopMost = true;
			base.Shown += new EventHandler(this.frmSystemOption_Shown);
			base.FormClosing += new FormClosingEventHandler(this.frmSystemOption_FormClosing);
			this.gbGeneral.ResumeLayout(false);
			this.gbTable.ResumeLayout(false);
			this.gbTable.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.gbFont.ResumeLayout(false);
			this.gbFont.PerformLayout();
			this.tabControl1.ResumeLayout(false);
			this.tpGeneral.ResumeLayout(false);
			this.tabViewOrders.ResumeLayout(false);
			this.tabViewOrders.PerformLayout();
			this.tabHotkey.ResumeLayout(false);
			this.tabConnection.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.pnlProxy.ResumeLayout(false);
			this.pnlProxy.PerformLayout();
			this.gbProtocal.ResumeLayout(false);
			this.gbProtocal.PerformLayout();
			this.tabPagePassword.ResumeLayout(false);
			this.tabPagePassword.PerformLayout();
			this.tabPagePincode.ResumeLayout(false);
			this.tabPagePincode.PerformLayout();
			this.tabPageError.ResumeLayout(false);
			this.tabPageError.PerformLayout();
			this.gbDescription.ResumeLayout(false);
			this.gbDescription.PerformLayout();
			this.gbException.ResumeLayout(false);
			this.gbException.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		public frmSystemOption()
		{
			try
			{
				this.InitializeComponent();
				this.pwdEntryChangePassword.MinimumPasswordLength = ApplicationInfo.MinPasswordLength;
				this.pwdEntryChangePincode.MinimumPincodeLength = ApplicationInfo.MinPincodeLength;
				this.lblPwdDescription.Text = string.Format(this.passwordDescription, this.pwdEntryChangePassword.MinimumPasswordLength);
				this.lblPincodeDescription.Text = string.Format(this.pincodeDescription, this.pwdEntryChangePincode.MinimumPincodeLength);
				if (ApplicationInfo.UserLoginMode == "T")
				{
					this.pwdEntryChangePassword.IsCustomer = false;
				}
				else
				{
					this.pwdEntryChangePassword.IsCustomer = true;
				}
				this.tabControl1.TabPages.Remove(this.tabPagePassword);
				this.tabControl1.TabPages.Remove(this.tabPagePincode);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		private void frmSystemOption_Shown(object sender, EventArgs e)
		{
			this.LoadProfileConfig();
			this.ChangeLanguage();
			this.templateTreeMenus1.Initial();
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		private void frmSystemOption_FormClosing(object sender, FormClosingEventArgs e)
		{
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		private void btnOk_Click(object sender, EventArgs e)
		{
			try
			{
				this.SaveProxySetting();
				FontStyle style = FontStyle.Regular;
				if (this.cbDefaultFontStyle.Text == "Bold")
				{
					style = FontStyle.Bold;
				}
				Font font = new Font(this.cbDefaultFontName.Text, (float)Convert.ToInt32(this.cbDefaultFontSize.Text), style);
				if (!font.Equals(Settings.Default.Default_Font))
				{
					Settings.Default.Default_Font = font;
					TemplateManager.Instance.CurrentActiveTemplateView.SetFont();
				}
				int num;
				int.TryParse(this.cbMaxViewOrderRows.Text, out num);
				if (num >= 20 && num <= 9999)
				{
					Settings.Default.ViewOrderRows = num;
				}
				Settings.Default.IsWriteErrorLog = this.chkIsWriteLog.Checked;
				this.SetBlinkTP2AllForm();
				if (this.rdbFontColorSoftStyle.Checked)
				{
					ApplicationInfo.IsFrontSoftStyle = true;
				}
				else if (this.rdbFontColorOriginalStyle.Checked)
				{
					ApplicationInfo.IsFrontSoftStyle = false;
				}
				Settings.Default.Save();
			}
			catch (Exception ex)
			{
				this.ShowError("SaveProfileConfig", ex);
			}
			base.Close();
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		private void btnCancel_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		private void btnFontDialog_Click(object sender, EventArgs e)
		{
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		private void LoadProfileConfig()
		{
			if (base.InvokeRequired)
			{
				base.Invoke(new MethodInvoker(this.LoadProfileConfig));
			}
			else
			{
				try
				{
					this.defFont = Settings.Default.Default_Font;
					this.cbDefaultFontName.Text = Settings.Default.Default_Font.Name;
					this.cbDefaultFontStyle.Text = Settings.Default.Default_Font.Style.ToString();
					this.cbDefaultFontSize.Text = Math.Round((double)Settings.Default.Default_Font.Size).ToString();
					if (ApplicationInfo.IsFrontSoftStyle)
					{
						this.rdbFontColorSoftStyle.Checked = true;
					}
					else
					{
						this.rdbFontColorOriginalStyle.Checked = true;
					}
					if (ApplicationInfo.UserLoginMode == "I")
					{
						this.tabControl1.TabPages.Remove(this.tabPagePassword);
						this.tabControl1.TabPages.Remove(this.tabPagePincode);
					}
					else if (ApplicationInfo.UserLoginMode == "C")
					{
						this.lblPwdDescription.Visible = true;
						this.lblPincodeDescription.Visible = true;
					}
					else if (ApplicationInfo.UserLoginMode == "T")
					{
						this.lblPwdDescription.Visible = false;
						this.tabControl1.TabPages.Remove(this.tabPagePincode);
					}
					this.chkIsWriteLog.Checked = Settings.Default.IsWriteErrorLog;
					this.InitializeServerList();
					this.rdoPush.Checked = ApplicationInfo.IsPushMode;
					this.rdoPoll.Checked = !this.rdoPush.Checked;
					this.cbMaxViewOrderRows.Text = Settings.Default.ViewOrderRows.ToString();
					this.lbAutoGetOrderInfoInterval.Text = ApplicationInfo.AutoGetOrderInfoInterval.ToString();
					this.cbBidOfferColorBlink.Checked = ApplicationInfo.IsSupportTPBlinkColor;
					this.lbHeaderColor.BackColor = Settings.Default.HeaderBackGColor;
					this.lbGridColor.BackColor = Settings.Default.GridColor;
					this.lbFontHeaderColor.BackColor = Settings.Default.HeaderFontColor;
					this.OpenProxySetting();
				}
				catch (Exception ex)
				{
					this.ShowError("LoadProfileConfig", ex);
				}
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		private string DisplayKeyFormat(Keys key)
		{
			string[] array = key.ToString().Split(new char[]
			{
				','
			});
			string text = (array.Length > 1) ? string.Format("{0}+{1}", array[1], array[0]) : array[0];
			return text.Trim();
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				if (this.tabControl1.SelectedTab.Name.ToLower() == "tabpagepincode")
				{
					this.pwdEntryChangePassword.IsPincode = true;
				}
				else
				{
					this.pwdEntryChangePassword.IsPincode = false;
				}
				if (this.tabControl1.SelectedTab == this.tabPageError)
				{
					if (this.lsvError.Items.Count != ExceptionManager.Items.Count)
					{
						this.ShowErrorToGrid();
					}
				}
				else if (this.tabControl1.SelectedTab == this.tabConnection)
				{
					if (ApplicationInfo.IsPushMode)
					{
						if (ApplicationInfo.TunnelCounter <= this.lsbTunnelServer.Items.Count)
						{
							this.lsbTunnelServer.SelectedIndex = ApplicationInfo.TunnelCounter;
						}
					}
					else
					{
						this.lsbTunnelServer.SelectedIndex = -1;
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError("tabControl1_SelectedIndexChanged", ex);
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		private void lnkOpenErrorLog_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		private void ShowError(string methodName, Exception ex)
		{
			ExceptionManager.Show(new ErrorItem(DateTime.Now, base.Name, methodName, ex.Message, ex.ToString()));
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		private void InitializeServerList()
		{
			try
			{
				this.lsbTunnelServer.Items.Clear();
				int num = 0;
				foreach (TunnelInfo current in ApplicationInfo.TunnelHosts)
				{
					this.lsbTunnelServer.Items.Add(string.Concat(new object[]
					{
						"Node ",
						++num,
						" ==> IP : ",
						current.HostIP,
						" Port : ",
						current.Port
					}));
				}
			}
			catch (Exception ex)
			{
				this.ShowError("InitializeServerList", ex);
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		private void OpenProxySetting()
		{
			string registryStringValue = BusinessServiceFactory.GetRegistryStringValue(Registry.CurrentUser, "Software\\Microsoft\\Windows\\CurrentVersion\\Internet Settings", "ProxyEnable");
			bool arg_39_0 = !string.IsNullOrEmpty(registryStringValue) && registryStringValue == "1";
			if (!string.IsNullOrEmpty(Settings.Default.ProxyHost))
			{
				this.txtAddress.Text = Settings.Default.ProxyHost;
				this.txtPort.Text = Settings.Default.ProxyPort.ToString();
			}
			else
			{
				string registryStringValue2 = BusinessServiceFactory.GetRegistryStringValue(Registry.CurrentUser, "Software\\Microsoft\\Windows\\CurrentVersion\\Internet Settings", "ProxyServer");
				if (!string.IsNullOrEmpty(registryStringValue2))
				{
					string[] array = registryStringValue2.Split(new char[]
					{
						':'
					});
					if (array != null && array.Length > 1)
					{
						this.txtAddress.Text = array[0];
						this.txtPort.Text = array[1];
					}
				}
			}
			this.txtUserName.Text = Settings.Default.ProxyUsername;
			this.txtPassword.Text = Settings.Default.ProxyPassword;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		private void SaveProxySetting()
		{
			try
			{
				Settings.Default.ProxyHost = this.txtAddress.Text;
				Settings.Default.ProxyPort = (string.IsNullOrEmpty(this.txtPort.Text) ? 1080 : Convert.ToInt32(this.txtPort.Text));
				Settings.Default.ProxyUsername = this.txtUserName.Text;
				ApplicationInfo.ProxyPassword = this.txtPassword.Text;
				Settings.Default.ProxyPassword = this.txtPassword.Text;
			}
			catch (Exception ex)
			{
				this.ShowError("SaveProxySetting", ex);
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		private void ChangeLanguage()
		{
			if (base.InvokeRequired)
			{
				base.Invoke(new MethodInvoker(this.ChangeLanguage));
			}
			else
			{
				try
				{
					if (this.rdoLangThai.Checked)
					{
						this.lbNameFont.Text = "ชื่อ";
						this.lbFontStyle.Text = "รูปแบบ";
						this.lbFontSize.Text = "ขนาด";
						this.cbBidOfferColorBlink.Text = "เปิดการทำงาน";
						this.gbFont.Text = "ตัวอักษร";
						this.groupBox2.Text = "สีกระพริบบอกการเสนอซื้อ ยกเลิก หรือจับคู่";
						this.lbBid.Text = "ใส่";
						this.lbQuoteCancel.Text = "ถอน";
						this.lbOffer.Text = "จับคู่";
						this.lbMaxViewOrderRows.Text = "จำนวนรายการสูงสุดต่อหน้า";
						this.lbPullBuySell.Text = "ดึงข้อมูลรายการซื้อ/ขายแบบอัตโนมัติหลังส่งคำสั่งเป็นเวลา (วินาที)";
						this.gbException.Text = "การจัดการข้อผิดพลาด";
						this.chkIsWriteLog.Text = "เก็บบันทึกรายการข้อผิดพลาดแบบอัตโนมัติ";
						this.gbTable.Text = "ตาราง";
					}
					else
					{
						this.lbNameFont.Text = "Name";
						this.lbFontStyle.Text = "Style";
						this.lbFontSize.Text = "Size";
						this.cbBidOfferColorBlink.Text = "Active";
						this.gbFont.Text = "Font";
						this.groupBox2.Text = "Bid-Offer Bussiness Field Color";
						this.lbBid.Text = "Bid";
						this.lbQuoteCancel.Text = "Quote Cancel";
						this.lbOffer.Text = "Offer";
						this.lbMaxViewOrderRows.Text = "The max number of transaction per page";
						this.lbPullBuySell.Text = "Pull Buy/Sell data automatically after sending order for (seconds)";
						this.gbException.Text = "Error Management";
						this.chkIsWriteLog.Text = "Save Error transaction automatically.";
						this.gbTable.Text = "Table";
					}
				}
				catch
				{
				}
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		private void rdoLangThai_CheckedChanged(object sender, EventArgs e)
		{
			this.ChangeLanguage();
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		private void SetBlinkTP2AllForm()
		{
			try
			{
				ApplicationInfo.IsSupportTPBlinkColor = this.cbBidOfferColorBlink.Checked;
				if (TemplateManager.Instance.TemplateViews.ContainsKey("Market Watch"))
				{
					TemplateView templateView = TemplateManager.Instance.TemplateViews["Market Watch"];
					if (templateView != null)
					{
						(templateView.FormObj as frmMarketWatch).SetBlinkModeTopPrice();
					}
				}
				if (TemplateManager.Instance.TemplateViews.ContainsKey("Top BBO"))
				{
					TemplateView templateView = TemplateManager.Instance.TemplateViews["Top BBO"];
					if (templateView != null)
					{
						(templateView.FormObj as frmTopBBOs).SetBlinkModeTopPrice();
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError("SetBlinkTP2AllForm", ex);
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		private void btnRefreshError_Click(object sender, EventArgs e)
		{
			this.ShowErrorToGrid();
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		private void ShowErrorToGrid()
		{
			if (base.InvokeRequired)
			{
				base.Invoke(new MethodInvoker(this.ShowErrorToGrid));
			}
			else
			{
				try
				{
					this.lsvError.Items.Clear();
					foreach (ErrorItem current in ExceptionManager.Items)
					{
						string[] array = new string[4];
						string[] arg_7A_0 = array;
						int arg_7A_1 = 0;
						DateTime time = current.Time;
						arg_7A_0[arg_7A_1] = time.ToLongTimeString();
						array[1] = current.Information;
						array[2] = current.ModuleName;
						array[3] = current.Function;
						ListViewItem listViewItem = new ListViewItem(array);
						listViewItem.Tag = current.Description;
						this.lsvError.Items.Add(listViewItem);
					}
				}
				catch
				{
				}
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		private void btnClearErrorList_Click(object sender, EventArgs e)
		{
			try
			{
				ExceptionManager.Items.Clear();
				this.ShowErrorToGrid();
			}
			catch
			{
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		private void lsvError_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			try
			{
				if (this.lsvError.SelectedItems.Count > 0)
				{
					this.txtError.Text = string.Format("Time : {0}\r\nModule & Function : {1}-{2}\r\nInfomation : {3}\r\nDescription : {4}", new object[]
					{
						this.lsvError.SelectedItems[0].SubItems[0].Text,
						this.lsvError.SelectedItems[0].SubItems[2].Text,
						this.lsvError.SelectedItems[0].SubItems[3].Text,
						this.lsvError.SelectedItems[0].SubItems[1].Text,
						this.lsvError.SelectedItems[0].Tag
					});
					this.gbDescription.Visible = true;
				}
			}
			catch
			{
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		private void lnkClose_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			this.gbDescription.Visible = false;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		private void btnHeader_Click(object sender, EventArgs e)
		{
			try
			{
				this.colorDialog1.Color = Settings.Default.HeaderBackGColor;
				if (this.colorDialog1.ShowDialog() == DialogResult.OK)
				{
					Settings.Default.HeaderBackGColor = this.colorDialog1.Color;
					this.lbHeaderColor.BackColor = this.colorDialog1.Color;
					this.SetColorToControl();
				}
			}
			catch (Exception ex)
			{
				this.ShowError("btnHeader_Click", ex);
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		private void btnGridColor_Click(object sender, EventArgs e)
		{
			try
			{
				this.colorDialog1.Color = Settings.Default.GridColor;
				if (this.colorDialog1.ShowDialog() == DialogResult.OK)
				{
					Settings.Default.GridColor = this.colorDialog1.Color;
					this.lbGridColor.BackColor = this.colorDialog1.Color;
					this.SetColorToControl();
				}
			}
			catch (Exception ex)
			{
				this.ShowError("btnGridColor_Click", ex);
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		private void btnFontHeaderColor_Click(object sender, EventArgs e)
		{
			try
			{
				this.colorDialog1.Color = Settings.Default.HeaderFontColor;
				if (this.colorDialog1.ShowDialog() == DialogResult.OK)
				{
					Settings.Default.HeaderFontColor = this.colorDialog1.Color;
					this.lbFontHeaderColor.BackColor = this.colorDialog1.Color;
					this.SetColorToControl();
				}
			}
			catch (Exception ex)
			{
				this.ShowError("btnFontHeaderColor_Click", ex);
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		private void btnResetColor_Click(object sender, EventArgs e)
		{
			try
			{
				Settings.Default.HeaderBackGColor = Color.FromArgb(50, 50, 50);
				Settings.Default.HeaderFontColor = Color.LightGray;
				Settings.Default.GridColor = Color.FromArgb(45, 45, 45);
				this.lbGridColor.BackColor = Settings.Default.GridColor;
				this.lbFontHeaderColor.BackColor = Settings.Default.HeaderFontColor;
				this.lbHeaderColor.BackColor = Settings.Default.HeaderBackGColor;
				this.SetColorToControl();
			}
			catch (Exception ex)
			{
				this.ShowError("btnResetColor_Click", ex);
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		private void SetColorToControl()
		{
			try
			{
				((ClientBaseForm)TemplateManager.Instance.CurrentActiveTemplateView.FormObj).SetHeaderColor(true);
				TemplateManager.Instance.MainForm.SetHeaderToChild();
			}
			catch (Exception ex)
			{
				this.ShowError("SetColorToControl", ex);
			}
		}
	}
}
