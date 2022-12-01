﻿using BizimKafe.DATA;
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
    public partial class SiparisForm : Form
    {
        private readonly KafeVeri _db;
        private readonly Siparis _siparis;

        public SiparisForm(KafeVeri db, Siparis siparis)
        {
            _db = db;
            _siparis = siparis;            
            InitializeComponent();

            BilgileriGuncelle();
            UrunleriListele();
        }

        private void UrunleriListele()
        {
            cmbUrun.DataSource = _db.Urunler;
        }

        private void BilgileriGuncelle()
        {
            Text = $"Masa {_siparis.MasaNo}";
            lblMasaNo.Text = _siparis.MasaNo.ToString("00");
            lblOdemeTutari.Text = _siparis.ToplamTutarTL;
            dgvSiparisDetaylar.DataSource = _siparis.SiparisDetaylar.ToList();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Urun urun = (Urun)cmbUrun.SelectedItem;
            if (urun == null)
                return;

            SiparisDetay sd = _siparis.SiparisDetaylar.FirstOrDefault(x => x.UrunAd == urun.UrunAd);
           
            if (sd != null)
            {
                sd.Adet += (int)nudAdet.Value;
            }
            else
            {
                sd = new SiparisDetay()
                {
                    UrunAd = urun.UrunAd,
                    BirimFiyat = urun.BirimFiyat,
                    Adet = (int)nudAdet.Value,
                };
                _siparis.SiparisDetaylar.Add(sd);
            } 

            BilgileriGuncelle();

        }

        private void btnAnaSayfa_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnOde_Click(object sender, EventArgs e)
        {
            SiparisKapat(_siparis.ToplamTutar(), SiparisDurumu.Odendi);
        }

        private void btnIptal_Click(object sender, EventArgs e)
        {
            SiparisKapat(0, SiparisDurumu.Iptal);
        }


        private void SiparisKapat(decimal odenenTutar, SiparisDurumu yeniDurum)
        {
            _siparis.KapanisZamani = DateTime.Now;
            _siparis.OdenenTutar = odenenTutar;
            _siparis.Durum = yeniDurum;
            _db.AktifSiparisler.Remove(_siparis);
            _db.GecmisSiparisler.Add(_siparis);
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}