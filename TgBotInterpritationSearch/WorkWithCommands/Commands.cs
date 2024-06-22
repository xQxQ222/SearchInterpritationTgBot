using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace TgBotInterpritationSearch.WorkWithCommands
{
    public class StartCommand : Command,ICommand
    {
        public StartCommand(Sender sender):base(sender) { }
        public async Task Execute(Chat chat)
        {
            await Sender.StartButton(chat);
        }
    }
    public class AboutUsCommand:Command,ICommand
    {
        public AboutUsCommand(Sender sender):base (sender) { }

        public async Task Execute(Chat chat)
        {
            await Sender.AboutUsButton(chat);
        }
    }
    public class WrongCommand:Command,ICommand
    {
        public WrongCommand(Sender sender):base(sender) { }

        public async Task Execute(Chat chat)
        {
            await Sender.SendWrongCommand(chat);
        }
    }
}
