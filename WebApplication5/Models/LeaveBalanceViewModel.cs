using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace WebApplication5.Models
{
    public class LeaveBalanceViewModel
    {
        public int? AnnualBalance { get; set; }
        public int? SickBalance { get; set; }
        public int? SuddenBalance { get; set; }

    }
}
