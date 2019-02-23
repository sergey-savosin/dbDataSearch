using dbDataSearch.BusinessLogic;
using dbDataSearch.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace dbDataSearch.Setup
{
    public partial class FormSetupEntity : Form
    {
        public FormSetupEntity()
        {
            InitializeComponent();
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            string path = Application.StartupPath;
            TSetupEntityCollection col = SetupRepository.LoadSetupConnectionCollection(path);
        }

        private TSetupEntityQuery GetSetupEntityQuery(string entityName, EntityQueryType queryType, IEntityConfig config, string parentEntityName = null)
        {
            TSetupEntityQuery res = new TSetupEntityQuery();
            res.queryType = queryType;
            if (queryType == EntityQueryType.FindByParentKey)
            {
                res.parentEntityName = parentEntityName;
                res.querySql = config.GetEntityQuery(entityName, queryType, parentEntityName);
            }
            else
            {
                res.querySql = config.GetEntityQuery(entityName, queryType);
            }

            return res;
        }

        private TSetupEntityCollection GetSetupEntityCollection(IEntityConfig config)
        {
            TSetupEntityCollection entityCollection = new TSetupEntityCollection();
            entityCollection.entityCollection = new List<TSetupEntity>();

            // City entity
            TSetupEntity entity1 = new TSetupEntity()
            {
                EntityName = "City",
                EntityQueryCollection = new List<TSetupEntityQuery>()
                {
                    GetSetupEntityQuery("City", EntityQueryType.FindByKey, config),
                    GetSetupEntityQuery("City", EntityQueryType.FindByString, config)
                }
            };

            // Order entity
            TSetupEntity entity2 = new TSetupEntity()
            {
                EntityName = "Order",
                EntityQueryCollection = new List<TSetupEntityQuery>()
                {
                    GetSetupEntityQuery("Order", EntityQueryType.FindByKey, config),
                    GetSetupEntityQuery("Order", EntityQueryType.FindByString, config),
                    GetSetupEntityQuery("Order", EntityQueryType.FindByParentKey, config, "City")
                }
            };

            entityCollection.entityCollection.Add(entity1);
            entityCollection.entityCollection.Add(entity2);

            return entityCollection;
        }

        private void m_SaveSetup_Button_Click(object sender, EventArgs e)
        {
            string path = Application.StartupPath;
            IEntityConfig entityConfig = new EntityConfig();
            XmlSerializer xmlser = new XmlSerializer(typeof(TSetupEntityCollection));

            TSetupEntityCollection setupEntityCollection = GetSetupEntityCollection(entityConfig);
            SetupRepository.SaveSetupEntityCollection(path, setupEntityCollection);
        }
    }
}
