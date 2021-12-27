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
				string name = item.Name;
				object? value = item.GetValue(obj, null);
				if (item.PropertyType.IsValueType || item.PropertyType.Name.StartsWith("String"))
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

		public static IList<KeyValuePair<string, dynamic?>> GetProperties1<T>(T obj)
		{
			return obj.GetProperties();
		}
	}
}
