namespace Project1_Soft4You.Modals
{
    partial class ClientEditForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            txtNazwa = new TextBox();
            lblNazwa = new Label();
            lblNip = new Label();
            txtNip = new TextBox();
            lblAdres = new Label();
            txtAdres = new TextBox();
            lblTelefon = new Label();
            txtTel = new TextBox();
            lblEmail = new Label();
            txtEmail = new TextBox();
            btnCancel = new Button();
            btnSave = new Button();
            SuspendLayout();
            // 
            // txtNazwa
            // 
            txtNazwa.Location = new Point(79, 19);
            txtNazwa.Name = "txtNazwa";
            txtNazwa.Size = new Size(349, 27);
            txtNazwa.TabIndex = 0;
            // 
            // lblNazwa
            // 
            lblNazwa.AutoSize = true;
            lblNazwa.Location = new Point(8, 22);
            lblNazwa.Name = "lblNazwa";
            lblNazwa.Size = new Size(54, 20);
            lblNazwa.TabIndex = 1;
            lblNazwa.Text = "Nazwa";
            // 
            // lblNip
            // 
            lblNip.AutoSize = true;
            lblNip.Location = new Point(8, 60);
            lblNip.Name = "lblNip";
            lblNip.Size = new Size(32, 20);
            lblNip.TabIndex = 3;
            lblNip.Text = "NIP";
            // 
            // txtNip
            // 
            txtNip.Location = new Point(78, 60);
            txtNip.Name = "txtNip";
            txtNip.Size = new Size(350, 27);
            txtNip.TabIndex = 2;
            // 
            // lblAdres
            // 
            lblAdres.AutoSize = true;
            lblAdres.Location = new Point(8, 100);
            lblAdres.Name = "lblAdres";
            lblAdres.Size = new Size(47, 20);
            lblAdres.TabIndex = 5;
            lblAdres.Text = "Adres";
            // 
            // txtAdres
            // 
            txtAdres.Location = new Point(78, 100);
            txtAdres.Name = "txtAdres";
            txtAdres.Size = new Size(350, 27);
            txtAdres.TabIndex = 4;
            // 
            // lblTelefon
            // 
            lblTelefon.AutoSize = true;
            lblTelefon.Location = new Point(7, 138);
            lblTelefon.Name = "lblTelefon";
            lblTelefon.Size = new Size(58, 20);
            lblTelefon.TabIndex = 7;
            lblTelefon.Text = "Telefon";
            // 
            // txtTel
            // 
            txtTel.Location = new Point(78, 138);
            txtTel.Name = "txtTel";
            txtTel.Size = new Size(350, 27);
            txtTel.TabIndex = 6;
            // 
            // lblEmail
            // 
            lblEmail.AutoSize = true;
            lblEmail.Location = new Point(8, 175);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(52, 20);
            lblEmail.TabIndex = 9;
            lblEmail.Text = "E-mail";
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(78, 175);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(350, 27);
            txtEmail.TabIndex = 8;
            // 
            // btnCancel
            // 
            btnCancel.BackgroundImageLayout = ImageLayout.Stretch;
            btnCancel.Font = new Font("Segoe UI", 7.8F, FontStyle.Regular, GraphicsUnit.Point, 238);
            btnCancel.Location = new Point(358, 222);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(70, 40);
            btnCancel.TabIndex = 18;
            btnCancel.Text = "Rezygnuj";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // btnSave
            // 
            btnSave.BackgroundImageLayout = ImageLayout.Stretch;
            btnSave.Font = new Font("Segoe UI", 7.8F, FontStyle.Regular, GraphicsUnit.Point, 238);
            btnSave.Location = new Point(279, 222);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(70, 40);
            btnSave.TabIndex = 17;
            btnSave.Text = "Zapisz";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // ClientEditForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(447, 281);
            Controls.Add(btnCancel);
            Controls.Add(btnSave);
            Controls.Add(lblEmail);
            Controls.Add(txtEmail);
            Controls.Add(lblTelefon);
            Controls.Add(txtTel);
            Controls.Add(lblAdres);
            Controls.Add(txtAdres);
            Controls.Add(lblNip);
            Controls.Add(txtNip);
            Controls.Add(lblNazwa);
            Controls.Add(txtNazwa);
            Name = "ClientEditForm";
            Text = "ClientEditForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtNazwa;
        private Label lblNazwa;
        private Label lblNip;
        private TextBox txtNip;
        private Label lblAdres;
        private TextBox txtAdres;
        private Label lblTelefon;
        private TextBox txtTel;
        private Label lblEmail;
        private TextBox txtEmail;
        private Button btnCancel;
        private Button btnSave;
    }
}