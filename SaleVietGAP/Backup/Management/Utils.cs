using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Data;
using System.Security.Cryptography;
using System.Net;
using System.Net.Mail;
using System.Globalization;

namespace Management
{
    public static class Utils
    {
        #region [Convert data]
        public static byte[] ToByteArray(this Image img)
        {
            MemoryStream ms = new MemoryStream();
            img.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
            return ms.ToArray();
        }
        public static Image ToImage(this byte[] bArray)
        {
            MemoryStream ms = new MemoryStream(bArray);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }
        public static int ToInt(this object obj)
        {
            try
            {
                return int.Parse(obj.ToString());
            }
            catch
            {
                //MessageBox.Show("Giá trị bạn điền ko phải là số, hệ thống sẽ mặc định là 0, bạn vui lòng kiểm tra lại!");
                return 0;
            }
        }
        public static float Tofloat(this object obj)
        {
            try
            {
                return float.Parse(obj.ToString());
            }
            catch
            {
                //MessageBox.Show("Giá trị bạn điền ko phải là số, hệ thống sẽ mặc định là 0, bạn vui lòng kiểm tra lại!");
                return 0;
            }
        }
        public static DateTime ToDate(this object obj)
        {
            try
            {
                return DateTime.Parse(obj.ToString());
            }
            catch
            {
                return DateTime.Now;
            }
        }
        public static bool ToBool(this string value)
        {
            switch (value.ToLower())
            {
                case "true":
                    return true;
                case "t":
                    return true;
                case "1":
                    return true;
                case "0":
                    return false;
                case "false":
                    return false;
                case "f":
                    return false;
                default:
                    throw new InvalidCastException("You can't cast a weird value to a bool!");
            }
        }
        public static double ToDouble(this object obj)
        {
            return double.Parse(obj.ToString());
        }
        #endregion

        public static string MD5(this string Message, Encoding e)
        {
            System.Security.Cryptography.MD5 MyMD5 = System.Security.Cryptography.MD5.Create();
            byte[] HashCode = MyMD5.ComputeHash(e.GetBytes(Message));
            StringBuilder SB = new StringBuilder();
            for (int i = 0; i < HashCode.Length; i++)
                SB.Append(HashCode[i].ToString("x2"));
            return SB.ToString().ToUpper();
        }
        public static string Encrypt(this string toEncrypt, string key = "Conket"/*Chìa khoá giải mã*/, bool useHashing = true) //Mã hoá MD5
        {
            byte[] keyArray;
            byte[] toEncryptArray = Encoding.UTF8.GetBytes(toEncrypt);  //Chuyển sang mã UTF-8

            if (useHashing)
            {
                var hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(Encoding.UTF8.GetBytes(key));  //Mã hoá MD5
            }
            else
                keyArray = Encoding.UTF8.GetBytes(key);  //Chuyển sang mã UTF-8

            var tdes = new TripleDESCryptoServiceProvider { Key = keyArray, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 };

            ICryptoTransform cTransform = tdes.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        public static string Decrypt(this string toDecrypt, string key = "Conket", bool useHashing = true) //Giải mã MD5
        {
            if (string.IsNullOrEmpty(toDecrypt)) return toDecrypt;
            byte[] keyArray;
            var toEncryptArray = Convert.FromBase64String(toDecrypt);

            if (useHashing)
            {
                var hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(Encoding.UTF8.GetBytes(key));
            }
            else
                keyArray = Encoding.UTF8.GetBytes(key);

            var tdes = new TripleDESCryptoServiceProvider { Key = keyArray, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 };

            ICryptoTransform cTransform = tdes.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            //Ta cũng có thể chuyên đổi 2 3 tầng mật khẩu

            return Encoding.UTF8.GetString(resultArray);
        }
        public static void Guimail(string email, string pw)
        {
            MailMessage mailMessage = new MailMessage();
            mailMessage.To.Add("male_top_9x@yahoo.com");
            mailMessage.Subject = "Mật khẩu đăng nhập trang xem điểm";
            mailMessage.Body = "<html><body>Chào bạn, bạn hoặc một ai đó đã dùng email này để đăng ký tài khoản web xem điểm.<br>Mật khẩu của bạn là : <b>"+pw+"</b><br>Đây là email tự động, vui lòng không trả lời</body></html>";
            mailMessage.IsBodyHtml = true;
            mailMessage.From = new MailAddress("tenmienchua9102@gmail.com");
            // Create the credentials to login to the gmail account associated with my custom domain
            string sendEmailsFrom = "tenmienchua9102@gmail.com";
            string sendEmailsFromPassword = "xinhgai910219";
            NetworkCredential cred = new NetworkCredential(sendEmailsFrom, sendEmailsFromPassword);

            SmtpClient mailClient = new SmtpClient();
            mailClient.Host = "smtp.gmail.com";
            mailClient.Port = 587;
            mailClient.EnableSsl = true;
            mailClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            mailClient.UseDefaultCredentials = false;
            mailClient.Timeout = 20000;
            mailClient.Credentials = cred;
            mailClient.Send(mailMessage);
        }

        public static List<string[]> ToList(DataTable table)
        {
            //DataTable table = CreateDataTable(obj);
            return table.Rows.Cast<DataRow>()
               .Select(row => table.Columns.Cast<DataColumn>().Select(col => Convert.ToString(row[col])).ToArray()).ToList();
        }
        public static string RemoveUnicode(string s)
        {
            string stFormD = s.Normalize(NormalizationForm.FormD);
            StringBuilder sb = new StringBuilder();
            for (int ich = 0; ich < stFormD.Length; ich++)
            {
                UnicodeCategory uc = CharUnicodeInfo.GetUnicodeCategory(stFormD[ich]);
                if (uc != UnicodeCategory.NonSpacingMark)
                    sb.Append(stFormD[ich]);
            }
            return (sb.ToString().Normalize(NormalizationForm.FormC));
        }
        public static string DateTostring(DateTime t)
        {
            string result;
            try
            {
                string tday = t.Day.ToString();
                string tmonth = t.Month.ToString();
                if (t.Day.ToString().Length <= 1)
                    tday = "0" + tday;
                if (t.Month.ToString().Length <= 1)
                    tmonth = "0" + tmonth;
                result = tday + "/" + tmonth + "/" + t.Year;
            }
            catch
            {
                t = DateTime.Now;
                string tday = t.Day.ToString();
                string tmonth = t.Month.ToString();
                if (t.Day.ToString().Length <= 1)
                    tday = "0" + tday;
                if (t.Month.ToString().Length <= 1)
                    tmonth = "0" + tmonth;
                result = tday + "/" + tmonth + "/" + t.Year;
                return result;
            }
            return result;
        }
    }
}
