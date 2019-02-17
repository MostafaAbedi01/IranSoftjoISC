using System.Linq;
using System.Collections.Generic;
using System;
namespace Mehr.Web.Mvc
{
	public static class SortHelper
	{
		public static string ApplySort(string sortExpression, string newSortColumn)
		{
			if (string.IsNullOrEmpty(newSortColumn)) return sortExpression;

			Dictionary<string, string> sortExpressionDic =
				(from s in sortExpression.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
				 select s.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)).ToDictionary(parts => parts[0], parts => parts[1]);

			string orderType = null;
			sortExpressionDic.TryGetValue(newSortColumn, out orderType);
			if (string.IsNullOrEmpty(orderType))//Add sorting
				sortExpressionDic.Add(newSortColumn, "desc");
			else if (orderType == "desc")//Toggle sorting
				sortExpressionDic[newSortColumn] = "asc";
			else//Remove sorting
				sortExpressionDic.Remove(newSortColumn);

			string returnValue = string.Empty;
			sortExpressionDic.Select(t => t.Key + ' ' + t.Value + ',').ToList().ForEach(s => returnValue += s);

			return returnValue.TrimEnd(',');
		}

		public static SortType GetSortType(string sortExpression, string newSortColumn)
		{
			Dictionary<string, string> sortExpressionDic =
				(from s in sortExpression.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
				 select s.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)).ToDictionary(parts => parts[0], parts => parts[1]);

			string orderType = null;
			sortExpressionDic.TryGetValue(newSortColumn, out orderType);

			if (string.IsNullOrEmpty(orderType)) return SortType.None;
			else if (orderType == "desc") return SortType.Desc;
			else return SortType.Asc;
		}

		public static SortType GetNextSortType(SortType sortType)
		{
			switch (sortType)
			{
				case SortType.Asc: return SortType.None;
				case SortType.Desc: return SortType.Asc;
				case SortType.None: return SortType.Desc;
			}
			return SortType.None;
		}

		public enum SortType
		{
			Asc,
			Desc,
			None
		}
	}

}