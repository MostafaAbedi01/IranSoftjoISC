using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Mehr.Web.Mvc.JqGrid
{
   public static class ControllerExtesnions
    {
       public static ActionResult GridDataAsJson<TQueryRecord>(this Controller controller, Grid grid, IQueryable<TQueryRecord> allRecords)
       {
           var gridLogic = DefaultGridLogicCreator.Create(grid, allRecords);
           return GridDataAsJson(controller, gridLogic);
       }

       public static ActionResult GridDataAsJsonp<TQueryRecord>(this Controller controller, Grid grid, IQueryable<TQueryRecord> allRecords)
       {
           var gridLogic = DefaultGridLogicCreator.Create(grid, allRecords);
           return GridDataAsJsonp(controller, gridLogic);
       }

       public static ActionResult GridDataAsJson(this Controller controller, IGridLogic gridLogic)
       {
           var exportFileInfo = gridLogic.ExportFileInfo;
           if (exportFileInfo != null)
               return new FileContentResult(exportFileInfo.Content, exportFileInfo.ContentType)
               {
                   FileDownloadName = exportFileInfo.FileName
               };

           return new JsonResult()
           {
               JsonRequestBehavior = JsonRequestBehavior.AllowGet,
               Data = gridLogic.JqGridFormattedData,
           };
       }

       public static ActionResult GridDataAsJsonp(this Controller controller, IGridLogic gridLogic)
       {
           var exportFileInfo = gridLogic.ExportFileInfo;
           if (exportFileInfo != null)
               return new FileContentResult(exportFileInfo.Content, exportFileInfo.ContentType)
               {
                   FileDownloadName = exportFileInfo.FileName
               };

           return new JsonpResult()
           {
               JsonRequestBehavior = JsonRequestBehavior.AllowGet,
               Data = gridLogic.JqGridFormattedData,
           };
       }
    }
}
