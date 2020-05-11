using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.WebJobs.Extensions.Kafka;
using System.Threading;
using Microsoft.Extensions.Configuration;

namespace App.Functions.Kafka
{
    public class KafkaFunctions
    {

        [FunctionName("kafkaApp")]
        public void ConfluentCloudStringTrigger(
             [KafkaTrigger(
                "KafkaBrokerUrl",
                "users",
                ConsumerGroup = "cg-01",
                Protocol = BrokerProtocol.SaslSsl,
                AuthenticationMode = BrokerAuthenticationMode.Plain,
                Username = "KafkaUsername",
                Password = "KafkaPassword",
                SslCaLocation = "confluent_cloud_cacert.pem")]
            KafkaEventData<string> kafkaEvent,
            ILogger logger)
        {
            logger.LogInformation(kafkaEvent.Value.ToString());
        }
    }
}
