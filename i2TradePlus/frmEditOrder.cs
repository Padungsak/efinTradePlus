using i2TradePlus.Properties;
using ITSNet.Common.BIZ;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace i2TradePlus
{
	public class frmEditOrder : Form
	{
		public struct OrderEditRecord
		{
			public long OrderNumber;

			public string Side;

			public string Stock;

			public int TrusteeID;

			public long Volume;

			public string Price;

			public long PubVol;

			public string EntryDate;

			public long MatVolume;
		}

		private delegate void ShowMessageInFormConfirmCallBack(string message, frmOrderFormConfirm.OpenStyle openStyle);

		private delegate void ShowOrderFormConfirmCallBack(string message, string orderParam, frmOrderFormConfirm.OpenStyle openStyle);

		private IContainer components = null;

		private Button btnClose;

		private Label lbSide;

		private Button btnSendOrder;

		private Label lbPublic;

		private Label lbPrice;

		private TextBox tbVolume;

		private Label lbStock;

		private TextBox tbSide;

		private Label lbVolume;

		private CheckBox chbNVDR;

		private TextBox tbPublic;

		private TextBox tbStock;

		private Label lbLoading;

		private ComboBox cbPrice;

		private Label label1;

		private TextBox tbOrderNo;

		private frmEditOrder.OrderEditRecord _record;

		private long _volume;

		private long _pubVol;

		private string _price = string.Empty;

		private int _trusteeId;

		private bool _verifyResult_Pin = false;

		private string _verifyResultStr_Pin = string.Empty;

		private frmOrderFormConfirm _frmConfirm = null;

		private BackgroundWorker bgw = null;

		private bool _sendResult = false;

		private string _sendResultMessage = string.Empty;

		private bool _isLockPubVol = false;

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
			this.btnClose = new Button();
			this.lbSide = new Label();
			this.btnSendOrder = new Button();
			this.lbPublic = new Label();
			this.lbPrice = new Label();
			this.tbVolume = new TextBox();
			this.lbStock = new Label();
			this.tbSide = new TextBox();
			this.lbVolume = new Label();
			this.chbNVDR = new CheckBox();
			this.tbPublic = new TextBox();
			this.tbStock = new TextBox();
			this.lbLoading = new Label();
			this.cbPrice = new ComboBox();
			this.label1 = new Label();
			this.tbOrderNo = new TextBox();
			base.SuspendLayout();
			this.btnClose.BackColor = SystemColors.Control;
			this.btnClose.FlatAppearance.BorderColor = Color.Gray;
			this.btnClose.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 128, 0);
			this.btnClose.FlatAppearance.MouseOverBackColor = Color.Turquoise;
			this.btnClose.FlatStyle = FlatStyle.Flat;
			this.btnClose.ForeColor = Color.Black;
			this.btnClose.Location = new Point(486, 19);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new Size(50, 21);
			this.btnClose.TabIndex = 95;
			this.btnClose.Text = "Cancel";
			this.btnClose.UseVisualStyleBackColor = false;
			this.btnClose.Click += new EventHandler(this.btnClose_Click);
			this.lbSide.AutoSize = true;
			this.lbSide.ForeColor = Color.Black;
			this.lbSide.Location = new Point(78, 5);
			this.lbSide.Margin = new Padding(2, 0, 2, 0);
			this.lbSide.Name = "lbSide";
			this.lbSide.Size = new Size(28, 13);
			this.lbSide.TabIndex = 90;
			this.lbSide.Text = "Side";
			this.lbSide.TextAlign = ContentAlignment.MiddleLeft;
			this.btnSendOrder.BackColor = Color.WhiteSmoke;
			this.btnSendOrder.FlatAppearance.BorderColor = Color.Gray;
			this.btnSendOrder.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 128, 0);
			this.btnSendOrder.FlatAppearance.MouseOverBackColor = Color.Turquoise;
			this.btnSendOrder.FlatStyle = FlatStyle.Flat;
			this.btnSendOrder.ForeColor = Color.Black;
			this.btnSendOrder.Location = new Point(432, 19);
			this.btnSendOrder.Margin = new Padding(1, 3, 1, 3);
			this.btnSendOrder.MaximumSize = new Size(58, 23);
			this.btnSendOrder.Name = "btnSendOrder";
			this.btnSendOrder.Size = new Size(52, 22);
			this.btnSendOrder.TabIndex = 93;
			this.btnSendOrder.TabStop = false;
			this.btnSendOrder.Text = "Confirm";
			this.btnSendOrder.UseVisualStyleBackColor = false;
			this.btnSendOrder.Click += new EventHandler(this.btnSendOrder_Click);
			this.lbPublic.AutoSize = true;
			this.lbPublic.ForeColor = Color.Black;
			this.lbPublic.Location = new Point(372, 5);
			this.lbPublic.Margin = new Padding(2, 0, 2, 0);
			this.lbPublic.Name = "lbPublic";
			this.lbPublic.Size = new Size(44, 13);
			this.lbPublic.TabIndex = 91;
			this.lbPublic.Text = "P/B Vol";
			this.lbPublic.TextAlign = ContentAlignment.MiddleLeft;
			this.lbPrice.AutoSize = true;
			this.lbPrice.ForeColor = Color.Black;
			this.lbPrice.Location = new Point(311, 5);
			this.lbPrice.Margin = new Padding(2, 0, 2, 0);
			this.lbPrice.Name = "lbPrice";
			this.lbPrice.Size = new Size(31, 13);
			this.lbPrice.TabIndex = 89;
			this.lbPrice.Text = "Price";
			this.lbPrice.TextAlign = ContentAlignment.MiddleLeft;
			this.tbVolume.AllowDrop = true;
			this.tbVolume.BorderStyle = BorderStyle.FixedSingle;
			this.tbVolume.Location = new Point(237, 21);
			this.tbVolume.Margin = new Padding(2, 3, 2, 3);
			this.tbVolume.MaxLength = 10;
			this.tbVolume.Name = "tbVolume";
			this.tbVolume.Size = new Size(60, 20);
			this.tbVolume.TabIndex = 83;
			this.tbVolume.TextChanged += new EventHandler(this.tbVolume_TextChanged);
			this.tbVolume.KeyDown += new KeyEventHandler(this.tbVolume_KeyDown);
			this.tbVolume.Leave += new EventHandler(this.controlOrder_Leave);
			this.tbVolume.Enter += new EventHandler(this.controlOrder_Enter);
			this.lbStock.AutoSize = true;
			this.lbStock.ForeColor = Color.Black;
			this.lbStock.Location = new Point(110, 5);
			this.lbStock.Margin = new Padding(2, 0, 2, 0);
			this.lbStock.Name = "lbStock";
			this.lbStock.Size = new Size(35, 13);
			this.lbStock.TabIndex = 87;
			this.lbStock.Text = "Stock";
			this.lbStock.TextAlign = ContentAlignment.MiddleLeft;
			this.tbSide.AllowDrop = true;
			this.tbSide.BorderStyle = BorderStyle.FixedSingle;
			this.tbSide.CharacterCasing = CharacterCasing.Upper;
			this.tbSide.Location = new Point(79, 21);
			this.tbSide.Margin = new Padding(2, 3, 2, 3);
			this.tbSide.MaxLength = 1;
			this.tbSide.Name = "tbSide";
			this.tbSide.ReadOnly = true;
			this.tbSide.Size = new Size(24, 20);
			this.tbSide.TabIndex = 80;
			this.tbSide.TabStop = false;
			this.tbSide.TextAlign = HorizontalAlignment.Center;
			this.lbVolume.AutoSize = true;
			this.lbVolume.ForeColor = Color.Black;
			this.lbVolume.Location = new Point(246, 5);
			this.lbVolume.Margin = new Padding(2, 0, 2, 0);
			this.lbVolume.Name = "lbVolume";
			this.lbVolume.Size = new Size(42, 13);
			this.lbVolume.TabIndex = 88;
			this.lbVolume.Text = "Volume";
			this.lbVolume.TextAlign = ContentAlignment.MiddleLeft;
			this.chbNVDR.AutoSize = true;
			this.chbNVDR.ForeColor = Color.Black;
			this.chbNVDR.Location = new Point(176, 24);
			this.chbNVDR.Margin = new Padding(2, 3, 0, 3);
			this.chbNVDR.Name = "chbNVDR";
			this.chbNVDR.Size = new Size(57, 17);
			this.chbNVDR.TabIndex = 82;
			this.chbNVDR.Text = "NVDR";
			this.chbNVDR.UseVisualStyleBackColor = false;
			this.tbPublic.AllowDrop = true;
			this.tbPublic.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
			this.tbPublic.AutoCompleteSource = AutoCompleteSource.CustomSource;
			this.tbPublic.BorderStyle = BorderStyle.FixedSingle;
			this.tbPublic.CharacterCasing = CharacterCasing.Upper;
			this.tbPublic.Location = new Point(364, 21);
			this.tbPublic.Margin = new Padding(2, 3, 2, 3);
			this.tbPublic.MaxLength = 10;
			this.tbPublic.Name = "tbPublic";
			this.tbPublic.Size = new Size(60, 20);
			this.tbPublic.TabIndex = 85;
			this.tbPublic.TextChanged += new EventHandler(this.tbPublic_TextChanged);
			this.tbPublic.Leave += new EventHandler(this.controlOrder_Leave);
			this.tbPublic.Enter += new EventHandler(this.controlOrder_Enter);
			this.tbStock.AllowDrop = true;
			this.tbStock.BorderStyle = BorderStyle.FixedSingle;
			this.tbStock.CharacterCasing = CharacterCasing.Upper;
			this.tbStock.Location = new Point(107, 21);
			this.tbStock.Margin = new Padding(2, 3, 2, 3);
			this.tbStock.MaxLength = 10;
			this.tbStock.Name = "tbStock";
			this.tbStock.ReadOnly = true;
			this.tbStock.Size = new Size(66, 20);
			this.tbStock.TabIndex = 81;
			this.tbStock.TabStop = false;
			this.lbLoading.AutoSize = true;
			this.lbLoading.BackColor = Color.FromArgb(64, 64, 64);
			this.lbLoading.BorderStyle = BorderStyle.FixedSingle;
			this.lbLoading.Font = new Font("Microsoft Sans Serif", 9f, FontStyle.Bold, GraphicsUnit.Point, 222);
			this.lbLoading.ForeColor = Color.Yellow;
			this.lbLoading.Location = new Point(108, 46);
			this.lbLoading.Name = "lbLoading";
			this.lbLoading.Padding = new Padding(4, 3, 4, 3);
			this.lbLoading.Size = new Size(158, 23);
			this.lbLoading.TabIndex = 98;
			this.lbLoading.Text = "Sending New Order ...";
			this.lbLoading.TextAlign = ContentAlignment.MiddleCenter;
			this.lbLoading.Visible = false;
			this.cbPrice.AutoCompleteCustomSource.AddRange(new string[]
			{
				"",
				"IOC",
				"FOK"
			});
			this.cbPrice.AutoCompleteMode = AutoCompleteMode.Append;
			this.cbPrice.AutoCompleteSource = AutoCompleteSource.CustomSource;
			this.cbPrice.BackColor = Color.FromArgb(224, 224, 224);
			this.cbPrice.FlatStyle = FlatStyle.Popup;
			this.cbPrice.ForeColor = Color.Black;
			this.cbPrice.FormattingEnabled = true;
			this.cbPrice.Items.AddRange(new object[]
			{
				"",
				"ATO",
				"ATC",
				"MP",
				"MO",
				"ML"
			});
			this.cbPrice.Location = new Point(301, 21);
			this.cbPrice.Name = "cbPrice";
			this.cbPrice.Size = new Size(59, 21);
			this.cbPrice.TabIndex = 99;
			this.cbPrice.Leave += new EventHandler(this.controlOrder_Leave);
			this.cbPrice.Enter += new EventHandler(this.controlOrder_Enter);
			this.cbPrice.KeyPress += new KeyPressEventHandler(this.cbPrice_KeyPress);
			this.cbPrice.KeyDown += new KeyEventHandler(this.cbPrice_KeyDown);
			this.label1.AutoSize = true;
			this.label1.ForeColor = Color.Black;
			this.label1.Location = new Point(13, 5);
			this.label1.Margin = new Padding(2, 0, 2, 0);
			this.label1.Name = "label1";
			this.label1.Size = new Size(47, 13);
			this.label1.TabIndex = 101;
			this.label1.Text = "OrderNo";
			this.label1.TextAlign = ContentAlignment.MiddleLeft;
			this.tbOrderNo.AllowDrop = true;
			this.tbOrderNo.BorderStyle = BorderStyle.FixedSingle;
			this.tbOrderNo.CharacterCasing = CharacterCasing.Upper;
			this.tbOrderNo.Location = new Point(3, 21);
			this.tbOrderNo.Margin = new Padding(2, 3, 2, 3);
			this.tbOrderNo.MaxLength = 1;
			this.tbOrderNo.Name = "tbOrderNo";
			this.tbOrderNo.ReadOnly = true;
			this.tbOrderNo.Size = new Size(73, 20);
			this.tbOrderNo.TabIndex = 100;
			this.tbOrderNo.TabStop = false;
			this.tbOrderNo.TextAlign = HorizontalAlignment.Center;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			this.BackColor = Color.LightGray;
			base.ClientSize = new Size(545, 45);
			base.ControlBox = false;
			base.Controls.Add(this.label1);
			base.Controls.Add(this.tbOrderNo);
			base.Controls.Add(this.cbPrice);
			base.Controls.Add(this.lbLoading);
			base.Controls.Add(this.btnClose);
			base.Controls.Add(this.lbSide);
			base.Controls.Add(this.btnSendOrder);
			base.Controls.Add(this.lbPublic);
			base.Controls.Add(this.lbPrice);
			base.Controls.Add(this.tbVolume);
			base.Controls.Add(this.lbStock);
			base.Controls.Add(this.tbSide);
			base.Controls.Add(this.lbVolume);
			base.Controls.Add(this.chbNVDR);
			base.Controls.Add(this.tbPublic);
			base.Controls.Add(this.tbStock);
			base.FormBorderStyle = FormBorderStyle.None;
			base.KeyPreview = true;
			base.MaximizeBox = false;
			base.Name = "frmEditOrder";
			this.Text = "-";
			base.Paint += new PaintEventHandler(this.frmEditOrder_Paint);
			base.Shown += new EventHandler(this.frmEditOrder_Shown);
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		public frmEditOrder(frmEditOrder.OrderEditRecord recordData)
		{
			this.InitializeComponent();
			this._record = recordData;
			this.SetResize();
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		private void frmEditOrder_Shown(object sender, EventArgs e)
		{
			try
			{
				Color foreColor = Color.White;
				string side = this._record.Side;
				if (side != null)
				{
					if (!(side == "B"))
					{
						if (!(side == "S"))
						{
							if (!(side == "H"))
							{
								if (side == "C")
								{
									this.BackColor = Color.Turquoise;
									foreColor = Color.Black;
								}
							}
							else
							{
								this.BackColor = Color.Pink;
								foreColor = Color.Black;
							}
						}
						else
						{
							this.BackColor = Color.Maroon;
						}
					}
					else
					{
						this.BackColor = Color.DarkGreen;
					}
				}
				foreach (Control control in base.Controls)
				{
					if (control != this.lbLoading)
					{
						if (control.GetType() == typeof(Label) || control.GetType() == typeof(CheckBox))
						{
							control.ForeColor = foreColor;
						}
					}
				}
				this.tbOrderNo.Text = this._record.OrderNumber.ToString();
				this.tbSide.Text = this._record.Side;
				this.tbStock.Text = this._record.Stock;
				this.tbVolume.Text = Utilities.VolumeFormat(this._record.Volume, true);
				this.cbPrice.Text = this._record.Price;
				this.tbPublic.Text = Utilities.VolumeFormat(this._record.PubVol, true);
				this.chbNVDR.Checked = (this._record.TrusteeID == 2);
				if (!ApplicationInfo.SupportFreewill)
				{
					this.tbVolume.Focus();
				}
				else
				{
					this.chbNVDR.Focus();
				}
				if (base.Top + base.Height > base.Parent.Height)
				{
					base.Top = base.Parent.Height - base.Height;
				}
			}
			catch (Exception ex)
			{
				this.ShowError("frmEditOrder_Shown", ex);
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		private void SetResize()
		{
			try
			{
				this.Font = new Font(Settings.Default.Default_Font.Name, Settings.Default.Default_Font.Size - 1f, Settings.Default.Default_Font.Style);
			}
			catch (Exception ex)
			{
				this.ShowError("SetResize", ex);
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		private void ShowError(string methodName, Exception ex)
		{
			ExceptionManager.Show(new ErrorItem(DateTime.Now, base.Name, methodName, ex.Message, ex.ToString()));
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		private void btnClose_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		private void controlOrder_Enter(object sender, EventArgs e)
		{
			try
			{
				((Control)sender).BackColor = Color.Yellow;
				((Control)sender).ForeColor = Color.Black;
				if (sender.GetType() == typeof(TextBox))
				{
					((TextBox)sender).SelectAll();
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
					if (this.BackColor == Color.Maroon || this.BackColor == Color.DarkGreen)
					{
						((Control)sender).ForeColor = Color.White;
					}
					else
					{
						((Control)sender).ForeColor = Color.Black;
					}
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
		private void frmEditOrder_Paint(object sender, PaintEventArgs e)
		{
			if (e.ClipRectangle.Width == base.Width)
			{
				e.Graphics.DrawRectangle(Pens.White, e.ClipRectangle.Left, e.ClipRectangle.Top, e.ClipRectangle.Width - 1, e.ClipRectangle.Height - 1);
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		private void btnSendOrder_Click(object sender, EventArgs e)
		{
			try
			{
				this._volume = 0L;
				try
				{
					this._volume = Convert.ToInt64(this.tbVolume.Text.Replace(",", ""));
					if (this._volume <= 0L)
					{
						this.ShowMessageInFormConfirm("Invalid Volume [More than Zero]!", frmOrderFormConfirm.OpenStyle.ShowBox);
						return;
					}
				}
				catch
				{
					this.ShowMessageInFormConfirm("Invalid volume.", frmOrderFormConfirm.OpenStyle.ShowBox);
					return;
				}
				if (this.IsValidPrice(this.cbPrice.Text, true))
				{
					this._price = this.cbPrice.Text.ToUpper().Trim();
					this._pubVol = 0L;
					try
					{
						this._pubVol = ((this.tbPublic.Text != "") ? Convert.ToInt64(this.tbPublic.Text.Replace(",", "")) : 0L);
						if (this._pubVol > this._volume)
						{
							this.ShowMessageInFormConfirm("Published is Greater than Volume", frmOrderFormConfirm.OpenStyle.ShowBox);
							return;
						}
						if (this._pubVol > 0L && this._pubVol < this._volume)
						{
							string price = this._price;
							if (price != null)
							{
								if (price == "MP" || price == "ATO" || price == "ATC" || price == "MO" || price == "ML")
								{
									this.ShowMessageInFormConfirm("Price condition cannot use Published.", frmOrderFormConfirm.OpenStyle.ShowBox);
									return;
								}
							}
						}
					}
					catch
					{
						this.ShowMessageInFormConfirm("Invalid public volume", frmOrderFormConfirm.OpenStyle.ShowBox);
						return;
					}
					this._trusteeId = (this.chbNVDR.Checked ? 2 : 0);
					this._sendResult = false;
					this.btnSendOrder.Enabled = false;
					string orderParam = string.Concat(new string[]
					{
						"Change Order::  ",
						(this._record.Volume != this._volume) ? string.Concat(new object[]
						{
							"\nVolume from ",
							this._record.Volume,
							" to ",
							FormatUtil.VolumeFormat(this._volume, true)
						}) : "",
						(this._record.Price != this._price) ? ("\nPrice from " + this._record.Price + " to " + this._price) : "",
						(this._record.TrusteeID != this._trusteeId) ? string.Concat(new object[]
						{
							"\nTrustee from ",
							this._record.TrusteeID,
							" to ",
							this._trusteeId
						}) : "",
						(this._record.PubVol != this._pubVol) ? string.Concat(new object[]
						{
							"\nPublish from ",
							this._record.PubVol,
							" to ",
							FormatUtil.VolumeFormat(this._pubVol, true)
						}) : ""
					});
					this.ShowOrderFormConfirm("Confirm to send?", orderParam, frmOrderFormConfirm.OpenStyle.ConfirmSendNew);
				}
			}
			catch (Exception ex)
			{
				this.ShowError("btnSendOrder_Click", ex);
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		private bool IsValidPrice(string price, bool IsShowMessage)
		{
			bool result;
			try
			{
				if (price != null)
				{
					if (price == "ATO" || price == "ATC" || price == "MP" || price == "MO" || price == "ML")
					{
						result = true;
						return result;
					}
				}
				if (!FormatUtil.Isnumeric(price))
				{
					if (IsShowMessage)
					{
						this.ShowMessageInFormConfirm("Invalid price.", frmOrderFormConfirm.OpenStyle.ShowBox);
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
								this.ShowMessageInFormConfirm("Invalid price format [2 digits]!.", frmOrderFormConfirm.OpenStyle.ShowBox);
							}
							result = false;
							return result;
						}
					}
					else if (text.Length > 2)
					{
						if (IsShowMessage)
						{
							this.ShowMessageInFormConfirm("Invalid price format [2 digits]!.", frmOrderFormConfirm.OpenStyle.ShowBox);
						}
						result = false;
						return result;
					}
				}
				if (Convert.ToDecimal(price) <= 0m)
				{
					if (IsShowMessage)
					{
						this.ShowMessageInFormConfirm("Invalid price [More than 0]!.", frmOrderFormConfirm.OpenStyle.ShowBox);
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
		private void ShowMessageInFormConfirm(string message, frmOrderFormConfirm.OpenStyle openStyle)
		{
			if (base.InvokeRequired)
			{
				base.Invoke(new frmEditOrder.ShowMessageInFormConfirmCallBack(this.ShowMessageInFormConfirm), new object[]
				{
					message,
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
							this._frmConfirm.Dispose();
						}
						this._frmConfirm = null;
					}
					this._frmConfirm = new frmOrderFormConfirm(message, "", openStyle);
					this._frmConfirm.FormClosing -= new FormClosingEventHandler(this.frmConfirm_FormClosing);
					this._frmConfirm.FormClosing += new FormClosingEventHandler(this.frmConfirm_FormClosing);
					this._frmConfirm.TopLevel = false;
					if (base.Parent.GetType() == typeof(Panel))
					{
						this._frmConfirm.Parent = base.Parent.Parent;
					}
					else if (base.Parent.GetType().BaseType == typeof(ClientBaseForm))
					{
						this._frmConfirm.Parent = base.Parent;
					}
					else if (base.Parent.Parent.Parent.GetType() == typeof(frmMain))
					{
						this._frmConfirm.Parent = base.Parent.Parent.Parent;
					}
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
		private void bgwReloadData_DoWork(object sender, DoWorkEventArgs e)
		{
			try
			{
				this._verifyResult_Pin = ApplicationInfo.VerifyPincode(ApplicationInfo.UserPincode, ref this._verifyResultStr_Pin);
				if (this._verifyResult_Pin)
				{
					string data = ApplicationInfo.WebOrderService.SendEditOrder(ApplicationInfo.AuthenKey, ApplicationInfo.GetSession(), this._record.OrderNumber, this._record.EntryDate, this._volume, this._price, ApplicationInfo.AccInfo.CurrentAccount, this._pubVol, this._trusteeId, ApplicationInfo.AccInfo.CurrInternetUser);
					using (DataSet dataSet = new DataSet())
					{
						MyDataHelper.StringToDataSet(data, dataSet);
						if (ApplicationInfo.SupportFreewill)
						{
							if (dataSet.Tables.Contains("Results") && dataSet.Tables["Results"].Rows.Count > 0)
							{
								this._sendResult = (dataSet.Tables["Results"].Rows[0]["code"].ToString() == "0");
								this._sendResultMessage = dataSet.Tables["Results"].Rows[0]["message"].ToString();
							}
						}
						else if (dataSet.Tables.Contains("TABLE") && dataSet.Tables["TABLE"].Rows.Count > 0)
						{
							if (dataSet.Tables["TABLE"].Rows[0][0].ToString() == "Y")
							{
								ApplicationInfo.AddOrderNoToAutoRefreshList(this._record.OrderNumber.ToString(), 1);
								this._sendResult = true;
							}
							else
							{
								foreach (DataTable dataTable in dataSet.Tables)
								{
									if (dataTable.Rows.Count > 0 && dataTable.Rows[0][0].ToString() != "N")
									{
										this._sendResultMessage = dataTable.Rows[0][0].ToString();
									}
								}
							}
						}
						dataSet.Clear();
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowMessageInFormConfirm("Request fail =>" + ex.Message, frmOrderFormConfirm.OpenStyle.ShowBox);
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		private void bgwReloadData_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			try
			{
				if (!this._verifyResult_Pin)
				{
					this.ShowMessageInFormConfirm(this._verifyResultStr_Pin, frmOrderFormConfirm.OpenStyle.ShowBox);
				}
				else if (this._sendResult)
				{
					base.Close();
				}
				else
				{
					this.ShowMessageInFormConfirm("Request fail =>" + this._sendResultMessage, frmOrderFormConfirm.OpenStyle.ShowBox);
				}
			}
			catch (Exception ex)
			{
				this.ShowError("SecurityInfo:RunWorkerCompleted", ex);
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tbVolume_TextChanged(object sender, EventArgs e)
		{
			try
			{
				if (this.tbVolume.Text.Trim() != string.Empty)
				{
					if (FormatUtil.Isnumeric(this.tbVolume.Text))
					{
						try
						{
							decimal num = Convert.ToInt64(this.tbVolume.Text.Replace(",", ""));
							this.tbVolume.Text = num.ToString("#,###");
							this.tbVolume.Select(this.tbVolume.Text.Length, 0);
							this._isLockPubVol = true;
							this.tbPublic.Text = this.tbVolume.Text;
						}
						catch
						{
							this.tbVolume.Text = this.tbVolume.Text.Substring(0, this.tbVolume.Text.Length - 1);
						}
					}
					else
					{
						this.tbVolume.Text = this.tbVolume.Text.Substring(0, this.tbVolume.Text.Length - 1);
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError("tbVolume_TextChanged", ex);
			}
			this._isLockPubVol = false;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tbPublic_TextChanged(object sender, EventArgs e)
		{
			try
			{
				if (!this._isLockPubVol && this.tbPublic.Text.Trim() != string.Empty)
				{
					if (FormatUtil.Isnumeric(this.tbPublic.Text))
					{
						try
						{
							decimal num = Convert.ToInt64(this.tbPublic.Text.Replace(",", ""));
							this.tbPublic.Text = num.ToString("#,###");
							this.tbPublic.Select(this.tbPublic.Text.Length, 0);
						}
						catch
						{
							this.tbPublic.Text = this.tbPublic.Text.Substring(0, this.tbPublic.Text.Length - 1);
						}
					}
					else
					{
						this.tbPublic.Text = this.tbPublic.Text.Substring(0, this.tbPublic.Text.Length - 1);
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError("tbPublic_TextChanged", ex);
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tbVolume_KeyDown(object sender, KeyEventArgs e)
		{
			Keys keyCode = e.KeyCode;
			if (keyCode != Keys.Return)
			{
				if (keyCode == Keys.Right)
				{
					this.cbPrice.Focus();
				}
			}
			else
			{
				this.btnSendOrder.PerformClick();
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		private void cbPrice_KeyDown(object sender, KeyEventArgs e)
		{
			Keys keyCode = e.KeyCode;
			if (keyCode != Keys.Return)
			{
				if (keyCode == Keys.Left)
				{
					this.tbVolume.Focus();
				}
			}
			else
			{
				this.btnSendOrder.PerformClick();
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		private void cbPrice_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.KeyChar = Convert.ToChar(e.KeyChar.ToString().ToUpper());
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		private void ShowOrderFormConfirm(string message, string orderParam, frmOrderFormConfirm.OpenStyle openStyle)
		{
			if (base.InvokeRequired)
			{
				base.Invoke(new frmEditOrder.ShowOrderFormConfirmCallBack(this.ShowOrderFormConfirm), new object[]
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
					this._frmConfirm = new frmOrderFormConfirm(message, "", openStyle);
					this._frmConfirm.FormClosing -= new FormClosingEventHandler(this.frmConfirm_FormClosing);
					this._frmConfirm.FormClosing += new FormClosingEventHandler(this.frmConfirm_FormClosing);
					this._frmConfirm.TopLevel = false;
					this._frmConfirm.OrderParam = orderParam;
					if (base.Parent.GetType() == typeof(Panel))
					{
						this._frmConfirm.Parent = base.Parent.Parent;
					}
					else if (base.Parent.GetType().BaseType == typeof(ClientBaseForm))
					{
						this._frmConfirm.Parent = base.Parent;
					}
					else if (base.Parent.Parent.Parent.GetType() == typeof(frmMain))
					{
						this._frmConfirm.Parent = base.Parent.Parent.Parent;
					}
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
				frmOrderFormConfirm.OpenStyle openFormStyle = ((frmOrderFormConfirm)sender).OpenFormStyle;
				DialogResult result = ((frmOrderFormConfirm)sender).Result;
				if (ApplicationInfo.IsEquityAccount)
				{
					if (openFormStyle == frmOrderFormConfirm.OpenStyle.ConfirmSendNew)
					{
						if (result == DialogResult.OK)
						{
							if (this.bgw == null)
							{
								this.bgw = new BackgroundWorker();
								this.bgw.DoWork += new DoWorkEventHandler(this.bgwReloadData_DoWork);
								this.bgw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.bgwReloadData_RunWorkerCompleted);
							}
							if (!this.bgw.IsBusy)
							{
								this.bgw.RunWorkerAsync();
							}
							else
							{
								this.ShowMessageInFormConfirm("The system is not ready yet.", frmOrderFormConfirm.OpenStyle.Error);
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError("ConfirmForm", ex);
			}
			this.btnSendOrder.Enabled = true;
		}
	}
}
