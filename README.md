# MaxBot C# 

![.NET](https://img.shields.io/badge/.NET-8.0-purple)
[![NuGet](https://img.shields.io/nuget/v/KmsDev.MaxBot)](https://www.nuget.org/packages/KmsDev.MaxBot)
![License](https://img.shields.io/github/license/kmsmarik9/maxbot)

# Быстрый старт
### Подключение пакета
```
dotnet add package KmsDev.MaxBot
```

### Подключение MaxBotSystem
```csharp
using KmsDev.MaxBot;

serviceCollections.AddMaxBotSystem();
```

Если в системе пралируется 1 бот, то можно использовать `AddSingletonClient`
```csharp
//бот будет доступен по интерфейсу IMaxBotClient
serviceCollections.AddMaxBotSystem(sc =>
{
    sc.AddSingletonClient(БОТ_ТОКЕН);
});
```

### Ручное создание MaxBotClient
```csharp
//получить client builder
var maxBotClientBuilder = serviceProvider.GetRequiredService<IMaxBotClientBuilder>();

//создание бота:
//на основе БОТ_ТОКЕН создается `botHash` с использованием `HMACSHA256`
//(опционально - рекомендуется вторым параметром добавить `botHashSecretKey`)
var maxBot = maxBotClientBuilder.Build(БОТ_ТОКЕН);
```

## Использование api
Бот разделен на секции в соотвествии с [api документацией](https://dev.max.ru/docs-api)

реализовано:
- `Api.Bots`
- `Api.Subscriptions`
- `Api.Upload`
- `Api.Messages`

в разработке:
- `Api.Chats`

пример использования api:
```csharp
using KmsDev.MaxBot.Requests;
using KmsDev.MaxBot.Models;

var response = await maxBot.Api.Messages.SendMessageAsync(new SendMessageRequest
{
    Target = (SendMessageRequest.TargetType.User, {USER_ID}),
    Text = "тест сообщение",
}.WithInlineKeyboardAttachment
(
    [
        new ApiOutputInlineKeyboardCallbackButton
        {
            Text = "тест кнопка",
            Payload = "test_button"
        }
    ]
));
```

### Route Handler
[В отдельном проекте KmsDev.MaxBot.Handlers](src/KmsDev.MaxBot.Handlers/README.md)

### LongPolling + Manager
[В отдельном проекте KmsDev.MaxBot.LongPollingManager](src/KmsDev.MaxBot.LongPollingManager/README.md)

## Системная информация
### Подключен пакет `Microsoft.Extensions.Http.Resilience`
- для всех ботов и api запросов используется один RateLimiter pipeline для ограничения в 30rps к серверу 
- для LongPolling запроса добавлен RateLimiter с 2rps индивидуально для каждого бота (ограничение со стороны Api начиная с 15.05.2026)
- для каждого api запроса индивидуально добавлен Retry и Timeout pipeline (можно задавать свои лимиты при вызове api)
- для некоторых запросов (например SendMessage с 'attachment.not.ready') используется свой уникальный pipeline, детально в `MaxBotRequestsConfigurer`

### Подключен пакет `OneOf`
- для поддержки Union types
- в планах перейти на C#14 unions

### Подключен пакет `Metalama.Framework`
- для Source Generation в ядре
- для DI чтобы пользователя каждый раз не заставлять в конструкторе добавлять системные контракты, Metalama будет сама генерировать нужный
- в планах добавить Source Generation для json converters и тд...