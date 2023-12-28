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
            var botClient = new TelegramBotClient("6739860750:AAE2_cJKOsBoew7VtaES0saW30XdkXHcdkE");
            var receiverOptions = new ReceiverOptions { AllowedUpdates = new[] { UpdateType.Message }, ThrowPendingUpdates = true };
            using var cts = new CancellationTokenSource();
            botClient.StartReceiving(UpdateHandler, ErrorHandler, receiverOptions, cts.Token);
            var myself = await botClient.GetMeAsync();
            Console.WriteLine($"{myself.FirstName} запущен!");
            await Task.Delay(-1);
        }
        private static async Task UpdateHandler(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            try
            {
                switch (update.Type)
                {
                    case UpdateType.Message:
                    {
                        var message = update.Message;
                        var user = message.From;
                        Console.WriteLine($"{user.FirstName} ({user.Id}) написал сообщение: {message.Text}");
                        var chat = message.Chat;
                        await botClient.SendTextMessageAsync(chat.Id,message.Text,replyToMessageId: message.MessageId);
                        return;
                    }
                }
            }
            catch (Exception ex)
            {Console.WriteLine(ex.ToString());}
        }

        private static Task ErrorHandler(ITelegramBotClient botClient, Exception error, CancellationToken cancellationToken)
        {
            var ErrorMessage = error switch
            {ApiRequestException apiRequestException=> $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}", _ => error.ToString()};
            Console.WriteLine(ErrorMessage);
            return Task.CompletedTask;
        }
    }
}