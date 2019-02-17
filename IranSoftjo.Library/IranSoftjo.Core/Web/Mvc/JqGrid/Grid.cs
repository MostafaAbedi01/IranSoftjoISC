using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mehr.Web.Mvc.ClientModel;
using Mehr.Web.Mvc.JqGrid.ClientModel;

namespace Mehr.Web.Mvc.JqGrid
{
    public class Grid : ClientObject
    {
        public virtual string DefinitionVersion { get; set; }
        public string ClientBuildEventName { get; set; }

        public Grid()
        {
            DefinitionVersion = string.Empty;
            Columns = new ColumnsGridProperty();
            this["useTitleAsColNames"] = new ClientProperty<bool>("useTitleAsColNames", true);
            Caption = "لیست";
            DisplayRowNumber = false;
            RowCount = 10;
            ExportExcludeColumnNames = new string[0];
        }

        public const string ColumnsProperty = ColumnsGridProperty.PropertyName;
        public ColumnsGridProperty Columns
        {
            get { return this[ColumnsProperty] as ColumnsGridProperty; }
            set { this[ColumnsProperty] = value; }
        }

        public const string CaptionProperty = "caption";
        public string Caption
        {
            get { return this[CaptionProperty].Value as string; }
            set { this[CaptionProperty] = new ClientProperty(CaptionProperty, value); }
        }

        public const string DataUrlProperty = "url";
        public string DataUrl
        {
            get { return GetProprty<string>(DataUrlProperty); }
            set { SetProprty<string>(DataUrlProperty, value); }
        }

        public const string DetailPageUrlIdplaceHolderValue = "_IdValue";
        public const string DetailPageUrlProperty = "detailPageUrl";
        public string DetailPageUrl
        {
            get { return GetProprty<string>(DetailPageUrlProperty); }
            set { SetProprty<string>(DetailPageUrlProperty, value); }
        }

        public const string DisplayRowNumberProperty = "rownumbers";
        public bool DisplayRowNumber
        {
            get { return GetProprty<bool>(DisplayRowNumberProperty); }
            set { SetProprty<bool>(DisplayRowNumberProperty, value); }
        }

        public const string RowCountProperty = "rowNum";
        public int RowCount
        {
            get { return GetProprty<int>(RowCountProperty); }
            set { SetProprty<int>(RowCountProperty, value); }
        }

        public const string RowCountListProperty = "rowList";
        public ClientList<int> RowCountList
        {
            get { return GetProprty<ClientList<int>>(RowCountListProperty); }
            set { SetProprty<ClientList<int>>(RowCountListProperty, value); }
        }

        public const string LoadCompleteProperty = "loadComplete";
        public Function LoadComplete
        {
            get { return (this[LoadCompleteProperty] as ClientProperty<Function>).Value; }
            set { this[LoadCompleteProperty] = new ClientProperty<Function>(LoadCompleteProperty, value); }
        }

        public const string MultiselectEnabledProperty = "multiselect";
        public bool MultiselectEnabled
        {
            get { return GetProprty<bool>(MultiselectEnabledProperty); }
            set { SetProprty<bool>(MultiselectEnabledProperty, value); }
        }

        public string[] ExportExcludeColumnNames { get; set; }

        public const string DataTypeProperty = "dataType";
        public GridDataType DataType
        {
            get { return (this[DataTypeProperty] as ClientProperty<EnumValue<GridDataType>>).Value.Value; }
            set
            {
                this[DataTypeProperty] = new ClientProperty<EnumValue<GridDataType>>(
                    DataTypeProperty,
                    new EnumValue<GridDataType>(value));
            }
        }

        public const string ColumnsLayoutCommandVisibleProperty = "columnChooserButton";
        public bool ColumnsLayoutCommandVisible
        {
            get { return GetProprty<bool>(ColumnsLayoutCommandVisibleProperty); }
            set { SetProprty<bool>(ColumnsLayoutCommandVisibleProperty, value); }
        }

    }
}
