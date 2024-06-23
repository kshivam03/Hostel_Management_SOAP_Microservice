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
    public partial class RoomList : Form
    {
        public RoomList()
        {
            InitializeComponent();
        }

        private void RoomList_Load(object sender, EventArgs e)
        {
            HM_Cient.RoomServiceReference.RoomServiceClient proxy = new HM_Cient.RoomServiceReference.RoomServiceClient("NetTcpBinding_IRoomService");
            int n=proxy.NoOfRooms();
            CreatePanels(n);
        }
        private void CreatePanels(int numberOfRooms)
        {
            HM_Cient.RoomServiceReference.RoomServiceClient proxy = new HM_Cient.RoomServiceReference.RoomServiceClient("NetTcpBinding_IRoomService");
            DataSet roomsDataSet = proxy.GetAllRooms();
            DataTable roomsTable = roomsDataSet.Tables["Room"];

            int buttonsPerRow = 5;
            int buttonWidth = 107;
            int buttonHeight = 107;
            int buttonSpacing = 10;

            int roomIndex = 0;

            for (int i = 0; i < roomsTable.Rows.Count; i++)
            {
                DataRow roomRow = roomsTable.Rows[i];

                // Create a new button
                Button button = new Button();
                button.Name = "button" + roomRow["room_id"]; // Set the room_id as the button's name
                button.Size = new System.Drawing.Size(buttonWidth, buttonHeight);
                button.BackColor = Color.SteelBlue;
                button.ForeColor = Color.White;
                button.Text = roomRow["room_id"].ToString();
                // Calculate the x-coordinate for the button based on its index and row
                int row = roomIndex / buttonsPerRow;
                int col = roomIndex % buttonsPerRow;
                int buttonX = col * (buttonWidth + buttonSpacing);
                int buttonY = row * (buttonHeight + buttonSpacing);

                // Set the location of the button
                button.Location = new System.Drawing.Point(buttonX, buttonY);

                // Add the button to the form
                this.Controls.Add(button);

                // Attach the Click event handler
                button.Click += (sender, e) =>
                {
                    // Extract the room_id from the button's name
                    int roomId;
                    if (int.TryParse(button.Name.Replace("button", ""), out roomId))
                    {
                        // Call the method to get the list of students for the room
                        string[] studentsInRoomArray = proxy.GetStudentsInRoom(roomId);
                        List<string> studentsInRoom = new List<string>(studentsInRoomArray);

                        // Show the list of students in a panel
                        ShowStudentsPanel(studentsInRoom);
                    }
                };

                roomIndex++;
            }
        }

        private void ShowStudentsPanel(List<string> students)
        {
            // Create a panel to display the list of students
            Panel studentPanel = new Panel();
            studentPanel.Size = new System.Drawing.Size(300, 200);
            studentPanel.BackColor = Color.White;

            // Create a label to display the list of students
            Label studentLabel = new Label();
            studentLabel.Text = string.Join("\n", students);
            studentLabel.Dock = DockStyle.Fill;

            // Add the label to the panel
            studentPanel.Controls.Add(studentLabel);

            // Show the panel in a new form or a MessageBox, depending on your preference
            Form studentForm = new Form();
            studentForm.Controls.Add(studentPanel);
            studentForm.ShowDialog();
        }

    }
}
