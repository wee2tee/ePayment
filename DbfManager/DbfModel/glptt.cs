using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DbfManager.DbfModel
{
	public class glptt {
		public string rectyp { get; set;}
		public string docnum { get; set;}
		public DateTime? docdat { get; set;}
		public string doctyp { get; set;}
		public string pttstat { get; set;}
		public string docstat { get; set;}
		public decimal amount { get; set;}
		public string remark { get; set;}
		public string refnum { get; set;}
		public decimal chqamt { get; set;}
		public decimal extamt { get; set;}
		public DateTime? extdat { get; set;}
		public string userid { get; set;}
		public DateTime? chgdat { get; set;}
		public string postid { get; set;}
		public DateTime? posdat { get; set;}
	}
}
