﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mehr.Web.Mvc.ClientModel
{
    public interface IJsonSerializable
    {
        string GetClientModelAsJson();
    }
}
