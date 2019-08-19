using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DbfManager.DbfModel
{
	public class isfixcod {
		public string codtyp { get; set;}
		public string oldcod { get; set;}
		public string olddep { get; set;}
		public string newcod { get; set;}
		public string newdep { get; set;}
		public string flag { get; set;}
		public string userid { get; set;}
		public DateTime? chgdat { get; set;}
		public string userpos { get; set;}
		public DateTime? posdat { get; set;}
	}
}
