using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Devart.Data.SQLite;
using Devart.Data.SQLite.Linq;
using MainContext;
using DevExpress.XtraEditors;
using DevExpress.XtraRichEdit;
using System.IO;

namespace CodeBank
{
    public partial class Form_Ana : Form
    {
        public Form_Ana()
        {
            InitializeComponent();
        }

        MainDataContext ctx;
        Kodlar Kod;
        private void Form1_Load(object sender, EventArgs e)
        {
            VeriTabaniKontroluYap();
            SQLiteConnectionStringBuilder builder = new SQLiteConnectionStringBuilder();
            builder.DataSource = Application.StartupPath + "\\db.db";
            ctx = new MainDataContext(builder.ToString());
            KategorileriCek();
            listBoxControl_Kodlar.DataSource = ctx.Kodlars.Select(k => k);
        }

        private void VeriTabaniKontroluYap()
        {
            if (!File.Exists("db.db"))
            {
                MessageBox.Show("Test");
                File.Copy("cdb.db", "db.db");
            }
        }

        private void KategorileriCek()
        {
            repositoryItemLookUpEdit_Kategori.DataSource = ctx.Kategorilers.Select(k => new
            {
                Ad = k.Ad,
                ID = k.ID
            });
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

        private void barEditItem_AltKategori_EditValueChanged(object sender, EventArgs e)
        {
            int ID = Convert.ToInt32(barEditItem_AltKategori.EditValue);
            listBoxControl_Kodlar.DataSource = ctx.Kodlars.Where(k => k.AltKategori1.ID == ID).Select(k => new
                {
                    ID = k.ID,
                    Baslik = k.Baslik
                });
        }

        private void listBoxControl_Kodlar_SelectedValueChanged_1(object sender, EventArgs e)
        {
            if (listBoxControl_Kodlar.ItemCount == 0)
            {
                return;
            }
            int ID = Convert.ToInt32(listBoxControl_Kodlar.SelectedValue);
            Kod = ctx.Kodlars.Where(k => k.ID == ID).Select(k => k).Single();
            richEditControl_Kod.Text = Kod.Baslik + '\n' + '\n' + Kod.Kod;
        }

        private void barButtonItem_YeniKodEkle_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            XtraForm_YeniKodEkle ekle = new XtraForm_YeniKodEkle();
            ekle.ShowDialog();
            barEditItem_Kategori.EditValue = null;
            KategorileriCek();
        }

        private void barButtonItem_KategoriIslemleri_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form_Kategori kat = new Form_Kategori();
            kat.ShowDialog();
            barEditItem_Kategori.EditValue = null;
            KategorileriCek();
        }

        private void barButtonItem_KoduSil_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (Kod == null)
                return;

            DialogResult result = XtraMessageBox.Show(Kod.Baslik + " silinecek. Onaylıyor musunuz?", "Uyarı!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result != DialogResult.Yes)
            {
                return;
            }

            int ID = Kod.AltKategori;
            ctx.Kodlars.DeleteOnSubmit(Kod);
            ctx.SubmitChanges();
            Kod = null;
            richEditControl_Kod.Text = "";
            listBoxControl_Kodlar.DataSource = ctx.Kodlars.Where(k => k.AltKategori == ID).Select(k => k);
        }

        private void barButtonItem_koduGuncelle_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (Kod == null)
                return;
            XtraForm_YeniKodEkle kodEkle = new XtraForm_YeniKodEkle(Kod.ID);
            kodEkle.ShowDialog();
            listBoxControl_Kodlar.DataSource = ctx.Kodlars.Where(k => k.AltKategori == Kod.AltKategori).Select(k => k);
            barEditItem_AltKategori.EditValue = null;
            barEditItem_AltKategori.EditValue = Kod.AltKategori;
        }

        private void barButtonItem_yazdir_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (Kod == null)
                return;
            richEditControl_Kod.ShowPrintPreview();
        }

        private void barButtonItem_farkliKaydet_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (Kod == null)
                return;
            if (DialogResult.OK == saveFileDialog1.ShowDialog())
            {
                richEditControl_Kod.SaveDocument(saveFileDialog1.FileName, DocumentFormat.Doc);
                XtraMessageBox.Show(Kod.Baslik + " başarı ile kaydedildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void barButtonItem_Ara_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (barEditItem_AranacakMetin.EditValue == null || barEditItem_AranacakMetin.EditValue.ToString().Trim().Length == 0)
            {
                return;
            }

            string aranacakMetin = barEditItem_AranacakMetin.EditValue.ToString().Trim();

            int[] AramaSecenegiDeger = new int[4];
            AramaSecenegiDeger[0] = Convert.ToInt32(barEditItem_KategoriAdindaAra.EditValue);
            AramaSecenegiDeger[1] = Convert.ToInt32(barEditItem_AltKategoriAdindaAra.EditValue);
            AramaSecenegiDeger[2] = Convert.ToInt32(barEditItem_KodBasligindaAra.EditValue);
            AramaSecenegiDeger[3] = Convert.ToInt32(barEditItem_KodicindeAra.EditValue);
            string aramaSecenegi = "_";
            foreach (int item in AramaSecenegiDeger)
            {
                aramaSecenegi += item;
            }

            AramaIslemleri.AramaSecenekleri AramaSecenegi = (AramaIslemleri.AramaSecenekleri)Enum.Parse(typeof(AramaIslemleri.AramaSecenekleri), aramaSecenegi);

            if (AramaSecenegi == AramaIslemleri.AramaSecenekleri._0000)
            {
                return;
            }

            int KayitSayisi = 0;
            IEnumerable<Kodlar> kodlar = AramaIslemleri.Ara(aranacakMetin, AramaSecenegi, ctx, out KayitSayisi);
            if (KayitSayisi > 0)
            {
                listBoxControl_Kodlar.DataSource = kodlar;
            }
            else
            {
                XtraMessageBox.Show(aranacakMetin + " içeren bir kayıt bulunamadı.", "Sonuç", MessageBoxButtons.OK, MessageBoxIcon.None);
            }
        }

        private void ribbonControl1_SelectedPageChanged(object sender, EventArgs e)
        {
            if (ribbonControl1.SelectedPage.Text == "Ara")
            {
                listBoxControl_Kodlar.DataSource = null;
            }
        }

        private void listBoxControl_Kodlar_DataSourceChanged(object sender, EventArgs e)
        {
            if (listBoxControl_Kodlar.DataSource==null)
            {
                richEditControl_Kod.Text = "";
            }
        }

    }
}
