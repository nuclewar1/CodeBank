using MainContext;
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
using DevExpress.XtraEditors;

namespace CodeBank
{
    public partial class Form_Kategori : Form
    {
        public Form_Kategori()
        {
            InitializeComponent();
        }

        MainDataContext ctx;
        Kategoriler kategori;
        AltKategori Altkategori;

        private void Form_Kategori_Load(object sender, EventArgs e)
        {
            SQLiteConnectionStringBuilder builder = new SQLiteConnectionStringBuilder();
            builder.DataSource = Application.StartupPath + "\\db.db";
            ctx = new MainDataContext(builder.ToString());
            KategorileriGetir();
        }

        private void KategorileriGetir()
        {
            lookUpEdit_kategoriler.Properties.DataSource = ctx.Kategorilers.Select(k => k);
            lookUpEdit_AltKategoriUstu.Properties.DataSource = ctx.Kategorilers.Select(k => k);
            lookUpEdit_GuncellenecekAltUstu.Properties.DataSource = ctx.Kategorilers.Select(k => k);
        }

        private void lookUpEdit_kategoriler_EditValueChanged(object sender, EventArgs e)
        {
            int ID = Convert.ToInt32(lookUpEdit_kategoriler.EditValue);
            kategori = ctx.Kategorilers.Where(k => k.ID == ID).Select(k => k).Single();
            textEdit_guncellenecek.EditValue = kategori.Ad;
        }

