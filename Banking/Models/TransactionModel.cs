using System;
using System.ComponentModel.DataAnnotations;

namespace Banking.Models
{
    public class Customer
    {
        [Required(ErrorMessage = "AccountNumber is required.")]

        [Display(Name = "AccountNumber")]
        public long AccNumber
        {
            get; set;

        }
        public int CRN { get; set; }

        public string IBPassword { get; set; }
        [Required(ErrorMessage = "password is required.")]
        [Display(Name = "TransactionPassword")]
        [RegularExpression(@"[a-zA-z0-9]{4,10}", ErrorMessage = "Password format is wrong")]
        [DataType(DataType.Password)]
        public string TxnPassword { get; set; }
        public string BranchName { get; set; }
        public string IFSC { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public long MoblieNumber { get; set; }
        public string EmailID { get; set; }
        public string Status { get; set; }
        public string DOB { get; set; }
        public float Balance { get; set; }
    }


    public class TransactionModel
    {
        public long Sender { get; set; }

        public long Receiver { get; set; }
        public string TransactionId { get; set; }
        public string Date { get; set; }
        public float Amount { get; set; }
    }

    public class Beneficiary
    {
        public long Holder_Account_Number { get; set; }
        public long Payee_Account_Number { get; set; }

        public string Nickname { get; set; }
        public string Branch_Name { get; set; }
        public string IFSC { get; set; }


    }

}
