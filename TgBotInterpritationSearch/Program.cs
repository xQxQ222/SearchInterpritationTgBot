using Microsoft.Extensions.Configuration;
using System;
using System.Security.Authentication;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TgBotInterpritationSearch.Configuration;
using TgBotInterpritationSearch.WorkWithCommands;
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
            var token = Configuration.BotSettings.BotTocken;

            if (string.IsNullOrEmpty(token))
                return;

            var botClient = new TelegramBotClient(token);

            var receiverOptions = new ReceiverOptions { AllowedUpdates = { }, };
            var cts = new CancellationTokenSource();

            botClient.StartReceiving(UpdateHandler, ErrorHandler, receiverOptions, cts.Token);
            
            Sender sender = new Sender(botClient);
            cmndhandler.SetCommand("/start", new StartCommand(sender));
            cmndhandler.SetCommand("О нас", new AboutUsCommand(sender));
            cmndhandler.SetCommand("",new WrongCommand(sender));

            var myself = await botClient.GetMeAsync();

            Console.WriteLine($"{myself.FirstName} запущен!");
            Console.ReadLine();
        }
        private static CommandHandler cmndhandler = new CommandHandler();
        private static async Task UpdateHandler(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(update));

            if (update.Type != UpdateType.Message)  
                return;

            var message = update.Message;

            await cmndhandler.Reply(message.Text, message.Chat);      
        }

        private static Task ErrorHandler(ITelegramBotClient botClient, Exception error, CancellationToken cancellationToken)
        {
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(error));
            return Task.CompletedTask;
        }
    }
}