        private void simpleButton_guncelle_Click(object sender, EventArgs e)
        {
            if (kategori == null)
            {
                XtraMessageBox.Show("Güncellenecek kategoriyi seçmediniz.", "Dikkat!", MessageBoxButtons.OK, MessageBoxIcon.None);
                return;
            }
            string kategoriAdi=textEdit_guncellenecek.EditValue.ToString().Trim();
            textEdit_guncellenecek.EditValue = kategoriAdi;
            if (kategoriAdi.Length==0)
            {
                XtraMessageBox.Show("Kategori adı boş bırakılamaz!", "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            kategori.Ad = kategoriAdi;
            try
            {
                ctx.SubmitChanges();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            textEdit_guncellenecek.EditValue = null;
            kategori = null;
            KategorileriGetir();
            this.Close();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (textEdit_kategoriAdi.EditValue==null)
            {
                XtraMessageBox.Show("Kategori adı boş bırakılamaz!", "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            kategori = new Kategoriler();
            string kategoriAdi = textEdit_kategoriAdi.EditValue.ToString().Trim();
            if (kategoriAdi.Length == 0)
            {
                XtraMessageBox.Show("Kategori adı boş bırakılamaz!", "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            kategori.Ad = kategoriAdi;
            kategori.ID = ctx.Kategorilers.Max(k => k.ID) + 1;
            ctx.Kategorilers.InsertOnSubmit(kategori);
            try
            {
                ctx.SubmitChanges();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            textEdit_kategoriAdi.EditValue = null;
            kategori = null;
            KategorileriGetir();
            this.Close();
        }

        private void simpleButton_sil_Click(object sender, EventArgs e)
        {
            if (kategori == null)
            {
                XtraMessageBox.Show("Silinecek kategoriyi seçmediniz.", "Dikkat!", MessageBoxButtons.OK, MessageBoxIcon.None);
                return;
            }
            DialogResult result = XtraMessageBox.Show(kategori.Ad + " silinecek. Bu kategori altındaki bütün alt kategori ve kodlar da silinecek. Onaylıyor musunuz?", "Uyarı!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result==DialogResult.Yes)
            {
                ctx.Kodlars.DeleteAllOnSubmit
                    (
                    ctx.Kodlars.Where(k=>k.Kategoriler.ID == kategori.ID)
                    );
                ctx.AltKategoris.DeleteAllOnSubmit
                    (
                    ctx.AltKategoris.Where(k=>k.Kategoriler.ID==kategori.ID)
                    );
                ctx.Kategorilers.DeleteOnSubmit(kategori);
                ctx.SubmitChanges();
                KategorileriGetir();
                textEdit_guncellenecek.EditValue = null;
                kategori = null;
            }
            this.Close();
        }

        private void simpleButton_AltKategoriEkle_Click(object sender, EventArgs e)
        {
            if (lookUpEdit_AltKategoriUstu.EditValue==null)
            {
                XtraMessageBox.Show("Üst kategori seçmediniz.", "Dikkat!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            if (textEdit_AltKategoriAdi.EditValue==null)
            {
                XtraMessageBox.Show("Alt kategori adı boş geçilemez.", "Dikkat!", MessageBoxButtons.OK, MessageBoxIcon.None);
                return;
            }

            Altkategori = new AltKategori();
            int UstID = Convert.ToInt32(lookUpEdit_AltKategoriUstu.EditValue);
            string katAdi = textEdit_AltKategoriAdi.EditValue.ToString().Trim();
            Altkategori.Ad = katAdi;
            Altkategori.Kategori = UstID;
            Altkategori.ID = ctx.AltKategoris.Max(K => K.ID) + 1;

            try
            {
                ctx.AltKategoris.InsertOnSubmit(Altkategori);
                ctx.SubmitChanges();
                textEdit_AltKategoriAdi.EditValue = null;
                Altkategori = null;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            this.Close();
        }

        private void lookUpEdit_GuncellenecekAltUstu_EditValueChanged(object sender, EventArgs e)
        {
            int ID = Convert.ToInt32(lookUpEdit_GuncellenecekAltUstu.EditValue);
            lookUpEdit_GuncellenecekAlt.Properties.DataSource = ctx.AltKategoris.Where(k => k.Kategori == ID).Select(k => k);
        }

        private void lookUpEdit_GuncellenecekAlt_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            
        }

        private void simpleButton_AltKategoriGuncelle_Click(object sender, EventArgs e)
        {
            if (Altkategori==null)
            {
                XtraMessageBox.Show("Güncellenecek alt kategoriyi seçmediniz.", "Dikkat!", MessageBoxButtons.OK, MessageBoxIcon.None);
                return;
            }

            string altAd = textEdit_guncellenecekAlt.EditValue.ToString().Trim();
            if (altAd.Length==0)
            {
                XtraMessageBox.Show("Alt kategori adı boş geçilemez.", "Dikkat!", MessageBoxButtons.OK, MessageBoxIcon.None);
                return;
            }
            Altkategori.Ad = altAd;
            try
            {
                ctx.SubmitChanges();
                kategori = null;
                Altkategori = null;
                textEdit_guncellenecekAlt.EditValue = null;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            this.Close();
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            if (Altkategori == null)
            {
                XtraMessageBox.Show("Silinecek alt kategoriyi seçmediniz.", "Dikkat!", MessageBoxButtons.OK, MessageBoxIcon.None);
                return;
            }
            DialogResult result = XtraMessageBox.Show(Altkategori.Ad + " silinecek. Bu kategori altındaki bütün kodlar da silinecek. Onaylıyor musunuz?", "Uyarı!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                ctx.Kodlars.DeleteAllOnSubmit
                    (
                    ctx.Kodlars.Where(k => k.AltKategori==Altkategori.ID)
                    );

                ctx.AltKategoris.DeleteOnSubmit(Altkategori);
                ctx.SubmitChanges();
                lookUpEdit_GuncellenecekAlt.Properties.DataSource = ctx.AltKategoris.Select(k => k);
                textEdit_guncellenecekAlt.EditValue = null;
            }
            this.Close();
        }

        private void lookUpEdit_GuncellenecekAlt_EditValueChanged(object sender, EventArgs e)
        {
            int ID = Convert.ToInt32(lookUpEdit_GuncellenecekAlt.EditValue);
            Altkategori = ctx.AltKategoris.Where(k => k.ID == ID).Select(k => k).Single();
            textEdit_guncellenecekAlt.EditValue = Altkategori.Ad;
        }

        private void textEdit_kategoriAdi_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Char.ToUpper(e.KeyChar);
        }
    }
}
