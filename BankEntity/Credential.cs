using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankEntity
{
    public class Credential
    {
        public int CRN { get; set; }
        public long AccNo { get; set; }
        public string TransactionPwd { get; set; }
        public string IbPassword { get; set; }

        public string BranchCode { get; set; }
    }
}
