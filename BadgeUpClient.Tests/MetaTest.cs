using BadgeUp.Types;
using System;
using System.Collections.Generic;
using Xunit;

namespace BadgeUp.Tests
{
	public class MetaTest
	{
		[Fact]
		public void WhenCustomFieldNameNotFound_GetCustomField_ShouldThrowKeyNotFoundException()
		{
			var meta = new Meta();
			Assert.Throws<KeyNotFoundException>(() =>
			{
				var field = meta.GetCustomField<object>("some field");
			});
		}

		[Fact]
		public void WhenCustomFieldNameIsNull_GetCustomField_ShouldThrowArgumentNullException()
		{
			var meta = new Meta();
			Assert.Throws<ArgumentNullException>(() =>
			{
				var field = meta.GetCustomField<object>(null);
			});
		}

		[Fact]
		public void WhenCustomFieldIsSet_GetCustomField_ShouldReturnCorrectValue()
		{
			var meta = new Meta();
			meta.SetCustomField("test int", 12345);

			object fieldValue = meta.GetCustomField<int>("test int");
			Assert.IsType<int>(fieldValue);
			Assert.Equal(12345, fieldValue);

			meta.SetCustomField("test string", "hello");
			fieldValue = meta.GetCustomField<string>("test string");
			Assert.IsType<string>(fieldValue);
			Assert.Equal("hello", fieldValue);
		}

		[Fact]
		public void SettingCustomField_ShouldNotThrowException()
		{
			var meta = new Meta();
			meta.SetCustomField("test", 12345);
		}

		[Fact]
		public void WhenCustomFieldHasValue_SettingNewFieldValue_ShouldOverwriteExistingValue()
		{
			var meta = new Meta();
			meta.SetCustomField("test", 12345);

			meta.SetCustomField("test", "hello!");

			var fieldValue = meta.GetCustomField<string>("test");
			Assert.Equal("hello!", fieldValue);
		}
	}
}
