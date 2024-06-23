using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Threading.Tasks;
using System.Data;

namespace HM_Service
{

    [ServiceContract]
    public interface IFeesService
    {
        [OperationContract]
        DataSet GetAllFees();
        [OperationContract]
        DataSet StudentAllFees(int s_id);
        [OperationContract]
        int AddFees(Fees fee);


    }
    [DataContract]
    public class Fees
    {
        private int fee_id;
        private int stu_id;
        private int paid_amount;
        private DateTime payment_date;

        [DataMember]
        public int FeesId
        {
            get { return fee_id; }
            set { fee_id = value; }
        }
        [DataMember]
        public int StudentId
        {
            get { return stu_id; }
            set { stu_id = value; }
        }
        [DataMember]
        public int PaidAmount
        {
            get { return paid_amount; }
            set { paid_amount = value; }
        }
        [DataMember]
        public DateTime PaymentDate
        {
            get { return payment_date; }
            set { payment_date = value; }
        }
    }
}
