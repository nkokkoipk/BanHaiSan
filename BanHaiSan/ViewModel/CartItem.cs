namespace BanHaiSan.ViewModel
{
    public class CartItem
    {
        public int MaHh {  get; set; }  
        public string TenHH {  get; set; }
        public string Hinh { get; set; }
        public double DonGia { get; set; }
        public int SoLuong {  get; set; }
        public double ThanhTien => DonGia * SoLuong;
    }
}
