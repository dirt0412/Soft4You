using Project1_Soft4You.Data;
using Project1_Soft4You.Modals;
using Project1_Soft4You.Utility;

namespace Project1_Soft4You
{
    public partial class Form1 : Form
    {
        private readonly KlienciRepository klienciRepository = new KlienciRepository();
        private int _currentPage = 1;
        private const int PageSize = 10;
        private int _totalRecords = 0;
        private int _totalPages = 1;
        private string _orderBy = "nazwa";
        private bool _orderDesc = false; // pierwsze kliknięcie da DESC

        public Form1()
        {
            InitializeComponent();

            btnDelete.Visible = false;
            dataGridView1.SelectionChanged += dataGridView1_SelectionChanged;

            // Ustawienia tabeli
            dataGridView1.AutoGenerateColumns = true;
            dataGridView1.ReadOnly = true;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245);

            LoadMetaAndFirstPage();

            // Obsługa ENTER w polach filtrów
            txtFilterNazwa.KeyDown += FilterBox_KeyDown;
            txtFilterNip.KeyDown += FilterBox_KeyDown;
            txtFilterTel.KeyDown += FilterBox_KeyDown;
            txtFilterEmail.KeyDown += FilterBox_KeyDown;
        }

        private void LoadMetaAndFirstPage()
        {
            _totalRecords = klienciRepository.GetCount(txtFilterNazwa.Text, txtFilterNip.Text, txtFilterTel.Text, txtFilterEmail.Text);
            _totalPages = Math.Max(1, (_totalRecords + PageSize - 1) / PageSize);
            _currentPage = 1;
            LoadPage();
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (_currentPage > 1)
            {
                _currentPage--;
                LoadPage();
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (_currentPage < _totalPages)
            {
                _currentPage++;
                LoadPage();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadMetaAndFirstPage();
        }

        private void FilterBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                LoadMetaAndFirstPage();
                e.SuppressKeyPress = true;
            }
        }

        private void dataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            var col = dataGridView1.Columns[e.ColumnIndex];
            var key = (col.DataPropertyName ?? col.Name ?? string.Empty).ToLowerInvariant();
            if (key != "nazwa") return;

            _orderDesc = string.Equals(_orderBy, "nazwa", StringComparison.OrdinalIgnoreCase) ? !_orderDesc : false;
            _orderBy = "nazwa";
            _currentPage = 1;
            LoadPage();
        }
