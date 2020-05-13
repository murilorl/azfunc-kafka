using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Kafka;
using Microsoft.Extensions.Logging;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using App.Services;

namespace App.Functions.Kafka
{
    public class KafkaFunctions
    {
        private readonly IMaterialService _materialService;
        public KafkaFunctions(IMaterialService materialService)
        {
            _materialService = materialService;
        }

        [FunctionName("kafkaApp")]
        public void ConfluentCloudStringTrigger(
             [KafkaTrigger(
                "KafkaBrokerUrl",
                "materials",
                ConsumerGroup = "cg-01",
                Protocol = BrokerProtocol.SaslSsl,
                AuthenticationMode = BrokerAuthenticationMode.Plain,
                Username = "KafkaUsername",
                Password = "KafkaPassword",
                SslCaLocation = "confluent_cloud_cacert.pem")]
            KafkaEventData<string> kafkaEvent,
            ILogger logger)
        {
            string kafkaEventValue = kafkaEvent.Value.ToString();

            try
            {
                JObject obj = JObject.Parse(kafkaEventValue);
                _materialService.AddAsync(obj);
            }
            catch (JsonReaderException ex)
            {
                //TODO: add retries and handle the commit of the Kafka event
                logger.LogError(ex, "The Kafka event value [{kafkaEventValue}] is not valid");
            }
        }
    }
}
