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
        TSetupEntityCollection m_SetupEntityCollection;

        public FormSetupEntity()
        {
            InitializeComponent();

            m_EntityBindingSource.DataSource = typeof(TSetupEntityCollection);
            m_EntityBindingSource.DataMember = "EntityCollection";
            m_EntityBindingSource.AddNew();

            m_EntityName_ListBox.DataSource = m_EntityBindingSource;
            m_EntityName_ListBox.DisplayMember = "EntityName";

            m_EntityComment_TextBox.DataBindings.Add("Text", m_EntityBindingSource, "EntityName");
        }

        private TSetupEntityQuery GetSetupEntityQuery(string entityName, EntityQueryType queryType, IEntityConfig config, string parentEntityName = null)
        {
            TSetupEntityQuery res = new TSetupEntityQuery();
            res.QueryType = queryType;
            if (queryType == EntityQueryType.FindByParentKey)
            {
                res.ParentEntityName = parentEntityName;
                res.QuerySql = config.GetEntityQuery(entityName, queryType, parentEntityName);
            }
            else
            {
                res.QuerySql = config.GetEntityQuery(entityName, queryType);
            }

            return res;
        }

        private TSetupEntityCollection GetSetupEntityCollection(IEntityConfig config)
        {
            TSetupEntityCollection entityCollection = new TSetupEntityCollection();
            entityCollection.EntityCollection = new List<TSetupEntity>();

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

            entityCollection.EntityCollection.Add(entity1);
            entityCollection.EntityCollection.Add(entity2);

            return entityCollection;
        }

        private void SaveSetup()
        {
            string path = Application.StartupPath;
            IEntityConfig entityConfig = new EntityConfig();
            XmlSerializer xmlser = new XmlSerializer(typeof(TSetupEntityCollection));

            TSetupEntityCollection setupEntityCollection = GetSetupEntityCollection(entityConfig);
            SetupRepository.SaveSetupEntityCollection(path, setupEntityCollection);
        }

        private void FormSetupEntity_Load(object sender, EventArgs e)
        {
            SaveSetup();

            string path = Application.StartupPath;
            m_SetupEntityCollection = SetupRepository.LoadSetupConnectionCollection(path);

            m_EntityBindingSource.DataSource = m_SetupEntityCollection;

        }

        private void FormSetupEntity_FormClosing(object sender, FormClosingEventArgs e)
        {
            m_EntityBindingSource.EndEdit();
            DialogResult = DialogResult.Yes;
        }
    }
}
