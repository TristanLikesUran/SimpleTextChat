using System;
using CommandSystem;
using Exiled.API.Features;
using RemoteAdmin;
using Player = Exiled.API.Features.Player;

namespace SimpleTextChat.Commands
{
    [CommandHandler(typeof(ClientCommandHandler))]
    public class Chathilfe : ICommand
    {
        public string Command => "chathilfe";
        public string[] Aliases => new string[] { "helpchat" }; // Optional: andere Namen für den Befehl
        public string Description => "Zeigt eine Liste der verfügbaren Chatbefehle an."; // Beschreibung des Befehls

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (sender is not PlayerCommandSender)
            {
                response = "Dieser Befehl kann nur von Spielern ausgeführt werden.";
                return false;
            }

            // Nachricht, die die verfügbaren Befehle beschreibt
            response =
                "Verfügbare Chatbefehle:\n" +
                ".scpchat Nachricht - Sendet eine Nachricht im SCP-Chat.\n" +
                ".chat Nachricht - Sendet eine Nachricht im Proximity-Chat.\n" +
                ".mute Spielername - Stummschaltet einen Spieler.\n" +
                ".disablechat - Deaktiviert den Text-Chat.\n" +
                "Nutze diese Befehle, um die Chatfunktionalität zu steuern!";
            return true;
        }
    }
}
