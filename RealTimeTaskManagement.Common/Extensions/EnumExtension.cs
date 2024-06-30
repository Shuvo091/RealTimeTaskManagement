using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RealTimeTaskManagement.Common.Extensions
{
    public static class EnumExtension
    {
        public static List<DropdownModel>? EnumToDropdownList<T>()
        {
            var t = typeof(T);
            if (!t.IsEnum)
            {
                return null;
            }

            return (Enum.GetValues(typeof(T)).Cast<T>()
                .Select(v => new DropdownModel
                {
                    Text = GetDisplayName(v!),
                    Value = Convert.ToInt32(v)
                }).ToList());
        }

        public static string GetDisplayName(this object? value)
        {
            if (value == null) return string.Empty;

            var type = value.GetType();

            // Get the enum field.
            var field = type.GetField(value.ToString()!);
            if (field == null)
            {
                return value.ToString()!;
            }

            // Gets the value of the Name property on the DisplayAttribute, this can be null.
            var attributes = field.GetCustomAttribute<DisplayAttribute>();
            return attributes != null ? attributes.Name! : value.ToString()!;
        }
    }

    public class DropdownModel
    {
        public string Text { get; set; } = string.Empty;
        public int Value { get; set; }
    }
}
