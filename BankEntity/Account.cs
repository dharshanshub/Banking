using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankEntity
{
    public  class Account
    {
  
        public int CRN { get; set; }
        [Required(ErrorMessage = "AccountNumber is required.")]

        [Display(Name = "AccountNumber")]
        public long AccNo { get; set; }
        public string BranchCode { get; set; }
        public string OpenDate { get; set; }
        public string Status { get; set; }
        public double Balance { get; set; }


   




    }
}
