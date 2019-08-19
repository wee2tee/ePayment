using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DbfManager.DbfModel
{
	public class bktrn {
		public string bktrntyp { get; set;}
		public DateTime? trndat { get; set;}
		public string chqnum { get; set;}
		public DateTime? chqdat { get; set;}
		public string bnkcod { get; set;}
		public string branch { get; set;}
		public string cuscod { get; set;}
		public string name { get; set;}
		public string depcod { get; set;}
		public string postgl { get; set;}
		public DateTime? getdat { get; set;}
		public DateTime? payindat { get; set;}
		public double amount { get; set;}
		public double charge { get; set;}
		public double vatamt { get; set;}
		public double netamt { get; set;}
		public double remamt { get; set;}
		public double remcut { get; set;}
		public string cmplapp { get; set;}
		public string chqstat { get; set;}
		public string bnkacc { get; set;}
		public string jnltrntyp { get; set;}
		public string remark { get; set;}
		public string refdoc { get; set;}
		public string refnum { get; set;}
		public DateTime? vatdat { get; set;}
		public DateTime? vatprd { get; set;}
		public string vatlate { get; set;}
		public string vattyp { get; set;}
		public string voucher { get; set;}
		public string userid { get; set;}
		public DateTime? chgdat { get; set;}
		public string authid { get; set;}
		public DateTime? approve { get; set;}
		public string taxid { get; set;}
		public decimal orgnum { get; set;}
	}
}
