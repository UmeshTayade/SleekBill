using System;
using System.Collections.Generic;
using System.Reflection;
using System.ComponentModel;

namespace DataObjectHelpers
{
    public static class DataMapper
    {
        private static Type GetPropertyType(Type propertyType)
        {
            Type nullableType = propertyType;
            if (nullableType.IsGenericType && (nullableType.GetGenericTypeDefinition() == typeof(Nullable<>)))
            {
                return Nullable.GetUnderlyingType(nullableType);
            }
            return nullableType;
        }

        public static PropertyInfo[] GetSourceProperties(Type sourceType)
        {
            List<PropertyInfo> list = new List<PropertyInfo>();
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(sourceType);
            foreach (PropertyDescriptor descriptor in properties)
            {
                if (descriptor.IsBrowsable)
                {
                    list.Add(sourceType.GetProperty(descriptor.Name));
                }
            }
            return list.ToArray();
        }

        public static void SetPropertyValue(object target, string propertyName, object value)
        {
            PropertyInfo property = target.GetType().GetProperty(propertyName);
            if (value == null)
            {
                property.SetValue(target, value, null);
            }
            else
            {
                Type propertyType = GetPropertyType(property.PropertyType);
                Type o = GetPropertyType(value.GetType());
                if (propertyType.Equals(o))
                {
                    property.SetValue(target, value, null);
                }
                else if (propertyType.Equals(typeof(Guid)))
                {
                    property.SetValue(target, new Guid(value.ToString()), null);
                }
                else if (propertyType.IsEnum && o.Equals(typeof(string)))
                {
                    property.SetValue(target, Enum.Parse(propertyType, value.ToString()), null);
                }
                else if (propertyType.IsEnum && o.Equals(typeof(int)))
                {
                    property.SetValue(target, Enum.Parse(propertyType, value.ToString()), null);
                }
                else
                {
                    property.SetValue(target, Convert.ChangeType(value, propertyType), null);
                }
            }
        }
    }
}
