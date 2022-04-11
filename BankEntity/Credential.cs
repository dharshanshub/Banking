using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankEntity
{
    public class Credential
    {
        public int CRN { get; set; }
        public long AccNo { get; set; }
     
        [Display(Name ="InternetBanking Password")]
        public string IbPassword { get; set; }

        public string BranchCode { get; set; }
    }
}
