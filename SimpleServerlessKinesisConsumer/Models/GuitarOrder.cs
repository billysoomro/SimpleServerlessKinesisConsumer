using System.Text.Json.Serialization;

namespace SimpleServerlessKinesisConsumer.Models
{
    public class GuitarOrder
    {
        [JsonPropertyName("orderId")]
        public int OrderId { get; set; }

        [JsonPropertyName("orderStatus")]
        public string OrderStatus { get; set; }

        [JsonPropertyName("guitarBrand")]
        public string GuitarBrand { get; set; }

        [JsonPropertyName("bodyType")]
        public string BodyType { get; set; }

        [JsonPropertyName("color")]
        public string Color { get; set; }

        [JsonPropertyName("totalPrice")]
        public int TotalPrice { get; set; }
    }
}
