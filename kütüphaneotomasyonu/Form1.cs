using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using System.Data;

namespace kütüphaneotomasyonu
{
    public partial class frmAnaSayfa : Form

    {
        SqlConnection baglanti = new SqlConnection(
    @"Server=localhost\SQLEXPRESS;
    Database=KutuphaneDB;
    Trusted_Connection=True;
    TrustServerCertificate=True;");

        
        List<Kitap> kitaplar = new List<Kitap>();
        public frmAnaSayfa()
        {
            InitializeComponent();
        }

        private void frmAnaSayfa_Load(object sender, EventArgs e)

        {
            txtKitapAd.Text = "";
            txtYazar.Text = "";
            txtSayfa.Text = "";

        }

        private void txtSayfa_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtKitapAd.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtYazar.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtSayfa.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            txtTur.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
        }
        
       
            private void btnEkle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand(
"INSERT INTO Kitaplar (KitapAdi, Yazar, SayfaSayisi, Tur) VALUES (@p1,@p2,@p3,@p4)", baglanti);

            komut.Parameters.AddWithValue("@p1", txtKitapAd.Text);
            komut.Parameters.AddWithValue("@p2", txtYazar.Text);
            komut.Parameters.AddWithValue("@p3", txtSayfa.Text);
            komut.Parameters.AddWithValue("@p4", txtTur.Text);

            baglanti.Open();

            komut.ExecuteNonQuery();

            baglanti.Close();

            MessageBox.Show("Kitap eklendi");
            if (txtKitapAd.Text == "" || txtYazar.Text == "" || txtSayfa.Text == "" || txtTur.Text == "")
            {
                MessageBox.Show("Boş bırakma!");
                return;
            }

            Kitap k = new Kitap();
            k.KitapAd = txtKitapAd.Text;
            k.Yazar = txtYazar.Text;
            int sayfaSayisi;

            if (!int.TryParse(txtSayfa.Text, out sayfaSayisi))
            {
                MessageBox.Show("Sayfa sayısı sadece rakam olmalıdır!");
                return;
            }

            if (sayfaSayisi <= 0 || sayfaSayisi > 10000)
            {
                MessageBox.Show("Sayfa sayısı 1 ile 10000 arasında olmalıdır!");
                return;
            }

            k.SayfaSayisi = sayfaSayisi;
            k.Tur = txtTur.Text;

            kitaplar.Add(k);

            dataGridView1.DataSource = null;
            dataGridView1.DataSource = kitaplar;
            lblToplamKitap.Text = "Toplam Kitap: " + kitaplar.Count;

            txtKitapAd.Text = "";
            txtYazar.Text = "";
            txtSayfa.Text = "";
            txtTur.Text = "";
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
        {
            if (dataGridView1.CurrentRow == null) return;

            txtKitapAd.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            txtYazar.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            txtSayfa.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
        }
    }

        private void btnSil_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand(
        "DELETE FROM Kitaplar WHERE KitapAdi=@p1", baglanti);

            komut.Parameters.AddWithValue("@p1", txtKitapAd.Text);

            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();

            MessageBox.Show("Kitap silindi");
        }


        private void Temizle()
        {
            throw new NotImplementedException();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand(
    "UPDATE Kitaplar SET Yazar=@p2, SayfaSayisi=@p3, Tur=@p4 WHERE KitapAdi=@p1", baglanti);

            komut.Parameters.AddWithValue("@p1", txtKitapAd.Text);
            komut.Parameters.AddWithValue("@p2", txtYazar.Text);
            komut.Parameters.AddWithValue("@p3", txtSayfa.Text);
            komut.Parameters.AddWithValue("@p4", txtTur.Text);

            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();

            MessageBox.Show("Kitap güncellendi");


            Temizle();
            void Temizle()
            {
                txtKitapAd.Text = "";
                txtYazar.Text = "";
                txtSayfa.Text = "";
                txtTur.Text = "";
            }
        }



        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnAra_Click(object sender, EventArgs e)
        {
        
        {
                {
                    SqlDataAdapter da = new SqlDataAdapter(
                        "SELECT KitapAdi, Yazar, SayfaSayisi, Tur FROM Kitaplar WHERE KitapAdi LIKE @p1", baglanti);

                    da.SelectCommand.Parameters.AddWithValue("@p1", "%" + txtAra.Text + "%");

                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dataGridView1.DataSource = dt;
                }
            }
        }



        private void btnListele_Click(object sender, EventArgs e)
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT KitapAdi, Yazar, SayfaSayisi, Tur FROM Kitaplar", baglanti);

            DataTable dt = new DataTable();
            da.Fill(dt);

            dataGridView1.DataSource = dt;
        }
        
    

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void txtTur_TextChanged(object sender, EventArgs e)
        {

        }
    }
    }

public class Kitap
{
    public string KitapAd { get; set; }
    public string Yazar { get; set; }
    public int SayfaSayisi { get; set; }
    public string Tur { get; set; }
}
