namespace CodeBank
{
    partial class XtraForm_YeniKodEkle
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XtraForm_YeniKodEkle));
            this.ribbonControl1 = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.barEditItem_Kategori = new DevExpress.XtraBars.BarEditItem();
            this.repositoryItemLookUpEdit_Kategori = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.barEditItem_AltKategori = new DevExpress.XtraBars.BarEditItem();
            this.repositoryItemLookUpEdit_AltKategori = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.barButtonItem_Kaydet = new DevExpress.XtraBars.BarButtonItem();
            this.barEditItem_baslik = new DevExpress.XtraBars.BarEditItem();
            this.repositoryItemTextEdit_Baslik = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.barStaticItem_kalanHarf = new DevExpress.XtraBars.BarStaticItem();
            this.barButtonItem_kategoriIslemleri = new DevExpress.XtraBars.BarButtonItem();
            this.ribbonPage1 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup_Kategori = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonStatusBar1 = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            this.richEditControl_Kod = new DevExpress.XtraRichEdit.RichEditControl();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit_Kategori)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit_AltKategori)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit_Baslik)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbonControl1
            // 
            this.ribbonControl1.ExpandCollapseItem.Id = 0;
            this.ribbonControl1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbonControl1.ExpandCollapseItem,
            this.barEditItem_Kategori,
            this.barEditItem_AltKategori,
            this.barButtonItem_Kaydet,
            this.barEditItem_baslik,
            this.barStaticItem_kalanHarf,
            this.barButtonItem_kategoriIslemleri});
            this.ribbonControl1.Location = new System.Drawing.Point(0, 0);
            this.ribbonControl1.MaxItemId = 7;
            this.ribbonControl1.Name = "ribbonControl1";
            this.ribbonControl1.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPage1});
            this.ribbonControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemLookUpEdit_Kategori,
            this.repositoryItemLookUpEdit_AltKategori,
            this.repositoryItemTextEdit_Baslik});
            this.ribbonControl1.ShowPageHeadersMode = DevExpress.XtraBars.Ribbon.ShowPageHeadersMode.Hide;
            this.ribbonControl1.Size = new System.Drawing.Size(1184, 96);
            this.ribbonControl1.StatusBar = this.ribbonStatusBar1;
            this.ribbonControl1.ToolbarLocation = DevExpress.XtraBars.Ribbon.RibbonQuickAccessToolbarLocation.Hidden;
            // 
            // barEditItem_Kategori
            // 
            this.barEditItem_Kategori.Caption = "Kategori";
            this.barEditItem_Kategori.Edit = this.repositoryItemLookUpEdit_Kategori;
            this.barEditItem_Kategori.Id = 1;
            this.barEditItem_Kategori.Name = "barEditItem_Kategori";
            this.barEditItem_Kategori.Width = 160;
            this.barEditItem_Kategori.EditValueChanged += new System.EventHandler(this.barEditItem_Kategori_EditValueChanged);
            // 
            // repositoryItemLookUpEdit_Kategori
            // 
            this.repositoryItemLookUpEdit_Kategori.AutoHeight = false;
            this.repositoryItemLookUpEdit_Kategori.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemLookUpEdit_Kategori.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Ad", "Kategori", 20, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.Ascending)});
            this.repositoryItemLookUpEdit_Kategori.DisplayMember = "Ad";
            this.repositoryItemLookUpEdit_Kategori.Name = "repositoryItemLookUpEdit_Kategori";
            this.repositoryItemLookUpEdit_Kategori.NullText = "Kategori Seçin";
            this.repositoryItemLookUpEdit_Kategori.ValueMember = "ID";
            // 
            // barEditItem_AltKategori
            // 
            this.barEditItem_AltKategori.Caption = "Alt Kategori";
            this.barEditItem_AltKategori.Edit = this.repositoryItemLookUpEdit_AltKategori;
            this.barEditItem_AltKategori.Id = 2;
            this.barEditItem_AltKategori.Name = "barEditItem_AltKategori";
            this.barEditItem_AltKategori.Width = 145;
            // 
            // repositoryItemLookUpEdit_AltKategori
            // 
            this.repositoryItemLookUpEdit_AltKategori.AutoHeight = false;
            this.repositoryItemLookUpEdit_AltKategori.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemLookUpEdit_AltKategori.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Isim", "AltKategori")});
            this.repositoryItemLookUpEdit_AltKategori.DisplayMember = "Isim";
            this.repositoryItemLookUpEdit_AltKategori.Name = "repositoryItemLookUpEdit_AltKategori";
            this.repositoryItemLookUpEdit_AltKategori.NullText = "Alt Kategori Seçin";
            this.repositoryItemLookUpEdit_AltKategori.ValueMember = "ID";
            // 
            // barButtonItem_Kaydet
            // 
            this.barButtonItem_Kaydet.Caption = "Kaydet";
            this.barButtonItem_Kaydet.Id = 3;
            this.barButtonItem_Kaydet.Name = "barButtonItem_Kaydet";
            this.barButtonItem_Kaydet.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem_Kaydet_ItemClick);
            // 
            // barEditItem_baslik
            // 
            this.barEditItem_baslik.Caption = "Başlık";
            this.barEditItem_baslik.Edit = this.repositoryItemTextEdit_Baslik;
            this.barEditItem_baslik.Id = 4;
            this.barEditItem_baslik.Name = "barEditItem_baslik";
            this.barEditItem_baslik.Width = 300;
            // 
            // repositoryItemTextEdit_Baslik
            // 
            this.repositoryItemTextEdit_Baslik.AutoHeight = false;
            this.repositoryItemTextEdit_Baslik.MaxLength = 200;
            this.repositoryItemTextEdit_Baslik.Name = "repositoryItemTextEdit_Baslik";
            this.repositoryItemTextEdit_Baslik.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.repositoryItemTextEdit_Baslik_KeyPress);
            // 
            // barStaticItem_kalanHarf
            // 
            this.barStaticItem_kalanHarf.Id = 5;
            this.barStaticItem_kalanHarf.Name = "barStaticItem_kalanHarf";
            this.barStaticItem_kalanHarf.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // barButtonItem_kategoriIslemleri
            // 
            this.barButtonItem_kategoriIslemleri.Caption = "Yeni Üst veya Alt Kategori Ekleyin";
            this.barButtonItem_kategoriIslemleri.Id = 6;
            this.barButtonItem_kategoriIslemleri.Name = "barButtonItem_kategoriIslemleri";
            this.barButtonItem_kategoriIslemleri.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem_kategoriIslemleri_ItemClick);
            // 
            // ribbonPage1
            // 
            this.ribbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroup_Kategori,
            this.ribbonPageGroup1});
            this.ribbonPage1.Name = "ribbonPage1";
            this.ribbonPage1.Text = "ribbonPage1";
            // 
            // ribbonPageGroup_Kategori
            // 
            this.ribbonPageGroup_Kategori.ItemLinks.Add(this.barEditItem_Kategori);
            this.ribbonPageGroup_Kategori.ItemLinks.Add(this.barEditItem_AltKategori);
            this.ribbonPageGroup_Kategori.ItemLinks.Add(this.barButtonItem_kategoriIslemleri);
            this.ribbonPageGroup_Kategori.Name = "ribbonPageGroup_Kategori";
            this.ribbonPageGroup_Kategori.Text = "Kategoriler";
            // 
            // ribbonPageGroup1
            // 
            this.ribbonPageGroup1.ItemLinks.Add(this.barEditItem_baslik);
            this.ribbonPageGroup1.ItemLinks.Add(this.barButtonItem_Kaydet);
            this.ribbonPageGroup1.Name = "ribbonPageGroup1";
            this.ribbonPageGroup1.Text = "İşlemler";
            // 
            // ribbonStatusBar1
            // 
            this.ribbonStatusBar1.ItemLinks.Add(this.barStaticItem_kalanHarf);
            this.ribbonStatusBar1.Location = new System.Drawing.Point(0, 634);
            this.ribbonStatusBar1.Name = "ribbonStatusBar1";
            this.ribbonStatusBar1.Ribbon = this.ribbonControl1;
            this.ribbonStatusBar1.Size = new System.Drawing.Size(1184, 27);
            // 
            // richEditControl_Kod
            // 
            this.richEditControl_Kod.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richEditControl_Kod.Location = new System.Drawing.Point(0, 96);
            this.richEditControl_Kod.MenuManager = this.ribbonControl1;
            this.richEditControl_Kod.Name = "richEditControl_Kod";
            this.richEditControl_Kod.Options.Fields.UseCurrentCultureDateTimeFormat = false;
            this.richEditControl_Kod.Options.MailMerge.KeepLastParagraph = false;
            this.richEditControl_Kod.Size = new System.Drawing.Size(1184, 565);
            this.richEditControl_Kod.TabIndex = 1;
            this.richEditControl_Kod.TextChanged += new System.EventHandler(this.richEditControl_Kod_TextChanged);
            // 
            // XtraForm_YeniKodEkle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 661);
            this.Controls.Add(this.ribbonStatusBar1);
            this.Controls.Add(this.richEditControl_Kod);
            this.Controls.Add(this.ribbonControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "XtraForm_YeniKodEkle";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Yeni Kod Ekle";
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit_Kategori)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit_AltKategori)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit_Baslik)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl1;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup_Kategori;
        private DevExpress.XtraBars.BarEditItem barEditItem_Kategori;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositoryItemLookUpEdit_Kategori;
        private DevExpress.XtraBars.BarEditItem barEditItem_AltKategori;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositoryItemLookUpEdit_AltKategori;
        private DevExpress.XtraBars.Ribbon.RibbonStatusBar ribbonStatusBar1;
        private DevExpress.XtraRichEdit.RichEditControl richEditControl_Kod;
        private DevExpress.XtraBars.BarButtonItem barButtonItem_Kaydet;
        private DevExpress.XtraBars.BarEditItem barEditItem_baslik;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit_Baslik;
        private DevExpress.XtraBars.BarStaticItem barStaticItem_kalanHarf;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup1;
        private DevExpress.XtraBars.BarButtonItem barButtonItem_kategoriIslemleri;
    }
}