using Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

public static class PropertyInfoExtensions
{
    public static TValue GetClassAttributValue<T,TAttribute, TValue>(this T prop, Func<TAttribute, TValue> value) where TAttribute : Attribute where T : class
    {
        if (prop.GetType().GetCustomAttributes(typeof(TAttribute), true).FirstOrDefault() is TAttribute att)
        {
            return value(att);
        }
        return default(TValue);
    }

    public static TValue GetPropertyAttributValue<T, TAttribute, TValue>(this string propertyName, T className, Func<TAttribute, TValue> value) where TAttribute : Attribute where T : class
    {

        var prop = className.GetType().GetProperty(propertyName);

        if (prop.GetCustomAttributes(typeof(TAttribute), true).FirstOrDefault() is TAttribute att)
        {
            return value(att);
        }
        return default(TValue);
    }


}

namespace Attributes
{
    class Program
    {
        static void Main(string[] args)
        {
            var test = new TEST();

            var ggattr = test.GetClassAttributValue((ExcelAttribute x) => x.Title);

            var CC = nameof(test.MyProperty1).GetPropertyAttributValue(test, (ExcelAttribute x) => x.Title);
        }
    }



}

[AttributeUsage(AttributeTargets.Class |
                AttributeTargets.Property,
                AllowMultiple = true)]
public class ExcelAttribute : Attribute
{
    public ExcelAttribute(string title)
    {
        Title = title;
    }

    public string Title { get; set; }
}

[Excel("很棒喔")]
public class TEST
{
    [Excel("欄位1")]
    public int MyProperty1 { get; set; }
    [Excel("欄位2")]
    public int MyProperty2 { get; set; }
    [Excel("欄位3")]
    public int MyProperty3 { get; set; }
}


