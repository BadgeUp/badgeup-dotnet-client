using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace BadgeUp.Types
{
	public class Meta
	{
		[JsonExtensionData]
		private readonly Dictionary<string, JToken> customFields = new Dictionary<string, JToken>();

		public System.DateTime? Created { get; set; }

		/// <summary>
		/// Gets the value of the specified custom field.
		/// </summary>
		/// <typeparam name="T">Type to which to convert the return value.</typeparam>
		/// <param name="fieldName">Field name.</param>
		/// <returns>The retrieved value.</returns>
		public T GetCustomField<T>(string fieldName)
		{
			return this.customFields[fieldName].ToObject<T>();
		}

		/// <summary>
		/// Sets the value of the specified custom field.
		/// </summary>
		/// <param name="fieldName">Name of the field to set.</param>
		/// <param name="value">Value to set.</param>
		public void SetCustomField(string fieldName, object value)
		{
			this.customFields[fieldName] = JToken.FromObject(value);
		}
	}
}
