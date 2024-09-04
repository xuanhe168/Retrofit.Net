using System.Reflection;

namespace Retrofit.Net.Core.Extensions
{
    public static class ConvertExtensions
	{
		/// <summary>
		/// Get all properties from class T
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="obj"></param>
		/// <returns></returns>
		public static IList<KeyValuePair<string,dynamic?>> GetProperties<T>(this T obj)
		{
			List<KeyValuePair<string, dynamic?>> rtn = new List<KeyValuePair<string, dynamic?>>();
			if (obj is null) return rtn;

            PropertyInfo[] properties = obj.GetType().GetProperties(BindingFlags.Instance|BindingFlags.Public);
			if (properties.Length <= 0) return rtn;
			foreach (PropertyInfo item in properties)
			{
				Type type = item.PropertyType;

                string name = item.Name;
                object? value = item.GetValue(obj, null);

                if (type.IsValueType || type.Name.StartsWith("String") || type.Name.StartsWith("FieldFile"))
				{
					rtn.Add(new KeyValuePair<string,dynamic?>(name, value));
				}
				else
				{
					rtn.Add(new KeyValuePair<string, dynamic?>(name, value?.GetProperties()));
				}
			}
			return rtn;
		}
	}
}
