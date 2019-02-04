using dbDataSearch.Contracts;
using dbDataSearch.Repository;
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
        IRepository rep;

        public FormMain()
        {
            InitializeComponent();
        }

        private void btnLightScheme_Click(object sender, EventArgs e)
        {
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
            if (rep == null)
            {
                rep = new SqlServerRepository();
            }

            List<string> cons = rep.GetAllConnectionNames();
            comboboxConnection.ComboBox.DataSource = cons;
        }

        private void btnFindEntity_Click(object sender, EventArgs e)
        {
            treeEntities.Nodes.Clear();
            string strFind = textboxSearchString.Text;

            List<IEntity> lst = rep.GetAllRootEntities();
            foreach(var entity in lst)
            {
                List<FindAllResult> findResult = entity.FindByString(strFind);
                foreach(var elt in findResult)
                {
                    string strValue = $"[{elt.EntityName}] {elt.StrValue} [{elt.Id}]";
                    TreeNode node = new TreeNode(strValue);
                    node.Tag = elt;
                    treeEntities.Nodes.Add(node);
                }
            }
        }

        private void treeEntities_AfterSelect(object sender, TreeViewEventArgs e)
        {
            FindAllResult elt = e.Node.Tag as FindAllResult;
            DataTable data = rep.GetEntityDetails(elt.EntityName, elt.Id);
            gridEntityValues.DataSource = data;
        }
    }
}
