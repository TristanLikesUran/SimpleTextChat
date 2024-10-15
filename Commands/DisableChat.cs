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
    public class DisableChat : ICommand
    {
        public string Command => "disablechat";

        public string[] Aliases => Array.Empty<string>();

        public string Description => "Deaktiviert den Text-Chat vollständig."; // Übersetzt

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (sender is not PlayerCommandSender)
            {
                response = "Nein"; // Übersetzt
                return false;
            }

            Player player = Player.Get(sender);

            if (!Plugin.Instance.DisabledTextChat.Contains(player.UserId))
            {
                Plugin.Instance.DisabledTextChat.Remove(player.UserId);

                response = "Text-Chat wieder aktiviert!"; // Übersetzt
                return true;
            }

            Plugin.Instance.DisabledTextChat.Add(player.UserId);
            response = "Text-Chat deaktiviert!"; // Übersetzt
            return true;
        }
    }
}

// please note that all of this is old code, didnt bother for shit to fix it
// literally just ctrl c ctrl v
// ALL CREDITS TO warden161
