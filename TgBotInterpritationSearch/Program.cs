using System;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
namespace TgBot
{
    class Program
    {
        static async Task Main()
        {
            var botClient = new TelegramBotClient(Environment.GetEnvironmentVariable("TELEGRAM_BOT_TOCKEN"));
            var receiverOptions = new ReceiverOptions { AllowedUpdates = {}, };
            using var cts = new CancellationTokenSource();
            botClient.StartReceiving(UpdateHandler, ErrorHandler, receiverOptions, cts.Token);
            var myself = await botClient.GetMeAsync();
            Console.WriteLine($"{myself.FirstName} запущен!");
            Console.ReadLine();
        }
        private static async Task UpdateHandler(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(update));
            if (update.Type == UpdateType.Message)
            {
                var message = update.Message;
                var user = message.From;
                var chat = message.Chat;
                await botClient.SendTextMessageAsync(chat.Id, message.Text, replyToMessageId: message.MessageId);
                return;
            }
            else return;
        }

        private static Task ErrorHandler(ITelegramBotClient botClient, Exception error, CancellationToken cancellationToken)
        {
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(error));
            return Task.CompletedTask;
        }
    }
}