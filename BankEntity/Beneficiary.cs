using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankEntity
{
    public class Beneficiary
    {
        public long SenderAccNo { get; set; }
        public long ReceiverAccNo { get; set; }
        public string NickName { get; set; }
        public string Branchcode { get; set; }

    }
}
