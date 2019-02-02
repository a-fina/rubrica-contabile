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
            double num = 0.0;
            double num2 = 0.0;
            double num3 = 0.0;
            double num4 = 0.0;
            try
            {
                Form2 form = new Form2();
                double num5 = ((Convert.ToInt32(this.Anno.Text) * 0x2710) + (Convert.ToInt32(this.Mese.Text) * 100)) + Convert.ToInt32(this.Giorno.Text);
                double num6 = ((Convert.ToInt32(this.AnnoF.Text) * 0x2710) + (Convert.ToInt32(this.MeseF.Text) * 100)) + Convert.ToInt32(this.GiornoF.Text);
                int num7 = 0;
                while (true)
                {
                    if (num7 >= this.listView1.Items.Count)
                    {
                        if (form.listView1.Items.Count > 0)
                        {
                            form.Show();
                        }
                        this.totV.Text = num.ToString();
                        if (num4 < 0.0)
                        {
                            num4 = 0.0;
                        }
                        this.totR.Text = num4.ToString();
                        this.totC.Text = num3.ToString();
                        this.totInc.Text = num2.ToString();
                        this.Refresh();
                        break;
                    }
                    this.listView1.Items[num7].BackColor = Color.White;
                    flag = false;
                    string[] strArray = new string[3];
                    char[] separator = new char[] { '\\' };
                    strArray = this.listView1.Items[num7].SubItems[0].Text.Split(separator);
                    double num8 = ((Convert.ToInt32(strArray[0]) * 0x2710) + (Convert.ToInt32(strArray[1]) * 100)) + Convert.ToInt32(strArray[2]);
                    if (((Convert.ToInt32(this.AnnoInc.Text) == Convert.ToInt32(strArray[0])) && (Convert.ToInt32(this.MeseInc.Text) == Convert.ToInt32(strArray[1]))) && (this.listView1.Items[num7].SubItems[4].Text != "C"))
                    {
                        num2 += Convert.ToInt32(this.listView1.Items[num7].SubItems[3].Text);
                    }
                    if (((this.Cli.Text != "") && this.checkBoxCli.Checked) && this.listView1.Items[num7].SubItems[1].Text.StartsWith(this.Cli.Text))
                    {
                        if (!this.checkBoxData.Checked)
                        {
                            flag = true;
                        }
                        else if ((num5 <= num8) && (num8 <= num6))
                        {
                            flag = true;
                        }
                    }
                    if ((this.checkBoxData.Checked && (num5 <= num8)) && (num8 <= num6))
                    {
                        if (!this.checkBoxCli.Checked)
                        {
                            flag = true;
                        }
                        else if ((this.Cli.Text != "") && this.listView1.Items[num7].SubItems[1].Text.StartsWith(this.Cli.Text))
                        {
                            flag = true;
                        }
                    }
                    ListViewItem item = new ListViewItem(this.listView1.Items[num7].SubItems[0].Text);
                    if (flag)
                    {
                        this.listView1.Items[num7].BackColor = Color.Yellow;
                        if (this.listView1.Items[num7].SubItems[4].Text == "C")
                        {
                            num3 += Convert.ToDouble(this.listView1.Items[num7].SubItems[3].Text);
                            num4 += Convert.ToDouble(this.listView1.Items[num7].SubItems[3].Text);
                        }
                        else
                        {
                            num += Convert.ToDouble(this.listView1.Items[num7].SubItems[3].Text);
                            num4 -= Convert.ToDouble(this.listView1.Items[num7].SubItems[3].Text);
                        }
                        item.SubItems.Add(this.listView1.Items[num7].SubItems[1].Text);
                        item.SubItems.Add(this.listView1.Items[num7].SubItems[2].Text);
                        item.SubItems.Add(this.listView1.Items[num7].SubItems[3].Text);
                        item.SubItems.Add(this.listView1.Items[num7].SubItems[4].Text);
                        form.listView1.Items.Add(item);
                    }
                    num7++;
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
                                        string[] strArray = str2.Replace("###", "\x00b1").Split(separator);
                                        if ((filtra_cliente == "") || strArray[2].StartsWith(filtra_cliente))
                                        {
                                            ListViewItem item = new ListViewItem(strArray[1]) {
                                                SubItems = { 
                                                    strArray[2],
                                                    strArray[3],
                                                    strArray[4],
                                                    strArray[5]
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
                                            bool flag1 = strArray[3] != "C";
                                        }
                                        else
                                        {
                                            this.menuItem3.Enabled = false;
                                            this.menuItem1.Enabled = false;
                                            if ((filtra_cliente == strArray[2]) && (strArray[3] == "C"))
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
            this.CAS = new ColumnHeader();
            this.Concordato = new RadioButton();
            this.Contro = new TextBox();
            this.totR = new Label();
            this.mainMenu1 = new MainMenu();
            this.menuItem1 = new MenuItem();
            this.menuItem3 = new MenuItem();
            this.Giorno = new NumericUpDown();
            this.checkBoxData = new CheckBox();
            this.totC = new Label();
            this.MeseF = new NumericUpDown();
            this.Mese = new NumericUpDown();
            this.tutti = new CheckBox();
            this.AnnoInc = new NumericUpDown();
            this.label5 = new Label();
            this.label17 = new Label();
            this.label19 = new Label();
            this.label23 = new Label();
            this.label20 = new Label();
            this.MeseInc = new NumericUpDown();
            this.label18 = new Label();
            this.label15 = new Label();
            this.label14 = new Label();
            this.totInc = new Label();
            this.label16 = new Label();
            this.label11 = new Label();
            this.label10 = new Label();
            this.label13 = new Label();
            this.label12 = new Label();
            this.Importo = new NumericUpDown();
            this.Saldo = new RadioButton();
            this.button3 = new Button();
            this.Import = new ColumnHeader();
            this.label9 = new Label();
            this.labelCli = new Label();
            this.label4 = new Label();
            this.Acconto = new RadioButton();
            this.label6 = new Label();
            this.label7 = new Label();
            this.label1 = new Label();
            this.label2 = new Label();
            this.label3 = new Label();
            this.numericUpDown5 = new NumericUpDown();
            this.Controparte = new ColumnHeader();
            this.totV = new Label();
            this.checkBoxCli = new CheckBox();
            this.Data = new ColumnHeader();
            this.numericUpDown6 = new NumericUpDown();
            this.AnnoF = new NumericUpDown();
            this.numericUpDown4 = new NumericUpDown();
            this.button4 = new Button();
            this.button5 = new Button();
            this.button6 = new Button();
            this.button7 = new Button();
            this.Anno = new NumericUpDown();
            this.button1 = new Button();
            this.Cliente = new ColumnHeader();
            this.button2 = new Button();
            this.Cli = new TextBox();
            this.groupBox2 = new GroupBox();
            this.label8 = new Label();
            this.groupBox4 = new GroupBox();
            this.AnnoSel = new NumericUpDown();
            this.DriveL = new ComboBox();
            this.listView1 = new ListView();
            this.groupBox1 = new GroupBox();
            this.groupBox3 = new GroupBox();
            this.GiornoF = new NumericUpDown();
            this.Giorno.BeginInit();
            this.MeseF.BeginInit();
            this.Mese.BeginInit();
            this.AnnoInc.BeginInit();
            this.MeseInc.BeginInit();
            this.Importo.BeginInit();
            this.numericUpDown5.BeginInit();
            this.numericUpDown6.BeginInit();
            this.AnnoF.BeginInit();
            this.numericUpDown4.BeginInit();
            this.Anno.BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.AnnoSel.BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.GiornoF.BeginInit();
            base.SuspendLayout();
            this.CAS.Text = "C/A/S";
            this.CAS.TextAlign = HorizontalAlignment.Center;
            this.CAS.Width = 0x35;
            this.Concordato.Location = new Point(0x60, 160);
            this.Concordato.Name = "Concordato";
            this.Concordato.Size = new Size(0x58, 0x10);
            this.Concordato.TabIndex = 8;
            this.Concordato.TabStop = true;
            this.Concordato.Text = "Concordato";
            this.Contro.CharacterCasing = CharacterCasing.Upper;
            this.Contro.Location = new Point(0x60, 100);
            this.Contro.Name = "Contro";
            this.Contro.Size = new Size(0x70, 20);
            this.Contro.TabIndex = 5;
            this.Contro.Text = "";
            this.Contro.KeyDown += new KeyEventHandler(this.ControKeyDown);
            this.totR.Location = new Point(0x60, 0x58);
            this.totR.Name = "totR";
            this.totR.Size = new Size(0x40, 0x10);
            this.totR.TabIndex = 0x33;
            this.totR.Text = "0";
            this.totR.TextAlign = ContentAlignment.MiddleRight;
            MenuItem[] items = new MenuItem[] { this.menuItem1 };
            this.mainMenu1.MenuItems.AddRange(items);
            this.menuItem1.Index = 0;
            items = new MenuItem[] { this.menuItem3 };
            this.menuItem1.MenuItems.AddRange(items);
            this.menuItem1.Text = "Menu";
            this.menuItem3.Index = 0;
            this.menuItem3.Text = "Salva dati";
            this.menuItem3.Click += new EventHandler(this.MenuItem3Click);
            this.Giorno.Location = new Point(0x34, 0x20);
            this.Giorno.Maximum = new decimal(new int[] { 100,0,0,0 });
            this.Giorno.Minimum = new decimal(new int[] { 1,0,0,0 });
            this.Giorno.Name = "Giorno";
            this.Giorno.Size = new Size(0x24, 20);
            this.Giorno.TabIndex = 1;
            this.Giorno.TextAlign = HorizontalAlignment.Right;
            // this.Giorno.Value = new decimal(new int[] { 1,0,0,0 });
            this.checkBoxData.Location = new Point(0x4c, 0x5c);
            this.checkBoxData.Name = "checkBoxData";
            this.checkBoxData.Size = new Size(0x54, 20);
            this.checkBoxData.TabIndex = 0x21;
            this.checkBoxData.Text = "Data";
            this.totC.Location = new Point(0x60, 0x48);
            this.totC.Name = "totC";
            this.totC.Size = new Size(0x40, 0x10);
            this.totC.TabIndex = 0x26;
            this.totC.Text = "0";
            this.totC.TextAlign = ContentAlignment.MiddleRight;
            this.MeseF.Location = new Point(0x9c, 0x70);
           // this.MeseF.Maximum = new decimal(new int[] { 12 });
           // this.MeseF.Minimum = new decimal(new int[] { 1 });
            this.MeseF.Name = "MeseF";
            this.MeseF.Size = new Size(0x20, 20);
            this.MeseF.TabIndex = 15;
           // this.MeseF.Value = new decimal(new int[] { 1 });
            this.Mese.Location = new Point(100, 0x20);
           // this.Mese.Maximum = new decimal(new int[] { 12 });
           // this.Mese.Minimum = new decimal(new int[] { 1 });
            this.Mese.Name = "Mese";
            this.Mese.Size = new Size(0x24, 20);
            this.Mese.TabIndex = 2;
          //  this.Mese.Value = new decimal(new int[] { 1 });
            this.tutti.Location = new Point(0x1a8, 0x1c);
            this.tutti.Name = "tutti";
            this.tutti.Size = new Size(0x40, 0x10);
            this.tutti.TabIndex = 0x10;
            this.tutti.Text = "Tutti";
            this.AnnoInc.Location = new Point(0x30, 0x90);
         	this.AnnoInc.Maximum = new decimal(new int[] { 2025,0,0,0 });
         	this.AnnoInc.Minimum = new decimal(new int[] { 0,0,0,0 });
            this.AnnoInc.Name = "AnnoInc";
            this.AnnoInc.Size = new Size(0x2c, 0x15);
            this.AnnoInc.TabIndex = 0x37;
         //   this.AnnoInc.Value = new decimal(new int[] { 0x7d5 });
            this.label5.Location = new Point(0x1c, 0x48);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x44, 0x10);
            this.label5.TabIndex = 30;
            this.label5.Text = "Concordato:";
            this.label17.Location = new Point(0, 0);
            this.label17.Name = "label17";
            this.label17.TabIndex = 0;
            this.label19.Font = new Font("Tahoma", 11f, FontStyle.Underline | FontStyle.Bold, GraphicsUnit.World);
            this.label19.Location = new Point(8, 0x24);
            this.label19.Name = "label19";
            this.label19.Size = new Size(0x7c, 0x10);
            this.label19.TabIndex = 0x2d;
            this.label19.Text = "Operazioni trovate";
            this.label23.Location = new Point(0x1c, 0x58);
            this.label23.Name = "label23";
            this.label23.Size = new Size(0x44, 0x10);
            this.label23.TabIndex = 50;
            this.label23.Text = "Rimanenza:";
            this.label20.Location = new Point(500, 0x20);
            this.label20.Name = "label20";
            this.label20.Size = new Size(0x30, 0x10);
            this.label20.TabIndex = 0x13;
            this.label20.Text = "Cliente:";
            this.label20.TextAlign = ContentAlignment.TopRight;
            this.MeseInc.Location = new Point(8, 0x90);
          //  this.MeseInc.Maximum = new decimal(new int[] { 12 });
          //  this.MeseInc.Minimum = new decimal(new int[] { 1 });
            this.MeseInc.Name = "MeseInc";
            this.MeseInc.Size = new Size(0x20, 0x15);
            this.MeseInc.TabIndex = 0x36;
          //  this.MeseInc.Value = new decimal(new int[] { 1 });
            this.label18.Location = new Point(0x27c, 0x1c);
            this.label18.Name = "label18";
            this.label18.Size = new Size(40, 20);
            this.label18.TabIndex = 0x11;
            this.label18.Text = "Drive:";
            this.label18.TextAlign = ContentAlignment.MiddleRight;
            this.label15.Location = new Point(0, 0);
            this.label15.Name = "label15";
            this.label15.TabIndex = 0;
            this.label14.Font = new Font("Tahoma", 11f, FontStyle.Underline | FontStyle.Bold, GraphicsUnit.World);
            this.label14.Location = new Point(8, 0x80);
            this.label14.Name = "label14";
            this.label14.Size = new Size(0x2c, 0x10);
            this.label14.TabIndex = 0x39;
            this.label14.Text = "Mese";
            this.totInc.Location = new Point(0xac, 0x94);
            this.totInc.Name = "totInc";
            this.totInc.Size = new Size(0x48, 0x10);
            this.totInc.TabIndex = 0x30;
            this.totInc.Text = "0";
            this.totInc.TextAlign = ContentAlignment.MiddleRight;
            this.label16.Location = new Point(0, 0);
            this.label16.Name = "label16";
            this.label16.TabIndex = 0;
            this.label11.Location = new Point(0x94, 0x74);
            this.label11.Name = "label11";
            this.label11.Size = new Size(8, 12);
            this.label11.TabIndex = 0x1d;
            this.label11.Text = "/";
            this.label10.Location = new Point(0xbc, 0x74);
            this.label10.Name = "label10";
            this.label10.Size = new Size(8, 12);
            this.label10.TabIndex = 0x1f;
            this.label10.Text = "/";
            this.label13.Location = new Point(0x60, 0x94);
            this.label13.Name = "label13";
            this.label13.Size = new Size(80, 0x10);
            this.label13.TabIndex = 0x27;
            this.label13.Text = "Totale incasso:";
            this.label12.Location = new Point(0x4c, 0x74);
            this.label12.Name = "label12";
            this.label12.Size = new Size(0x2c, 0x10);
            this.label12.TabIndex = 0x1a;
            this.label12.Text = "Fino a:";
            this.Importo.Location = new Point(0xb0, 0x88);
       //     this.Importo.Maximum = new decimal(new int[] { 0xf4240 });
            this.Importo.Name = "Importo";
            this.Importo.Size = new Size(0x54, 20);
            this.Importo.TabIndex = 9;
            this.Importo.TextAlign = HorizontalAlignment.Right;
            this.Saldo.Location = new Point(0x60, 0x90);
            this.Saldo.Name = "Saldo";
            this.Saldo.Size = new Size(0x40, 0x10);
            this.Saldo.TabIndex = 7;
            this.Saldo.TabStop = true;
            this.Saldo.Text = "Saldo";
            this.button3.Enabled = false;
            this.button3.Location = new Point(0x84, 0x18);
            this.button3.Name = "button3";
            this.button3.Size = new Size(0x38, 20);
            this.button3.TabIndex = 12;
            this.button3.Text = "Rimuovi";
            this.button3.Click += new EventHandler(this.Button3Click);
            this.Import.Text = "Importo";
            this.Import.TextAlign = HorizontalAlignment.Right;
            this.Import.Width = 0x3f;
            this.label9.Location = new Point(40, 0x94);
            this.label9.Name = "label9";
            this.label9.Size = new Size(8, 12);
            this.label9.TabIndex = 0x38;
            this.label9.Text = "/";
            this.labelCli.Location = new Point(0x228, 0x20);
            this.labelCli.Name = "labelCli";
            this.labelCli.Size = new Size(80, 0x10);
            this.labelCli.TabIndex = 0x12;
            this.labelCli.Text = "label20";
            this.label4.Location = new Point(0x10, 0x24);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x20, 0x10);
            this.label4.TabIndex = 0x11;
            this.label4.Text = "Data:";
            this.Acconto.Checked = true;
            this.Acconto.Location = new Point(0x60, 0x80);
            this.Acconto.Name = "Acconto";
            this.Acconto.Size = new Size(0x40, 0x10);
            this.Acconto.TabIndex = 6;
            this.Acconto.TabStop = true;
            this.Acconto.Text = "Acconto";
            this.label6.Location = new Point(0x58, 0x24);
            this.label6.Name = "label6";
            this.label6.Size = new Size(8, 12);
            this.label6.TabIndex = 0x17;
            this.label6.Text = "/";
            this.label7.Location = new Point(0x88, 0x24);
            this.label7.Name = "label7";
            this.label7.Size = new Size(8, 12);
            this.label7.TabIndex = 0x19;
            this.label7.Text = "/";
            this.label1.Location = new Point(20, 80);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x2c, 0x10);
            this.label1.TabIndex = 0x1a;
            this.label1.Text = "Cliente:";
            this.label2.Location = new Point(20, 0x68);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x44, 0x10);
            this.label2.TabIndex = 0x1c;
            this.label2.Text = "Controparte:";
            this.label3.Location = new Point(20, 0x90);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x30, 0x10);
            this.label3.TabIndex = 0x1d;
            this.label3.Text = "Importo:";
            this.numericUpDown5.Location = new Point(0, 0);
            this.numericUpDown5.Name = "numericUpDown5";
            this.numericUpDown5.TabIndex = 0;
            this.Controparte.Text = "Controparte";
            this.Controparte.Width = 120;
            this.totV.Location = new Point(0x60, 0x38);
            this.totV.Name = "totV";
            this.totV.Size = new Size(0x40, 0x10);
            this.totV.TabIndex = 0x25;
            this.totV.Text = "0";
            this.totV.TextAlign = ContentAlignment.MiddleRight;
            this.checkBoxCli.Location = new Point(0x4c, 80);
            this.checkBoxCli.Name = "checkBoxCli";
            this.checkBoxCli.Size = new Size(80, 0x10);
            this.checkBoxCli.TabIndex = 0x20;
            this.checkBoxCli.Text = "Cliente";
            this.Data.Text = @"Anno\Mese\Giorno";
            this.Data.TextAlign = HorizontalAlignment.Center;
            this.Data.Width = 0x5e;
            this.numericUpDown6.Location = new Point(0, 0);
            this.numericUpDown6.Name = "numericUpDown6";
            this.numericUpDown6.TabIndex = 0;
            this.AnnoF.Location = new Point(0xc4, 0x70);
       		this.AnnoF.Maximum = new decimal(new int[] { 2025,0,0,0 });
       		this.AnnoF.Minimum = new decimal(new int[] { 0,0,0,0 });
            this.AnnoF.Name = "AnnoF";
            this.AnnoF.Size = new Size(0x2c, 20);
            this.AnnoF.TabIndex = 0x10;
         //   this.AnnoF.Value = new decimal(new int[] { 0x7d5 });
            this.numericUpDown4.Location = new Point(0, 0);
            this.numericUpDown4.Name = "numericUpDown4";
            this.numericUpDown4.TabIndex = 0;
            this.button4.Enabled = false;
            this.button4.Location = new Point(8, 80);
            this.button4.Name = "button4";
            this.button4.Size = new Size(0x38, 0x20);
            this.button4.TabIndex = 13;
            this.button4.Text = "Cerca";
            this.button4.Click += new EventHandler(this.Button4Click);
            this.button5.Location = new Point(0xc4, 0x20);
            this.button5.Name = "button5";
            this.button5.Size = new Size(0x24, 20);
            this.button5.TabIndex = 50;
            this.button5.TabStop = false;
            this.button5.Text = "Oggi";
            this.button5.Click += new EventHandler(this.Button5Click);
            this.button6.Location = new Point(0x120, 0x1c);
            this.button6.Name = "button6";
            this.button6.TabIndex = 12;
            this.button6.Text = "Carica";
            this.button6.Click += new EventHandler(this.Button6Click);
            this.button7.Location = new Point(0xe0, 0xb0);
            this.button7.Name = "button7";
            this.button7.Size = new Size(0x2c, 20);
            this.button7.TabIndex = 0x33;
            this.button7.TabStop = false;
            this.button7.Text = "Reset";
            this.button7.Click += new EventHandler(this.Button7Click);
            this.Anno.Location = new Point(0x94, 0x20);
            this.Anno.Maximum = new decimal(new int[] { 2025,0,0,0 });
     		this.Anno.Minimum = new decimal(new int[] { 10,0,0,0 });
            this.Anno.Name = "Anno";
            this.Anno.Size = new Size(0x30, 20);
            this.Anno.TabIndex = 3;
      //      this.Anno.Value = new decimal(new int[] { 0x7d5 });
            this.button1.Location = new Point(8, 0x18);
            this.button1.Name = "button1";
            this.button1.Size = new Size(0x38, 0x20);
            this.button1.TabIndex = 9;
            this.button1.Text = "Aggiungi";
            this.button1.Click += new EventHandler(this.Button1Click);
            this.Cliente.Text = "Cliente";
            this.Cliente.Width = 120;
            this.button2.Enabled = false;
            this.button2.Location = new Point(0x4c, 0x18);
            this.button2.Name = "button2";
            this.button2.Size = new Size(0x38, 20);
            this.button2.TabIndex = 11;
            this.button2.Text = "Modifica";
            this.button2.Click += new EventHandler(this.Button2Click);
            this.Cli.CharacterCasing = CharacterCasing.Upper;
            this.Cli.Location = new Point(0x60, 0x4c);
            this.Cli.Name = "Cli";
            this.Cli.Size = new Size(0x70, 20);
            this.Cli.TabIndex = 4;
            this.Cli.Text = "";
            this.Cli.KeyDown += new KeyEventHandler(this.CliKeyDown);
            this.groupBox2.Controls.Add(this.listView1);
            this.groupBox2.Location = new Point(0x120, 0x34);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(520, 0x270);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Archivio:";
            this.label8.Location = new Point(0x1c, 0x38);
            this.label8.Name = "label8";
            this.label8.Size = new Size(0x30, 0x10);
            this.label8.TabIndex = 0x1f;
            this.label8.Text = "Versato:";
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
            this.groupBox4.Font = new Font("Tahoma", 11f, FontStyle.Regular, GraphicsUnit.World);
            this.groupBox4.Location = new Point(4, 380);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new Size(280, 180);
            this.groupBox4.TabIndex = 11;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Contabilit\x00e0\x00a0:";
            this.AnnoSel.Location = new Point(0x174, 0x1c);
        	this.AnnoSel.Maximum = new decimal(new int[] { 2025,0,0,0 });
        	this.AnnoSel.Minimum = new decimal(new int[] { 0,0,0,0 });
            this.AnnoSel.Name = "AnnoSel";
            this.AnnoSel.Size = new Size(0x34, 20);
            this.AnnoSel.TabIndex = 13;
        //    this.AnnoSel.Value = new decimal(new int[] { 0x7d0 });
            object[] objArray = new object[] { "C:", "D:", "E:", "F:", "G:", "H:" };
            this.DriveL.Items.AddRange(objArray);
            this.DriveL.Location = new Point(680, 0x1c);
            this.DriveL.Name = "DriveL";
            this.DriveL.Size = new Size(40, 0x15);
            this.DriveL.TabIndex = 15;
            this.DriveL.SelectedIndexChanged += new EventHandler(this.DriveLSelectedIndexChanged);
            ColumnHeader[] values = new ColumnHeader[] { this.Data, this.Cliente, this.Controparte, this.Import, this.CAS };
            this.listView1.Columns.AddRange(values);
            this.listView1.Dock = DockStyle.Left;
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            this.listView1.HideSelection = false;
            this.listView1.LabelWrap = false;
            this.listView1.Location = new Point(3, 0x10);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new Size(0x201, 0x25d);
            this.listView1.Sorting = SortOrder.Descending;
            this.listView1.TabIndex = 7;
            this.listView1.View = View.Details;
            this.listView1.SelectedIndexChanged += new EventHandler(this.ListView1SelectedIndexChanged);
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
            this.groupBox1.Location = new Point(4, 0x18);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(280, 0xd0);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Dettagli:";
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
            this.groupBox3.Location = new Point(4, 0xe8);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new Size(280, 0x94);
            this.groupBox3.TabIndex = 9;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Comandi:";
            this.GiornoF.Location = new Point(0x74, 0x70);
          //  this.GiornoF.Maximum = new decimal(new int[] { 0x1f });
          //  this.GiornoF.Minimum = new decimal(new int[] { 1 });
            this.GiornoF.Name = "GiornoF";
            this.GiornoF.Size = new Size(0x20, 20);
            this.GiornoF.TabIndex = 14;
         //   this.GiornoF.Value = new decimal(new int[] { 1 });
            this.AutoScaleBaseSize = new Size(5, 13);
            base.ClientSize = new Size(0x32c, 0x2b1);
            base.Controls.Add(this.label20);
            base.Controls.Add(this.labelCli);
            base.Controls.Add(this.label18);
            base.Controls.Add(this.tutti);
            base.Controls.Add(this.DriveL);
            base.Controls.Add(this.AnnoSel);
            base.Controls.Add(this.button6);
            base.Controls.Add(this.groupBox4);
            base.Controls.Add(this.groupBox3);
            base.Controls.Add(this.groupBox1);
            base.Controls.Add(this.groupBox2);
            base.Menu = this.mainMenu1;
            base.Name = "MainForm";
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Rubrica contabile";
            base.Closing += new CancelEventHandler(this.MainFormClosing);
            base.Load += new EventHandler(this.MainFormLoad);
            this.Giorno.EndInit();
            this.MeseF.EndInit();
            this.Mese.EndInit();
            this.AnnoInc.EndInit();
            this.MeseInc.EndInit();
            this.Importo.EndInit();
            this.numericUpDown5.EndInit();
            this.numericUpDown6.EndInit();
            this.AnnoF.EndInit();
            this.numericUpDown4.EndInit();
            this.Anno.EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.AnnoSel.EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.GiornoF.EndInit();
            base.ResumeLayout(false);
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
            string[] strArray = new string[3];
            char[] separator = new char[] { '\\' };
            foreach (ListViewItem item in this.listView1.SelectedItems)
            {
                strArray = item.SubItems[0].Text.Split(separator);
                this.Anno.Text = strArray[0];
                this.Mese.Text = strArray[1];
                this.Giorno.Text = strArray[2];
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
                string[] strArray = new string[] { "null" };
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
                    strArray = this.listView1.Items[num].SubItems[0].Text.Split(separator);
                    string path = this.drive + @"\archivi\dati" + strArray[0] + ".mf";
                    FileStream stream = new FileStream(path, FileMode.Create, FileAccess.Write);
                    try
                    {
                        StreamWriter writer = new StreamWriter(stream);
                        strArray = this.listView1.Items[num].SubItems[0].Text.Split(separator);
                        while (true)
                        {
                            if (strArray[0] == this.listView1.Items[num].SubItems[0].Text.Substring(0, 4))
                            {
                                writer.Write("###" + this.listView1.Items[num].SubItems[0].Text);
                                writer.Write("###" + this.listView1.Items[num].SubItems[1].Text);
                                writer.Write("###" + this.listView1.Items[num].SubItems[2].Text);
                                writer.Write("###" + this.listView1.Items[num].SubItems[3].Text);
                                string str2 = "###" + this.listView1.Items[num].SubItems[4].Text;
                                writer.WriteLine(str2);
                                if (num < (this.listView1.Items.Count - 1))
                                {
                                    strArray = this.listView1.Items[num].SubItems[0].Text.Split(separator);
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
    }
}

