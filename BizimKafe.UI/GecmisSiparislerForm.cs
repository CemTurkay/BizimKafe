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
    public partial class GecmisSiparislerForm : Form
    {
        private readonly KafeVeri _db;
        public GecmisSiparislerForm(KafeVeri db)
        {
            _db = db;
            InitializeComponent();
            dgvSiparisler.DataSource = _db.GecmisSiparisler;

        }

        private void dgvSiparisler_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvSiparisler.SelectedRows.Count == 0)
            {
                dgvSiparisDetaylar.DataSource = null;
            }
            else
            {
                DataGridViewRow secilenSatir = dgvSiparisler.SelectedRows[0];
                Siparis secilenSiparsi = (Siparis)secilenSatir.DataBoundItem; //tag gibi bişi. prop(ad, soyad) ayrı değil tamamını getiryor.
                dgvSiparisDetaylar.DataSource = secilenSiparsi.SiparisDetaylar;

            }
        }
    }
}
