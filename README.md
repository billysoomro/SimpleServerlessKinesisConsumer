# SimpleServerlessKinesisConsumer
A simple .NET Core Lambda Function that acts as a Kinesis Data Streams Consumer to process and store data to S3. It automatically triggers whenever new records are pushed onto the specified Kinesis stream. The function processes each record and stores it as a JSON file in the specified S3 bucket.

The expected format of each Kinesis record is as follows:
```json
{
  "orderId": 36,
  "orderLocation": "UK",
  "orderStatus": "Shipped",
  "guitarBrand": "Fender",
  "bodyType": "Solid",
  "color": "Black",
  "totalPrice": 913
}
```
## Prerequisites

Before you begin, ensure you have the following installed:

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [AWS CLI](https://aws.amazon.com/cli/)
- AWS credentials configured on your machine (using `aws configure`)

## Installation

1. **Clone the repository:**

    ```bash
    git clone https://github.com/billysoomro/SimpleServerlessKinesisConsumer.git
    cd SimpleServerlessKinesisConsumer
    ```

2. **Restore dependencies and build the project:**

    ```bash
    dotnet restore
    dotnet build
    ```

3. **Run the application:**

    ```bash
    dotnet run
    ```

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.