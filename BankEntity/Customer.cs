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
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }
        public string BranchCode { get; set; }
        public string TransactionPwd { get; set; }
        public string IbPassword { get; set; }
        [Required(ErrorMessage = "Email is required.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Address is required.")]
        public string Address { get; set; }
        [Required(ErrorMessage = "BirthDate is required.")]
        public string BirthDate { get; set; }
        [Required(ErrorMessage = "Mobile Number is required.")]
        public string MobileNo { get; set; }

       


    }
    

 
 
    
}

