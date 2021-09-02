using Microsoft.Extensions.Caching.Memory;
using Sample.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace Sample.Services.Enums
{
    public class EnumManager : IEnumManager
    {
        #region Fields

        private readonly IMemoryCache _memoryCache;

        #endregion Fields

        #region Constructor

        public EnumManager(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        #endregion Constructor

        #region Methods

        public List<SelectListItem> GetEnums<T>()
        {
            var selectListItems = new List<SelectListItem>();

            var list = Enum.GetValues(typeof(T));

            foreach (var item in list)
            {
                var enumItem = (Enum)item;
                var description = GetDescription(enumItem);
                selectListItems.Add(new SelectListItem() { Text = description, Value = ((int)Enum.Parse(enumItem.GetType(), enumItem.ToString())).ToString() });
            }

            return selectListItems;
        }

        public string GetDescription(Enum value)
        {
            if (value != null)
            {
                var field = value.GetType().GetField(value.ToString());

                DescriptionAttribute attribute
                        = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute))
                            as DescriptionAttribute;

                if (attribute != null && !string.IsNullOrEmpty(attribute.Description))
                {
                    return _memoryCache.TryGetValue(attribute.Description, out string description) ? description : attribute.Description;
                }
            }

            return value.ToString();
        }

        public string GetDescription(object value)
        {
            Type type = value.GetType();

            if (type.BaseType == typeof(Enum))
            {
                string name = Enum.GetName(type, value);

                if (name != null)
                {
                    FieldInfo field = type.GetField(name);

                    if (field != null)
                    {
                        DescriptionAttribute attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;

                        if (attribute != null && !string.IsNullOrEmpty(attribute.Description))
                        {
                            return _memoryCache.TryGetValue(attribute.Description, out string description) ? description : attribute.Description;
                        }
                    }
                }
            }

            return value.ToString();
        }

        #endregion Methods
    }
}