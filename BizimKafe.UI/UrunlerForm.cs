using BizimKafe.DATA;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BizimKafe.UI
{
    public partial class UrunlerForm : Form
    {
        private readonly KafeVeri _db;
        public UrunlerForm(KafeVeri db)
        {
            InitializeComponent();
            _db = db;
            UrunleriListele();


        }

        private void UrunleriListele()
        {
            dgvUrunler.DataSource = _db.Urunler.ToList();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.Text == "EKLE")
            {
                string ad = txtbxUrunAdi.Text.Trim();
              

                if (string.IsNullOrEmpty(ad))
                {
                    MessageBox.Show("Ürün adi zorunlu");
                    return;
                }
                _db.Urunler.Add(new Urun()
                {
                    UrunAd = ad,
                    BirimFiyat = nudBirimFiyat.Value
                });
                UrunleriListele();
            }
            if (txtbxUrunAdi.Text =="")
            {
                MessageBox.Show("Lütfen ürün giriniz!");
                return ;
            }

            else if (button1.Text == "KAYDET")
            {
                DataGridViewRow satir = dgvUrunler.SelectedRows[0];
                Urun urun = (Urun)satir.DataBoundItem;
                urun.BirimFiyat = nudBirimFiyat.Value;
                urun.UrunAd = txtbxUrunAdi.Text;
                UrunleriListele();
                button1.Text = "EKLE";
                btnIptal.Enabled = false;
                txtbxUrunAdi.Clear();
                nudBirimFiyat.Value = 0;    
            }
           


        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            if (dgvUrunler.SelectedRows.Count == 0)
            {
                MessageBox.Show("Önce ürün seçiniz");
                return;
            }

            DialogResult dr = MessageBox.Show("Seçili ürünü silmek istediğinize emin misiniz?", "Silme Onayı", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (dr == DialogResult.No)
                return;

            DataGridViewRow satir = dgvUrunler.SelectedRows[0];
            Urun urun = (Urun)satir.DataBoundItem;
            _db.Urunler.Remove(urun);
            UrunleriListele();

        }

        private void btnDuzenle_Click(object sender, EventArgs e)
        {
            button1.Text = "KAYDET";
            btnIptal.Visible = true;
            btnIptal.Enabled = true;
            DataGridViewRow satir = dgvUrunler.SelectedRows[0];
            Urun urun = (Urun)satir.DataBoundItem;
            txtbxUrunAdi.Text = urun.UrunAd;
            nudBirimFiyat.Value = urun.BirimFiyat;          

        }

        private void btnIptal_Click(object sender, EventArgs e)
        {
            button1.Text = "EKLE";
            btnIptal.Enabled = false;
            btnIptal.Visible = false;
            txtbxUrunAdi.Clear();
            nudBirimFiyat.Value = 0;
        }
    }
}
