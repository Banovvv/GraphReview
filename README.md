# GraphReview
Simple API used to schedule review meetings and send emails via MicrosoftGraph.

## Design
![test drawio (6)](https://user-images.githubusercontent.com/44908454/224314726-03609490-defe-402c-8fc2-ebb723fa2942.png)

## Prerequisites
```JSON
  "ConnectionStrings": {
    "SqlServer": "Server=localhost,1433;Database=Master;User Id=SA;Password=yourStrong(!)Password;TrustServerCertificate=True"
  },
  "MicrosofrGraph": {
    "TenantId": "your-tenant-id",
    "ClientId": "your-client-id",
    "ClientSecret": "your-client-secret",
    "DefaultSender": "your-default-email"
  }
```
