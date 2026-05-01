![.NET](https://img.shields.io/badge/.NET-8.0-purple)
[![NuGet](https://img.shields.io/nuget/v/KmsDev.MaxBot)](https://www.nuget.org/packages/KmsDev.MaxBot.LongPollingManager)
![License](https://img.shields.io/github/license/kmsmarik9/maxbot)


# Быстрый старт
Советуем ознакомиться с [KmsDev.MaxBot.Handlers](../KmsDev.MaxBot.Handlers/README.md)

### Подключение пакета
```
dotnet add package KmsDev.MaxBot.LongPollingManager
```

### Подключение менеджера
- Вызов `AddLongPollingManager` добавляет в DI интерфейс `IMaxBotManager`
- Можно использовать свою реализацию `IMaxBotMessageHandlerRunner` для обработки update messages
```csharp
using KmsDev.MaxBot.LongPollingManager;

//подключение основной системы
serviceCollections.AddMaxBotSystem(sc =>
{
    //подключение LongPolling
    sc.AddLongPollingManager();
});

//регистрация своего обработчика, если не используется `AddMaxBotMessageHandlerSystem`
serviceCollections.AddSingleton<IMaxBotMessageHandlerRunner, MyHandlersRunner>();
```

### Если в системе планируется использование 1 бота
```csharp
serviceCollections.AddMaxBotSystem(sc =>
{
    sc.AddMaxBotSingletonWithLongPolling((БОТ_ТОКЕН, HashSecretKey), new MyHandlersRouteBuilder());
});
```



Кастомная логика обработки update messages если не используется `AddMaxBotMessageHandlerSystem`
```csharp
//регистрация своего обработчика
serviceCollections.AddSingleton<IMaxBotMessageHandlerRunner, MyHandlersRunner>();

serviceCollections.AddMaxBotSystem(sc =>
{
    sc.AddMaxBotSingletonWithLongPolling((БОТ_ТОКЕН, HashSecretKey));
});
```
