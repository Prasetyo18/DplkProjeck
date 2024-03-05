using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DPLKCORE.Class
{
    public class Utility
    {
        public static int GetAge(DateTime tglLahir, DateTime tglMulai)
        {
            int umur = 0;
            int tahun = 0, bulan = 0;

            tahun = tglMulai.Year - tglLahir.Year;

            if (tglMulai.Month < tglLahir.Month)
            {
                tahun = tahun - 1;
                bulan = 12 - (tglLahir.Month - tglMulai.Month);
            }
            else
            {
                bulan = tglMulai.Month - tglLahir.Month;
            }

            if (tglMulai.Day < tglLahir.Day)
            {
                if (bulan == 0)
                {
                    bulan = 12;
                    tahun -= 1;
                }
                bulan -= 1;
            }

            if (bulan >= 6)
                umur = tahun + 1;
            else
                umur = tahun;

            return umur;
        }



        //public int GetMasaAsuransi(DateTime tglLahir, DateTime tglMulaiAsuransi)
        //{
        //    int umur = 0;
        //    int tahun = 0, bulan = 0;

        //    tahun = tglMulaiAsuransi.Year - tglLahir.Year;

        //    if (tglMulaiAsuransi.Month < tglLahir.Month)
        //    {
        //        tahun = tahun - 1;
        //        //bulan = 12 - (tglLahir.Month - tglMulaiAsuransi.Month);
        //    }

        //    else if (tglMulaiAsuransi.Month == tglLahir.Month)
        //    {
        //        if (tglMulaiAsuransi.Day < tglLahir.Day)
        //        {
        //            tahun = tahun - 1;
        //        }

        //    }

        //    umur = tahun;

        //    return umur;
        //}

        public static int GetBulan(DateTime tglMulai, DateTime tglMutasi)
        {
            int umur = 0;
            int tahun = 0, bulan = 0;
            int day = 0;

            tahun = (tglMutasi.Year - tglMulai.Year) * 12;

            if (tglMutasi.Month < tglMulai.Month)
            {
                tahun = tahun - 12;
                bulan = 12 - (tglMulai.Month - tglMutasi.Month);
            }

            else if (tglMulai.Month < tglMutasi.Month)
            {
                bulan = tglMutasi.Month - tglMulai.Month;
            }

            day = tglMutasi.Day - tglMulai.Day;

            if (day >= 0)
                bulan = bulan + 1;

            bulan = bulan + tahun;

            umur = bulan;

            return umur;
        }


    }
}