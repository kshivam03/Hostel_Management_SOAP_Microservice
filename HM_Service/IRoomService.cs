using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace HM_Service
{
    [ServiceContract]
    public interface IRoomService
    {
        [OperationContract]
        DataSet GetAllRooms();
        [OperationContract]
        Room GetRoom(int room_id);
        [OperationContract]
        int AddRoom(Room room);
        [OperationContract]
        int UpdateRoom(Room room);
        [OperationContract]
        int DeleteRoom(int room_id);
        [OperationContract]
        List<int> GetVacantRoomIds();
        [OperationContract]
        List<int> GetVacantRoomIdsWithAC();
        [OperationContract]
        List<int> GetVacantRoomIdsWithNonAC();
        [OperationContract]
        int NoOfRooms();
        [OperationContract]
        int GetTotalVacantRooms();
        [OperationContract]
        List<string> GetStudentsInRoom(int room_id);

    }
    [DataContract]
    public class Room
    {
        private int room_id;
        private string room_type;
        private int bed_vacancy;

        [DataMember]
        public int RoomId
        {
            get { return room_id; }
            set { room_id = value; }
        }

        [DataMember]
        public string RoomType
        {
            get { return room_type; }
            set { room_type = value; }
        }
        [DataMember]
        public int BedVacancy
        {
            get { return bed_vacancy; }
            set { bed_vacancy = value; }
        }
    }

}
