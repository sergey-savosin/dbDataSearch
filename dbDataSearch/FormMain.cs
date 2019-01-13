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
        }

        private void btnDarkScheme_Click(object sender, EventArgs e)
        {
            kryptonManagerMain.GlobalPalette = kryptonPalette_Office2010_Black;
            treeEntities.StateCommon.Back.Color1 = Color.DimGray;
            treeEntities.StateCommon.Node.Content.ShortText.Color1 = Color.White;
            treeEntities.StateCommon.Node.Back.Color1 = Color.RoyalBlue;
        }
    }
}
