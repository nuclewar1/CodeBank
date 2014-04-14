using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MainContext;

namespace CodeBank
{
    static class AramaIslemleri
    {
        public enum AramaSecenekleri
        {
            _0000,
            _0001,
            _0010,
            _0011,
            _0100,
            _0101,
            _0110,
            _0111,
            _1000,
            _1001,
            _1010,
            _1011,
            _1100,
            _1101,
            _1110,
            _1111

        }

        internal static IEnumerable<Kodlar> Ara(string AranacakMetin, AramaSecenekleri AramaSecenegi, MainDataContext ctx, out int KayitSayisi)
        {
            IEnumerable<Kodlar> Kodlar = null;
            //Kategori - AltKategori - Başlık - Kod
            try
            {
                switch (AramaSecenegi)
                {
                    case AramaSecenekleri._0000:
                        break;

                    case AramaSecenekleri._0001:
                        Kodlar = ctx.Kodlars.Where(k => k.Kod.Contains(AranacakMetin)).Select(k => k);
                        break;

                    case AramaSecenekleri._0010:
                        Kodlar = ctx.Kodlars.Where(k => k.Baslik.Contains(AranacakMetin)).Select(k => k);
                        break;

                    case AramaSecenekleri._0011:
                        Kodlar = from k in ctx.Kodlars
                                 where k.Baslik.Contains(AranacakMetin) || k.Kod.Contains(AranacakMetin)
                                 select k;
                        break;

                    case AramaSecenekleri._0100:
                        Kodlar = ctx.Kodlars.Where(k => k.AltKategori1.Ad.Contains(AranacakMetin)).Select(k => k);
                        break;

                    case AramaSecenekleri._0101:
                        Kodlar = from k in ctx.Kodlars
                                 where k.AltKategori1.Ad.Contains(AranacakMetin) || k.Kod.Contains(AranacakMetin)
                                 select k;
                        break;

                    case AramaSecenekleri._0110:
                        Kodlar = from k in ctx.Kodlars
                                 where k.AltKategori1.Ad.Contains(AranacakMetin) || k.Baslik.Contains(AranacakMetin)
                                 select k;
                        break;

                    case AramaSecenekleri._0111:
                        Kodlar = from k in ctx.Kodlars
                                 where k.AltKategori1.Ad.Contains(AranacakMetin) || k.Baslik.Contains(AranacakMetin) || k.Kod.Contains(AranacakMetin)
                                 select k;
                        break;

                    case AramaSecenekleri._1000:
                        Kodlar = ctx.Kodlars.Where(k => k.Kategoriler.Ad.Contains(AranacakMetin)).Select(k => k);
                        break;
                    case AramaSecenekleri._1001:
                        Kodlar = from k in ctx.Kodlars
                                 where k.Kategoriler.Ad.Contains(AranacakMetin) || k.Kod.Contains(AranacakMetin)
                                 select k;
                        break;

                    case AramaSecenekleri._1010:
                        Kodlar = from k in ctx.Kodlars
                                 where k.Kategoriler.Ad.Contains(AranacakMetin) || k.Baslik.Contains(AranacakMetin)
                                 select k;
                        break;

                    case AramaSecenekleri._1011:
                        Kodlar = from k in ctx.Kodlars
                                 where k.Kategoriler.Ad.Contains(AranacakMetin) || k.Baslik.Contains(AranacakMetin) || k.Kod.Contains(AranacakMetin)
                                 select k;
                        break;

                    case AramaSecenekleri._1100:
                        Kodlar = from k in ctx.Kodlars
                                 where k.Kategoriler.Ad.Contains(AranacakMetin) || k.AltKategori1.Ad.Contains(AranacakMetin)
                                 select k;
                        break;

                    case AramaSecenekleri._1101:
                        Kodlar = from k in ctx.Kodlars
                                 where k.Kategoriler.Ad.Contains(AranacakMetin) || k.AltKategori1.Ad.Contains(AranacakMetin) || k.Kod.Contains(AranacakMetin)
                                 select k;
                        break;

                    case AramaSecenekleri._1110:
                        Kodlar = from k in ctx.Kodlars
                                 where k.Kategoriler.Ad.Contains(AranacakMetin) || k.AltKategori1.Ad.Contains(AranacakMetin) || k.Baslik.Contains(AranacakMetin)
                                 select k;
                        break;

                    case AramaSecenekleri._1111:
                        Kodlar = from k in ctx.Kodlars
                                 where k.Kategoriler.Ad.Contains(AranacakMetin) || k.AltKategori1.Ad.Contains(AranacakMetin) || k.Baslik.Contains(AranacakMetin) || k.Kod.Contains(AranacakMetin)
                                 select k;
                        break;
                }
            }
            catch (Exception)
            {
                Kodlar = null;
            }

            KayitSayisi = Kodlar == null ? 0 : Kodlar.Count();
            return Kodlar;
        }
    }
}
