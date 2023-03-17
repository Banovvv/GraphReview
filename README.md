# GraphReview
[![Hack Together: Microsoft Graph and .NET](https://img.shields.io/badge/Microsoft%20-Hack--Together-orange?style=for-the-badge&logo=microsoft)](https://github.com/microsoft/hack-together)

Simple API used to schedule review meetings and send emails via MicrosoftGraph.

## Design
![test drawio (6)](https://user-images.githubusercontent.com/44908454/224314726-03609490-defe-402c-8fc2-ebb723fa2942.png)

## Prerequisites
You need an appsettings.json file with the following:
```JSON
  "ConnectionStrings": {
    "SqlServer": "your-connection-string"
  },
  "MicrosofrGraph": {
    "TenantId": "your-tenant-id",
    "ClientId": "your-client-id",
    "ClientSecret": "your-client-secret",
    "DefaultSender": "your-default-email"
  }
```
