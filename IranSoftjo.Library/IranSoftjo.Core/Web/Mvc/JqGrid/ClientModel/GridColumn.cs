using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using Mehr.Web.Mvc.JqGrid.ClientModel.ColumnPropertyModel;
using Mehr.Web.Mvc.ClientModel;

namespace Mehr.Web.Mvc.JqGrid.ClientModel
{
    public class GridColumn : ClientObject
    {
        public static readonly GridColumn IdColumn =
            new GridColumn("Id", "سریال") { Width = 40, SearchOptions = SearchOptionsGridProperty.JustEqual, IsHidden = true };

        public GridColumn(string name, string title)
        {
            Name = name;
            Title = title;
            Width = 100;
            IsHidden = false;
            IsSearchable = true;
            IsSortable = true;
        }

        public GridColumn(string name) : this(name, "") { }

        public GridColumn() : this(Guid.NewGuid().ToString().Substring(0,4)) { }

        public const string NameProperty = "name";
        public string Name
        {
            get { return this[NameProperty].Value as string; }
            set { this[NameProperty] = new ClientProperty(NameProperty, value); }
        }

        public const string TitleProperty = "titleTxt";
        public string Title
        {
            get { return this[TitleProperty].Value as string; }
            set { this[TitleProperty] = new ClientProperty(TitleProperty, value); }
        }

        public const string WidthProperty = "width";
        public int Width
        {
            get { return (this[WidthProperty] as ClientProperty<int>).Value; }
            set { this[WidthProperty] = new ClientProperty<int>(WidthProperty, value); }
        }

        public const string IsHiddenProperty = "hidden";
        public bool IsHidden
        {
            get { return (this[IsHiddenProperty] as ClientProperty<bool>).Value; }
            set { this[IsHiddenProperty] = new ClientProperty<bool>(IsHiddenProperty, value); }
        }

        public const string IsSearchableProperty = "search";
        public bool IsSearchable
        {
            get { return (this[IsSearchableProperty] as ClientProperty<bool>).Value; }
            set { this[IsSearchableProperty] = new ClientProperty<bool>(IsSearchableProperty, value); }
        }

        public const string IsSortableProperty = "sortable";
        public bool IsSortable
        {
            get { return (this[IsSortableProperty] as ClientProperty<bool>).Value; }
            set { this[IsSortableProperty] = new ClientProperty<bool>(IsSortableProperty, value); }
        }

        public const string EnumNameProperty = "enumName";
        public string EnumName
        {
            get { return this[EnumNameProperty].Value as string; }
            set { this[EnumNameProperty] = new ClientProperty(EnumNameProperty, value); }
        }

        public const string SearchOptionsProperty = SearchOptionsGridProperty.PropertyName;
        public SearchOptionsGridProperty SearchOptions
        {
            get { return this[SearchOptionsProperty].Value as SearchOptionsGridProperty; }
            set
            {
                this[SearchOptionsProperty] = new SearchOptionsGridProperty()
                {
                    Value = value.Value,
                };
            }
        }

        public const string FormatterProperty = "formatter";
        public Function Formatter
        {
            get { return (this[FormatterProperty] as ClientProperty<Function>).Value; }
            set { this[FormatterProperty] = new ClientProperty<Function>(FormatterProperty, value); }
        }

        public new GridColumn Add(ClientProperty newProperty)
        {
            base.Add(newProperty);
            return this;
        }
    }
}
