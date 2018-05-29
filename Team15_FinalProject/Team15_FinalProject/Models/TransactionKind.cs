using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Team15_FinalProject.Models
{
    public class TransactionKind
    {
        public Int32 TransactionKindID { get; set; }

        public string Name { get; set; }

        /*public TransactionKind()
        {
            new TransactionKind { TransactionKindID = 1, Name = "deposit" };
            new TransactionKind { TransactionKindID = 2, Name = "withdrawal" };
            new TransactionKind { TransactionKindID = 3, Name = "transfer" };
            new TransactionKind { TransactionKindID = 4, Name = "fee" };
        }*/
    }
}