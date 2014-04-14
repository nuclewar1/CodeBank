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
            SQLiteConnectionStringBuilder builder = new SQLiteConnectionStringBuilder();
            builder.DataSource = Application.StartupPath + "\\db.db";
            ctx = new MainDataContext(builder.ToString());
            VeriTabaniKontroluYap();
            KategorileriCek();
        }

        private void VeriTabaniKontroluYap()
        {
            if (!ctx.DatabaseExists())
            {
                File.Open(ctx.Connection.DataSource, FileMode.CreateNew);
                ctx.CreateDatabase();
                File.SetAttributes(ctx.Connection.DataSource, FileAttributes.Hidden); 
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

    }
}
