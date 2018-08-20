using System;

namespace BadgeUpClient.Encoding
{
	/// <summary>
	/// Represents a common helper class to encode strings like URI params.
	/// </summary>
	internal static class EncodingExtensions
	{
		/// <summary>
		/// Converts a text string into a URL-encoded string.
		/// </summary>
		/// <param name="stringToEncode">String to encode.</param>
		/// <returns>Returns the encoded string.</returns>
		public static string UrlEncode(this string stringToEncode) => Uri.EscapeDataString(stringToEncode);

		/// <summary>
		/// Converts an object into a URL-encoded string by using the object's .ToString() implementation.
		/// </summary>
		/// <param name="objectToEncode">Object to encode.</param>
		/// <returns>Returns the encoded string.</returns>
		public static string UrlEncode(this object objectToEncode) => objectToEncode?.ToString()?.UrlEncode();
	}
}
