using Sample.Core;
using System;
using System.Collections.Generic;

namespace Sample.Services.Enums
{
    public interface IEnumManager
    {
        List<SelectListItem> GetEnums<T>();

        string GetDescription(Enum value);

        string GetDescription(object value);
    }
}