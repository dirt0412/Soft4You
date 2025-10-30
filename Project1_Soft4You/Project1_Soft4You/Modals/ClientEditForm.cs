using Project1_Soft4You.Models;
using System.Text.RegularExpressions;

namespace Project1_Soft4You.Modals
{
    public partial class ClientEditForm : Form
    {
        public KlientDto Klient { get; private set; }   // wynik po zapisie
        private readonly bool _isEdit;

        public ClientEditForm() : this(null) { }

        public ClientEditForm(KlientDto existing)
        {
            InitializeComponent(); 

            if (existing == null)
            {
                _isEdit = false;
                this.Text = "Dodaj klienta";
            }
            else
            {
                _isEdit = true;
                this.Text = "Edytuj klienta";
                txtNazwa.Text = existing.Nazwa;
                txtNip.Text = existing.Nip;
                txtAdres.Text = existing.Adres;
                txtTel.Text = existing.NrTel;
                txtEmail.Text = existing.Email;
                Klient = existing; // zapamiętujemy istniejącego
            }

            btnCancel.Click += (s, e) => this.DialogResult = DialogResult.Cancel;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string nazwa = txtNazwa.Text.Trim();
            string nip = txtNip.Text.Trim();
            string adres = txtAdres.Text.Trim();
            string tel = txtTel.Text.Trim();
            string mail = txtEmail.Text.Trim();

            if (string.IsNullOrWhiteSpace(nazwa))
            {
                MessageBox.Show("Nazwa jest wymagana.", "Walidacja", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNazwa.Focus(); return;
            }

            if (!Regex.IsMatch(nip, @"^\d{10}$"))
            {
                MessageBox.Show("NIP musi mieć 10 cyfr.", "Walidacja", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNip.Focus(); return;
            }

            if (!Regex.IsMatch(mail, @"^.+@.+\..+$"))
            {
                MessageBox.Show("Podaj poprawny adres e-mail.", "Walidacja", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus(); return;
            }

            if (!Regex.IsMatch(tel, @"^[0-9 +\-]{7,20}$"))
            {
                MessageBox.Show("Podaj poprawny numer telefonu (7–20 znaków: cyfry, spacje, +, -).", "Walidacja",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTel.Focus(); return;
            }

            if (Klient == null) Klient = new KlientDto();
            Klient.Nazwa = nazwa;
            Klient.Nip = nip;
            Klient.Adres = adres;
            Klient.NrTel = tel;
            Klient.Email = mail;

            this.DialogResult = DialogResult.OK;
        }


        private void btnCancel_Click(object sender, EventArgs e)
        {

        }
    }
}
