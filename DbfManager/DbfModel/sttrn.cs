using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DbfManager.DbfModel
{
	public class sttrn {
		public string docnum { get; set;}
		public DateTime? docdat { get; set;}
		public string posopr { get; set;}
		public string people { get; set;}
		public string remark { get; set;}
		public string loccod { get; set;}
		public double trnval { get; set;}
		public string refnum { get; set;}
		public DateTime? refdat { get; set;}
		public string nxtseq { get; set;}
		public string accnumdr { get; set;}
		public string accnumcr { get; set;}
		public string depcod { get; set;}
		public string postgl { get; set;}
		public string docstat { get; set;}
		public string userid { get; set;}
		public DateTime? chgdat { get; set;}
		public string userprn { get; set;}
		public DateTime? prndat { get; set;}
		public decimal prncnt { get; set;}
		public string prntim { get; set;}
		public string authid { get; set;}
		public DateTime? approve { get; set;}
	}
}
