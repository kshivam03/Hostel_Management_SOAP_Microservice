using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace HM_Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IStudentService
    {
        [OperationContract]
        DataSet GetAllStudents();

        [OperationContract]
        Student GetStudent(int id);
        [OperationContract]
        int AddStudent(Student student);
        [OperationContract]
        int UpdateStudent(Student student);
        [OperationContract]
        int DeleteStudent(int id);
        [OperationContract]
        int PendingFees(int id);
        [OperationContract]
        int NoOfStudents();
        [OperationContract]
        int CountStudentsWithPendingFees();
        [OperationContract]
        DataSet GetStudentsByFirstName(string firstName);
    }

    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    // You can add XSD files into the project. After building the project, you can directly use the data types defined there, with the namespace "HM_Service.ContractType".
    [DataContract]
    public class Student
    {
        private int id;
        private string f_name;
        private string m_name;
        private string l_name;
        private string email;
        private string phone;
        private DateTime dob;
        private DateTime date_of_joining;
        private DateTime date_of_leaving;
        private int room_id;
        private string address;

        [DataMember]
        public int StudentId
        {
            get { return id; }
            set { id = value; }
        }

        [DataMember]
        public string FirstName
        {
            get { return f_name; }
            set { f_name = value; }
        }
        [DataMember]
        public string LastName
        {
            get { return l_name; }
            set { l_name = value; }
        }
        [DataMember]
        public string MiddleName
        {
            get { return m_name; }
            set { m_name = value; }
        }
        [DataMember]
        public string Email
        {
            get { return email; }
            set { email = value; }
        }
        [DataMember]
        public string PhoneNo
        {
            get { return phone; }
            set { phone = value; }
        }
        [DataMember]
        public DateTime DateOfBirth
        {
            get { return dob; }
            set { dob = value; }
        }
        [DataMember]
        public DateTime DateOfJoining
        {
            get { return date_of_joining; }
            set { date_of_joining = value; }
        }

        [DataMember]
        public int RoomID
        {
            get { return room_id; }
            set { room_id = value; }
        }
        [DataMember]
        public string Address
        {
            get { return address; }
            set { address = value; }
        }
    }
}
