using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace Sample.Core.Helpers
{
    public static class EnumHelper//<T>
    {
        public static List<SelectListItem> GetEnums<T>()
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

        public static string GetDescription(Enum value)
        {
            if (value != null)
            {
                var field = value.GetType().GetField(value.ToString());

                DescriptionAttribute attribute
                        = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute))
                            as DescriptionAttribute;

                if (attribute != null)
                {
                    return attribute.Description;
                }
            }

            return value.ToString();
        }

        public static string GetDescription(object value)
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

                        if (attribute != null)
                        {
                            return attribute.Description;
                        }
                    }
                }
            }

            return value.ToString();
        }
    }
}