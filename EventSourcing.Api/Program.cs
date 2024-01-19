using System.Reflection;
using System.Text.Json;
using EventSourcing;
using EventSourcing.Api.Models.InputModels;
using EventSourcing.Events.AccountEvents;
using EventSourcing.Framework;
using EventSourcing.Services;
using EventSourcing.Services.Framework;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<IEventStore, LiteDbEventStore>();
builder.Services.AddSingleton<ISnapshot, Snapshot>();
builder.Services.AddSingleton<IAccountService, AccountService>();
builder.Services.AddSingleton<EventSourcing.Framework.ISession, Session>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/{accountId}", async ([FromRoute] Guid accountId, IAccountService accountService) =>
{
    var account = await accountService.GetByIdAsync(accountId);
    return account;
})
.WithName("GetAccount");

app.MapGet("/{accountId}/events", async ([FromRoute] Guid accountId, IEventStore eventStore) =>
{
    Func<IEvent, string> toJson = @event =>
    {
        Assembly asm = Assembly.GetAssembly(typeof(AccountCreatedEvent))!;
        Type type = asm.GetType(@event.TypeFullName)!;
        return JsonSerializer.Serialize(@event, type);
    };

    var events = await eventStore.GetEventsAsync(accountId);
    return events.Select(x => toJson(x)).ToList();
})
.WithName("GetEvents");

app.MapPost("/create-account", async ([FromBody] CreateAccountInputModel model, IAccountService accountService) =>
{
    var accountId = await accountService.CreateAccountAsync(model.Name, model.InitialBalance);
    return accountId;
})
.WithName("CreateAccount");

app.MapPost("/deposit/{accountId}", async ([FromRoute] Guid accountId, [FromBody] DepositInputModel model, IAccountService accountService) =>
{
    var currentBalance = await accountService.DepositAsync(accountId, model.Amount);
    return currentBalance;
})
.WithName("Deposit");

app.MapPost("/withdraw/{accountId}", async ([FromRoute] Guid accountId, [FromBody] WithdrawInputModel model, IAccountService accountService) =>
{
    var currentBalance = await accountService.WithdrawAsync(accountId, model.Amount);
    return currentBalance;
})
.WithName("Withdraw");

app.MapPost("/update-name/{accountId}", async ([FromRoute] Guid accountId, [FromBody] UpdateNameInputModel model, IAccountService accountService) =>
{
    await accountService.UpdateNameAsync(accountId, model.AccountName);
})
.WithName("UpdateName");

app.Run();
