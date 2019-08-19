using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DbfManager.DbfModel
{
	public class stpri {
		public string stkcod { get; set;}
		public string qu { get; set;}
		public double factor { get; set;}
		public double qty { get; set;}
		public string percent { get; set;}
		public double sellpr1 { get; set;}
		public double sellpr2 { get; set;}
		public double sellpr3 { get; set;}
		public double sellpr4 { get; set;}
		public double sellpr5 { get; set;}
		public DateTime? start { get; set;}
		public DateTime? end { get; set;}
		public string userid { get; set;}
		public DateTime? chgdat { get; set;}
	}
}
