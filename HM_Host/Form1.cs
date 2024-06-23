using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ServiceModel;
using HM_Service;
using System.ServiceModel.Description;

namespace HM_Host
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        ServiceHost sh = null;
        ServiceMetadataBehavior metadataBehavior = new ServiceMetadataBehavior();

        private void Form1_Load(object sender, EventArgs e)
        {

            try
            {
                Uri tcpa = new Uri("net.tcp://localhost:8000/TcpBinding");
                sh = new ServiceHost(typeof(StudentService), tcpa);

                // Add StudentService endpoints
                NetTcpBinding tcpb = new NetTcpBinding();
                sh.Description.Behaviors.Add(metadataBehavior);
                sh.AddServiceEndpoint(typeof(IMetadataExchange),
                    MetadataExchangeBindings.CreateMexTcpBinding(), "mex");
                sh.AddServiceEndpoint(typeof(IStudentService), tcpb, tcpa);

                // Add StaffService endpoints
                Uri staffTcpUri = new Uri("net.tcp://localhost:8001/StaffTcpBinding");
                ServiceHost staffHost = new ServiceHost(typeof(StaffService), staffTcpUri);
                staffHost.Description.Behaviors.Add(metadataBehavior);
                staffHost.AddServiceEndpoint(typeof(IMetadataExchange),
                    MetadataExchangeBindings.CreateMexTcpBinding(), "mex");
                staffHost.AddServiceEndpoint(typeof(IStaffService), tcpb, staffTcpUri);

                // Add RoomService endpoints
                Uri roomTcpUri = new Uri("net.tcp://localhost:8002/RoomTcpBinding");
                ServiceHost roomHost = new ServiceHost(typeof(RoomService), roomTcpUri);
                roomHost.Description.Behaviors.Add(metadataBehavior);
                roomHost.AddServiceEndpoint(typeof(IMetadataExchange),
                    MetadataExchangeBindings.CreateMexTcpBinding(), "mex");
                roomHost.AddServiceEndpoint(typeof(IRoomService), tcpb, roomTcpUri);

                // Add FeesService endpoints
                Uri feesTcpUri = new Uri("net.tcp://localhost:8003/FeesTcpBinding");
                ServiceHost feesHost = new ServiceHost(typeof(FeesService), feesTcpUri);
                feesHost.Description.Behaviors.Add(metadataBehavior);
                feesHost.AddServiceEndpoint(typeof(IMetadataExchange),
                    MetadataExchangeBindings.CreateMexTcpBinding(), "mex");
                feesHost.AddServiceEndpoint(typeof(IFeesService), tcpb, feesTcpUri);

                // Open all service hosts
                sh.Open();
                staffHost.Open();
                roomHost.Open();
                feesHost.Open();

                label1.Text = "Server Running";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }


    }
}
