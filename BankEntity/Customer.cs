using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankEntity
{
    public class Customer
    {
        [Display(Name = "Enter CRN Number to update")]
        public int CRN { get; set; }
        public string Name { get; set; }
        public string BranchCode { get; set; }
        public string TransactionPwd { get; set; }
        public string IbPassword { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
       
        public string BirthDate { get; set; }
        public string MobileNo { get; set; }

       


    }
    

 
 
    
}

