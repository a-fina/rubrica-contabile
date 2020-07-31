namespace Rubrica
{
	using System;
	using System.ComponentModel;
	using System.Drawing;
	using System.IO;
	using System.Windows.Forms;

	public class MainForm : Form
	{
		private NumericUpDown GiornoF;
		private GroupBox groupBox3;
		private GroupBox groupBox1;
		private ListView listView1;
		private ComboBox DriveL;
		private NumericUpDown AnnoSel;
		private GroupBox groupBox4;
		private Label label8;
		private GroupBox groupBox2;
		private TextBox Cli;
		private Button button2;
		private ColumnHeader Cliente;
		private Button button1;
		private NumericUpDown Anno;
		private Button button7;
		private Button button6;
		private Button button5;
		private Button button4;
		private NumericUpDown numericUpDown4;
		private NumericUpDown AnnoF;
		private NumericUpDown numericUpDown6;
		private ColumnHeader Data;
		private CheckBox checkBoxCli;
		private Label totV;
		private ColumnHeader Controparte;
		private NumericUpDown numericUpDown5;
		private Label label3;
		private Label label2;
		private Label label1;
		private Label label7;
		private Label label6;
		private RadioButton Acconto;
		private Label label4;
		private Label labelCli;
		private Label label9;
		private ColumnHeader Import;
		private Button button3;
		private RadioButton Saldo;
		private NumericUpDown Importo;
		private Label label12;
		private Label label13;
		private Label label10;
		private Label label11;
		private Label label16;
		private Label totInc;
		private Label label14;
		private Label label15;
		private Label label18;
		private NumericUpDown MeseInc;
		private Label label20;
		private Label label23;
		private Label label19;
		private Label label17;
		private Label label5;
		private NumericUpDown AnnoInc;
		private CheckBox tutti;
		private NumericUpDown Mese;
		private NumericUpDown MeseF;
		private Label totC;
		private CheckBox checkBoxData;
		private NumericUpDown Giorno;
		private MenuItem menuItem3;
		private MenuItem menuItem1;
		private MainMenu mainMenu1;
		private Label totR;
		private TextBox Contro;
		private RadioButton Concordato;
		private ColumnHeader CAS;
		public static string campo;
		private bool salvato;
		private bool caricato;
		public static string filtra_cliente;
		private string AnnoCorrente;
		private string drive;
		private string wrk_dir;
		private System.ComponentModel.IContainer components;

		public MainForm()
		{
			this.InitializeComponent();
		}

		private void Button1Click(object sender, EventArgs e)
		{
			if (this.CheckField(1))
			{
				double num = Convert.ToInt32(this.totInc.Text);
				string str = "";
				if (this.Concordato.Checked)
				{
					str = "C";
				}
				else
				{
					if (this.Acconto.Checked)
					{
						str = "A";
					}
					if (this.Saldo.Checked)
					{
						str = "S";
					}
					if ((this.Mese.Text == this.MeseInc.Text) && (this.Anno.Text == this.AnnoInc.Text))
					{
						num += Convert.ToDouble(this.Importo.Text);
					}
				}
				this.totInc.Text = num.ToString();
				
				string str2 = this.Anno.Text + @"\";
				if (this.Mese.Text.Length == 1)
				{
					str2 = str2 + "0";
				}
				str2 = str2 + this.Mese.Text + @"\";
				if (this.Giorno.Text.Length == 1)
				{
					str2 = str2 + "0";
				}
				ListViewItem item = new ListViewItem((str2 + this.Giorno.Text).ToString()) {
					SubItems = {
						this.Cli.Text,
						this.Contro.Text,
						this.Importo.Text,
						str
					}
				};
				this.listView1.Items.Add(item);
				this.Cli.Text = "";
				this.Contro.Text = "";
				this.Importo.Text = "0";
				this.Acconto.Checked = false;
				this.Saldo.Checked = false;
				this.Concordato.Checked = false;
				this.ResetField();
				this.salvato = false;
			}
		}

		private void Button2Click(object sender, EventArgs e)
		{
			if (this.CheckField(0))
			{
				string str = "";
				if (this.Acconto.Checked)
				{
					str = "A";
				}
				if (this.Concordato.Checked)
				{
					str = "C";
				}
				if (this.Saldo.Checked)
				{
					str = "S";
				}
				string str2 = this.Anno.Text + @"\";
				if (this.Mese.Text.Length == 1)
				{
					str2 = str2 + "0";
				}
				str2 = str2 + this.Mese.Text + @"\";
				if (this.Giorno.Text.Length == 1)
				{
					str2 = str2 + "0";
				}
				ListViewItem item = new ListViewItem(str2 + this.Giorno.Text) {
					SubItems = {
						this.Cli.Text,
						this.Contro.Text,
						this.Importo.Text,
						str
					}
				};
				this.listView1.Items.Add(item);
				this.listView1.Items.Remove(this.listView1.SelectedItems[0]);
				this.ResetField();
				this.salvato = false;
			}
		}

		private void Button3Click(object sender, EventArgs e)
		{
			if (MessageBox.Show("Cancellare?", "Conferma", MessageBoxButtons.YesNo) == DialogResult.Yes)
			{
				bool flag1 = this.Acconto.Checked;
				bool flag2 = this.Concordato.Checked;
				bool flag3 = this.Saldo.Checked;
				this.listView1.Items.Remove(this.listView1.SelectedItems[0]);
				this.ResetField();
				this.salvato = false;
			}
		}

		private void Button4Click(object sender, EventArgs e)
		{
			
			bool flag = false;
			double numTotVers = 0.0;
			double numTotIncassato = 0.0;
			double numTotConco = 0.0;
			double numTotRiman = 0.0;
			try
			{
				Form2 form = new Form2();
				double cercaFrom = ((Convert.ToInt32(this.Anno.Text) * 0x2710) + (Convert.ToInt32(this.Mese.Text) * 100)) + Convert.ToInt32(this.Giorno.Text);
				double cercaFinoA = ((Convert.ToInt32(this.AnnoF.Text) * 0x2710) + (Convert.ToInt32(this.MeseF.Text) * 100)) + Convert.ToInt32(this.GiornoF.Text);
				int numRigaCorrente = 0;
				
				while (true)
				{
					if (numRigaCorrente >= this.listView1.Items.Count)
					{
						// Ultimo elemento, tiriamo le somme
						if (form.listView1.Items.Count > 0)
						{
							form.Show();
						}
						this.totV.Text = numTotVers.ToString();
						if (numTotRiman < 0.0)
						{
							numTotRiman = 0.0;
						}
						this.totR.Text = numTotRiman.ToString();
						this.totC.Text = numTotConco.ToString();
						this.totInc.Text = numTotIncassato.ToString();
						this.Refresh();
						break;
					}
					this.listView1.Items[numRigaCorrente].BackColor = Color.White;
					flag = false;
					string[] strDataCorrente = new string[3];
					char[] separator = new char[] { '\\' };
					strDataCorrente = this.listView1.Items[numRigaCorrente].SubItems[0].Text.Split(separator);
					double dataCorrente = ((Convert.ToInt32(strDataCorrente[0]) * 0x2710) + 
					                       (Convert.ToInt32(strDataCorrente[1]) * 100)) + 
											Convert.ToInt32(strDataCorrente[2]);
					
					double dataTotaleIncMese = (Convert.ToInt32(this.AnnoInc.Text) * 0x2710) + 
					                           (Convert.ToInt32(this.MeseInc.Text) * 100);
					//if ( (Convert.ToInt32(dataTotaleIncMese) >= ( (Convert.ToInt32(strDataCorrente[0]) * 0x2710) + (Convert.ToInt32(strDataCorrente[1]) * 100)) ) && 

					// Calcolo il totale incassato per il mese selezionato in Mese	
					if (( (Convert.ToInt32(this.AnnoInc.Text) == Convert.ToInt32(strDataCorrente[0])) && 
					      (Convert.ToInt32(this.MeseInc.Text) == Convert.ToInt32(strDataCorrente[1])) ) && 
					      this.listView1.Items[numRigaCorrente].SubItems[4].Text != "C")
					{
						numTotIncassato += Convert.ToInt32(this.listView1.Items[numRigaCorrente].SubItems[3].Text);
					}
					
					// Flag evidenzia in giallo e calcolo totalini
					if (((this.Cli.Text != "") && this.checkBoxCli.Checked) && this.listView1.Items[numRigaCorrente].SubItems[1].Text.StartsWith(this.Cli.Text))
					{
						if (!this.checkBoxData.Checked)
						{
							flag = true;
						}
						else if ((cercaFrom <= dataCorrente) && (dataCorrente <= cercaFinoA))
						{
							flag = true;
						}
					}
					if ((this.checkBoxData.Checked && (cercaFrom <= dataCorrente)) && (dataCorrente <= cercaFinoA))
					{
						if (!this.checkBoxCli.Checked)
						{
							flag = true;
						}
						else if ((this.Cli.Text != "") && this.listView1.Items[numRigaCorrente].SubItems[1].Text.StartsWith(this.Cli.Text))
						{
							flag = true;
						}
					}
					ListViewItem item = new ListViewItem(this.listView1.Items[numRigaCorrente].SubItems[0].Text);
					if (flag)
					{
						this.listView1.Items[numRigaCorrente].BackColor = Color.Yellow;
						if (this.listView1.Items[numRigaCorrente].SubItems[4].Text == "C")
						{
							numTotConco += Convert.ToDouble(this.listView1.Items[numRigaCorrente].SubItems[3].Text);
							numTotRiman += Convert.ToDouble(this.listView1.Items[numRigaCorrente].SubItems[3].Text);
						}
						else
						{
							numTotVers += Convert.ToDouble(this.listView1.Items[numRigaCorrente].SubItems[3].Text);
							numTotRiman -= Convert.ToDouble(this.listView1.Items[numRigaCorrente].SubItems[3].Text);
						}
						item.SubItems.Add(this.listView1.Items[numRigaCorrente].SubItems[1].Text);
						item.SubItems.Add(this.listView1.Items[numRigaCorrente].SubItems[2].Text);
						item.SubItems.Add(this.listView1.Items[numRigaCorrente].SubItems[3].Text);
						item.SubItems.Add(this.listView1.Items[numRigaCorrente].SubItems[4].Text);
						form.listView1.Items.Add(item);
					}
					numRigaCorrente++;
				}
			}
			catch (Exception exception1)
			{
				MessageBox.Show(exception1.Message.ToString());
			}
		}

		private void Button5Click(object sender, EventArgs e)
		{
			this.Anno.Text = DateTime.Now.Year.ToString();
			this.Mese.Text = DateTime.Now.Month.ToString();
			this.Giorno.Text = DateTime.Now.Day.ToString();
		}

		private void Button6Click(object sender, EventArgs e)
		{
			
			try
			{
				if (this.salvato || (MessageBox.Show("Si perderanno le modifiche. Caricare senza salvare?", "Conferma", MessageBoxButtons.YesNo) != DialogResult.No))
				{
					campo = "";
					filtra_cliente = "";
					Form1 form = new Form1 {
						filtra = true,
						funzione = "carica",
						drive = this.drive,
						wrkdir = this.wrk_dir
					};
					form.ShowDialog();
					form.Focus();
					decimal minimum = 0M;
					if (campo != "carica")
					{
						MessageBox.Show("Password errata");
					}
					else
					{
						this.listView1.Items.Clear();
						this.listView1.ResetText();
						minimum = this.AnnoSel.Minimum;
						while (true)
						{
							if (minimum > this.AnnoSel.Maximum)
							{
								this.salvato = true;
								this.menuItem3.Enabled = true;
								this.DriveL.Enabled = false;
								break;
							}
							string path = this.drive + @"\archivi\dati" + minimum.ToString() + ".mf";
							if (((this.AnnoSel.Value == minimum) || this.tutti.Checked) && File.Exists(path))
							{
								this.AnnoCorrente = this.AnnoSel.Value.ToString();
								this.button4.Enabled = true;
								this.caricato = true;
								using (StreamReader reader = new StreamReader(path))
								{
									while (true)
									{
										string str2 = reader.ReadLine();
										if (str2 == null)
										{
											reader.Close();
											this.Anno.Value = this.AnnoSel.Value;
											this.AnnoF.Value = this.AnnoSel.Value;
											this.AnnoInc.Value = this.AnnoSel.Value;
											break;
										}
										char[] separator = new char[] { '\x00b1' };
										string[] strDataCorrente = str2.Replace("###", "\x00b1").Split(separator);
										if ((filtra_cliente == "") || strDataCorrente[2].StartsWith(filtra_cliente))
										{
											ListViewItem item = new ListViewItem(strDataCorrente[1]) {
												SubItems = {
													strDataCorrente[2],
													strDataCorrente[3],
													strDataCorrente[4],
													strDataCorrente[5]
												},
												ForeColor = Color.Black
											};
											this.listView1.Items.Add(item);
										}
										this.labelCli.Text = filtra_cliente;
										if (filtra_cliente == "")
										{
											this.menuItem1.Enabled = true;
											string[] textArray1 = new string[3];
											bool flag1 = strDataCorrente[3] != "C";
										}
										else
										{
											this.menuItem3.Enabled = false;
											this.menuItem1.Enabled = false;
											if ((filtra_cliente == strDataCorrente[2]) && (strDataCorrente[3] == "C"))
											{
											}
										}
									}
								}
							}
							minimum += 1;
						}
					}
				}
			}
			catch (Exception exception1)
			{
				MessageBox.Show(exception1.Message.ToString());
			}
		}

		private void Button7Click(object sender, EventArgs e)
		{
			this.ResetField();
		}

		private bool CheckField(int check)
		{
			if (campo != "carica")
			{
				MessageBox.Show("Necessario Caricare");
				return false;
			}
			if (this.Cli.Text.ToString() == "")
			{
				MessageBox.Show("Inserire Cliente");
				return false;
			}
			if ((filtra_cliente != "") && (filtra_cliente != this.Cli.Text))
			{
				MessageBox.Show("Cliente non valido");
				return false;
			}
			if (this.Contro.Text.ToString() == "")
			{
				MessageBox.Show("Inserire Controparte");
				return false;
			}
			if (this.Importo.Text.ToString() == "")
			{
				MessageBox.Show("Inserire Importo");
				return false;
			}
			if ((!this.Acconto.Checked && !this.Saldo.Checked) && !this.Concordato.Checked)
			{
				MessageBox.Show("Inserire tipo di pagamento");
				return false;
			}
			string str = "";
			string str2 = "";
			if (this.Acconto.Checked)
			{
				str = "A";
				str2 = "Acconto";
			}
			else if (this.Concordato.Checked)
			{
				str = "C";
				str2 = "Concordato";
			}
			else
			{
				str = "S";
				str2 = "Saldo";
			}
			for (int i = 0; i < this.listView1.Items.Count; i++)
			{
				if ((((this.listView1.Items[i].SubItems[4].Text == "C") || (this.listView1.Items[i].SubItems[4].Text == "S")) && ((this.listView1.Items[i].SubItems[1].Text == this.Cli.Text) && ((this.listView1.Items[i].SubItems[2].Text == this.Contro.Text) && (this.listView1.Items[i].SubItems[4].Text == str)))) && (check == 1))
				{
					MessageBox.Show(str2 + " gi\x00e0\x00a0 esistente");
					return false;
				}
			}
			return true;
		}

		private void CliKeyDown(object sender, KeyEventArgs e)
		{
			this.Acconto.Enabled = true;
			this.Saldo.Enabled = true;
			this.Concordato.Enabled = true;
		}

		private void ControKeyDown(object sender, KeyEventArgs e)
		{
			this.Acconto.Enabled = true;
			this.Saldo.Enabled = true;
			this.Concordato.Enabled = true;
		}

		private void DriveLSelectedIndexChanged(object sender, EventArgs e)
		{
			this.drive = this.DriveL.Text + this.drive.Substring(2, this.drive.Length - 2);
		}

		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.CAS = new System.Windows.Forms.ColumnHeader();
			this.Concordato = new System.Windows.Forms.RadioButton();
			this.Contro = new System.Windows.Forms.TextBox();
			this.totR = new System.Windows.Forms.Label();
			this.mainMenu1 = new System.Windows.Forms.MainMenu(this.components);
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.menuItem3 = new System.Windows.Forms.MenuItem();
			this.Giorno = new System.Windows.Forms.NumericUpDown();
			this.checkBoxData = new System.Windows.Forms.CheckBox();
			this.totC = new System.Windows.Forms.Label();
			this.MeseF = new System.Windows.Forms.NumericUpDown();
			this.Mese = new System.Windows.Forms.NumericUpDown();
			this.tutti = new System.Windows.Forms.CheckBox();
			this.AnnoInc = new System.Windows.Forms.NumericUpDown();
			this.label5 = new System.Windows.Forms.Label();
			this.label17 = new System.Windows.Forms.Label();
			this.label19 = new System.Windows.Forms.Label();
			this.label23 = new System.Windows.Forms.Label();
			this.label20 = new System.Windows.Forms.Label();
			this.MeseInc = new System.Windows.Forms.NumericUpDown();
			this.label18 = new System.Windows.Forms.Label();
			this.label15 = new System.Windows.Forms.Label();
			this.label14 = new System.Windows.Forms.Label();
			this.totInc = new System.Windows.Forms.Label();
			this.label16 = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.label13 = new System.Windows.Forms.Label();
			this.label12 = new System.Windows.Forms.Label();
			this.Importo = new System.Windows.Forms.NumericUpDown();
			this.Saldo = new System.Windows.Forms.RadioButton();
			this.button3 = new System.Windows.Forms.Button();
			this.Import = new System.Windows.Forms.ColumnHeader();
			this.label9 = new System.Windows.Forms.Label();
			this.labelCli = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.Acconto = new System.Windows.Forms.RadioButton();
			this.label6 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.numericUpDown5 = new System.Windows.Forms.NumericUpDown();
			this.Controparte = new System.Windows.Forms.ColumnHeader();
			this.totV = new System.Windows.Forms.Label();
			this.checkBoxCli = new System.Windows.Forms.CheckBox();
			this.Data = new System.Windows.Forms.ColumnHeader();
			this.numericUpDown6 = new System.Windows.Forms.NumericUpDown();
			this.AnnoF = new System.Windows.Forms.NumericUpDown();
			this.numericUpDown4 = new System.Windows.Forms.NumericUpDown();
			this.button4 = new System.Windows.Forms.Button();
			this.button5 = new System.Windows.Forms.Button();
			this.button6 = new System.Windows.Forms.Button();
			this.button7 = new System.Windows.Forms.Button();
			this.Anno = new System.Windows.Forms.NumericUpDown();
			this.button1 = new System.Windows.Forms.Button();
			this.Cliente = new System.Windows.Forms.ColumnHeader();
			this.button2 = new System.Windows.Forms.Button();
			this.Cli = new System.Windows.Forms.TextBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.listView1 = new System.Windows.Forms.ListView();
			this.label8 = new System.Windows.Forms.Label();
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this.AnnoSel = new System.Windows.Forms.NumericUpDown();
			this.DriveL = new System.Windows.Forms.ComboBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.GiornoF = new System.Windows.Forms.NumericUpDown();
			((System.ComponentModel.ISupportInitialize)(this.Giorno)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.MeseF)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Mese)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.AnnoInc)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.MeseInc)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Importo)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown5)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown6)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.AnnoF)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown4)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Anno)).BeginInit();
			this.groupBox2.SuspendLayout();
			this.groupBox4.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.AnnoSel)).BeginInit();
			this.groupBox1.SuspendLayout();
			this.groupBox3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.GiornoF)).BeginInit();
			this.SuspendLayout();
			// 
			// CAS
			// 
			this.CAS.Text = "C/A/S";
			this.CAS.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.CAS.Width = 53;
			// 
			// Concordato
			// 
			this.Concordato.Location = new System.Drawing.Point(154, 234);
			this.Concordato.Name = "Concordato";
			this.Concordato.Size = new System.Drawing.Size(140, 23);
			this.Concordato.TabIndex = 8;
			this.Concordato.TabStop = true;
			this.Concordato.Text = "Concordato";
			// 
			// Contro
			// 
			this.Contro.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			this.Contro.Location = new System.Drawing.Point(154, 146);
			this.Contro.Name = "Contro";
			this.Contro.Size = new System.Drawing.Size(179, 26);
			this.Contro.TabIndex = 5;
			this.Contro.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControKeyDown);
			// 
			// totR
			// 
			this.totR.Location = new System.Drawing.Point(154, 129);
			this.totR.Name = "totR";
			this.totR.Size = new System.Drawing.Size(102, 23);
			this.totR.TabIndex = 51;
			this.totR.Text = "0";
			this.totR.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// mainMenu1
			// 
			this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
			this.menuItem1});
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 0;
			this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
			this.menuItem3});
			this.menuItem1.Text = "Menu";
			// 
			// menuItem3
			// 
			this.menuItem3.Index = 0;
			this.menuItem3.Text = "Salva dati";
			this.menuItem3.Click += new System.EventHandler(this.MenuItem3Click);
			// 
			// Giorno
			// 
			this.Giorno.Location = new System.Drawing.Point(83, 47);
			this.Giorno.Maximum = new decimal(new int[] {
			31,
			0,
			0,
			0});
			this.Giorno.Minimum = new decimal(new int[] {
			1,
			0,
			0,
			0});
			this.Giorno.Name = "Giorno";
			this.Giorno.Size = new System.Drawing.Size(58, 26);
			this.Giorno.TabIndex = 1;
			this.Giorno.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.Giorno.Value = new decimal(new int[] {
			1,
			0,
			0,
			0});
			// 
			// checkBoxData
			// 
			this.checkBoxData.Location = new System.Drawing.Point(122, 134);
			this.checkBoxData.Name = "checkBoxData";
			this.checkBoxData.Size = new System.Drawing.Size(134, 30);
			this.checkBoxData.TabIndex = 33;
			this.checkBoxData.Text = "Data";
			// 
			// totC
			// 
			this.totC.Location = new System.Drawing.Point(154, 105);
			this.totC.Name = "totC";
			this.totC.Size = new System.Drawing.Size(102, 24);
			this.totC.TabIndex = 38;
			this.totC.Text = "0";
			this.totC.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// MeseF
			// 
			this.MeseF.Location = new System.Drawing.Point(250, 164);
			this.MeseF.Maximum = new decimal(new int[] {
			12,
			0,
			0,
			0});
			this.MeseF.Minimum = new decimal(new int[] {
			1,
			0,
			0,
			0});
			this.MeseF.Name = "MeseF";
			this.MeseF.Size = new System.Drawing.Size(51, 26);
			this.MeseF.TabIndex = 15;
			this.MeseF.Value = new decimal(new int[] {
			1,
			0,
			0,
			0});
			// 
			// Mese
			// 
			this.Mese.Location = new System.Drawing.Point(160, 47);
			this.Mese.Maximum = new decimal(new int[] {
			12,
			0,
			0,
			0});
			this.Mese.Minimum = new decimal(new int[] {
			1,
			0,
			0,
			0});
			this.Mese.Name = "Mese";
			this.Mese.Size = new System.Drawing.Size(58, 26);
			this.Mese.TabIndex = 2;
			this.Mese.Value = new decimal(new int[] {
			1,
			0,
			0,
			0});
			// 
			// tutti
			// 
			this.tutti.Location = new System.Drawing.Point(678, 41);
			this.tutti.Name = "tutti";
			this.tutti.Size = new System.Drawing.Size(103, 23);
			this.tutti.TabIndex = 16;
			this.tutti.Text = "Tutti";
			// 
			// AnnoInc
			// 
			this.AnnoInc.Location = new System.Drawing.Point(77, 210);
			this.AnnoInc.Maximum = new decimal(new int[] {
			2125,
			0,
			0,
			0});
			this.AnnoInc.Name = "AnnoInc";
			this.AnnoInc.Size = new System.Drawing.Size(70, 21);
			this.AnnoInc.TabIndex = 55;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(45, 105);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(109, 24);
			this.label5.TabIndex = 30;
			this.label5.Text = "Concordato:";
			// 
			// label17
			// 
			this.label17.Location = new System.Drawing.Point(0, 0);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(100, 23);
			this.label17.TabIndex = 0;
			// 
			// label19
			// 
			this.label19.Font = new System.Drawing.Font("Tahoma", 11F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.World);
			this.label19.Location = new System.Drawing.Point(13, 53);
			this.label19.Name = "label19";
			this.label19.Size = new System.Drawing.Size(198, 23);
			this.label19.TabIndex = 45;
			this.label19.Text = "Operazioni trovate";
			// 
			// label23
			// 
			this.label23.Location = new System.Drawing.Point(45, 129);
			this.label23.Name = "label23";
			this.label23.Size = new System.Drawing.Size(109, 23);
			this.label23.TabIndex = 50;
			this.label23.Text = "Rimanenza:";
			// 
			// label20
			// 
			this.label20.Location = new System.Drawing.Point(800, 47);
			this.label20.Name = "label20";
			this.label20.Size = new System.Drawing.Size(77, 23);
			this.label20.TabIndex = 19;
			this.label20.Text = "Cliente:";
			this.label20.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// MeseInc
			// 
			this.MeseInc.Location = new System.Drawing.Point(13, 210);
			this.MeseInc.Maximum = new decimal(new int[] {
			12,
			0,
			0,
			0});
			this.MeseInc.Minimum = new decimal(new int[] {
			1,
			0,
			0,
			0});
			this.MeseInc.Name = "MeseInc";
			this.MeseInc.Size = new System.Drawing.Size(51, 21);
			this.MeseInc.TabIndex = 54;
			this.MeseInc.Value = new decimal(new int[] {
			1,
			0,
			0,
			0});
			// 
			// label18
			// 
			this.label18.Location = new System.Drawing.Point(1018, 41);
			this.label18.Name = "label18";
			this.label18.Size = new System.Drawing.Size(64, 29);
			this.label18.TabIndex = 17;
			this.label18.Text = "Drive:";
			this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label15
			// 
			this.label15.Location = new System.Drawing.Point(0, 0);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(100, 23);
			this.label15.TabIndex = 0;
			// 
			// label14
			// 
			this.label14.Font = new System.Drawing.Font("Tahoma", 11F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.World);
			this.label14.Location = new System.Drawing.Point(13, 187);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(70, 23);
			this.label14.TabIndex = 57;
			this.label14.Text = "Mese";
			// 
			// totInc
			// 
			this.totInc.Location = new System.Drawing.Point(269, 207);
			this.totInc.Name = "totInc";
			this.totInc.Size = new System.Drawing.Size(102, 24);
			this.totInc.TabIndex = 48;
			this.totInc.Text = "0";
			this.totInc.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label16
			// 
			this.label16.Location = new System.Drawing.Point(0, 0);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(100, 23);
			this.label16.TabIndex = 0;
			// 
			// label11
			// 
			this.label11.Location = new System.Drawing.Point(237, 170);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(13, 17);
			this.label11.TabIndex = 29;
			this.label11.Text = "/";
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(301, 170);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(13, 17);
			this.label10.TabIndex = 31;
			this.label10.Text = "/";
			// 
			// label13
			// 
			this.label13.Location = new System.Drawing.Point(154, 216);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(128, 24);
			this.label13.TabIndex = 39;
			this.label13.Text = "Totale incasso:";
			// 
			// label12
			// 
			this.label12.Location = new System.Drawing.Point(122, 170);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(70, 23);
			this.label12.TabIndex = 26;
			this.label12.Text = "Fino a:";
			// 
			// Importo
			// 
			this.Importo.Location = new System.Drawing.Point(282, 199);
			this.Importo.Maximum = new decimal(new int[] {
			100000000,
			0,
			0,
			0});
			this.Importo.Name = "Importo";
			this.Importo.Size = new System.Drawing.Size(134, 26);
			this.Importo.TabIndex = 9;
			this.Importo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// Saldo
			// 
			this.Saldo.Location = new System.Drawing.Point(154, 210);
			this.Saldo.Name = "Saldo";
			this.Saldo.Size = new System.Drawing.Size(102, 24);
			this.Saldo.TabIndex = 7;
			this.Saldo.TabStop = true;
			this.Saldo.Text = "Saldo";
			// 
			// button3
			// 
			this.button3.Enabled = false;
			this.button3.Location = new System.Drawing.Point(211, 35);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(90, 29);
			this.button3.TabIndex = 12;
			this.button3.Text = "Rimuovi";
			this.button3.Click += new System.EventHandler(this.Button3Click);
			// 
			// Import
			// 
			this.Import.Text = "Importo";
			this.Import.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.Import.Width = 63;
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(64, 216);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(13, 18);
			this.label9.TabIndex = 56;
			this.label9.Text = "/";
			// 
			// labelCli
			// 
			this.labelCli.Location = new System.Drawing.Point(883, 47);
			this.labelCli.Name = "labelCli";
			this.labelCli.Size = new System.Drawing.Size(128, 23);
			this.labelCli.TabIndex = 18;
			this.labelCli.Text = "label20";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(26, 53);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(51, 23);
			this.label4.TabIndex = 17;
			this.label4.Text = "Data:";
			// 
			// Acconto
			// 
			this.Acconto.Checked = true;
			this.Acconto.Location = new System.Drawing.Point(154, 187);
			this.Acconto.Name = "Acconto";
			this.Acconto.Size = new System.Drawing.Size(102, 23);
			this.Acconto.TabIndex = 6;
			this.Acconto.TabStop = true;
			this.Acconto.Text = "Acconto";
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(141, 53);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(13, 17);
			this.label6.TabIndex = 23;
			this.label6.Text = "/";
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(218, 53);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(12, 17);
			this.label7.TabIndex = 25;
			this.label7.Text = "/";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(32, 117);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(70, 23);
			this.label1.TabIndex = 26;
			this.label1.Text = "Cliente:";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(32, 152);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(109, 23);
			this.label2.TabIndex = 28;
			this.label2.Text = "Controparte:";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(32, 210);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(77, 24);
			this.label3.TabIndex = 29;
			this.label3.Text = "Importo:";
			// 
			// numericUpDown5
			// 
			this.numericUpDown5.Location = new System.Drawing.Point(0, 0);
			this.numericUpDown5.Name = "numericUpDown5";
			this.numericUpDown5.Size = new System.Drawing.Size(120, 26);
			this.numericUpDown5.TabIndex = 0;
			// 
			// Controparte
			// 
			this.Controparte.Text = "Controparte";
			this.Controparte.Width = 120;
			// 
			// totV
			// 
			this.totV.Location = new System.Drawing.Point(154, 82);
			this.totV.Name = "totV";
			this.totV.Size = new System.Drawing.Size(102, 23);
			this.totV.TabIndex = 37;
			this.totV.Text = "0";
			this.totV.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// checkBoxCli
			// 
			this.checkBoxCli.Location = new System.Drawing.Point(122, 117);
			this.checkBoxCli.Name = "checkBoxCli";
			this.checkBoxCli.Size = new System.Drawing.Size(128, 23);
			this.checkBoxCli.TabIndex = 32;
			this.checkBoxCli.Text = "Cliente";
			// 
			// Data
			// 
			this.Data.Text = "Anno\\Mese\\Giorno";
			this.Data.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.Data.Width = 94;
			// 
			// numericUpDown6
			// 
			this.numericUpDown6.Location = new System.Drawing.Point(0, 0);
			this.numericUpDown6.Name = "numericUpDown6";
			this.numericUpDown6.Size = new System.Drawing.Size(120, 26);
			this.numericUpDown6.TabIndex = 0;
			// 
			// AnnoF
			// 
			this.AnnoF.Location = new System.Drawing.Point(314, 164);
			this.AnnoF.Maximum = new decimal(new int[] {
			2125,
			0,
			0,
			0});
			this.AnnoF.Minimum = new decimal(new int[] {
			2000,
			0,
			0,
			0});
			this.AnnoF.Name = "AnnoF";
			this.AnnoF.Size = new System.Drawing.Size(70, 26);
			this.AnnoF.TabIndex = 16;
			this.AnnoF.Value = new decimal(new int[] {
			2000,
			0,
			0,
			0});
			// 
			// numericUpDown4
			// 
			this.numericUpDown4.Location = new System.Drawing.Point(0, 0);
			this.numericUpDown4.Name = "numericUpDown4";
			this.numericUpDown4.Size = new System.Drawing.Size(120, 26);
			this.numericUpDown4.TabIndex = 0;
			// 
			// button4
			// 
			this.button4.Enabled = false;
			this.button4.Location = new System.Drawing.Point(13, 117);
			this.button4.Name = "button4";
			this.button4.Size = new System.Drawing.Size(89, 47);
			this.button4.TabIndex = 13;
			this.button4.Text = "Cerca";
			this.button4.Click += new System.EventHandler(this.Button4Click);
			// 
			// button5
			// 
			this.button5.Location = new System.Drawing.Point(314, 47);
			this.button5.Name = "button5";
			this.button5.Size = new System.Drawing.Size(57, 29);
			this.button5.TabIndex = 50;
			this.button5.TabStop = false;
			this.button5.Text = "Oggi";
			this.button5.Click += new System.EventHandler(this.Button5Click);
			// 
			// button6
			// 
			this.button6.Location = new System.Drawing.Point(461, 41);
			this.button6.Name = "button6";
			this.button6.Size = new System.Drawing.Size(120, 34);
			this.button6.TabIndex = 12;
			this.button6.Text = "Carica";
			this.button6.Click += new System.EventHandler(this.Button6Click);
			// 
			// button7
			// 
			this.button7.Location = new System.Drawing.Point(358, 257);
			this.button7.Name = "button7";
			this.button7.Size = new System.Drawing.Size(71, 29);
			this.button7.TabIndex = 51;
			this.button7.TabStop = false;
			this.button7.Text = "Reset";
			this.button7.Click += new System.EventHandler(this.Button7Click);
			// 
			// Anno
			// 
			this.Anno.Location = new System.Drawing.Point(237, 47);
			this.Anno.Maximum = new decimal(new int[] {
			2125,
			0,
			0,
			0});
			this.Anno.Minimum = new decimal(new int[] {
			2000,
			0,
			0,
			0});
			this.Anno.Name = "Anno";
			this.Anno.Size = new System.Drawing.Size(77, 26);
			this.Anno.TabIndex = 3;
			this.Anno.Value = new decimal(new int[] {
			2019,
			0,
			0,
			0});
			this.Anno.ValueChanged += new System.EventHandler(this.AnnoValueChanged);
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(13, 35);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(89, 47);
			this.button1.TabIndex = 9;
			this.button1.Text = "Aggiungi";
			this.button1.Click += new System.EventHandler(this.Button1Click);
			// 
			// Cliente
			// 
			this.Cliente.Text = "Cliente";
			this.Cliente.Width = 120;
			// 
			// button2
			// 
			this.button2.Enabled = false;
			this.button2.Location = new System.Drawing.Point(122, 35);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(89, 29);
			this.button2.TabIndex = 11;
			this.button2.Text = "Modifica";
			this.button2.Click += new System.EventHandler(this.Button2Click);
			// 
			// Cli
			// 
			this.Cli.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			this.Cli.Location = new System.Drawing.Point(154, 111);
			this.Cli.Name = "Cli";
			this.Cli.Size = new System.Drawing.Size(179, 26);
			this.Cli.TabIndex = 4;
			this.Cli.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CliKeyDown);
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.listView1);
			this.groupBox2.Location = new System.Drawing.Point(461, 76);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(720, 912);
			this.groupBox2.TabIndex = 10;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Archivio:";
			// 
			// listView1
			// 
			this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
			this.Data,
			this.Cliente,
			this.Controparte,
			this.Import,
			this.CAS});
			this.listView1.Dock = System.Windows.Forms.DockStyle.Left;
			this.listView1.FullRowSelect = true;
			this.listView1.GridLines = true;
			this.listView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.listView1.HideSelection = false;
			this.listView1.LabelWrap = false;
			this.listView1.Location = new System.Drawing.Point(3, 22);
			this.listView1.MultiSelect = false;
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(705, 887);
			this.listView1.Sorting = System.Windows.Forms.SortOrder.Descending;
			this.listView1.TabIndex = 7;
			this.listView1.UseCompatibleStateImageBehavior = false;
			this.listView1.View = System.Windows.Forms.View.Details;
			this.listView1.SelectedIndexChanged += new System.EventHandler(this.ListView1SelectedIndexChanged);
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(45, 82);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(77, 23);
			this.label8.TabIndex = 31;
			this.label8.Text = "Versato:";
			// 
			// groupBox4
			// 
			this.groupBox4.Controls.Add(this.label14);
			this.groupBox4.Controls.Add(this.label9);
			this.groupBox4.Controls.Add(this.AnnoInc);
			this.groupBox4.Controls.Add(this.MeseInc);
			this.groupBox4.Controls.Add(this.totR);
			this.groupBox4.Controls.Add(this.label23);
			this.groupBox4.Controls.Add(this.totInc);
			this.groupBox4.Controls.Add(this.label19);
			this.groupBox4.Controls.Add(this.label13);
			this.groupBox4.Controls.Add(this.totC);
			this.groupBox4.Controls.Add(this.totV);
			this.groupBox4.Controls.Add(this.label8);
			this.groupBox4.Controls.Add(this.label5);
			this.groupBox4.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
			this.groupBox4.Location = new System.Drawing.Point(6, 555);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size(448, 285);
			this.groupBox4.TabIndex = 11;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "Contabilità :";
			// 
			// AnnoSel
			// 
			this.AnnoSel.Location = new System.Drawing.Point(595, 41);
			this.AnnoSel.Maximum = new decimal(new int[] {
			2125,
			0,
			0,
			0});
			this.AnnoSel.Minimum = new decimal(new int[] {
			2000,
			0,
			0,
			0});
			this.AnnoSel.Name = "AnnoSel";
			this.AnnoSel.Size = new System.Drawing.Size(83, 26);
			this.AnnoSel.TabIndex = 13;
			this.AnnoSel.Value = new decimal(new int[] {
			2019,
			0,
			0,
			0});
			// 
			// DriveL
			// 
			this.DriveL.Items.AddRange(new object[] {
			"C:",
			"D:",
			"E:",
			"F:",
			"G:",
			"H:"});
			this.DriveL.Location = new System.Drawing.Point(1088, 41);
			this.DriveL.Name = "DriveL";
			this.DriveL.Size = new System.Drawing.Size(64, 28);
			this.DriveL.TabIndex = 15;
			this.DriveL.SelectedIndexChanged += new System.EventHandler(this.DriveLSelectedIndexChanged);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.button7);
			this.groupBox1.Controls.Add(this.Importo);
			this.groupBox1.Controls.Add(this.button5);
			this.groupBox1.Controls.Add(this.Concordato);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.label7);
			this.groupBox1.Controls.Add(this.Anno);
			this.groupBox1.Controls.Add(this.label6);
			this.groupBox1.Controls.Add(this.Mese);
			this.groupBox1.Controls.Add(this.Giorno);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.Cli);
			this.groupBox1.Controls.Add(this.Contro);
			this.groupBox1.Controls.Add(this.Saldo);
			this.groupBox1.Controls.Add(this.Acconto);
			this.groupBox1.Location = new System.Drawing.Point(6, 35);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(448, 304);
			this.groupBox1.TabIndex = 8;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Dettagli:";
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.checkBoxData);
			this.groupBox3.Controls.Add(this.checkBoxCli);
			this.groupBox3.Controls.Add(this.label10);
			this.groupBox3.Controls.Add(this.AnnoF);
			this.groupBox3.Controls.Add(this.label11);
			this.groupBox3.Controls.Add(this.MeseF);
			this.groupBox3.Controls.Add(this.GiornoF);
			this.groupBox3.Controls.Add(this.label12);
			this.groupBox3.Controls.Add(this.button4);
			this.groupBox3.Controls.Add(this.button3);
			this.groupBox3.Controls.Add(this.button2);
			this.groupBox3.Controls.Add(this.button1);
			this.groupBox3.Location = new System.Drawing.Point(6, 339);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(448, 216);
			this.groupBox3.TabIndex = 9;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Comandi:";
			// 
			// GiornoF
			// 
			this.GiornoF.Location = new System.Drawing.Point(186, 164);
			this.GiornoF.Maximum = new decimal(new int[] {
			31,
			0,
			0,
			0});
			this.GiornoF.Minimum = new decimal(new int[] {
			1,
			0,
			0,
			0});
			this.GiornoF.Name = "GiornoF";
			this.GiornoF.Size = new System.Drawing.Size(51, 26);
			this.GiornoF.TabIndex = 14;
			this.GiornoF.Value = new decimal(new int[] {
			1,
			0,
			0,
			0});
			// 
			// MainForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(8, 19);
			this.ClientSize = new System.Drawing.Size(1188, 912);
			this.Controls.Add(this.label20);
			this.Controls.Add(this.labelCli);
			this.Controls.Add(this.label18);
			this.Controls.Add(this.tutti);
			this.Controls.Add(this.DriveL);
			this.Controls.Add(this.AnnoSel);
			this.Controls.Add(this.button6);
			this.Controls.Add(this.groupBox4);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.groupBox2);
			this.Menu = this.mainMenu1;
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Rubrica contabile - 19";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.MainFormClosing);
			this.Load += new System.EventHandler(this.MainFormLoad);
			((System.ComponentModel.ISupportInitialize)(this.Giorno)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.MeseF)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.Mese)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.AnnoInc)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.MeseInc)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.Importo)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown5)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown6)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.AnnoF)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown4)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.Anno)).EndInit();
			this.groupBox2.ResumeLayout(false);
			this.groupBox4.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.AnnoSel)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox3.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.GiornoF)).EndInit();
			this.ResumeLayout(false);

		}

		private void ListView1SelectedIndexChanged(object sender, EventArgs e)
		{
			this.button2.Enabled = true;
			this.button3.Enabled = true;
			this.Acconto.Checked = false;
			this.Saldo.Checked = false;
			this.Concordato.Checked = false;
			this.Acconto.Enabled = false;
			this.Saldo.Enabled = false;
			this.Concordato.Enabled = false;
			this.Importo.Value = 0M;
			this.Cli.Text = "";
			this.Contro.Text = "";
			string[] strDataCorrente = new string[3];
			char[] separator = new char[] { '\\' };
			foreach (ListViewItem item in this.listView1.SelectedItems)
			{
				strDataCorrente = item.SubItems[0].Text.Split(separator);
				this.Anno.Text = strDataCorrente[0];
				this.Mese.Text = strDataCorrente[1];
				this.Giorno.Text = strDataCorrente[2];
				this.Cli.Text = item.SubItems[1].Text;
				this.Contro.Text = item.SubItems[2].Text;
				if (item.SubItems[4].Text == "A")
				{
					this.Acconto.Checked = true;
				}
				else if (item.SubItems[4].Text == "S")
				{
					this.Saldo.Checked = true;
				}
				else if (item.SubItems[4].Text == "C")
				{
					this.Concordato.Checked = true;
				}
				this.Importo.Value = Convert.ToDecimal(item.SubItems[3].Text);
			}
		}

		[STAThread]
		public static void Main(string[] args)
		{
			Application.Run(new MainForm());
		}

		private void MainFormClosing(object sender, CancelEventArgs e)
		{
			if (!this.salvato && (MessageBox.Show("Uscire senza salvare?", "Conferma", MessageBoxButtons.YesNo) == DialogResult.No))
			{
				e.Cancel = true;
			}
		}

		private void MainFormLoad(object sender, EventArgs e)
		{
			this.Giorno.Value = DateTime.Now.Day;
			this.Mese.Value = DateTime.Now.Month;
			this.Anno.Value = DateTime.Now.Year;
			this.GiornoF.Value = DateTime.Now.Day;
			this.MeseF.Value = DateTime.Now.Month;
			this.AnnoF.Value = DateTime.Now.Year;
			this.MeseInc.Value = DateTime.Now.Month;
			this.AnnoInc.Value = DateTime.Now.Year;
			this.AnnoSel.Value = DateTime.Now.Year;
			campo = "";
			this.wrk_dir = @"\Rubrica\";
			this.salvato = true;
			this.drive = Environment.CurrentDirectory.ToString();
			if (File.Exists(this.drive + @"\archivi\dati" + this.Anno.ToString() + ".fm"))
			{
				this.menuItem1.Enabled = false;
			}
			else
			{
				this.menuItem1.Enabled = true;
			}
		}

		private void MenuItem3Click(object sender, EventArgs e)
		{
			campo = "";
			Form1 form = new Form1();
			if (!File.Exists(this.drive + @"\atad.fm"))
			{
				form.funzione = "salva";
			}
			else
			{
				if (!this.caricato)
				{
					MessageBox.Show("Prima di salvare \x00e9 necessario caricare i dati");
					return;
				}
				form.funzione = "carica";
			}
			form.wrkdir = this.wrk_dir;
			form.drive = this.drive;
			form.ShowDialog();
			if (campo != "carica")
			{
				if (campo == "errata")
				{
					MessageBox.Show("Password errata");
				}
				else if (campo == "")
				{
					MessageBox.Show("Password salvata");
				}
			}
			else
			{
				if (!Directory.Exists(this.drive + @"\archivi"))
				{
					Directory.CreateDirectory(this.drive + @"\archivi");
				}
				char[] separator = new char[] { '\\' };
				string[] strDataCorrente = new string[] { "null" };
				int num = 0;
				while (true)
				{
					if (num >= this.listView1.Items.Count)
					{
						FileStream stream2 = new FileStream("drv", FileMode.Create, FileAccess.Write);
						StreamWriter writer2 = new StreamWriter(stream2);
						writer2.Write(this.DriveL.Text);
						writer2.Close();
						stream2.Close();
						this.DriveL.Enabled = true;
						break;
					}
					strDataCorrente = this.listView1.Items[num].SubItems[0].Text.Split(separator);
					string path = this.drive + @"\archivi\dati" + strDataCorrente[0] + ".mf";
					FileStream stream = new FileStream(path, FileMode.Create, FileAccess.Write);
					try
					{
						StreamWriter writer = new StreamWriter(stream);
						strDataCorrente = this.listView1.Items[num].SubItems[0].Text.Split(separator);
						while (true)
						{
							if (strDataCorrente[0] == this.listView1.Items[num].SubItems[0].Text.Substring(0, 4))
							{
								writer.Write("###" + this.listView1.Items[num].SubItems[0].Text);
								writer.Write("###" + this.listView1.Items[num].SubItems[1].Text);
								writer.Write("###" + this.listView1.Items[num].SubItems[2].Text);
								writer.Write("###" + this.listView1.Items[num].SubItems[3].Text);
								string str2 = "###" + this.listView1.Items[num].SubItems[4].Text;
								writer.WriteLine(str2);
								if (num < (this.listView1.Items.Count - 1))
								{
									strDataCorrente = this.listView1.Items[num].SubItems[0].Text.Split(separator);
									num++;
									continue;
								}
								num++;
							}
							writer.Close();
							stream.Close();
							this.salvato = true;
							break;
						}
					}
					finally
					{
						if (stream == null)
						{
							//continue;
						}
						stream.Dispose();
					}
				}
			}
			this.salvato = true;
		}

		private bool ResetField()
		{
			this.button3.Enabled = false;
			this.button2.Enabled = false;
			this.Cli.Text = "";
			this.Contro.Text = "";
			this.Importo.Value = 0M;
			this.Acconto.Checked = true;
			this.Saldo.Checked = false;
			this.Concordato.Checked = false;
			return true;
		}
		void AnnoValueChanged(object sender, EventArgs e)
		{
	
		}
	}
}