private void UpdateDeleteButtonState()
        {
            bool hasSelection = dataGridView1.CurrentRow != null && !dataGridView1.CurrentRow.IsNewRow;
            btnDelete.Visible = hasSelection;
            // btnDelete.Enabled = hasSelection;
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            UpdateDeleteButtonState();
        }

        #region CRUD Operations
        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (var dlg = new ClientEditForm())
            {
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    try
                    {
                        int newId = klienciRepository.Insert(dlg.Klient);
                        LoadMetaAndFirstPage();
                        MessageBox.Show("Dodano nowego klienta.", "Sukces",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Microsoft.Data.SqlClient.SqlException ex) when (ex.Number == 2627 || ex.Number == 2601)
                    {
                        // 2627/2601 = naruszenie unikalności (UNIQUE na NIP lub Email)
                        MessageBox.Show("Rekord z takim NIP lub e-mailem już istnieje.", "Duplikat",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        Utility.ErrorLogger.Log("Form1.btnAdd: Duplikat przy dodawaniu klienta", ex);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Błąd podczas dodawania: {ex.Message}", "Błąd",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Utility.ErrorLogger.Log("Form1.btnAdd: Błąd podczas dodawania klienta", ex);
                    }
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Zaznacz wiersz do usunięcia.", "Informacja",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var cell = dataGridView1.CurrentRow.Cells["klient_id"];
            if (cell == null || cell.Value == null || cell.Value == DBNull.Value)
            {
                MessageBox.Show("Nie udało się odczytać klient_id z zaznaczonego wiersza.",
                    "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int klientId = Convert.ToInt32(cell.Value);
            string nazwa = dataGridView1.CurrentRow.Cells["nazwa"]?.Value?.ToString() ?? "(brak nazwy)";

            var confirm = MessageBox.Show(
                $"Na pewno usunąć klienta?\n\nID: {klientId}\nNazwa: {nazwa}",
                "Potwierdzenie usunięcia",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes) return;

            try
            {
                bool ok = klienciRepository.DeleteById(klientId);
                if (ok)
                {
                    // aktualizuj licznik i strony
                    _totalRecords = Math.Max(0, _totalRecords - 1);
                    _totalPages = Math.Max(1, (_totalRecords + PageSize - 1) / PageSize);

                    // jeśli po usunięciu strona jest pusta i nie jesteśmy na pierwszej — cofnij stronę
                    if (dataGridView1.Rows.Count <= 1 && _currentPage > 1)
                        _currentPage--;

                    LoadPage();
                }
                else
                {
                    MessageBox.Show("Rekord nie został znaleziony (mógł zostać już usunięty).",
                        "Informacja", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            //catch (SqlException ex) when (ex.Number == 547) // naruszenie FK
            //{
            //    MessageBox.Show("Nie można usunąć rekordu, ponieważ jest powiązany z innymi danymi.",
            //        "Operacja zablokowana", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //}
            catch (Exception ex)
            {
                MessageBox.Show($"Błąd podczas usuwania: {ex.Message}",
                    "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Utility.ErrorLogger.Log("Form1.btnDelete: Błąd podczas usuwania klienta", ex);
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var idCell = dataGridView1.Rows[e.RowIndex].Cells["klient_id"];
            if (idCell?.Value == null || idCell.Value == DBNull.Value) return;

            int klientId = Convert.ToInt32(idCell.Value);

            // Pobierz aktualny stan + ROWVERSION
            var dto = klienciRepository.GetById(klientId);
            if (dto == null)
            {
                MessageBox.Show("Nie znaleziono rekordu do edycji.", "Informacja",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (var dlg = new ClientEditForm(dto))
            {
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    try
                    {
                        // zachowaj id i rowver otrzymany przy odczycie/edycji
                        dlg.Klient.KlientId = klientId;
                        dlg.Klient.RowVer = dto.RowVer;

                        bool ok = klienciRepository.Update(dlg.Klient);
                        if (ok)
                        {
                            LoadPage();
                            MessageBox.Show("Zapisano zmiany.", "Sukces",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            // konflikt współbieżności
                            MessageBox.Show(
                                "Ten rekord został zmieniony przez innego użytkownika.\n" +
                                "Odświeżono dane – spróbuj ponownie edytować.",
                                "Konflikt współbieżności",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            Utility.ErrorLogger.Log("Form1.dataGridView1_CellDoubleClick: Konflikt rowversion przy edycji klienta");

                            LoadPage();
                        }
                    }
                    catch (Microsoft.Data.SqlClient.SqlException ex) when (ex.Number == 2627 || ex.Number == 2601)
                    {
                        MessageBox.Show("Rekord z takim NIP lub e-mailem już istnieje.", "Duplikat",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        Utility.ErrorLogger.Log("Form1.dataGridView1_CellDoubleClick: Błąd podczas edycji klienta - Rekord z takim NIP lub e-mailem już istnieje", ex);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Błąd podczas zapisu: {ex.Message}", "Błąd",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Utility.ErrorLogger.Log("Form1.dataGridView1_CellDoubleClick: Błąd podczas edycji klienta", ex);
                    }
                }
            }
        }

        private void LoadPage()
        {
            DataTable dt = klienciRepository.GetPage(
                _currentPage, PageSize,
                txtFilterNazwa.Text, txtFilterNip.Text, txtFilterTel.Text, txtFilterEmail.Text,
                _orderBy, _orderDesc);

            dataGridView1.DataSource = dt;

            DataGridViewFormatter.FormatKlienciGrid(dataGridView1);

            foreach (DataGridViewColumn c in dataGridView1.Columns)
                c.SortMode = DataGridViewColumnSortMode.NotSortable;

            // znajdź kolumnę "nazwa" 
            DataGridViewColumn? colNazwa =
                dataGridView1.Columns.Cast<DataGridViewColumn>()
                    .FirstOrDefault(c =>
                        string.Equals(c.DataPropertyName, "nazwa", StringComparison.OrdinalIgnoreCase) ||
                        string.Equals(c.Name, "nazwa", StringComparison.OrdinalIgnoreCase));

            if (colNazwa != null)
                colNazwa.SortMode = DataGridViewColumnSortMode.Programmatic;

            foreach (DataGridViewColumn c in dataGridView1.Columns)
                c.HeaderCell.SortGlyphDirection = System.Windows.Forms.SortOrder.None;

            DataGridViewColumn? sortCol =
                dataGridView1.Columns.Cast<DataGridViewColumn>()
                    .FirstOrDefault(c =>
                        string.Equals(c.DataPropertyName, _orderBy, StringComparison.OrdinalIgnoreCase) ||
                        string.Equals(c.Name, _orderBy, StringComparison.OrdinalIgnoreCase));

            if (sortCol != null && sortCol.SortMode == DataGridViewColumnSortMode.Programmatic)
                sortCol.HeaderCell.SortGlyphDirection =
                    _orderDesc ? System.Windows.Forms.SortOrder.Descending : System.Windows.Forms.SortOrder.Ascending;

            lblPage.Text = $"Strona {_currentPage} / {_totalPages}  (rekordów: {_totalRecords})";
            btnPrev.Enabled = _currentPage > 1;
            btnNext.Enabled = _currentPage < _totalPages;

            UpdateDeleteButtonState();
        }


        #endregion

    }
}
