
namespace Project1_Soft4You
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            dataGridView1 = new DataGridView();
            btnPrev = new Button();
            btnNext = new Button();
            lblPage = new Label();
            txtFilterNazwa = new TextBox();
            txtFilterNip = new TextBox();
            txtFilterTel = new TextBox();
            txtFilterEmail = new TextBox();
            btnSearch = new Button();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            groupBox1 = new GroupBox();
            btnDelete = new Button();
            btnAdd = new Button();
            groupBox2 = new GroupBox();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(12, 109);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(1136, 345);
            dataGridView1.TabIndex = 0;
            dataGridView1.CellDoubleClick += dataGridView1_CellDoubleClick;
            dataGridView1.ColumnHeaderMouseClick += dataGridView1_ColumnHeaderMouseClick;
            dataGridView1.SelectionChanged += dataGridView1_SelectionChanged;
            // 
            // btnPrev
            // 
            btnPrev.Location = new Point(7, 26);
            btnPrev.Name = "btnPrev";
            btnPrev.Size = new Size(40, 40);
            btnPrev.TabIndex = 1;
            btnPrev.Text = "<<";
            btnPrev.UseVisualStyleBackColor = true;
            btnPrev.Click += btnPrev_Click;
            // 
            // btnNext
            // 
            btnNext.Location = new Point(293, 23);
            btnNext.Name = "btnNext";
            btnNext.Size = new Size(40, 40);
            btnNext.TabIndex = 2;
            btnNext.Text = ">>";
            btnNext.UseVisualStyleBackColor = true;
            btnNext.Click += btnNext_Click;
            // 
            // lblPage
            // 
            lblPage.AutoSize = true;
            lblPage.Location = new Point(53, 37);
            lblPage.Name = "lblPage";
            lblPage.Size = new Size(86, 20);
            lblPage.TabIndex = 3;
            lblPage.Text = "Strona 1 / 1";
            // 
            // txtFilterNazwa
            // 
            txtFilterNazwa.Location = new Point(6, 48);
            txtFilterNazwa.Name = "txtFilterNazwa";
            txtFilterNazwa.Size = new Size(216, 27);
            txtFilterNazwa.TabIndex = 4;
            // 
            // txtFilterNip
            // 
            txtFilterNip.Location = new Point(235, 48);
            txtFilterNip.Name = "txtFilterNip";
            txtFilterNip.Size = new Size(216, 27);
            txtFilterNip.TabIndex = 5;
            // 
            // txtFilterTel
            // 
            txtFilterTel.Location = new Point(465, 48);
            txtFilterTel.Name = "txtFilterTel";
            txtFilterTel.Size = new Size(216, 27);
            txtFilterTel.TabIndex = 6;
            // 
            // txtFilterEmail
            // 
            txtFilterEmail.Location = new Point(695, 48);
            txtFilterEmail.Name = "txtFilterEmail";
            txtFilterEmail.Size = new Size(216, 27);
            txtFilterEmail.TabIndex = 7;
            // 
            // btnSearch
            // 
            btnSearch.Location = new Point(926, 46);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(94, 29);
            btnSearch.TabIndex = 8;
            btnSearch.Text = "Szukaj";
            btnSearch.UseVisualStyleBackColor = true;
            btnSearch.Click += btnSearch_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(10, 27);
            label1.Name = "label1";
            label1.Size = new Size(54, 20);
            label1.TabIndex = 9;
            label1.Text = "Nazwa";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(235, 27);
            label2.Name = "label2";
            label2.Size = new Size(32, 20);
            label2.TabIndex = 10;
            label2.Text = "NIP";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(465, 27);
            label3.Name = "label3";
            label3.Size = new Size(84, 20);
            label3.TabIndex = 11;
            label3.Text = "Nr telefonu";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(695, 25);
            label4.Name = "label4";
            label4.Size = new Size(52, 20);
            label4.TabIndex = 12;
            label4.Text = "E-mail";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(txtFilterNazwa);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(btnSearch);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(txtFilterNip);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(txtFilterTel);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(txtFilterEmail);
            groupBox1.Location = new Point(12, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(1029, 91);
            groupBox1.TabIndex = 13;
            groupBox1.TabStop = false;
            groupBox1.Text = "Filtrowanie";
            // 
            // btnDelete
            // 
            btnDelete.BackgroundImage = Properties.Resources.ic_delete_black_24dp;
            btnDelete.BackgroundImageLayout = ImageLayout.Stretch;
            btnDelete.Font = new Font("Segoe UI", 7.8F, FontStyle.Regular, GraphicsUnit.Point, 238);
            btnDelete.Location = new Point(63, 461);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(40, 40);
            btnDelete.TabIndex = 14;
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            // 
            // btnAdd
            // 
            btnAdd.BackgroundImage = Properties.Resources.ic_note_add_black_24dp;
            btnAdd.BackgroundImageLayout = ImageLayout.Stretch;
            btnAdd.Font = new Font("Segoe UI", 7.8F, FontStyle.Regular, GraphicsUnit.Point, 238);
            btnAdd.Location = new Point(12, 460);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(40, 40);
            btnAdd.TabIndex = 15;
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;
            // 
            // groupBox2
            // 
            groupBox2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            groupBox2.Controls.Add(btnNext);
            groupBox2.Controls.Add(btnPrev);
            groupBox2.Controls.Add(lblPage);
            groupBox2.Location = new Point(802, 463);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(343, 75);
            groupBox2.TabIndex = 17;
            groupBox2.TabStop = false;
            groupBox2.Text = "stronicowanie";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1155, 550);
            Controls.Add(groupBox2);
            Controls.Add(btnAdd);
            Controls.Add(btnDelete);
            Controls.Add(groupBox1);
            Controls.Add(dataGridView1);
            Name = "Form1";
            Text = "Aplikacja testowa – Katalog klientów";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dataGridView1;
        private Button btnPrev;
        private Button btnNext;
        private Label lblPage;
        private TextBox txtFilterNazwa;
        private TextBox txtFilterNip;
        private TextBox txtFilterTel;
        private TextBox txtFilterEmail;
        private Button btnSearch;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private GroupBox groupBox1;
        private Button btnDelete;
        private Button btnAdd;
        private GroupBox groupBox2;
    }
}
