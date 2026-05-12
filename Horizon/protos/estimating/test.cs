using Google.Protobuf;
using Tutorial;
using SciterSharp;

namespace Estimating.Protos
{
	public static class EstimatingProtos
	{
		public static void RunTest()
		{
			// create person
			var person = new Person
			{
				Name = "Njabulo",
				Id = 123,
				Email = "alice@example.com"
			};

			person.Phones.Add(new Person.Types.PhoneNumber
			{
					Number = "072-123-4567",
					Type = Person.Types.PhoneType.Mobile
			});

			// Serialize to byte array
			byte[] data = person.ToByteArray();

			// Deserialize back
			var parsed = Person.Parser.ParseFrom(data);

			MessageBox.Show(IntPtr.Zero,$"Name : {parsed.Name},\nEmail: {parsed.Email}\nNumber: {parsed.Phones[0].Number}", "Proto test" );
		}
	}
}