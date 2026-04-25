# MaxBot C# 

![.NET](https://img.shields.io/badge/.NET-8.0-purple)
[![NuGet](https://img.shields.io/nuget/v/KmsDev.MaxBot)](https://www.nuget.org/packages/KmsDev.MaxBot)
![License](https://img.shields.io/github/license/kmsmarik9/maxbot)
<!-- ![Build](https://github.com/kmsmarik9/maxbot/actions/workflows/publish.yml/badge.svg) -->

# Быстрый старт
### Подключение пакета
```
dotnet add package KmsDev.MaxBot
```

### Подключение MaxBotSystem
```csharp
using KmsDev.MaxBot.Full;

serviceCollections.AddMaxBotSystem();
```

### Создание MaxBotClient
```csharp
//получить client builder
var maxBotClientBuilder = serviceProvider.GetRequiredService<IMaxBotClientBuilder>();

//создание бота:
//на основе БОТ_ТОКЕН создается `botHash` с использованием `System.IO.Hashing.XxHash3.HashToUInt64`
//(опционально - рекомендуется вторым параметром добавить `seed` фразу)
var maxBot = maxBotClientBuilder.Build(БОТ_ТОКЕН);
```

### LongPolling
Вызов `AddLongPollingManager` добавляет в DI интерфейс `IMaxBotManager`
```csharp
using KmsDev.MaxBot.Full;

//подключение основной системы
serviceCollections.AddMaxBotSystem(sc =>
{
    //подключение LongPolling
    sc.AddLongPollingManager();
});
```
Так же ознакомьтесь с [Route Handlers](Handlers/README.md)

### WebHook
- В разработке

### Route Handler
[Информация по Route Handlers](Handlers/README.md)


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
using KmsDev.MaxBot.Full.Requests;
using KmsDev.MaxBot.Full.Models;

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

## Системная информация
### Подключен пакет `Microsoft.Extensions.Http.Resilience`
- для всех ботов и api запросов используется один RateLimiter pipeline для ограничения в 30rps к серверу 
- для каждого api запроса индивидуально добавлен Retry и Timeout pipeline (можно задавать свои лимиты при вызове api)
- для некоторых запросов (например SendMessage с 'attachment.not.ready') используется свой уникальный pipeline, детально в `MaxBotRequestsConfigurer`

### Подключен пакет `OneOf`
- для поддержки Union types
- в планах перейти на C#14 unions

### Подключен пакет `Metalama.Framework`
- для Source Generation в ядре
- для DI чтобы пользователя каждый раз не заставлять в конструкторе добавлять системные контракты, Metalama будет сама генерировать нужный
- в планах добавить Source Generation для json converters и тд...