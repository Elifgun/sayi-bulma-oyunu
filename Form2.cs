using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Elif_Gündoğdu_SayiTahminOyunu
{
   
        public partial class Form2 : Form
        {
            // Veritabanı bağlantısı
            SqlConnection baglanti = new SqlConnection(
            @"Server=(localdb)\mssqllocaldb;Database=OyunDB;Trusted_Connection=True;");

            public Form2()
            {
                InitializeComponent();
            }

            // Form açıldığında otomatik çalışır
            private void Form2_Load(object sender, EventArgs e)
            {
                EnIyiSkorlar(); //Form açılır açılmaz en iyi skorlar yüklenir
            }

            // En iyi 5 oyuncuyu getir
            void EnIyiSkorlar()
            {
                try
                {
                    baglanti.Open(); //Veri tabanı bağlantısını aç 

                    SqlDataAdapter da = new SqlDataAdapter(
                        "SELECT TOP 5 OyuncuAdi, HamleSayisi, Sure, Tarih " +
                        "FROM OyunLoglar " +
                        "ORDER BY HamleSayisi ASC, Sure ASC",
                        baglanti); // En az hamle ve kısa süreye göre sıralama 

                    DataTable dt = new DataTable();
                    da.Fill(dt); // Verileri tabloya doldur 

                    dataGridView1.DataSource = dt; // DataGridView'e bağla

                    // Sütun başlıkları
                    dataGridView1.Columns[0].HeaderText = "Oyuncu";
                    dataGridView1.Columns[1].HeaderText = "Hamle";
                    dataGridView1.Columns[2].HeaderText = "Süre (sn)";
                    dataGridView1.Columns[3].HeaderText = "Tarih";

                    // Eğer hiç veri yoksa hata almamak için kontrol 
                    if (dataGridView1.Rows.Count == 0)
                        return;


                    // İlk 3 kişiyi renklendir
                    if (dataGridView1.Rows.Count > 0)
                        dataGridView1.Rows[0].DefaultCellStyle.BackColor = Color.Gold; // 1. (Altın)

                    if (dataGridView1.Rows.Count > 1)
                        dataGridView1.Rows[1].DefaultCellStyle.BackColor = Color.Silver; // 2. (Gümüş)

                    if (dataGridView1.Rows.Count > 2)
                        dataGridView1.Rows[2].DefaultCellStyle.BackColor = Color.Peru;  // 3. (Bronz)
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message);  // Hata durumunda kullanıcıya bilgi ver 
                }
                finally
                {
                    baglanti.Close();  // Her durumda bağlantıyı kapat 
                }
            }
        }
    
}
