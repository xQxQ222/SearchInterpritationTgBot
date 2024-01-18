using Microsoft.Extensions.Configuration;
using System;
using System.Security.Authentication;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TgBotInterpritationSearch.Configuration;
namespace TgBot
{
    class Program
    {
        static async Task Main()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json") 
                .AddEnvironmentVariables()     
                .Build();
            Configuration.SetProperties(config);
            var botClient = new TelegramBotClient(Configuration.BotSettings.BotTocken);
            var receiverOptions = new ReceiverOptions { AllowedUpdates = {}, };
            var cts = new CancellationTokenSource();
            botClient.StartReceiving(UpdateHandler, ErrorHandler, receiverOptions, cts.Token);
            var myself = await botClient.GetMeAsync();
            Console.WriteLine($"{myself.FirstName} запущен!");
            Console.ReadLine();
        }
        private static async Task UpdateHandler(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(update));
            if (update.Type != UpdateType.Message)  return;
            var message = update.Message;
            await botClient.SendTextMessageAsync(message.Chat.Id, message.Text, replyToMessageId: message.MessageId);
        }

        private static Task ErrorHandler(ITelegramBotClient botClient, Exception error, CancellationToken cancellationToken)
        {
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(error));
            return Task.CompletedTask;
        }
    }
}