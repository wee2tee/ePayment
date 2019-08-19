using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DbfManager.DbfModel
{
	public class sttak {
		public string loccod { get; set;}
		public string stkcod { get; set;}
		public string area { get; set;}
		public DateTime? takdat { get; set;}
		public double cquqty { get; set;}
		public string cqucod { get; set;}
		public double cfactor { get; set;}
		public double xquqty { get; set;}
		public double phybal { get; set;}
		public double trnval { get; set;}
		public string takflg { get; set;}
		public double difqty { get; set;}
		public double difval { get; set;}
		public string docnum { get; set;}
		public string seqnum { get; set;}
		public string userid { get; set;}
		public DateTime? chgdat { get; set;}
	}
}
