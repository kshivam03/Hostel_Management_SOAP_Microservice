using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Data;

namespace HM_Service
{
    [ServiceContract]
    public interface IStaffService
    {
        [OperationContract]
        DataSet GetAllStaffs();
        [OperationContract]
        Staff GetStaff(int id);
        [OperationContract]
        int AddStaff(Staff staff);
        [OperationContract]
        int DeleteStaff(int id);
        [OperationContract]
        int UpdateStaff(Staff staff);
        [OperationContract]
        int NoOfStaffs();
        [OperationContract]
        DataSet GetStaffsByFirstName(string firstName);
    }

    [DataContract]
    public class Staff
    {
        private int id;
        private string f_name;
        private string m_name;
        private string l_name;
        private string email;
        private string phone;
        private DateTime date_of_joining;
        private string work_type;
        private DateTime dob;
        private string address;
        private int salary;

        [DataMember]
        public int StaffId
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
        public string WorkType
        {
            get { return work_type; }
            set { work_type = value; }
        }
        [DataMember]
        public string Address
        {
            get { return address; }
            set { address = value; }
        }
        [DataMember]
        public int Salary
        {
            get { return salary; }
            set { salary = value; }
        }
    }
}
