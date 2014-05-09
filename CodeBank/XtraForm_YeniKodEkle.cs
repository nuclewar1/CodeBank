using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using MainContext;
using Devart.Data.SQLite;

namespace CodeBank
{
    public partial class XtraForm_YeniKodEkle : DevExpress.XtraEditors.XtraForm
    {
        enum AcmaTipi
        {
            YeniKaydet,
            Guncelle
        }
        MainDataContext ctx;
        Kodlar EklenecekKod = new Kodlar();
        AcmaTipi ac;
        public XtraForm_YeniKodEkle()
        {
            InitializeComponent();
            VeriTabaniAyarla();
            ac = AcmaTipi.YeniKaydet;
        }

        public XtraForm_YeniKodEkle(int Kod)
        {
            InitializeComponent();
            VeriTabaniAyarla();
            KategorileriCek();
            EklenecekKod = ctx.Kodlars.Where(k => k.ID == Kod).Select(k => k).Single();
            barEditItem_Kategori.EditValue = EklenecekKod.Kategori;
            barEditItem_AltKategori.EditValue = EklenecekKod.AltKategori;
            barEditItem_baslik.EditValue = EklenecekKod.Baslik;
            richEditControl_Kod.Text = EklenecekKod.Kod;
            ac = AcmaTipi.Guncelle;
            barButtonItem_Kaydet.Caption = "Güncelle";
            this.Text = "Kod Güncelle ( " + EklenecekKod.Baslik + " )";
        }
        private void barEditItem_Kategori_EditValueChanged(object sender, EventArgs e)
        {
            int ID = Convert.ToInt32(barEditItem_Kategori.EditValue);
            repositoryItemLookUpEdit_AltKategori.DataSource = ctx.AltKategoris.Where(ak => ak.Kategoriler.ID == ID).Select(ak => new
            {
                Isim = ak.Ad,
                ID = ak.ID
            });
        }

        private void VeriTabaniAyarla()
        {
            SQLiteConnectionStringBuilder builder = new SQLiteConnectionStringBuilder();
            builder.DataSource = Application.StartupPath + "\\db.db";
            ctx = new MainDataContext(builder.ToString());
            KategorileriCek();
        }
        private void KategorileriCek()
        {
            repositoryItemLookUpEdit_Kategori.DataSource = ctx.Kategorilers.Select(k => new
            {
                Ad = k.Ad,
                ID = k.ID
            });
        }

        private void richEditControl_Kod_TextChanged(object sender, EventArgs e)
        {
            barStaticItem_kalanHarf.Caption = "Kalan harf: " + (2000 - richEditControl_Kod.Text.Length);
        }

        private void barButtonItem_Kaydet_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (barEditItem_Kategori.EditValue==null || barEditItem_AltKategori.EditValue==null || barEditItem_baslik.EditValue==null)
            {
                XtraMessageBox.Show("Üst kategori veya alt kategori veya başlık boş geçilemez. Lütfen kontrol ediniz.", "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            richEditControl_Kod.Text = richEditControl_Kod.Text.TrimEnd();
            if (richEditControl_Kod.Text.Length> 2000)
            {
                XtraMessageBox.Show("Kod fazla uzun!", "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            string baslik = barEditItem_baslik.EditValue.ToString().Trim();

            if (baslik.Length==0)
            {
                XtraMessageBox.Show("Başlık boş geçilemez. Lütfen kontrol ediniz.", "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                barEditItem_baslik.EditValue = baslik;
                return;
            }
            EklenecekKod.Kategori = Convert.ToInt32(barEditItem_Kategori.EditValue);
            EklenecekKod.AltKategori = Convert.ToInt32(barEditItem_AltKategori.EditValue);
            EklenecekKod.Baslik = baslik;
            EklenecekKod.Kod = richEditControl_Kod.Text;

            if (ac==AcmaTipi.YeniKaydet)
            {
                EklenecekKod.ID = ctx.Kodlars.Max(k => k.ID) + 1;
                ctx.Kodlars.InsertOnSubmit(EklenecekKod);
            }

            
            ctx.SubmitChanges();

            barEditItem_Kategori.EditValue = null;
            this.Close();
        }

        private void barButtonItem_kategoriIslemleri_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form_Kategori kat = new Form_Kategori();
            kat.ShowDialog();
            barEditItem_Kategori.EditValue = null;
            KategorileriCek();
        }

        private void repositoryItemTextEdit_Baslik_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Char.ToUpper(e.KeyChar);
        }

    }
}