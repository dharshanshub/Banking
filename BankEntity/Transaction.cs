using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankEntity
{
    public class Transaction
    {
        public long TransactionId{ get; set; }
        public string Branchcode { get;set; }
        public string TransactionDate { get; set; }
        public Double TransactionAmount { get; set; }
        public string TransactionType { get; set; }
        public string Description { get; set; }
       
    }
}
