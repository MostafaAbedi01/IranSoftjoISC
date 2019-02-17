using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mehr.Web.Mvc.JqGrid
{
    public class DefaultGridLogic<TGrid, TQueryRecord> : GridLogicBase<TQueryRecord, TQueryRecord>
       where TGrid : Grid, new()
    {
        public DefaultGridLogic()
            : base(new TGrid())
        {
            Formatter = q => q;
        }

        public static DefaultGridLogic<TGrid, TQueryRecord> Create(IQueryable<TQueryRecord> allRecords)
        {
            var billGridLogic = new DefaultGridLogic<TGrid, TQueryRecord>();
            billGridLogic.AllRecords = allRecords;
            return billGridLogic;
        }
    }

    public class DefaultGridLogic<TQueryRecord> : GridLogicBase<TQueryRecord, TQueryRecord>
    {
        public DefaultGridLogic(Grid grid)
            : base(grid)
        {
            Formatter = q => q;
        }
    }

    public class DefaultInlineGridLogic<TQueryRecord, TDisplayRecord> : GridLogicBase<TQueryRecord, TDisplayRecord>
    {
        public DefaultInlineGridLogic(Grid grid)
            : base(grid)
        {
        }
    }

    public class DefaultGridLogicCreator
    {
        public static DefaultGridLogic<TQueryRecord> Create<TQueryRecord>(Grid grid, IQueryable<TQueryRecord> allRecords)
        {
            var billGridLogic = new DefaultGridLogic<TQueryRecord>(grid);
            billGridLogic.AllRecords = allRecords;
            return billGridLogic;
        }

        public static DefaultInlineGridLogic<TQueryRecord, TDisplayRecord> Create<TQueryRecord, TDisplayRecord>(
            Grid grid, 
            IQueryable<TQueryRecord> allRecords,
            Func<TQueryRecord, TDisplayRecord> formatter)
        {
            var billGridLogic = new DefaultInlineGridLogic<TQueryRecord, TDisplayRecord>(grid);
            billGridLogic.AllRecords = allRecords;
            billGridLogic.Formatter = formatter;
            return billGridLogic;
        }
    }

}
