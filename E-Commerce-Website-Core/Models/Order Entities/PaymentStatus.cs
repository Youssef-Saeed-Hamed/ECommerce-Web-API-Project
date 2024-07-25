using System.Text.Json.Serialization;

namespace E_Commerce_Website_Core.Models.Order_Entities
{
	[JsonConverter(typeof(JsonStringEnumConverter))]
	public enum PaymentStatus
	{
		Pending,Failed,Received
	}
}
