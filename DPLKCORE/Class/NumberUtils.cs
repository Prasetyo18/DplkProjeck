using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace DPLKCORE.Class
{
    public struct NumberUtils {
        /// <summary>
        /// calculate age
        /// </summary>
        /// <param name="dob"></param>
        /// <returns></returns>
        public static int CalculateAge(DateTime dob) {
            int age = DateTime.Now.Year - dob.Year;

            if (DateTime.Today < dob.AddYears(age)) age--;

            return age;
        }

        /// <summary>
        /// Exctract numeric values from string
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static String ExtractNumbers(string input) {
            // Find matches
            //System.Text.RegularExpressions.MatchCollection matches
            //    = System.Text.RegularExpressions.Regex.Matches(input, @"(\d+\.?\d*|\,\d+)", System.Text.RegularExpressions.RegexOptions.Compiled);

            //string[] MatchList = new string[matches.Count];

            //// add each match
            //int c = 0;
            //foreach (System.Text.RegularExpressions.Match match in matches) {
            //    MatchList[c] = match.ToString();
            //    c++;
            //}

            //return String.Concat(MatchList);

            if (!String.IsNullOrEmpty(input)
                && !String.IsNullOrWhiteSpace(input)) {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                char[] list = input.ToCharArray();

                foreach (char c in list) {
                    short number = 0;

                    if (Int16.TryParse(c.ToString(), out number)) {
                        sb.Append(c);
                    } else {
                        if (c == ',' || c == '-') {
                            sb.Append(c);
                        }
                    }
                }

                return sb.ToString();
            } else {
                return "0";
            }
        }

        /// <summary>
        /// translate number to terbilang
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static String Terbilang(long number) {
            // array of string for bilangan
            String[] bilangan = new String[]
            {
                "", "satu", "dua", "tiga", "empat", "lima", "enam", "tujuh", "delapan", "sembilan"
                , "sepuluh", "sebelas"
            };

            String result = "";
            if (number < 12) {
                result = bilangan[number];
            } else if (number < 20) {
                result = Terbilang(number - 10) + " belas ";
            } else if (number < 100) {
                result = Terbilang(number / 10) + " puluh " + Terbilang(number % 10);
            } else if (number < 200) {
                result = " seratus " + Terbilang(number - 100);
            } else if (number < 1000) {
                result = Terbilang(number / 100) + " ratus " + Terbilang(number % 100);
            } else if (number < 2000) {
                result = " seribu" + Terbilang(number - 1000);
            } else if (number < 1000000) {
                result = Terbilang(number / 1000) + " ribu " + Terbilang(number % 1000);
            } else if (number < 1000000000) {
                result = Terbilang(number / 1000000) + " juta " + Terbilang(number % 1000000);
            } else if (number < 1000000000000) {
                result = Terbilang(number / 1000000000) + " milyar " + Terbilang(number % 1000000000);
            } else if (number < 1000000000000000) {
                result = Terbilang(number / 1000000000000) + " trilyun " + Terbilang(number % 1000000000000);
            }

            return result;
        }

        /// <summary>
        /// translate number to terbilang
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        private static String TerbilangKoma(short number) {
            String[] bilangan = new String[]
            {
                "", "satu", "dua", "tiga", "empat", "lima", "enam", "tujuh", "delapan", "sembilan"
                , "sepuluh", "sebelas"
            };


            String result = "";
            if (number < 10) {
                result = "nol " + bilangan[number];
            } else if (number < 12) {
                result = bilangan[number];
            } else if (number < 20) {
                result = Terbilang(number - 10) + " belas";
            } else if (number < 100) {
                result = Terbilang(number / 10) + " puluh " + Terbilang(number % 10);
            }

            return result;
        }

        /// <summary>
        /// translate number to terbilang indonesia
        /// </summary>
        /// <param name="number"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static String Terbilang(Decimal number, char separator) {
            // array of string for bilangan
            String[] bilangan = new String[]
            {
                "nol", "satu", "dua", "tiga", "empat", "lima", "enam", "tujuh", "delapan", "sembilan" 
            };

            String temp = number.ToString();
            String[] split = temp.Split(separator);
            String result = "";
            short koma = 0;

            // if length splitted array length is greater than 1
            if (split.Length > 1) {
                koma = Convert.ToInt16(split[1].Substring(0, 2));

                if (koma > 0) {
                    temp = TerbilangKoma(koma);
                    result = Terbilang(Convert.ToInt64(split[0]))
                        + " koma " + temp;
                } else {
                    result = Terbilang(Convert.ToInt64(split[0]));
                }
            } else {
                result = Terbilang(Convert.ToInt64(split[0]));
            }

            return result;
        }

        /// <summary>
        /// translate number to terbilang indonesia
        /// </summary>
        /// <param name="number"></param>
        /// <param name="separator"></param>
        /// <param name="kdMataUang"></param>
        public static String Terbilang(Decimal number, char separator, String kdMataUang) {
            String result = Terbilang(number, separator);

            if (kdMataUang.Trim().ToUpper() == "USD") {
                result += " dolar amerika";
            } else {
                result += " rupiah";
            }

            return result;
        }

        /// <summary>
        /// translate number to terbilang indonesia with sentence case
        /// </summary>
        /// <param name="number"></param>
        /// <param name="separator"></param>
        /// <param name="kdMataUang"></param>
        public static String TerbilangTitleCase(Decimal number, char separator, String kdMataUang) {
            CultureInfo cultureInfo = CultureInfo.CurrentCulture;
            TextInfo textInfo = cultureInfo.TextInfo;

            // convert string to title case / sentence case
            return textInfo.ToTitleCase(Terbilang(number, separator, kdMataUang));
        }
    }
}
