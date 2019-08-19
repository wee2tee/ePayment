using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DbfManager.DbfModel
{
	public class isvat {
		public string vatrec { get; set;}
		public string vattyp { get; set;}
		public string rectyp { get; set;}
		public DateTime? vatprd { get; set;}
		public string late { get; set;}
		public DateTime? vatdat { get; set;}
		public DateTime? docdat { get; set;}
		public string docnum { get; set;}
		public string refnum { get; set;}
		public string newnum { get; set;}
		public string depcod { get; set;}
		public string descrp { get; set;}
		public double amt01 { get; set;}
		public double vat01 { get; set;}
		public double amt02 { get; set;}
		public double vat02 { get; set;}
		public double amtrat0 { get; set;}
		public string remark { get; set;}
		public string self_added { get; set;}
		public string had_modify { get; set;}
		public string docstat { get; set;}
		public string taxid { get; set;}
		public decimal orgnum { get; set;}
		public string prenam { get; set;}
	}
}
