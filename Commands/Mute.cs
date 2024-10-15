using CommandSystem;
using RemoteAdmin;
using System;
using Player = Exiled.API.Features.Player;

namespace SimpleTextChat.Commands
{
    [CommandHandler(typeof(ClientCommandHandler))]
    public class Mute : ICommand
    {
        public string Command => "mute";

        public string[] Aliases => Array.Empty<string>();

        public string Description => "Stummschaltet einen Spieler (oder hebt die Stummschaltung auf)."; // Übersetzt

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (sender is not PlayerCommandSender)
            {
                response = "Nein"; // Übersetzt
                return false;
            }

            Player player = Player.Get(sender);
            if (arguments.IsEmpty())
            {
                response = "Du hast keinen Benutzernamen eingegeben."; // Übersetzt
                return false;
            }

            var name = string.Join(" ", arguments);
            Player target = null;
            foreach (var ply in Player.List)
            {
                if (ply.Nickname == name)
                {
                    target = ply;
                    break;
                }

                if (ply.DisplayNickname != null && ply.DisplayNickname == name)
                {
                    target = ply;
                    break;
                }
            }

            if (target == null)
            {
                response = "Kein Spieler mit diesem Namen gefunden."; // Übersetzt
                return false;
            }

            response = "Fertig! Spieler wurde stummgeschaltet."; // Übersetzt
            if (!Plugin.Instance.MutedPlayers.TryGetValue(player.UserId, out var mutedPlayers))
            {
                Plugin.Instance.MutedPlayers.Add(player.UserId, new() { target.UserId });
                return true;
            }

            if (mutedPlayers.Contains(target.UserId))
            {
                Plugin.Instance.MutedPlayers[player.UserId].Remove(target.UserId);
                response = "Fertig! Spieler wurde entstummschaltet."; // Übersetzt
                return true;
            }

            Plugin.Instance.MutedPlayers[player.UserId].Add(target.UserId);
            return true;
        }
    }
}

// please note that all of this is old code, didnt bother for shit to fix it
// literally just ctrl c ctrl v
// ALL CREDITS TO warden161
