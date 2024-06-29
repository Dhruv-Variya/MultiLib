using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MQTTnet;
using MQTTnet.Client;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

class Program
{
    private static IMqttClient mqttClient;
    private static Dictionary<string, Action<string>> topicHandlers = new Dictionary<string, Action<string>>();

    static async Task Main(string[] args)
    {
        var mqttFactory = new MqttFactory();
        mqttClient = mqttFactory.CreateMqttClient();

        // Configure MQTT Client Options
        var mqttClientOptions = new MqttClientOptionsBuilder()
            .WithTcpServer("broker.hivemq.com", 1883)
            .WithClientId($"TestClient_{Guid.NewGuid()}")
            .WithCleanSession()
            .Build();

        // Connect to MQTT Broker
        await ConnectMqttClient(mqttClientOptions);

        // Configure topics and their handlers
        ConfigureTopics();

        // Subscribe to all configured topics
        await SubscribeToTopics();

        Console.WriteLine("MQTT client connected and subscribed to topics.");
        Console.WriteLine("Press 'P' to publish a message, 'Q' to quit.");

        while (true)
        {
            var key = Console.ReadKey(true);
            if (key.Key == ConsoleKey.P)
            {
                await PublishMessage();
            }
            else if (key.Key == ConsoleKey.Q)
            {
                break;
            }
        }

        await mqttClient.DisconnectAsync();
    }

    private static async Task ConnectMqttClient(MqttClientOptions options)
    {
        mqttClient.ApplicationMessageReceivedAsync += e =>
        {
            string topic = e.ApplicationMessage.Topic;
            string payload = Encoding.UTF8.GetString(e.ApplicationMessage.PayloadSegment);
            Console.WriteLine($"Received message on topic: {topic}");

            if (topicHandlers.TryGetValue(topic, out var handler))
            {
                handler(payload);
            }
            else
            {
                Console.WriteLine($"No handler for topic: {topic}. Message: {payload}");
            }

            return Task.CompletedTask;
        };

        await mqttClient.ConnectAsync(options, CancellationToken.None);
    }

    private static void ConfigureTopics()
    {
        // Configure your topics and their handlers here
        topicHandlers["sensor/temperature"] = HandleTemperature;
        topicHandlers["sensor/humidity"] = HandleHumidity;
        topicHandlers["sensor/pressure"] = HandlePressure;
    }

    private static async Task SubscribeToTopics()
    {
        var mqttFactory = new MqttFactory();
        var mqttSubscribeOptions = mqttFactory.CreateSubscribeOptionsBuilder();

        foreach (var topic in topicHandlers.Keys)
        {
            mqttSubscribeOptions.WithTopicFilter(f => f.WithTopic(topic));
        }

        await mqttClient.SubscribeAsync(mqttSubscribeOptions.Build(), CancellationToken.None);
    }

    private static async Task PublishMessage()
    {
        Console.Write("Enter topic to publish to: ");
        string topic = Console.ReadLine();

        Console.Write("Enter message to publish: ");
        string message = Console.ReadLine();

        var applicationMessage = new MqttApplicationMessageBuilder()
            .WithTopic(topic)
            .WithPayload(message)
            .Build();

        await mqttClient.PublishAsync(applicationMessage, CancellationToken.None);
        Console.WriteLine("Message published");
    }

    private static void HandleTemperature(string payload)
    {
        if (IsValidJson(payload))
        {
            var data = JsonConvert.DeserializeObject<SensorData>(payload);
            Console.WriteLine($"Temperature: {data.Value} {data.Unit}");
            // Add your logic to process temperature data
        }
        else
        {
            Console.WriteLine($"Temperature (raw data): {payload}");
            // Handle non-JSON data here
        }
    }

    private static void HandleHumidity(string payload)
    {
        if (IsValidJson(payload))
        {
            var data = JsonConvert.DeserializeObject<SensorData>(payload);
            Console.WriteLine($"Humidity: {data.Value} {data.Unit}");
            // Add your logic to process humidity data
        }
        else
        {
            Console.WriteLine($"Humidity (raw data): {payload}");
            // Handle non-JSON data here
        }
    }

    private static void HandlePressure(string payload)
    {
        if (IsValidJson(payload))
        {
            var data = JsonConvert.DeserializeObject<SensorData>(payload);
            Console.WriteLine($"Pressure: {data.Value} {data.Unit}");
            // Add your logic to process pressure data
        }
        else
        {
            Console.WriteLine($"Pressure (raw data): {payload}");
            // Handle non-JSON data here
        }
    }

    private static bool IsValidJson(string strInput)
    {
        if (string.IsNullOrWhiteSpace(strInput)) { return false; }
        strInput = strInput.Trim();
        if ((strInput.StartsWith("{") && strInput.EndsWith("}")) || //For object
            (strInput.StartsWith("[") && strInput.EndsWith("]"))) //For array
        {
            try
            {
                var obj = JToken.Parse(strInput);
                return true;
            }
            catch (JsonReaderException)
            {
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }
}

class SensorData
{
    public double Value { get; set; }
    public string Unit { get; set; }
}