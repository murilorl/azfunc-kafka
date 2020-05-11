using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.WebJobs.Extensions.Kafka;
using System.Threading;

namespace App.Functions.Kafka
{
    public static class KafkaFunctions
    {

        [FunctionName("kafkaApp")]
        public static void ConfluentCloudStringTrigger(
             [KafkaTrigger(
                "pkc-lgwgm.eastus2.azure.confluent.cloud:9092",
                "users",
                ConsumerGroup = "cg-01",
                Protocol = BrokerProtocol.SaslSsl,
                AuthenticationMode = BrokerAuthenticationMode.Plain,
                Username = "RZVIIWXNXVKXBX3X",
                Password = "A1lGUniNmhMW0P1FHZzSjRKL169WHguEOIWJTF1/4mZu8LNNag9GIfHkMRWcoOy+",
                SslCaLocation = "confluent_cloud_cacert.pem")]
            KafkaEventData<string> kafkaEvent,
            ILogger logger)
            {
                logger.LogInformation(kafkaEvent.Value.ToString());
            }

    }
}
