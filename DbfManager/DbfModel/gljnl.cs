using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DbfManager.DbfModel
{
	public class gljnl {
		public string jnltyp { get; set;}
		public string batch { get; set;}
		public DateTime? voudat { get; set;}
		public string voucher { get; set;}
		public string refnum { get; set;}
		public string srcjnl { get; set;}
		public string descrp { get; set;}
		public string reverse { get; set;}
		public string trnstat { get; set;}
		public string docstat { get; set;}
		public string creby { get; set;}
		public DateTime? credat { get; set;}
		public string userid { get; set;}
		public DateTime? chgdat { get; set;}
		public string postid { get; set;}
		public DateTime? posdat { get; set;}
		public string userprn { get; set;}
		public DateTime? prndat { get; set;}
		public decimal prncnt { get; set;}
		public string prntim { get; set;}
		public string authid { get; set;}
		public DateTime? approve { get; set;}
	}
}
