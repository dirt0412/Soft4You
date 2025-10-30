
using System.Windows.Forms;

namespace Project1_Soft4You.Utility
{
    public static class DataGridViewFormatter
    {
        public static void FormatKlienciGrid(DataGridView dgv)
        {
            if (dgv == null || dgv.Columns.Count == 0)
                return;

            dgv.ReadOnly = true;
            dgv.MultiSelect = false;
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.RowHeadersVisible = false;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.EnableHeadersVisualStyles = false;

            dgv.BackgroundColor = Color.White;
            dgv.BorderStyle = BorderStyle.None;

            dgv.RowsDefaultCellStyle.BackColor = Color.White;
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245);

            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.LightSteelBlue;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);

            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 9F);
            dgv.DefaultCellStyle.ForeColor = Color.Black;
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(180, 200, 235);
            dgv.DefaultCellStyle.SelectionForeColor = Color.Black;


            if (dgv.Columns.Contains("klient_id"))
            {
                dgv.Columns["klient_id"].Visible = false;
                dgv.Columns["klient_id"].SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            if (dgv.Columns.Contains("nazwa"))
            {
                dgv.Columns["nazwa"].HeaderText = "Nazwa firmy";
                dgv.Columns["nazwa"].Width = 450;
                dgv.Columns["nazwa"].SortMode = DataGridViewColumnSortMode.Programmatic; //only sort for 'nazwa''
            }

            if (dgv.Columns.Contains("nip"))
            {
                dgv.Columns["nip"].HeaderText = "NIP";
                dgv.Columns["nip"].Width = 100;
                dgv.Columns["nip"].SortMode = DataGridViewColumnSortMode.NotSortable;// not sortable
            }

            if (dgv.Columns.Contains("adres"))
            {
                dgv.Columns["adres"].HeaderText = "Adres";
                dgv.Columns["adres"].Width = 450;
                dgv.Columns["adres"].SortMode = DataGridViewColumnSortMode.NotSortable;// not sortable
            }

            if (dgv.Columns.Contains("nr_tel"))
            {
                dgv.Columns["nr_tel"].HeaderText = "Nr telefonu";
                dgv.Columns["nr_tel"].Width = 130;
                dgv.Columns["nr_tel"].SortMode = DataGridViewColumnSortMode.NotSortable;// not sortable
            }

            if (dgv.Columns.Contains("email"))
            {
                dgv.Columns["email"].HeaderText = "E-mail";
                dgv.Columns["email"].Width = 400;
                dgv.Columns["email"].SortMode = DataGridViewColumnSortMode.NotSortable;// not sortable
            }

            if (dgv.Columns.Contains("created_at"))
            {
                dgv.Columns["created_at"].HeaderText = "Utworzono";
                dgv.Columns["created_at"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm";
                dgv.Columns["created_at"].Width = 140;
                dgv.Columns["created_at"].SortMode = DataGridViewColumnSortMode.NotSortable;// not sortable
            }

            if (dgv.Columns.Contains("updated_at"))
            {
                dgv.Columns["updated_at"].HeaderText = "Zaktualizowano";
                dgv.Columns["updated_at"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm";
                dgv.Columns["updated_at"].Width = 140;
                dgv.Columns["updated_at"].SortMode = DataGridViewColumnSortMode.NotSortable;// not sortable
            }
        }


    }
}
