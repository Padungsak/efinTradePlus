using i2TradePlus.Information;
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
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace i2TradePlus
{
	public class frmPzCreateNew : Form
	{
		private delegate void ShowMessageBoxCallBack(string message, frmOrderFormConfirm.OpenStyle openStyle, Control lastFocusOjb);

		private delegate void ShowOrderFormConfirmCallBack(string message, string orderParam, frmOrderFormConfirm.OpenStyle openStyle);

		private BackgroundWorker bgwSendOrder = null;

		private AutoTradePzCommand _commandPz = null;

		private frmOrderFormConfirm _frmConfirm = null;

		private Control _lastFocusOjb;

		private IContainer components = null;

		private TextBox tbPzPChg;

		private Label lbPzPchg;

		private TextBox tbPzSegment;

		private Label lbPzStep;

		private TextBox tbPzStartPrice;

		private Label lbPzPrice;

		private Button btnPzClose;

		private Button btnPzSimulate;

		private Label lbPzCreateNew;

		private Label lbPzStock;

		private ComboBox cbPzStock;

		private TextBox tbPzBudget;

		private Label lbPzBudget;

		private ExpandGrid gridPzSimm;

		private Button btnSendOrder;

		private TextBox tbPin;

		private Label lbPin;

		[MethodImpl(MethodImplOptions.NoInlining)]
		public frmPzCreateNew()
		{
			this.InitializeComponent();
			try
			{
				if (this.cbPzStock.AutoCompleteCustomSource != null)
				{
					if (this.cbPzStock.AutoCompleteCustomSource.Count == 0 && ApplicationInfo.StockAutoComp != null)
					{
						this.cbPzStock.AutoCompleteMode = AutoCompleteMode.Suggest;
						this.cbPzStock.AutoCompleteSource = AutoCompleteSource.CustomSource;
						this.cbPzStock.AutoCompleteCustomSource = ApplicationInfo.StockAutoComp;
					}
				}
				this.bgwSendOrder = new BackgroundWorker();
				this.bgwSendOrder.WorkerReportsProgress = true;
				this.bgwSendOrder.DoWork += new DoWorkEventHandler(this.bgwSendOrder_DoWork);
				this.bgwSendOrder.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.bgwSendOrder_RunWorkerCompleted);
				this.tbPin.Text = ApplicationInfo.UserPincodeLastEntry;
			}
			catch (Exception ex)
			{
				this.ShowError("frmPzCreateNew.Create", ex);
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		private void btnPzSimulate_Click(object sender, EventArgs e)
		{
			try
			{
				this._commandPz = new AutoTradePzCommand();
				decimal d = Convert.ToDecimal(this.tbPzBudget.Text.Replace(",", ""));
				string text = this.cbPzStock.Text.Trim();
				decimal num = Convert.ToDecimal(this.tbPzStartPrice.Text);
				decimal num2 = Convert.ToDecimal(this.tbPzPChg.Text);
				int num3 = Convert.ToInt32(this.tbPzSegment.Text);
				int num4 = num3;
				StockList.StockInformation stockInformation = ApplicationInfo.StockInfo[text];
				if (stockInformation.Number <= 0)
				{
					this.ShowMessageBox("Invalid Symbol!", frmOrderFormConfirm.OpenStyle.Error, this.cbPzStock);
				}
				else
				{
					decimal num5 = num;
					decimal spreadPrice = Utilities.GetSpreadPrice(num, stockInformation.PriorPrice, stockInformation.IsCheckSpread);
					if (num % spreadPrice != 0m)
					{
						num5 -= num % spreadPrice;
					}
					decimal num6 = 0m;
					long num7 = 0L;
					this.gridPzSimm.Rows = num3 + 1;
					long num8;
					RecordItem recordItem;
					for (int i = 0; i < num3; i++)
					{
						decimal d2 = d / num4;
						num4--;
						d2 -= d2 * ApplicationInfo.UserCommVAT;
						num8 = (long)(d2 / num5);
						if (num8 % (long)stockInformation.BoardLot != 0L)
						{
							num8 -= num8 % (long)stockInformation.BoardLot;
						}
						decimal num9 = num8 * num5;
						num9 += num9 * ApplicationInfo.UserCommVAT;
						d -= num9;
						recordItem = this.gridPzSimm.Records(i);
						recordItem.Fields("no").Text = i + 1;
						recordItem.Fields("price").Text = Utilities.PriceFormat(num5);
						recordItem.Fields("volume").Text = num8;
						recordItem.Fields("budget").Text = num9;
						recordItem.Fields("volume").FontColor = MyColor.UnChgColor;
						recordItem.Fields("budget").FontColor = Color.Cyan;
						spreadPrice = Utilities.GetSpreadPrice(num5 - 0.01m, stockInformation.PriorPrice, stockInformation.IsCheckSpread);
						num5 -= num5 * num2 / 100m;
						num5 -= num5 % spreadPrice;
						num7 += num8;
						num6 += num9;
					}
					recordItem = this.gridPzSimm.Records(this.gridPzSimm.Rows - 1);
					recordItem.Fields("no").Text = "Avg";
					recordItem.Fields("price").Text = ((num7 > 0L) ? Utilities.PriceFormat(num6 / num7, 4) : "0");
					recordItem.Fields("volume").Text = num7;
					recordItem.Fields("budget").Text = num6;
					recordItem.Fields("no").FontColor = Color.Cyan;
					recordItem.Fields("volume").FontColor = MyColor.UnChgColor;
					recordItem.Fields("volume").FontStyle = FontStyle.Bold;
					recordItem.Fields("budget").FontColor = Color.Cyan;
					recordItem.BackColor = Color.DimGray;
					this.gridPzSimm.Redraw();
					decimal num10 = Convert.ToDecimal(this.tbPzBudget.Text.Replace(",", ""));
					this._commandPz.UserId = ApplicationInfo.UserLoginID;
					this._commandPz.Account = ApplicationInfo.AccInfo.CurrentAccount;
					this._commandPz.CustType = ApplicationInfo.AccInfo.Items[ApplicationInfo.AccInfo.CurrentAccount].PcFlag;
					this._commandPz.Symbol = text;
					this._commandPz.CommandType = "ADD";
					this._commandPz.Budget = (long)num10;
					this._commandPz.PChg = num2;
					this._commandPz.StartPrice = num;
					this._commandPz.Segment = this.gridPzSimm.Rows - 1;
					this._commandPz.Items = new List<AutoTradePzItem>();
					num8 = 0L;
					for (int j = 0; j < this.gridPzSimm.Rows - 1; j++)
					{
						recordItem = this.gridPzSimm.Records(j);
						if (!string.IsNullOrEmpty(recordItem.Fields("no").Text.ToString()))
						{
							decimal buyPrice;
							decimal.TryParse(recordItem.Fields("price").Text.ToString(), out buyPrice);
							long.TryParse(recordItem.Fields("volume").Text.ToString().Replace(",", ""), out num8);
							decimal.TryParse(recordItem.Fields("budget").Text.ToString().Replace(",", ""), out num10);
							if (num8 <= 0L)
							{
								this.ShowMessageBox("The budget is not enough!", frmOrderFormConfirm.OpenStyle.Error, this.tbPzBudget);
								break;
							}
							AutoTradePzItem item = new AutoTradePzItem(j + 1, buyPrice, num8, num10, this._commandPz.UserId, this._commandPz.Account, this._commandPz.CustType, this._commandPz.Symbol);
							this._commandPz.Items.Add(item);
						}
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError("btnPzSimulate_Click", ex);
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		private void btnPzClose_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tbPzPChg_KeyDown(object sender, KeyEventArgs e)
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
					case Keys.Down:
						e.SuppressKeyPress = true;
						goto IL_7D;
					case Keys.Left:
						this.tbPzSegment.Focus();
						e.SuppressKeyPress = true;
						goto IL_7D;
					case Keys.Up:
						goto IL_7D;
					case Keys.Right:
						break;
					default:
						goto IL_7D;
					}
				}
				this.btnPzSimulate.PerformClick();
				e.SuppressKeyPress = true;
				IL_7D:;
			}
			catch (Exception ex)
			{
				this.ShowError("tbPzPChg_KeyDown", ex);
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tbPzStartPrice_KeyDown(object sender, KeyEventArgs e)
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
					case Keys.Down:
						e.SuppressKeyPress = true;
						goto IL_A1;
					case Keys.Left:
						this.tbPzBudget.Focus();
						e.SuppressKeyPress = true;
						goto IL_A1;
					case Keys.Up:
						goto IL_A1;
					case Keys.Right:
						break;
					default:
						goto IL_A1;
					}
				}
				if (this.tbPzStartPrice.Text.Trim() != string.Empty)
				{
					this.tbPzSegment.Focus();
				}
				e.SuppressKeyPress = true;
				IL_A1:;
			}
			catch (Exception ex)
			{
				this.ShowError("tbPzPrice_KeyDown", ex);
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tbPzSegment_KeyDown(object sender, KeyEventArgs e)
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
					case Keys.Down:
						e.SuppressKeyPress = true;
						goto IL_A1;
					case Keys.Left:
						this.tbPzStartPrice.Focus();
						e.SuppressKeyPress = true;
						goto IL_A1;
					case Keys.Up:
						goto IL_A1;
					case Keys.Right:
						break;
					default:
						goto IL_A1;
					}
				}
				if (this.tbPzStartPrice.Text.Trim() != string.Empty)
				{
					this.tbPzPChg.Focus();
				}
				e.SuppressKeyPress = true;
				IL_A1:;
			}
			catch (Exception ex)
			{
				this.ShowError("tbPzStep_KeyDown", ex);
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		private void tbPzBudget_TextChanged(object sender, EventArgs e)
		{
			try
			{
				if (this.tbPzBudget.Text.Trim() != string.Empty)
				{
					if (!Regex.IsMatch(this.tbPzBudget.Text.Replace(",", ""), "^[-+]?\\d+\\.?\\d*$"))
					{
						this.tbPzBudget.Text = this.tbPzBudget.Text.Substring(0, this.tbPzBudget.Text.Length - 1);
						this.tbPzBudget.Select(this.tbPzBudget.Text.Length, 0);
					}
					else
					{
						decimal num = Convert.ToInt64(this.tbPzBudget.Text.Replace(",", ""));
						this.tbPzBudget.Text = num.ToString("#,###");
						this.tbPzBudget.Select(this.tbPzBudget.Text.Length, 0);
					}
				}
			}
			catch (Exception ex)
			{
				this.ShowError("tbPzBudget_TextChanged", ex);
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		private void btnSendOrder_Click(object sender, EventArgs e)
		{
			try
			{
				if (this._commandPz == null)
				{
					this.ShowMessageBox("Simulate data before sending!!!", frmOrderFormConfirm.OpenStyle.Error, this.cbPzStock);
				}
				else
				{
					string orderParam = string.Concat(new string[]
					{
						"Pricing zone :",
						" Account : ",
						ApplicationInfo.AccInfo.CurrentAccount,
						"\nStock : ‘",
						this._commandPz.Symbol,
						"’",
						"\nBudget : ",
						FormatUtil.VolumeFormat(this._commandPz.Budget, true)
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
		private void cbPzStock_KeyDown(object sender, KeyEventArgs e)
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
						goto IL_8A;
					case Keys.Up:
						goto IL_8A;
					case Keys.Right:
						break;
					default:
						goto IL_8A;
					}
				}
				if (this.cbPzStock.Text.Trim() != string.Empty)
				{
					this.tbPzBudget.Focus();
				}
				e.SuppressKeyPress = true;
				IL_8A:;
			}
			catch (Exception ex)
			{
				this.ShowError("cbPzStock_KeyDown", ex);
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		private void ShowError(string functionName, Exception ex)
		{
			ExceptionManager.Show(new ErrorItem(DateTime.Now, base.Name, functionName, ex.Message, ex.ToString()));
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		private void ShowMessageBox(string message, frmOrderFormConfirm.OpenStyle openStyle, Control lastFocusOjb)
		{
			if (base.InvokeRequired)
			{
				base.Invoke(new frmPzCreateNew.ShowMessageBoxCallBack(this.ShowMessageBox), new object[]
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
		private void frmConfirm_FormClosing(object sender, FormClosingEventArgs e)
		{
			try
			{
				base.Focus();
				if (ApplicationInfo.IsEquityAccount)
				{
					frmOrderFormConfirm.OpenStyle openFormStyle = ((frmOrderFormConfirm)sender).OpenFormStyle;
					if (openFormStyle == frmOrderFormConfirm.OpenStyle.ConfirmSendNew)
					{
						DialogResult result = ((frmOrderFormConfirm)sender).Result;
						if (result == DialogResult.OK)
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
		private void ShowOrderFormConfirm(string message, string orderParam, frmOrderFormConfirm.OpenStyle openStyle)
		{
			if (base.InvokeRequired)
			{
				base.Invoke(new frmPzCreateNew.ShowOrderFormConfirmCallBack(this.ShowOrderFormConfirm), new object[]
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
		private void tbPzBudget_KeyDown(object sender, KeyEventArgs e)
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
					case Keys.Down:
						e.SuppressKeyPress = true;
						goto IL_A1;
					case Keys.Left:
						this.cbPzStock.Focus();
						e.SuppressKeyPress = true;
						goto IL_A1;
					case Keys.Up:
						goto IL_A1;
					case Keys.Right:
						break;
					default:
						goto IL_A1;
					}
				}
				if (this.tbPzBudget.Text.Trim() != string.Empty)
				{
					this.tbPzStartPrice.Focus();
				}
				e.SuppressKeyPress = true;
				IL_A1:;
			}
			catch (Exception ex)
			{
				this.ShowError("tbPzBudget_KeyDown", ex);
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		private void cbPzStock_KeyPress(object sender, KeyPressEventArgs e)
		{
			try
			{
				e.KeyChar = Convert.ToChar(e.KeyChar.ToString().ToUpper());
			}
			catch (Exception ex)
			{
				this.ShowError("cbPzStock_KeyPress", ex);
			}
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		private void bgwSendOrder_DoWork(object sender, DoWorkEventArgs e)
		{
			string result = string.Empty;
			try
			{
				string text = this.tbPin.Text.Trim();
				AutoTradeMain autoTradeMain = new AutoTradeMain();
				autoTradeMain.Pack("PZ", this._commandPz);
				result = ApplicationInfo.WebAlertService.SendAutoTrade(autoTradeMain.ToMessage());
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
								base.DialogResult = DialogResult.OK;
								base.Close();
							}
							else
							{
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
			catch (Exception ex)
			{
				this.ShowError("bgwSendOrder_RunWorkerCompleted", ex);
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
			ColumnItem columnItem = new ColumnItem();
			ColumnItem columnItem2 = new ColumnItem();
			ColumnItem columnItem3 = new ColumnItem();
			ColumnItem columnItem4 = new ColumnItem();
			this.tbPzPChg = new TextBox();
			this.lbPzPchg = new Label();
			this.tbPzSegment = new TextBox();
			this.lbPzStep = new Label();
			this.tbPzStartPrice = new TextBox();
			this.lbPzPrice = new Label();
			this.lbPzCreateNew = new Label();
			this.lbPzStock = new Label();
			this.cbPzStock = new ComboBox();
			this.tbPzBudget = new TextBox();
			this.lbPzBudget = new Label();
			this.gridPzSimm = new ExpandGrid();
			this.tbPin = new TextBox();
			this.lbPin = new Label();
			this.btnSendOrder = new Button();
			this.btnPzClose = new Button();
			this.btnPzSimulate = new Button();
			base.SuspendLayout();
			this.tbPzPChg.BackColor = Color.FromArgb(224, 224, 224);
			this.tbPzPChg.BorderStyle = BorderStyle.FixedSingle;
			this.tbPzPChg.Location = new Point(82, 149);
			this.tbPzPChg.Margin = new Padding(2, 3, 2, 3);
			this.tbPzPChg.MaxLength = 10;
			this.tbPzPChg.Name = "tbPzPChg";
			this.tbPzPChg.Size = new Size(95, 20);
			this.tbPzPChg.TabIndex = 142;
			this.tbPzPChg.KeyDown += new KeyEventHandler(this.tbPzPChg_KeyDown);
			this.lbPzPchg.AutoSize = true;
			this.lbPzPchg.ForeColor = Color.LightGray;
			this.lbPzPchg.Location = new Point(25, 151);
			this.lbPzPchg.Margin = new Padding(2, 0, 2, 0);
			this.lbPzPchg.Name = "lbPzPchg";
			this.lbPzPchg.Size = new Size(52, 13);
			this.lbPzPchg.TabIndex = 141;
			this.lbPzPchg.Text = "%Change";
			this.lbPzPchg.TextAlign = ContentAlignment.MiddleLeft;
			this.tbPzSegment.BackColor = Color.FromArgb(224, 224, 224);
			this.tbPzSegment.BorderStyle = BorderStyle.FixedSingle;
			this.tbPzSegment.Location = new Point(82, 123);
			this.tbPzSegment.Margin = new Padding(2, 3, 2, 3);
			this.tbPzSegment.MaxLength = 10;
			this.tbPzSegment.Name = "tbPzSegment";
			this.tbPzSegment.Size = new Size(95, 20);
			this.tbPzSegment.TabIndex = 140;
			this.tbPzSegment.KeyDown += new KeyEventHandler(this.tbPzSegment_KeyDown);
			this.lbPzStep.AutoSize = true;
			this.lbPzStep.ForeColor = Color.LightGray;
			this.lbPzStep.Location = new Point(28, 127);
			this.lbPzStep.Margin = new Padding(2, 0, 2, 0);
			this.lbPzStep.Name = "lbPzStep";
			this.lbPzStep.Size = new Size(49, 13);
			this.lbPzStep.TabIndex = 139;
			this.lbPzStep.Text = "Segment";
			this.lbPzStep.TextAlign = ContentAlignment.MiddleLeft;
			this.tbPzStartPrice.BackColor = Color.FromArgb(224, 224, 224);
			this.tbPzStartPrice.BorderStyle = BorderStyle.FixedSingle;
			this.tbPzStartPrice.Location = new Point(82, 97);
			this.tbPzStartPrice.Margin = new Padding(2, 3, 2, 3);
			this.tbPzStartPrice.MaxLength = 10;
			this.tbPzStartPrice.Name = "tbPzStartPrice";
			this.tbPzStartPrice.Size = new Size(95, 20);
			this.tbPzStartPrice.TabIndex = 138;
			this.tbPzStartPrice.KeyDown += new KeyEventHandler(this.tbPzStartPrice_KeyDown);
			this.lbPzPrice.AutoSize = true;
			this.lbPzPrice.ForeColor = Color.LightGray;
			this.lbPzPrice.Location = new Point(21, 101);
			this.lbPzPrice.Margin = new Padding(2, 0, 2, 0);
			this.lbPzPrice.Name = "lbPzPrice";
			this.lbPzPrice.Size = new Size(56, 13);
			this.lbPzPrice.TabIndex = 137;
			this.lbPzPrice.Text = "Start Price";
			this.lbPzPrice.TextAlign = ContentAlignment.MiddleLeft;
			this.lbPzCreateNew.AutoSize = true;
			this.lbPzCreateNew.BackColor = Color.FromArgb(64, 64, 64);
			this.lbPzCreateNew.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 222);
			this.lbPzCreateNew.ForeColor = Color.LightGray;
			this.lbPzCreateNew.Location = new Point(13, 10);
			this.lbPzCreateNew.Name = "lbPzCreateNew";
			this.lbPzCreateNew.Padding = new Padding(3);
			this.lbPzCreateNew.Size = new Size(126, 19);
			this.lbPzCreateNew.TabIndex = 133;
			this.lbPzCreateNew.Tag = "-1";
			this.lbPzCreateNew.Text = "Create Pricing Zone";
			this.lbPzCreateNew.TextAlign = ContentAlignment.MiddleLeft;
			this.lbPzStock.AutoSize = true;
			this.lbPzStock.ForeColor = Color.LightGray;
			this.lbPzStock.Location = new Point(36, 48);
			this.lbPzStock.Margin = new Padding(2, 0, 2, 0);
			this.lbPzStock.Name = "lbPzStock";
			this.lbPzStock.Size = new Size(41, 13);
			this.lbPzStock.TabIndex = 131;
			this.lbPzStock.Text = "Symbol";
			this.lbPzStock.TextAlign = ContentAlignment.MiddleLeft;
			this.cbPzStock.AutoCompleteMode = AutoCompleteMode.Suggest;
			this.cbPzStock.AutoCompleteSource = AutoCompleteSource.ListItems;
			this.cbPzStock.BackColor = Color.FromArgb(224, 224, 224);
			this.cbPzStock.FlatStyle = FlatStyle.Popup;
			this.cbPzStock.ForeColor = Color.Black;
			this.cbPzStock.FormattingEnabled = true;
			this.cbPzStock.Location = new Point(82, 44);
			this.cbPzStock.MaxLength = 20;
			this.cbPzStock.Name = "cbPzStock";
			this.cbPzStock.Size = new Size(95, 21);
			this.cbPzStock.TabIndex = 130;
			this.cbPzStock.KeyPress += new KeyPressEventHandler(this.cbPzStock_KeyPress);
			this.cbPzStock.KeyDown += new KeyEventHandler(this.cbPzStock_KeyDown);
			this.tbPzBudget.BackColor = Color.FromArgb(224, 224, 224);
			this.tbPzBudget.BorderStyle = BorderStyle.FixedSingle;
			this.tbPzBudget.Location = new Point(82, 71);
			this.tbPzBudget.Margin = new Padding(2, 3, 2, 3);
			this.tbPzBudget.MaxLength = 10;
			this.tbPzBudget.Name = "tbPzBudget";
			this.tbPzBudget.Size = new Size(95, 20);
			this.tbPzBudget.TabIndex = 133;
			this.tbPzBudget.TextChanged += new EventHandler(this.tbPzBudget_TextChanged);
			this.tbPzBudget.KeyDown += new KeyEventHandler(this.tbPzBudget_KeyDown);
			this.lbPzBudget.AutoSize = true;
			this.lbPzBudget.ForeColor = Color.LightGray;
			this.lbPzBudget.Location = new Point(36, 75);
			this.lbPzBudget.Margin = new Padding(2, 0, 2, 0);
			this.lbPzBudget.Name = "lbPzBudget";
			this.lbPzBudget.Size = new Size(41, 13);
			this.lbPzBudget.TabIndex = 11;
			this.lbPzBudget.Text = "Budget";
			this.lbPzBudget.TextAlign = ContentAlignment.MiddleLeft;
			this.gridPzSimm.AllowDrop = true;
			this.gridPzSimm.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.gridPzSimm.BackColor = Color.FromArgb(20, 20, 20);
			this.gridPzSimm.CanBlink = true;
			this.gridPzSimm.CanDrag = false;
			this.gridPzSimm.CanGetMouseMove = false;
			columnItem.Alignment = StringAlignment.Center;
			columnItem.BackColor = Color.FromArgb(64, 64, 64);
			columnItem.FontColor = Color.LightGray;
			columnItem.IsExpand = false;
			columnItem.MyStyle = FontStyle.Regular;
			columnItem.Name = "no";
			columnItem.Text = "No.";
			columnItem.ValueFormat = FormatType.Text;
			columnItem.Visible = true;
			columnItem.Width = 15;
			columnItem2.Alignment = StringAlignment.Far;
			columnItem2.BackColor = Color.FromArgb(64, 64, 64);
			columnItem2.FontColor = Color.LightGray;
			columnItem2.IsExpand = false;
			columnItem2.MyStyle = FontStyle.Regular;
			columnItem2.Name = "price";
			columnItem2.Text = "Price";
			columnItem2.ValueFormat = FormatType.Text;
			columnItem2.Visible = true;
			columnItem2.Width = 25;
			columnItem3.Alignment = StringAlignment.Far;
			columnItem3.BackColor = Color.FromArgb(64, 64, 64);
			columnItem3.FontColor = Color.LightGray;
			columnItem3.IsExpand = false;
			columnItem3.MyStyle = FontStyle.Regular;
			columnItem3.Name = "volume";
			columnItem3.Text = "Volume";
			columnItem3.ValueFormat = FormatType.Volume;
			columnItem3.Visible = true;
			columnItem3.Width = 30;
			columnItem4.Alignment = StringAlignment.Far;
			columnItem4.BackColor = Color.FromArgb(64, 64, 64);
			columnItem4.FontColor = Color.LightGray;
			columnItem4.IsExpand = false;
			columnItem4.MyStyle = FontStyle.Regular;
			columnItem4.Name = "budget";
			columnItem4.Text = "Budget";
			columnItem4.ValueFormat = FormatType.Price;
			columnItem4.Visible = true;
			columnItem4.Width = 30;
			this.gridPzSimm.Columns.Add(columnItem);
			this.gridPzSimm.Columns.Add(columnItem2);
			this.gridPzSimm.Columns.Add(columnItem3);
			this.gridPzSimm.Columns.Add(columnItem4);
			this.gridPzSimm.CurrentScroll = 0;
			this.gridPzSimm.FocusItemIndex = -1;
			this.gridPzSimm.ForeColor = Color.Black;
			this.gridPzSimm.GridColor = Color.FromArgb(50, 50, 50);
			this.gridPzSimm.HeaderPctHeight = 100f;
			this.gridPzSimm.IsAutoRepaint = true;
			this.gridPzSimm.IsDrawGrid = true;
			this.gridPzSimm.IsDrawHeader = true;
			this.gridPzSimm.IsScrollable = true;
			this.gridPzSimm.Location = new Point(195, 7);
			this.gridPzSimm.MainColumn = "";
			this.gridPzSimm.Name = "gridPzSimm";
			this.gridPzSimm.Rows = 0;
			this.gridPzSimm.RowSelectColor = Color.Navy;
			this.gridPzSimm.RowSelectType = 0;
			this.gridPzSimm.Size = new Size(404, 289);
			this.gridPzSimm.SortColumnName = "";
			this.gridPzSimm.SortType = SortType.Desc;
			this.gridPzSimm.TabIndex = 135;
			this.tbPin.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.tbPin.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
			this.tbPin.AutoCompleteSource = AutoCompleteSource.CustomSource;
			this.tbPin.BackColor = Color.FromArgb(224, 224, 224);
			this.tbPin.BorderStyle = BorderStyle.FixedSingle;
			this.tbPin.CharacterCasing = CharacterCasing.Upper;
			this.tbPin.Location = new Point(416, 305);
			this.tbPin.Margin = new Padding(2, 3, 2, 3);
			this.tbPin.MaxLength = 10;
			this.tbPin.Name = "tbPin";
			this.tbPin.PasswordChar = '*';
			this.tbPin.Size = new Size(55, 20);
			this.tbPin.TabIndex = 143;
			this.lbPin.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.lbPin.AutoSize = true;
			this.lbPin.ForeColor = Color.LightGray;
			this.lbPin.Location = new Point(387, 309);
			this.lbPin.Margin = new Padding(2, 0, 2, 0);
			this.lbPin.Name = "lbPin";
			this.lbPin.Size = new Size(25, 13);
			this.lbPin.TabIndex = 144;
			this.lbPin.Text = "PIN";
			this.lbPin.TextAlign = ContentAlignment.MiddleLeft;
			this.btnSendOrder.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.btnSendOrder.AutoEllipsis = true;
			this.btnSendOrder.AutoSizeMode = AutoSizeMode.GrowAndShrink;
			this.btnSendOrder.BackColor = Color.Transparent;
			this.btnSendOrder.Cursor = Cursors.Hand;
			this.btnSendOrder.FlatAppearance.BorderColor = Color.DimGray;
			this.btnSendOrder.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 128, 0);
			this.btnSendOrder.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 192, 192);
			this.btnSendOrder.FlatStyle = FlatStyle.Flat;
			this.btnSendOrder.ForeColor = Color.WhiteSmoke;
			this.btnSendOrder.Image = Resources.no_side_button;
			this.btnSendOrder.Location = new Point(478, 304);
			this.btnSendOrder.MaximumSize = new Size(65, 22);
			this.btnSendOrder.Name = "btnSendOrder";
			this.btnSendOrder.Size = new Size(57, 22);
			this.btnSendOrder.TabIndex = 145;
			this.btnSendOrder.TabStop = false;
			this.btnSendOrder.Text = "Send";
			this.btnSendOrder.UseVisualStyleBackColor = false;
			this.btnSendOrder.Click += new EventHandler(this.btnSendOrder_Click);
			this.btnPzClose.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.btnPzClose.AutoSizeMode = AutoSizeMode.GrowAndShrink;
			this.btnPzClose.BackColor = Color.Transparent;
			this.btnPzClose.Cursor = Cursors.Hand;
			this.btnPzClose.FlatAppearance.BorderColor = Color.DimGray;
			this.btnPzClose.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 128, 0);
			this.btnPzClose.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 192, 192);
			this.btnPzClose.FlatStyle = FlatStyle.Flat;
			this.btnPzClose.ForeColor = Color.LightGray;
			this.btnPzClose.Image = Resources._1_4type_bt;
			this.btnPzClose.Location = new Point(541, 304);
			this.btnPzClose.Name = "btnPzClose";
			this.btnPzClose.Size = new Size(57, 22);
			this.btnPzClose.TabIndex = 136;
			this.btnPzClose.TabStop = false;
			this.btnPzClose.Text = "Cancel";
			this.btnPzClose.UseVisualStyleBackColor = false;
			this.btnPzClose.Click += new EventHandler(this.btnPzClose_Click);
			this.btnPzSimulate.AutoSizeMode = AutoSizeMode.GrowAndShrink;
			this.btnPzSimulate.BackColor = Color.Transparent;
			this.btnPzSimulate.Cursor = Cursors.Hand;
			this.btnPzSimulate.FlatAppearance.BorderColor = Color.DimGray;
			this.btnPzSimulate.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 128, 0);
			this.btnPzSimulate.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 192, 192);
			this.btnPzSimulate.FlatStyle = FlatStyle.Flat;
			this.btnPzSimulate.ForeColor = Color.LightGray;
			this.btnPzSimulate.Image = Resources._1_4type_bt;
			this.btnPzSimulate.Location = new Point(112, 185);
			this.btnPzSimulate.Name = "btnPzSimulate";
			this.btnPzSimulate.Size = new Size(65, 22);
			this.btnPzSimulate.TabIndex = 134;
			this.btnPzSimulate.TabStop = false;
			this.btnPzSimulate.Text = "Simulate";
			this.btnPzSimulate.UseVisualStyleBackColor = false;
			this.btnPzSimulate.Click += new EventHandler(this.btnPzSimulate_Click);
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			this.BackColor = Color.FromArgb(30, 30, 30);
			base.ClientSize = new Size(605, 331);
			base.Controls.Add(this.btnSendOrder);
			base.Controls.Add(this.tbPin);
			base.Controls.Add(this.lbPin);
			base.Controls.Add(this.gridPzSimm);
			base.Controls.Add(this.tbPzPChg);
			base.Controls.Add(this.lbPzPchg);
			base.Controls.Add(this.lbPzCreateNew);
			base.Controls.Add(this.tbPzSegment);
			base.Controls.Add(this.lbPzBudget);
			base.Controls.Add(this.lbPzStep);
			base.Controls.Add(this.tbPzBudget);
			base.Controls.Add(this.tbPzStartPrice);
			base.Controls.Add(this.cbPzStock);
			base.Controls.Add(this.lbPzPrice);
			base.Controls.Add(this.lbPzStock);
			base.Controls.Add(this.btnPzClose);
			base.Controls.Add(this.btnPzSimulate);
			base.FormBorderStyle = FormBorderStyle.None;
			base.Name = "frmPzCreateNew";
			this.Text = "frmPzCreateNew";
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
