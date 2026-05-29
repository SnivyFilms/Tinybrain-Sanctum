using System.ComponentModel;
using Exiled.API.Interfaces;

namespace EveryoneVsScps
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; } = false;
        [Description("Allows friendly fire to teammates if the server is normally friendly fire true, doesnt do anything if the server has friendly fire off.")]
        public bool AllowFriendlyFire { get; set; } = false;
        [Description("If true, this plugin will always run, otherwise it will need a command ran each round.")]
        public bool AlwaysActive { get; set; } = false;
    }
}

