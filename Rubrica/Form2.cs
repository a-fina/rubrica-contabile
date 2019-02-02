namespace Rubrica
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    public class Form2 : Form
    {
        private ColumnHeader Import;
        private ColumnHeader Controparte;
        private ColumnHeader Cliente;
        private ColumnHeader CAS;
        private ColumnHeader Data;
        public ListView listView1;

        public Form2()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.listView1 = new ListView();
            this.Data = new ColumnHeader();
            this.CAS = new ColumnHeader();
            this.Cliente = new ColumnHeader();
            this.Controparte = new ColumnHeader();
            this.Import = new ColumnHeader();
            base.SuspendLayout();
            ColumnHeader[] values = new ColumnHeader[] { this.Data, this.Cliente, this.Controparte, this.Import, this.CAS };
            this.listView1.Columns.AddRange(values);
            this.listView1.Dock = DockStyle.Left;
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            this.listView1.HideSelection = false;
            this.listView1.LabelWrap = false;
            this.listView1.Location = new Point(0, 0);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new Size(0x1d0, 0x185);
            this.listView1.Sorting = SortOrder.Descending;
            this.listView1.TabIndex = 8;
            this.listView1.View = View.Details;
            this.Data.Text = @"Anno\Mese\Giorno";
            this.Data.TextAlign = HorizontalAlignment.Center;
            this.Data.Width = 0x5e;
            this.CAS.Text = "C/A/S";
            this.CAS.TextAlign = HorizontalAlignment.Center;
            this.CAS.Width = 0x35;
            this.Cliente.Text = "Cliente";
            this.Cliente.Width = 120;
            this.Controparte.Text = "Controparte";
            this.Controparte.Width = 120;
            this.Import.Text = "Importo";
            this.Import.TextAlign = HorizontalAlignment.Right;
            this.Import.Width = 0x3f;
            this.AutoScaleBaseSize = new Size(5, 14);
            base.ClientSize = new Size(0x1c8, 0x185);
            base.Controls.Add(this.listView1);
            base.Name = "Form2";
            this.Text = "Ricerca";
            base.ResumeLayout(false);
        }
    }
}

