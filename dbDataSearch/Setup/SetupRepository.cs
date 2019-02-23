using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace dbDataSearch.Setup
{
    public static class SetupRepository
    {
        //корневой путь к настройкам в реестре
        const string RegistryRoot = @"HKEY_CURRENT_USER\Software\LK\DBDataSearch";
        const string OcsBranch = @"\DBSettings";
        const string SettingsFileName = "entity_config.xml";

        public static TSetupEntityCollection LoadSetupConnectionCollection(string path)
        {
            Debug.Assert(!string.IsNullOrWhiteSpace(path));
            string fullFileName = Path.Combine(path, SettingsFileName);

            // Создание пустого файла настроек
            if (!File.Exists(fullFileName))
            {
                TSetupEntityCollection colTmp = new TSetupEntityCollection();
                SaveSetupEntityCollection(path, colTmp);
            }

            XmlSerializer xmlser = new XmlSerializer(typeof(TSetupEntityCollection));
            StreamReader sr = new StreamReader(fullFileName);
            TSetupEntityCollection col = (TSetupEntityCollection)xmlser.Deserialize(sr);
            sr.Close();

            return col;
        }

        public static void SaveSetupEntityCollection(string path, TSetupEntityCollection col)
        {
            Debug.Assert(!string.IsNullOrWhiteSpace(path));
            string fullFileName = Path.Combine(path, SettingsFileName);

            XmlSerializer xmlser = new XmlSerializer(typeof(TSetupEntityCollection));
            StreamWriter sw = new StreamWriter(fullFileName);
            xmlser.Serialize(sw, col);
            sw.Close();
        }


    }
}
