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
using DevExpress.XtraSplashScreen;
using System.Runtime.Serialization.Formatters.Binary;
using System.Net;

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
                if (File.Exists("cdb.db"))
                {
                    File.Copy("cdb.db", "db.db");
                }
                else
                {
                    VeriTabaniniInternettenIndir();
                    
                }
            }
        }

        private void VeriTabaniniInternettenIndir()
        {
            DialogResult sonuc = XtraMessageBox.Show("Veritabanı bulunamadı. Onarılması için internet bağlantısına ihtiyaç duyulmaktadır.\nOnarmak için bu mesajı oanylayınız? (Download miktarı 11KB)", "Hata!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (sonuc == DialogResult.Yes)
            {
                bool tryagain = true;
                do
                {
                    try
                    {
                        SplashScreenManager.ShowForm(typeof(WaitFormYukleniyor));

                        WebClient web = new WebClient();
                        string kaynak = @"https://doc-08-ac-docs.googleusercontent.com/docs/securesc/ha0ro937gcuc7l7deffksulhg5h7mbp1/mq2r7al91j2n60r75vmia2tadp1vh43q/1397570400000/13902253761435311488/*/0B2EAA8C7rXnhb0JrbUVmSzB4cDA?h=16653014193614665626&e=download";
                        web.DownloadFile(kaynak, "cdb.db");
                        if (File.Exists("cdb.db"))
                        {
                            File.SetAttributes("cdb.db", FileAttributes.Hidden);
                        }

                        tryagain = false;
                        XtraMessageBox.Show("Veritabı başarı ile onarıldı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        VeriTabaniKontroluYap();
                    }
                    catch (Exception ex)
                    {
                        sonuc = XtraMessageBox.Show(ex.Message + '\n' + "Tekrar denensin mi?", "Hata!", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        SplashScreenManager.CloseForm();
                    }
                } while (sonuc == DialogResult.Yes && tryagain);

                if (sonuc == DialogResult.No)
                {
                    Application.Exit();
                }

            }
            else
            {
                Application.Exit();
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
            //Yeni Arama fonksiyonu

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
            else if (ribbonControl1.SelectedPage.Name == "ribbonPage_Yonetim")
            {
                Ozellikler ozellik = ctx.Ozelliklers.Where(o => o.Ozellik == "SonYedekleme").Select(o => o).Single();
                barStaticItem_sonYedeklemeZamani.Caption = ozellik.Deger;

            }
        }

        private void listBoxControl_Kodlar_DataSourceChanged(object sender, EventArgs e)
        {
            if (listBoxControl_Kodlar.DataSource == null)
            {
                richEditControl_Kod.Text = "";
            }
        }

        private void barButtonItem_yedekle_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //Yeni Yedek al
            SaveFileDialog save = new SaveFileDialog();
            save.Filter = "Code Bank Files|*.sfr";
            if (DialogResult.OK == save.ShowDialog())
            {
                SplashScreenManager.ShowForm(typeof(WaitFormYukleniyor));

                DateTime simdi = DateTime.Now;
                Ozellikler ozellik = ozellik = ctx.Ozelliklers.Where(o => o.Ozellik == "SonYedekleme").Select(o => o).Single();
                ozellik.Deger = simdi.ToString("dd MMMM yyyy HH:mm:ss");
                ozellik.SonIslemTarihi = simdi;
                ctx.SubmitChanges();
                File.Copy("db.db", save.FileName, true);

                SplashScreenManager.CloseForm();

                ozellik = ctx.Ozelliklers.Where(o => o.Ozellik == "SonYedekleme").Select(o => o).Single();
                barStaticItem_sonYedeklemeZamani.Caption = ozellik.Deger;
                XtraMessageBox.Show("Yedekleme işlemi başarı ile gerçekleşmiştir.", "Bilgi!", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        private void barButtonItem_yedegiYukle_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //Yeni Yedekten yükleme
            //Gelistir Yedekten yüklemeden sonra veri kaybını önle

            OpenFileDialog open = new OpenFileDialog();
            if (DialogResult.OK == open.ShowDialog())
            {
                SQLiteConnectionStringBuilder str = new SQLiteConnectionStringBuilder();
                str.DataSource = open.FileName;
                MainDataContext ctx2 = new MainDataContext(str.ToString());
                string yedeklemeTarihi = ctx.Ozelliklers.Where(o => o.ID == 1).Select(o => o.Deger).Single();
                StringBuilder builder = new StringBuilder();
                builder.AppendFormat("Seçtiğiniz yedekleme dosyası {0} tarihinde alınmıştır.\nDevam etmeniz durumunda veritanınız bu yedek dosyası ile eşitlenecektir.\nBu tarihten sonraki kodlarınız silinecektir. Devam etmek istiyor musunuz?", yedeklemeTarihi);

                DialogResult result = XtraMessageBox.Show(builder.ToString(), "Uyarı!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    SplashScreenManager.ShowForm(typeof(WaitFormYukleniyor));

                    File.Copy(open.FileName, "db.db", true);

                    DateTime simdi = DateTime.Now;
                    Ozellikler ozellik = ctx.Ozelliklers.Where(o => o.ID == 2).Select(o => o).Single();
                    ozellik.Deger = simdi.ToString("dd MMMM yyyy HH:mm:ss");
                    ozellik.SonIslemTarihi = simdi;
                    ctx.SubmitChanges();

                    SplashScreenManager.CloseForm();

                    XtraMessageBox.Show("Yedekten geri yükleme işlemi başarı ile gerçekleşmiştir.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ozellik = ctx.Ozelliklers.Where(o => o.ID == 1).Select(o => o).Single();
                    barStaticItem_sonYedeklemeZamani.Caption = ozellik.Deger;
                    KategorileriCek();
                }
            }
        }

    }
}
