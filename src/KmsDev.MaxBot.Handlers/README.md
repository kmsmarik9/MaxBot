![.NET](https://img.shields.io/badge/.NET-8.0-purple)
[![NuGet](https://img.shields.io/nuget/v/KmsDev.MaxBot)](https://www.nuget.org/packages/KmsDev.MaxBot.LongPollingManager)
![License](https://img.shields.io/github/license/kmsmarik9/maxbot)

# Быстрый старт
### Подключение пакета
```
dotnet add package KmsDev.MaxBot.Handlers
```

## Подключение Route Handlers
```csharp
//подключение Route Handlers
serviceCollections.AddMaxBotMessageHandlerSystem(s =>
{ 
    //"default_routes" - handlers prefix
    //DefaultMaxBotHandlersRouteBuilder - реализация route handlers
    s.AddHandler("default_routes", new DefaultMaxBotHandlersRouteBuilder());
});

...
var maxBot = maxBotClientBuilder.Build(...);

//получение менеджера ботов
var maxBotManager = serviceProvider.GetRequiredService<IMaxBotManager>();

//добавление бота, вторым параметром указывается название Route Handlers
await maxBotManager.AddBotAsync(maxBot, "default_routes");
```

## Структура
- User State - хранение состояния между вызовами обработчиков
- User State по-умолчанию используется in-memory хранилище
- Handler - обработчик update message
- HandlersRouteBuilder - конструктор обработчиков

### TODO
- сделать отдельными пакетами хранение состояние в  Sqlite, Postgres и тд 

### User State
хранение информации между вызовами обработчиков
```csharp
public class DefaultRoutesUserState : IMaxBotMessageHandlerUserState
{
    public string TextForPublish { get; set; }
}
```

### Handlers 
```csharp
//обработчик для команды /start
public partial class StartHandler : MaxBotMessageHandlerBase<DefaultRoutesUserState>
{
    public override async Task RunAsync(string triggeredRoute)
    {
        //заполмнить текущий маршрут для активации дочернего обработчика 
        RequestAccessor.RouteContainer.PushRoutePath(triggeredRoute);
        await RequestAccessor.SaveStateChangesAsync();
    }
}

//обработчик для команды /child
public partial class ChildHandler : MaxBotMessageHandlerBase<DefaultRoutesUserState>
{
    public override async Task RunAsync(string triggeredRoute)
    {
        //очистить маршрут для доступа к корневым обработчикам
        RequestAccessor.RouteContainer.ClearPath();
        await RequestAccessor.SaveStateChangesAsync();
    }
}

//default обработчик, при любом действии
public partial class DefaultChildHandler : MaxBotMessageHandlerBase<DefaultRoutesUserState>
{
    public override async Task RunAsync(string triggeredRoute)
    {
        //отправить сообщение текущему Макс пользователю
        await RequestAccessor.RequestData.MaxBotClient.Api.Messages.SendMessageAsync(new SendMessageRequest
        {
            Target = (SendMessageRequest.TargetType.Chat, RequestAccessor.MaxUserId),
            Text = "Default message"
        });
    }
}
```

### HandlersRouteBuilder
```csharp
public class TestMaxBotHandlersRouteBuilder : MaxBotMessageHandlersRouteBuilder<DefaultRoutesUserState>
{
    public TestMaxBotHandlersRouteBuilder1()
    {
        AddCommandRoute<StartHandler>("start", cb =>
        {
            cb.AddCommandRoute<ChildHandler>("child");

            //default обработчик, при любом действии
            cb.AddRoute<DefaultChildHandler>("", p => true);
        });
    }
}
```

### Авторизация в Routes
- по-умолчанию используется allow-all авторизация
- можно установить кастомную авторизацию, общую на весь MaxBotMessageHandlersRouteBuilder или индивидуально для каждого route

пример:
```csharp
public class MyDefaultAuth : IMaxBotMessageHandlerAuth
{
    public Task<bool> AuthAsync(IMaxBotMessageHandlerRequestAccessor requestAccessor)
    {
        return Task.FromResult(true);
    }
}

public class AdminAuth : IMaxBotMessageHandlerAuth
{
    public Task<bool> AuthAsync(IMaxBotMessageHandlerRequestAccessor requestAccessor)
    {
        //проверка что Id пользователя соотвествует админу
        return Task.FromResult(requestAccessor.MaxUserId == 00000);
    }
}

//по-умолчанию обработчики вызываются для всех пользователей
public class TestMaxBotHandlersRouteBuilder2 : MaxBotMessageHandlersRouteBuilder<DefaultRoutesUserState, MyDefaultAuth>
{
    public TestMaxBotHandlersRouteBuilder2()
    {
        //но команда /admin разрешена только для определенных пользователей
        AddCommandRoute<AdminHandler, AdminAuth>("admin", cb =>
        {
            cb.AddCommandRoute<UsersHandler>("announcement");
        });
    }
}
```

Так же ознакомьтесь с [KmsDev.MaxBot.LongPollingManager](../KmsDev.MaxBot.LongPollingManager/README.md)

