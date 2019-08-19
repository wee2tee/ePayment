using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DbfManager.DbfModel
{
	public class isreport {
		public string fname { get; set;}
		public string desc { get; set;}
		public string repid { get; set;}
		public string param { get; set;}
		public bool ismodify { get; set;}
		public string userid { get; set;}
		public DateTime? chgdat { get; set;}
		public bool stdscp { get; set;}
	}
}
