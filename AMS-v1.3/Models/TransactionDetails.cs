//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AMS_v1._3.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class TransactionDetails
    {
        public int transactionID { get; set; }
        public string username { get; set; }
        public string passwd { get; set; }
        public string confirmpasswd { get; set; }
        public string firstname { get; set; }
        public string middlename { get; set; }
        public string lastname { get; set; }
        public string addrss { get; set; }
        public int balance { get; set; }
        public Nullable<System.DateTime> date_created { get; set; }
        public Nullable<int> phonenum { get; set; }
    }
}
