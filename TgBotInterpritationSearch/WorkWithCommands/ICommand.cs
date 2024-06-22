using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TgBotInterpritationSearch.WorkWithCommands
{
    public interface ICommand
    {
        public Task Execute(Chat chat);
    }
    public abstract class Command
    {
        protected Sender Sender { get; set; }

        public Command(Sender sender)=> Sender = sender;
    }
}
