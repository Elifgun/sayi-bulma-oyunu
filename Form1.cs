using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Elif_Gündoğdu_SayiTahminOyunu
{
    public partial class Form1 : Form
    {
        string hedef = "";     // Bilgisayarın tuttuğu gizli sayı
        int hamleSayisi = 0;   // Kullanıcının yaptığı tahmin sayısı
        DateTime baslangic;    // Oyunun başladığı zamanı tutar

        // Veritabanı bağlantısı
        SqlConnection baglanti = new SqlConnection(
        @"Server=(localdb)\mssqllocaldb;Database=OyunDB;Trusted_Connection=True;");
        public Form1()
        {
            InitializeComponent();





        }

        // Form yüklendiğinde çalışır
        private void Form1_Load(object sender, EventArgs e)
        {
            baslangic = DateTime.Now;  // Oyun başladığı anı kaydeder

            Random rnd = new Random();

            // 4 basamaklı, rakamları farklı bir sayı üret
            while (hedef.Length < 4)
            {
                int sayi = rnd.Next(0, 10);

                // Aynı rakam tekrar etmesin diye kontrol
                if (!hedef.Contains(sayi.ToString()))
                {
                    hedef += sayi.ToString();
                }
                labelHedef.Text = "Hedef Sayı: " + hedef;

                listBox1.Items.Clear();
            }

            

        }
        // Tahmin butonuna basıldığında çalışır
        private void button1_Click(object sender, EventArgs e)
        {
            string tahmin = textBox1.Text; // Kullanıcının girdiği tahmin

            // Girilen değerin 4 basamaklı ve sadece rakamlardan oluştuğunu kontrol et

            if (tahmin.Length != 4 || !tahmin.All(char.IsDigit))
            {
                label1.Text = "4 basamaklı sayı gir!";
                return;
            }

            hamleSayisi++; // Her tahminde hamle sayısını artır


            int arti = 0; // Doğru rakam + doğru yer
            int eksi = 0; // Doğru rakam + yanlış yer

            // Tahmini hedef sayı ile karşılaştır
            for (int i = 0; i < 4; i++)
            {
                if (tahmin[i] == hedef[i])
                    arti++;
                else if (hedef.Contains(tahmin[i]))
                    eksi++;
            }

            // Sonucu listeye yaz
            listBox1.Items.Add(tahmin + " → +" + arti + " -" + eksi);

            // Eğer tüm rakamlar doğruysa oyun kazanıldı
            if (arti == 4)
            {
                MessageBox.Show("Kazandın!");
                TimeSpan sure = DateTime.Now - baslangic;  // Oyunun ne kadar sürdüğünü hesaplar

                baglanti.Open(); // Veritabanı bağlantısını aç

                // Oyuncu bilgilerini veritabanına kaydet
                SqlCommand komut = new SqlCommand(
                "INSERT INTO OyunLoglar (OyuncuAdi, HedefSayi, HamleSayisi, Tarih, Sure) VALUES (@ad,@sayi,@hamle,@tarih,@sure)",
                baglanti);

                // Parametreleri ekle (SQL injection önler)
                komut.Parameters.AddWithValue("@ad", textBoxAd.Text);
                komut.Parameters.AddWithValue("@sayi", hedef);
                komut.Parameters.AddWithValue("@hamle", hamleSayisi);
                komut.Parameters.AddWithValue("@tarih", DateTime.Now);
                komut.Parameters.AddWithValue("@sure", (int)sure.TotalSeconds);

                komut.ExecuteNonQuery(); // Komutu çalıştır
                baglanti.Close();       // Bağlantıyı kapat
            }
        }
        // Skorları göster butonu
        private void btnSkor_Click(object sender, EventArgs e)
        {
            baglanti.Open(); // Veritabanı bağlantısını aç

            // En az hamle yapan üstte olacak şekilde verileri çek
            SqlDataAdapter da = new SqlDataAdapter(
                "SELECT OyuncuAdi, HamleSayisi, Tarih FROM OyunLoglar ORDER BY HamleSayisi ASC",
                baglanti);

            DataTable dt = new DataTable();
            da.Fill(dt); // Verileri tabloya doldur

            dataGridView1.DataSource = dt;  // Grid'e bağla

            baglanti.Close(); // Bağlantıyı kapat
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBoxAd_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            hedef = "";

            hamleSayisi = 0;

            baslangic = DateTime.Now;

            Random rnd = new Random();

            while (hedef.Length < 4)

            {

                int sayi = rnd.Next(0, 10);

                if (!hedef.Contains(sayi.ToString()))

                {

                    hedef += sayi.ToString();

                }

            }

            labelHedef.Text = "Hedef Sayı: " + hedef;

            listBox1.Items.Clear();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form2 frm = new Form2();
            frm.ShowDialog();            // Form2'yi açar
        }

        private void lblortalama_Click(object sender, EventArgs e)
        {

        }
    }

}
