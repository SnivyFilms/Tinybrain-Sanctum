using System;
using Exiled.API.Features;
using Player = Exiled.Events.Handlers.Player;
using Server = Exiled.Events.Handlers.Server;

namespace EveryoneVsScps
{
    public class Plugin : Plugin<Config>
    {
        public static Plugin Instance;
        public override string Name { get; } = "Custom Lights";
        public override string Prefix { get; } = "CustomLights";
        public override string Author { get; } = "Vicious Vikki";
        public override Version Version { get; } = new Version(1, 0, 1);
        public override Version RequiredExiledVersion { get; } = new Version(9, 13, 3);
        public EventHandler EventHandler;
        public bool IsActiveForRound = false;
        public override void OnEnabled()
        {
            Instance = this;
            EventHandler = new EventHandler(this);
            Player.Hurting += EventHandler.OnHurting;
            Server.RoundStarted += EventHandler.OnRoundStarted;
            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            Player.Hurting -= EventHandler.OnHurting;
            Server.RoundStarted -= EventHandler.OnRoundStarted;
            IsActiveForRound = false;
            EventHandler = null;
            Instance = null;
            base.OnDisabled();
        }
    }
}