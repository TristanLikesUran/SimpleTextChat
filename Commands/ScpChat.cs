using System;
using System.Linq;
using System.Collections.Generic;

using CommandSystem;
using RemoteAdmin;
using UnityEngine;
using PlayerStatsSystem;
using Player = Exiled.API.Features.Player;
using Exiled.API.Enums;
using Exiled.API.Features;

namespace SimpleTextChat.Commands
{
    [CommandHandler(typeof(ClientCommandHandler))]
    public class ScpChat : ICommand
    {
        public string Command => "scpchat";

        public string[] Aliases => new string[] { "schat" };

        public string Description => "Sendet eine Nachricht.";


        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (sender is not PlayerCommandSender)
            {
                response = "Muss vom Client gesendet werden";
                return false;
            }

            if (arguments.Count < 1)
            {
                response = "Benutzung des Befehls: .scpchat <Nachricht>";
                return false;
            }

            var message = string.Join(" ", arguments);
            Player player = Player.Get(sender);

            if (player.IsMuted)
            {
                response = "Du bist stummgeschaltet.";
                return false;
            }

            if (!player.IsScp)
            {
                response = "\"Du bist kein SCP.";
                return false;
            }

            float duration = 3;
            if (message.Length > Plugin.Instance.Config.MaxCharacters)
            {
                response = $"Zu lange Nachricht (überschreitet {Plugin.Instance.Config.MaxCharacters} Zeichen).";
                return false;
            }

            if (message.Length > 15)
                duration = Plugin.Instance.Config.MessageDuration; // Verwende die konfigurierbare Dauer

            var display = $"<b><color={player.Role.Color.ToHex()}>{player.Nickname}</color></b> (<color={player.Role.Color.ToHex()}>{player.Role.Name}</color>)\n";

            foreach (Player scp in Player.Get(Side.Scp))
            {
                if (Plugin.Instance.HasPlayerMuted(scp, player))
                    continue;

                scp.ShowHint($"[<b><color=red>SCP Chat</color></b>]\n{display}\n{message.Replace("<size", "")}\n<size=15>Du kannst einen Spieler mit <color=red>.mute Spielernamen</color> in deiner Konsole oder <color=red>.disablechat</color> stummschalten</size>", duration);
            }

            response = "Gesendet!";
            return true;
        }
    }
}

// please note that all of this is old code, didnt bother for shit to fix it
// literally just ctrl c ctrl v
