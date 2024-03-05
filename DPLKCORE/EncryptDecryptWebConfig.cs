using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using DPLKCORE.Framework;
using System.Web.Configuration;



namespace DPLKCORE
{
    public class EncryptDecryptWebConfig
    {
        public static void EncryptConnString()
        {
            Configuration config = WebConfigurationManager.OpenWebConfiguration("~");
            ConfigurationSection section = config.GetSection("connectionStrings");

            if (!section.SectionInformation.IsProtected)
            {
                section.SectionInformation.ProtectSection("RsaProtectedConfigurationProvider");
                config.Save();
            }
        }

        public static void DecryptConnString()
        {
            Configuration config = WebConfigurationManager.OpenWebConfiguration("~");
            ConfigurationSection section = config.GetSection("connectionStrings");
            if (section.SectionInformation.IsProtected)
            {
                section.SectionInformation.UnprotectSection();
                config.Save();
            }
        }

        public static void DecryptConnStringSHA()
        {
            Configuration config = WebConfigurationManager.OpenWebConfiguration("~");
            ConfigurationSection section = config.GetSection("connectionStrings");
            String a = ConfigurationManager.ConnectionStrings["askumconnection"].ToString();
            //section.SectionInformation.SectionName
        }



        //private void ProtectSection(string sectionName,
        //                    string provider)
        //{
        //    Configuration config = WebConfigurationManager.OpenWebConfiguration(Request.ApplicationPath);

        //    ConfigurationSection section =
        //                 config.GetSection(sectionName);

        //    if (section != null &&
        //              !section.SectionInformation.IsProtected)
        //    {
        //        section.SectionInformation.ProtectSection(provider);
        //        config.Save();
        //    }
        //}

        //private void UnProtectSection(string sectionName)
        //{
        //    Configuration config =
        //        WebConfigurationManager.
        //            OpenWebConfiguration(Request.ApplicationPath);

        //    ConfigurationSection section =
        //              config.GetSection(sectionName);

        //    if (section != null &&
        //          section.SectionInformation.IsProtected)
        //    {
        //        section.SectionInformation.UnprotectSection();
        //        config.Save();
        //    }
        //}

    }
}