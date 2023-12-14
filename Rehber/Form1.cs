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

namespace Rehber
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection baglanti=new SqlConnection(@"Data Source=DESKTOP-A21VQ07\SQLEXPRESS;Initial Catalog=Rehber;Integrated Security=True");

        void Listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da=new SqlDataAdapter("Select * from kisiler ",baglanti);
            da.Fill(dt);
            dgvRehber.DataSource = dt;
        }
        void Temizle()
        {
            txtAd.Text = "";
            txtId.Text = "";
            txtSoyad.Text = "";
            txtTel.Text = "";
            txtMail.Text = "";
            txtAd.Focus();
        }

       
        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Listele();
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("insert into kisiler (AD,SOYAD,TELEFON,MAIL) values (@P1,@P2,@P3,@P4)", baglanti);
            komut.Parameters.AddWithValue("@P1",txtAd.Text);
            komut.Parameters.AddWithValue("@P2",txtSoyad.Text);
            komut.Parameters.AddWithValue("@P3",txtTel.Text);
            komut.Parameters.AddWithValue("@P4",txtMail.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kişi kaydı başarılı.","Bilgi",MessageBoxButtons.OK, MessageBoxIcon.Information);
            Listele();
            Temizle();
        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            Temizle();
        }

        private void dgvRehber_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dgvRehber.SelectedCells[0].RowIndex;

            txtId.Text = dgvRehber.Rows[secilen].Cells[0].Value.ToString();
            txtAd.Text = dgvRehber.Rows[secilen].Cells[1].Value.ToString();
            txtSoyad.Text = dgvRehber.Rows[secilen].Cells[2].Value.ToString();
            txtTel.Text = dgvRehber.Rows[secilen].Cells[3].Value.ToString();
            txtMail.Text = dgvRehber.Rows[secilen].Cells[4].Value.ToString();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Delete from kisiler where ID=" + txtId.Text, baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kişi silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Listele();

        }

        private void btnGüncelle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("update kisiler set AD=@P1,SOYAD=@P2,TELEFON=@P3,MAIL=@P4 where ID=@P5", baglanti);
            komut.Parameters.AddWithValue("@P1",txtAd.Text);
            komut.Parameters.AddWithValue("@P2", txtSoyad.Text);
            komut.Parameters.AddWithValue("@P3", txtTel.Text);
            komut.Parameters.AddWithValue("@P4", txtMail.Text);
            komut.Parameters.AddWithValue("@P5", txtId.Text);
            komut.ExecuteNonQuery();
            baglanti.Close() ;
            MessageBox.Show("Kişi bilgileri güncellendi.","Uyarı",MessageBoxButtons.OK, MessageBoxIcon.Information);
            Listele() ;
            Temizle();
        }
    }
}

