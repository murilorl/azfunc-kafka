using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.WebJobs.Extensions.Kafka;
using System.Threading;
using Microsoft.Extensions.Configuration;
using System.Text.Json;
using Newtonsoft.Json.Linq;

using Newtonsoft.Json;

using App.Services;
using App.Model;

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
            logger.LogInformation(kafkaEventValue);

            JObject obj = JObject.Parse(kafkaEventValue);
            logger.LogInformation(obj.ToString());
            //Material material = JsonSerializer.Deserialize<Material>(kafkaEventValue);

            _materialService.AddAsync(obj);
        }
    }
}
