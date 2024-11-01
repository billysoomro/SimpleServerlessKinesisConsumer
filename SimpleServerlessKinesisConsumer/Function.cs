using System.Text;
using System.Text.Json;
using Amazon.Lambda.Core;
using Amazon.Lambda.KinesisEvents;
using Amazon.S3.Model;
using Amazon.S3;
using SimpleServerlessKinesisConsumer.Models;
using Amazon;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace SimpleServerlessKinesisConsumer;

public class Function
{
    private static readonly string bucketName = "guitar-orders";
    private static readonly RegionEndpoint bucketRegion = RegionEndpoint.EUWest2;
    private static readonly IAmazonS3 s3Client = new AmazonS3Client(bucketRegion);

    public async Task FunctionHandler(KinesisEvent kinesisEvent, ILambdaContext context)
    {
        context.Logger.LogInformation($"Beginning to process {kinesisEvent.Records.Count} records...");

        foreach (var record in kinesisEvent.Records)
        {
            context.Logger.LogInformation($"Event ID: {record.EventId}");
            context.Logger.LogInformation($"Event Name: {record.EventName}");

            var recordData = GetRecordContents(record.Kinesis);
            context.Logger.LogInformation($"Record Data:");
            context.Logger.LogInformation(recordData);

            var guitarOrder = JsonSerializer.Deserialize<GuitarOrder>(recordData);
            await UploadJsonToS3Async(recordData, guitarOrder.OrderId);
        }

        context.Logger.LogInformation("Stream processing complete.");
    }

    private string GetRecordContents(KinesisEvent.Record streamRecord)
    {
        using (var reader = new StreamReader(streamRecord.Data, Encoding.ASCII))
        {
            return reader.ReadToEnd();
        }
    }

    private static async Task UploadJsonToS3Async(string jsonData, int orderId)
    {
        try
        {
            var byteArray = Encoding.UTF8.GetBytes(jsonData);

            using var stream = new MemoryStream(byteArray);
            
            var putRequest = new PutObjectRequest
            {
                BucketName = bucketName,
                Key = $"{orderId}.json",
                InputStream = stream,
                ContentType = "application/json"
            };
           
            var response = await s3Client.PutObjectAsync(putRequest);

            Console.WriteLine($"File uploaded to S3 with status: {response.HttpStatusCode}");
        }

        catch (AmazonS3Exception e)
        {
            Console.WriteLine($"Error encountered while writing object to S3. Message:{e.Message}");
        }

        catch (Exception e)
        {
            Console.WriteLine($"Error encountered. Message:{e.Message}");
        }
    }
}