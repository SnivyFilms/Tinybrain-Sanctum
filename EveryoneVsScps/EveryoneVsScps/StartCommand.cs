using System;
using CommandSystem;
using Exiled.Permissions.Extensions;

namespace EveryoneVsScps
{
    public class StartCommand : ICommand
    {
        public string Command { get; } = "StartEVS";
        public string[] Aliases { get; } = new[] { "StartEveryoneVsScps", "StartEveryoneVsSCPs", "EVS" };
        public string Description { get; } = "Starts everyone vs scps";
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!sender.CheckPermission("sve.start"))
            {
                response = "You do not have the required permission to use this command";
                return false;
            }

            if (!Plugin.Instance.IsActiveForRound)
            {
                response = "Started Everyone VS SCPs";
                Plugin.Instance.IsActiveForRound = true;
            }
            else
            {
                response = "Stopped Everyone VS SCPs";
                Plugin.Instance.IsActiveForRound = false;
            }
            return true;
        }
    }
}