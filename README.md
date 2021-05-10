# Easify Extensions - Notification Services

The project consist of several packages which facilitate sending notifications. It covers the following scenarios:
![Release](https://github.com/icgam/Easify.Extensions.Notifications/workflows/Release%20build%20on%20master/main/badge.svg) ![CI](https://github.com/icgam/Easify.Extensions.Notifications/workflows/CI%20on%20Branches%20and%20PRs/badge.svg)  ![](https://img.shields.io/nuget/v/Easify.Extensions.Notifications.Extensions.svg?style=flat-square)

1. Rendering the output message content (Current support is for handle bars using https://github.com/Antaris/FuManchu library)

2. Sending messages to multiple audiences using Smtp protocol (It has been implemented using https://github.com/jstedfast/MailKit)

## Usage

### Installation

Using NuGet

```
Install-Package Easify.Extensions.Notifications
```

Using dotnet cli

```
dotnet add package Easify.Extensions.Notifications
```

### Configuration

There is an extension which help to setup the service for IServiceCollection.

```csharp
services.AddNotification(configuration)
```

which is adding the following services to the DI container

```csharp

services.AddOptions().Configure<NotificationOptions>(configuration.GetSection(nameof(NotificationOptions)));
services.AddOptions().Configure<SmtpOptions>(configuration.GetSection(nameof(SmtpOptions)));

services.AddTransient<ITemplateProvider, FileBasedTemplateProvider>();
services.AddTransient<IMessagingService, MailKitMessagingService>();
services.AddTransient<ITemplateContentRenderer, HandleBarsTemplateContentRenderer>();
services.AddTransient<ITemplateRenderer, TemplateRenderer>();
services.AddTransient<INotificationService, NotificationService>();

```

You should notice every one of the services can be customize in order to implement something different.

Also the following sections need to be added to the _appsettings.json_ file.

```json
"NotificationOptions": {
    "Sender": "sender email address",
    "Profiles": [
        {
            "ProfileName": "profile name",
            "Audiences": [
                {"Email": "email or distribution group name" }
            ]
        },
    ],
    "Templates": [
        {
            "Name": "template name",
            "Path": "template default location relative to application"
        },
    ]
},
"SmtpOptions": {
    "Server": "Smtp server name",
    "LocalDomain": "ICGPLC",
    "Port": "Optional. Default to 25"
}

```

Then you can inject **INotificationService** to your classes to be able to send the email.

**Note:** Use Relay Messaging so there is no setup to be made on application servers to support smtp. Also the sender can be a fake email address or even a descriptive email address.
