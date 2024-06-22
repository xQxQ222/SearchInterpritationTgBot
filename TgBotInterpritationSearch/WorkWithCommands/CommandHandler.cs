using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace TgBotInterpritationSearch.WorkWithCommands
{
    public class CommandHandler
    {
        private Dictionary<string, ICommand> commands { get; set; }=new Dictionary<string, ICommand>();
        public void SetCommand(string messageCommand,ICommand command) => commands[messageCommand]=command;
        public async Task Reply(string messageCommand,Chat chat)
        {
            if (!commands.ContainsKey(messageCommand))
                await commands[messageCommand].Execute(chat);
            else 
                return; 
        }
    }
}
