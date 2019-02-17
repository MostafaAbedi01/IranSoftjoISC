using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mehr.Web.Mvc.ClientModel;
using System.Web.Mvc;

namespace Mehr.Web.Mvc.JqGrid.ClientModel.ColumnPropertyModel
{
    public class CommandLinkFormatOptions : ClientObject, IFormatOptions
    {
        public static string FormatterFunctionName = "commandLink";

        public const string IdPlaceHolder = "_IdValue";
        public const string TextPlaceHolder = "_LinkText";
        public const string TitlePlaceHolder = "_LinkTitle";

        public const string LinkTemplateProperty = "linkTemplate";
        public string LinkTemplate
        {
            get { return this[LinkTemplateProperty].Value as string; }
            set { this[LinkTemplateProperty] = new ClientProperty(LinkTemplateProperty, value); }
        }

        public const string TextProperty = "linkText";
        public string Text
        {
            get { return this[TextProperty].Value as string; }
            set { this[TextProperty] = new ClientProperty(TextProperty, value); }
        }

        public const string TitleProperty = "linkTitle";
        public string Title
        {
            get { return this[TitleProperty].Value as string; }
            set { this[TitleProperty] = new ClientProperty(TitleProperty, value); }
        }

        public const string IconProperty = "iconClass";
        public Icons Icon
        {
            get { return this.GetProprty<EnumValue<Icons>>(IconProperty).Value; }
            set { this.SetProprty<EnumValue<Icons>>(IconProperty, new EnumValue<Icons>(value)); }
        }

        public const string ConfirmProperty = CommandLinkFormatOptionsConfirmProperty.PropertyName;
        public CommandLinkFormatOptionsConfirmProperty Confirm
        {
            get { return this[ConfirmProperty].Value as CommandLinkFormatOptionsConfirmProperty; }
            set
            {
                this[ConfirmProperty] = new CommandLinkFormatOptionsConfirmProperty()
                    {
                        Value = value.Value,
                    };
            }
        }

        public static TagBuilder BuildLinkTemplate(string urlTemplate)
        {
            var aTagBuilder = new TagBuilder("a");
            aTagBuilder.SetInnerText(CommandLinkFormatOptions.TextPlaceHolder);
            aTagBuilder.MergeAttribute("href", urlTemplate);
            aTagBuilder.MergeAttribute("title", CommandLinkFormatOptions.TitlePlaceHolder);
            return aTagBuilder;
        }

        public class CommandLinkFormatOptionsConfirmProperty : ClientProperty<ClientObject>
        {
            public const string PropertyName = "confirm";
            public CommandLinkFormatOptionsConfirmProperty()
            {
                base.Name = PropertyName;
                base.Value = new ClientObject();
            }

            public const string TitleProperty = "title";
            public string Title
            {
                get { return Value.GetProprty<string>(TitleProperty); }
                set { Value.SetProprty<string>(TitleProperty, value); }
            }

            public const string MessageProperty = "message";
            public string Message
            {
                get { return Value.GetProprty<string>(MessageProperty); }
                set { Value.SetProprty<string>(MessageProperty, value); }
            }
        }
    }
}
