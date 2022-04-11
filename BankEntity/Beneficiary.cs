using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankEntity
{
    public class Beneficiary
    {
        [Key]
        public int BId { get; set; }
        [Display(Name ="Enter your Account Number")]
        public long SenderAccNo { get; set; }
        public long ReceiverAccNo { get; set; }
        public string NickName { get; set; }
        public string BranchName { get; set; }
        public string IFSC { get; set; }

    }
}
