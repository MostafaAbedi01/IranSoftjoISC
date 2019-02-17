using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mehr.Web.Mvc.ClientModel;

namespace Mehr.Web.Mvc.JqGrid
{
    public enum Icons
    {
        [JsonEnumValue("")]
        None,

        [JsonEnumValue("ui-icon-pencil")]
        Edit,

        [JsonEnumValue("ui-icon-trash")]
        Delete,

        [JsonEnumValue("ui-icon-mail-closed")]
        MailClosed,

        [JsonEnumValue("ui-icon-play")]
        Play,
    }
}
