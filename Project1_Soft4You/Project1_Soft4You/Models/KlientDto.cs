namespace Project1_Soft4You.Models
{
    public class KlientDto
    {
        public int KlientId { get; set; }   // 0 -> nowy rekord
        public string Nazwa { get; set; }
        public string Nip { get; set; }
        public string Adres { get; set; }
        public string NrTel { get; set; }
        public string Email { get; set; }

        // do kontroli współbieżności
        public byte[] RowVer { get; set; }
        public override string ToString() => $"{Nazwa} ({Nip})";
    }
}
