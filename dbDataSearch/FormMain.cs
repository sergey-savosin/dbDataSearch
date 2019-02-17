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

namespace dbDataSearch
{
    public partial class FormMain : ComponentFactory.Krypton.Toolkit.KryptonForm
    {
        //IRepository rep;
        IConnectionRepository connectionRepository;
        IEntityRepository entityRepository;

        public FormMain()
        {
            InitializeComponent();
        }

        private void btnLightScheme_Click(object sender, EventArgs e)
        {
            treeEntities.Nodes.Clear();

            kryptonManagerMain.GlobalPalette = kryptonPalette_Office2010_Blue;
            treeEntities.StateCommon.Back.Color1 = Color.Empty;
            treeEntities.StateCommon.Node.Content.ShortText.Color1 = Color.Black;
            treeEntities.StateCommon.Node.Back.Color1 = Color.PaleGreen;

            kryptonHeaderGroupMain.Text = "Light scheme";
            kryptonHeaderGroupMain.StateCommon.HeaderPrimary.Back.Color1 = Color.LightGreen;
            kryptonHeaderGroupMain.StateCommon.HeaderPrimary.Back.Color2 = Color.Green;
            kryptonHeaderGroupMain.StateCommon.HeaderPrimary.Border.Color1 = Color.Empty;
            kryptonHeaderGroupMain.StateCommon.HeaderPrimary.Content.ShortText.Color1 = Color.Black;
        }

        private void btnDarkScheme_Click(object sender, EventArgs e)
        {
            treeEntities.Nodes.Clear();

            kryptonManagerMain.GlobalPalette = kryptonPalette_Office2010_Black;
            treeEntities.StateCommon.Back.Color1 = Color.DimGray;
            treeEntities.StateCommon.Node.Content.ShortText.Color1 = Color.White;
            treeEntities.StateCommon.Node.Content.ShortText.Color2 = Color.White;
            treeEntities.StateCommon.Node.Back.Color1 = Color.DarkGreen;
            treeEntities.StateCommon.Node.Back.Color2 = Color.DarkGreen;
            treeEntities.StateCommon.Node.Back.ColorStyle = ComponentFactory.Krypton.Toolkit.PaletteColorStyle.GlassSimpleFull;

            kryptonHeaderGroupMain.Text = "Dark scheme";
            kryptonHeaderGroupMain.StateCommon.HeaderPrimary.Back.Color1 = Color.LightBlue;
            kryptonHeaderGroupMain.StateCommon.HeaderPrimary.Back.Color2 = Color.Blue;
            kryptonHeaderGroupMain.StateCommon.HeaderPrimary.Border.Color1 = Color.Red;
            kryptonHeaderGroupMain.StateCommon.HeaderPrimary.Content.ShortText.Color1 = Color.White;

        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            if (connectionRepository == null)
            {
                connectionRepository = new ConnectionRepository();
            }

            if (entityRepository == null)
            {
                entityRepository = new EntityRepository(GetConnectionDetails());
            }

            List<string> cons = connectionRepository.GetAllConnectionNames();
            comboboxConnection.ComboBox.DataSource = cons;
        }

        private void btnFindEntity_Click(object sender, EventArgs e)
        {
            FindEntityByTextBoxString();
        }

        private void treeEntities_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TTreeNodeData nodeData = e.Node.Tag as TTreeNodeData;
            string entityName = nodeData.EntityName;
            long entityKey = nodeData.EntityKey;
            DataTable data = entityRepository.GetEntityDetailsByKey(entityName, entityKey);
            gridEntityValues.DataSource = data;
        }

        private void textboxSearchString_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                FindEntityByTextBoxString();
            }
        }

        #region utils
        private TConnectionDetails GetConnectionDetails()
        {
            string connName = comboboxConnection.Text;
            return connectionRepository.GetConnectionDetails(connName);
        }

        private void FindEntityByTextBoxString()
        {
            treeEntities.Nodes.Clear();
            string strFind = textboxSearchString.Text;

            List<TSearchEntityResult> findResult = entityRepository.SearchEntitiesByString(strFind);
            foreach (var elt in findResult)
            {
                string strValue = $"{elt.FoundColumn} = {elt.StrValue} [{elt.Key}]";
                TreeNode node = new TreeNode(strValue);
                TTreeNodeData nodeData = new TTreeNodeData()
                {
                    EntityName = elt.EntityName,
                    EntityKey = elt.Key,
                    Details = elt.FoundColumn
                };

                node.Tag = nodeData;
                node.Nodes.Add("to do");
                treeEntities.Nodes.Add(node);
            }
        }

        #endregion

        private void treeEntities_AfterExpand(object sender, TreeViewEventArgs e)
        {
            var nodeData = e.Node.Tag as TTreeNodeData;

            var searchResult = entityRepository.SearchChildEntitiesByParentKey(nodeData.EntityName, nodeData.EntityKey);

            // Заполнение открываемого уровня
            e.Node.Nodes.Clear();
            foreach (var elt in searchResult)
            {
                string strValue = $"{elt.FoundColumn} = {elt.Key}";
                TreeNode newNode = new TreeNode(strValue);
                TTreeNodeData newNodeData = new TTreeNodeData()
                {
                    EntityName = elt.EntityName,
                    EntityKey = elt.Key,
                    Details = elt.FoundColumn
                };

                newNode.Tag = newNodeData;
                newNode.Nodes.Add("to do");
                e.Node.Nodes.Add(newNode);

            }
        }
    }
}
