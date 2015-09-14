using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace i2TradePlus
{
	public class frmStopDisclaimer : Form
	{
		private IContainer components = null;

		private GroupBox gbSavePingDisclaimer;

		private Button btnSavePinDisAgree;

		private Button btnSavePinAgree;

		private Label lbSavePin;

		private Panel panel1;

		private Label lbOrderString;

		private Label lbDisclaimer;

		private Panel panel2;

		private Button btnAccept;

		private Button btnDeny;

		private BackgroundWorker bgwCheckStopOrderAccept = null;

		private string _registerResult = string.Empty;

		private bool _isViewOnly = false;

		private Timer tmTest = null;

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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(frmStopDisclaimer));
			this.gbSavePingDisclaimer = new GroupBox();
			this.btnAccept = new Button();
			this.btnDeny = new Button();
			this.btnSavePinDisAgree = new Button();
			this.btnSavePinAgree = new Button();
			this.lbSavePin = new Label();
			this.panel1 = new Panel();
			this.panel2 = new Panel();
			this.lbDisclaimer = new Label();
			this.lbOrderString = new Label();
			this.gbSavePingDisclaimer.SuspendLayout();
			this.panel1.SuspendLayout();
			this.panel2.SuspendLayout();
			base.SuspendLayout();
			this.gbSavePingDisclaimer.BackColor = Color.FromArgb(224, 224, 224);
			this.gbSavePingDisclaimer.Controls.Add(this.btnAccept);
			this.gbSavePingDisclaimer.Controls.Add(this.btnDeny);
			this.gbSavePingDisclaimer.Controls.Add(this.btnSavePinDisAgree);
			this.gbSavePingDisclaimer.Controls.Add(this.btnSavePinAgree);
			this.gbSavePingDisclaimer.Controls.Add(this.lbSavePin);
			this.gbSavePingDisclaimer.Dock = DockStyle.Fill;
			this.gbSavePingDisclaimer.Font = new Font("Microsoft Sans Serif", 9f, FontStyle.Regular, GraphicsUnit.Point, 222);
			this.gbSavePingDisclaimer.Location = new Point(0, 0);
			this.gbSavePingDisclaimer.Name = "gbSavePingDisclaimer";
			this.gbSavePingDisclaimer.Size = new Size(640, 323);
			this.gbSavePingDisclaimer.TabIndex = 7;
			this.gbSavePingDisclaimer.TabStop = false;
			this.gbSavePingDisclaimer.Text = "Disclaimer";
			this.btnAccept.Font = new Font("Microsoft Sans Serif", 11f, FontStyle.Regular, GraphicsUnit.Point, 222);
			this.btnAccept.Location = new Point(389, 289);
			this.btnAccept.Name = "btnAccept";
			this.btnAccept.Size = new Size(125, 28);
			this.btnAccept.TabIndex = 5;
			this.btnAccept.Text = "ยอมรับเงื่อนไข";
			this.btnAccept.UseVisualStyleBackColor = true;
			this.btnAccept.Click += new EventHandler(this.btnAccept_Click);
			this.btnDeny.Font = new Font("Microsoft Sans Serif", 11f, FontStyle.Regular, GraphicsUnit.Point, 222);
			this.btnDeny.Location = new Point(534, 289);
			this.btnDeny.Name = "btnDeny";
			this.btnDeny.Size = new Size(100, 28);
			this.btnDeny.TabIndex = 4;
			this.btnDeny.Text = "ปฏิเสธ";
			this.btnDeny.UseVisualStyleBackColor = true;
			this.btnDeny.Click += new EventHandler(this.btnDeny_Click);
			this.btnSavePinDisAgree.Location = new Point(200, 72);
			this.btnSavePinDisAgree.Name = "btnSavePinDisAgree";
			this.btnSavePinDisAgree.Size = new Size(96, 23);
			this.btnSavePinDisAgree.TabIndex = 2;
			this.btnSavePinDisAgree.Text = "I DISAGREE";
			this.btnSavePinDisAgree.UseVisualStyleBackColor = true;
			this.btnSavePinAgree.Location = new Point(114, 72);
			this.btnSavePinAgree.Name = "btnSavePinAgree";
			this.btnSavePinAgree.Size = new Size(72, 23);
			this.btnSavePinAgree.TabIndex = 1;
			this.btnSavePinAgree.Text = "I AGREE";
			this.btnSavePinAgree.UseVisualStyleBackColor = true;
			this.lbSavePin.AutoSize = true;
			this.lbSavePin.Font = new Font("Microsoft Sans Serif", 9f, FontStyle.Regular, GraphicsUnit.Point, 222);
			this.lbSavePin.Location = new Point(6, -19);
			this.lbSavePin.Name = "lbSavePin";
			this.lbSavePin.Size = new Size(414, 90);
			this.lbSavePin.TabIndex = 0;
			this.lbSavePin.Text = componentResourceManager.GetString("lbSavePin.Text");
			this.panel1.BackColor = Color.FromArgb(10, 10, 10);
			this.panel1.Controls.Add(this.panel2);
			this.panel1.Controls.Add(this.lbOrderString);
			this.panel1.Location = new Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new Size(651, 283);
			this.panel1.TabIndex = 8;
			this.panel2.AutoScroll = true;
			this.panel2.Controls.Add(this.lbDisclaimer);
			this.panel2.Location = new Point(12, 31);
			this.panel2.Name = "panel2";
			this.panel2.Size = new Size(620, 247);
			this.panel2.TabIndex = 2;
			this.lbDisclaimer.Font = new Font("Tahoma", 10f, FontStyle.Regular, GraphicsUnit.Point, 222);
			this.lbDisclaimer.ForeColor = Color.LightGray;
			this.lbDisclaimer.Location = new Point(5, 11);
			this.lbDisclaimer.Name = "lbDisclaimer";
			this.lbDisclaimer.Size = new Size(609, 227);
			this.lbDisclaimer.TabIndex = 1;
			this.lbDisclaimer.Text = componentResourceManager.GetString("lbDisclaimer.Text");
			this.lbOrderString.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.lbOrderString.AutoSize = true;
			this.lbOrderString.Font = new Font("Tahoma", 11.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.lbOrderString.ForeColor = Color.Yellow;
			this.lbOrderString.Location = new Point(201, 9);
			this.lbOrderString.Name = "lbOrderString";
			this.lbOrderString.Size = new Size(226, 18);
			this.lbOrderString.TabIndex = 0;
			this.lbOrderString.Text = "เงื่อนไขการใช้งานระบบ Auto Trade";
			base.AutoScaleDimensions = new SizeF(7f, 16f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new Size(640, 323);
			base.Controls.Add(this.panel1);
			base.Controls.Add(this.gbSavePingDisclaimer);
			this.Font = new Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 222);
			base.FormBorderStyle = FormBorderStyle.FixedToolWindow;
			base.KeyPreview = true;
			base.Name = "frmStopDisclaimer";
			base.StartPosition = FormStartPosition.Manual;
			this.Text = "Auto Trade Disclaimer";
			this.gbSavePingDisclaimer.ResumeLayout(false);
			this.gbSavePingDisclaimer.PerformLayout();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.panel2.ResumeLayout(false);
			base.ResumeLayout(false);
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		public frmStopDisclaimer(bool isViewOnly)
		{
			this._isViewOnly = isViewOnly;
			this.InitializeComponent();
			if (this._isViewOnly)
			{
				this.btnAccept.Hide();
				this.btnDeny.Text = "Close";
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		private void btnAccept_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.bgwCheckStopOrderAccept == null)
				{
					this.bgwCheckStopOrderAccept = new BackgroundWorker();
					this.bgwCheckStopOrderAccept.WorkerReportsProgress = true;
					this.bgwCheckStopOrderAccept.DoWork += new DoWorkEventHandler(this.bgwCheckStopOrderAccept_DoWork);
					this.bgwCheckStopOrderAccept.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.bgwCheckStopOrderAccept_RunWorkerCompleted);
				}
				this.bgwCheckStopOrderAccept.RunWorkerAsync();
			}
			catch (Exception ex)
			{
				this.ShowError("btnAccept_Click", ex);
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		private void bgwCheckStopOrderAccept_DoWork(object sender, DoWorkEventArgs e)
		{
			try
			{
				this._registerResult = ApplicationInfo.WebAlertService.StopOrderRegister(ApplicationInfo.UserLoginID, true);
				ApplicationInfo.IsAutoTradeAccepted = (this._registerResult == ApplicationInfo.UserLoginID);
                //<PS> Enable stop order feature
                ApplicationInfo.IsAutoTradeAccepted = true;
			}
			catch (Exception ex)
			{
				this.ShowError("bgwCheckStopOrderAccept_DoWork", ex);
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		private void bgwCheckStopOrderAccept_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			if (ApplicationInfo.IsAutoTradeAccepted)
			{
				this.CloseMe();
			}
			else
			{
				MessageBox.Show(this._registerResult, "Stop Order Registration.");
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		private void btnDeny_Click(object sender, EventArgs e)
		{
			this.CloseMe();
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		private void CloseMe()
		{
			try
			{
				base.Enabled = false;
				if (this.tmTest == null)
				{
					this.tmTest = new Timer();
					this.tmTest.Interval = 100;
					this.tmTest.Tick += new EventHandler(this.tmTest_Tick);
				}
				this.tmTest.Stop();
				this.tmTest.Start();
			}
			catch (Exception ex)
			{
				this.ShowError("CloseMe", ex);
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tmTest_Tick(object sender, EventArgs e)
		{
			this.tmTest.Stop();
			base.Close();
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		protected void ShowError(string functionName, Exception ex)
		{
			ExceptionManager.Show(new ErrorItem(DateTime.Now, base.Name, functionName, ex.Message, ex.ToString()));
		}
	}
}
