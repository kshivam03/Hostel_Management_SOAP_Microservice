using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HM_Cient
{
    public partial class FeeList : Form
    {
        public FeeList()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           

        }

        private void FeeList_Load(object sender, EventArgs e)
        {
            HM_Cient.FeesServiceReference.FeesServiceClient proxy = new HM_Cient.FeesServiceReference.FeesServiceClient("NetTcpBinding_IFeesService");
            DataSet ds = proxy.GetAllFees();
            dataGridView1.DataSource = ds.Tables["Fees"];
        }
    }
}
