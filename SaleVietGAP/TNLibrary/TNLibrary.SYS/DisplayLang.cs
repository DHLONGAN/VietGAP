using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace TNLibrary.SYS
{
    public static class DisplayLang
    {
        private static DataSet mDataSetLanguage = new DataSet();
        public static void LoadLanguage(string LanguageDisplay)
        {
            string nFileLanguage = "Languages/Language" + LanguageDisplay + ".xml";
            // Read the XML document into the DataSet.
            mDataSetLanguage.ReadXml(nFileLanguage);
        }
        public static string Menu(string MessageID)
        {
            string expression, strReult = ""; ;
            expression = "MessageID = '" + MessageID + "'";
            DataRow[] foundRows;

            // Use the Select method to find all rows matching the filter.
            foundRows = mDataSetLanguage.Tables["MENU"].Select(expression);
            if (foundRows.Length > 0)
                strReult = foundRows[0][1].ToString();

            return strReult;

        }
        public static string frm_ConnectServer(string MessageID)
        {
            string expression, strReult = ""; ;
            expression = "MessageID = '" + MessageID + "'";
            DataRow[] foundRows;

            // Use the Select method to find all rows matching the filter.
            foundRows = mDataSetLanguage.Tables["frm_ConnectServer"].Select(expression);
            if (foundRows.Length > 0)
                strReult = foundRows[0][1].ToString();

            return strReult;

        }
        public static string LanguageMessage(string MessageID)
        {
            string expression, strReult = ""; ;
            expression = "MessageID = '" + MessageID + "'";
            DataRow[] foundRows;

            // Use the Select method to find all rows matching the filter.
            foundRows = mDataSetLanguage.Tables["Message"].Select(expression);
            if (foundRows.Length > 0)
                strReult = foundRows[0][1].ToString();

            return strReult;

        }
    }
}
