using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TgBotInterpritationSearch.WorkWithCommands
{
    public class Sender
    {
        private ITelegramBotClient _botClient { get; set; }
        public Sender(ITelegramBotClient botClient)=>this._botClient = botClient;

        public async Task StartButton(Chat chat) => await _botClient.SendTextMessageAsync(chat.Id, "Привет, чем я могу тебе помочь?");

        public async Task AboutUsButton(Chat chat) => await _botClient.SendTextMessageAsync(chat.Id, "Interpritation Bot - бот для поиска определения из разных словарей и источников");
        public async Task SendWrongCommand(Chat chat) => await _botClient.SendTextMessageAsync(chat.Id, "Неверная команда");
    }
}
