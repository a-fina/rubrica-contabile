namespace Rubrica
{
    using System;
    using System.Drawing;
    using System.IO;
    using System.Security.Cryptography;
    using System.Text;
    using System.Windows.Forms;

    public class Form1 : Form
    {
        private Label label2;
        private Label label1;
        private Button button1;
        private TextBox textBox1;
        private TextBox textBox2;
        public string funzione;
        public bool filtra;
        public string drive;
        public string wrkdir;

        public Form1()
        {
            this.InitializeComponent();
        }

        private void Button1Click(object sender, EventArgs e)
        {
            if (this.funzione == "carica")
            {
                string str;
                using (StreamReader reader = new StreamReader(this.drive + @"\atad.fm"))
                {
                    str = reader.ReadLine();
                    reader.Close();
                }
                MainForm.campo = (str != BitConverter.ToString(new MD5CryptoServiceProvider().ComputeHash(Encoding.Default.GetBytes(this.textBox1.Text)))) ? "errata" : "carica";
            }
            else
            {
                using (StreamWriter writer = new StreamWriter(this.drive + @"\atad.fm"))
                {
                    writer.Write(BitConverter.ToString(new MD5CryptoServiceProvider().ComputeHash(Encoding.Default.GetBytes(this.textBox1.Text))));
                    writer.Close();
                    MainForm.campo = this.textBox1.Text;
                }
            }
            if (this.filtra && (this.textBox2.Text != ""))
            {
                MainForm.filtra_cliente = this.textBox2.Text;
            }
            base.Close();
        }

        private void Form1Load(object sender, EventArgs e)
        {
            if (!this.filtra)
            {
                this.textBox2.Visible = false;
            }
        }

        private void InitializeComponent()
        {
        	this.textBox2 = new System.Windows.Forms.TextBox();
        	this.textBox1 = new System.Windows.Forms.TextBox();
        	this.button1 = new System.Windows.Forms.Button();
        	this.label1 = new System.Windows.Forms.Label();
        	this.label2 = new System.Windows.Forms.Label();
        	this.SuspendLayout();
        	// 
        	// textBox2
        	// 
        	this.textBox2.Location = new System.Drawing.Point(19, 114);
        	this.textBox2.Name = "textBox2";
        	this.textBox2.Size = new System.Drawing.Size(221, 26);
        	this.textBox2.TabIndex = 4;
        	// 
        	// textBox1
        	// 
        	this.textBox1.AcceptsReturn = true;
        	this.textBox1.Location = new System.Drawing.Point(19, 44);
        	this.textBox1.Name = "textBox1";
        	this.textBox1.PasswordChar = '*';
        	this.textBox1.Size = new System.Drawing.Size(179, 26);
        	this.textBox1.TabIndex = 0;
        	// 
        	// button1
        	// 
        	this.button1.Location = new System.Drawing.Point(250, 18);
        	this.button1.Name = "button1";
        	this.button1.Size = new System.Drawing.Size(153, 58);
        	this.button1.TabIndex = 1;
        	this.button1.Text = "Conferma";
        	this.button1.Click += new System.EventHandler(this.Button1Click);
        	// 
        	// label1
        	// 
        	this.label1.Location = new System.Drawing.Point(19, 18);
        	this.label1.Name = "label1";
        	this.label1.Size = new System.Drawing.Size(179, 23);
        	this.label1.TabIndex = 2;
        	this.label1.Text = "Inserire password:";
        	// 
        	// label2
        	// 
        	this.label2.Location = new System.Drawing.Point(19, 92);
        	this.label2.Name = "label2";
        	this.label2.Size = new System.Drawing.Size(160, 22);
        	this.label2.TabIndex = 5;
        	this.label2.Text = "Seleziona cliente:";
        	// 
        	// Form1
        	// 
        	this.AutoScaleBaseSize = new System.Drawing.Size(8, 19);
        	this.ClientSize = new System.Drawing.Size(473, 169);
        	this.Controls.Add(this.label2);
        	this.Controls.Add(this.textBox2);
        	this.Controls.Add(this.label1);
        	this.Controls.Add(this.button1);
        	this.Controls.Add(this.textBox1);
        	this.Name = "Form1";
        	this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
        	this.Text = "Verifica password";
        	this.Load += new System.EventHandler(this.Form1Load);
        	this.ResumeLayout(false);
        	this.PerformLayout();

        }
    }
}

