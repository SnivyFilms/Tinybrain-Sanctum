using Exiled.API.Enums;
using Exiled.Events.EventArgs.Player;

namespace EveryoneVsScps
{
    public class EventHandler
    {
        public Plugin Plugin;
        public EventHandler(Plugin plugin) => Plugin = plugin;

        public void OnHurting(HurtingEventArgs ev)
        {
            if (ev.Player == null || ev.Attacker == null || ev.Attacker == ev.Player)
                return;
            
            if (Plugin.Instance.Config.AlwaysActive || Plugin.Instance.IsActiveForRound)
            {
                if (Plugin.Instance.Config.AllowFriendlyFire && ev.Player.Role.Team == ev.Attacker.Role.Team)
                    return;
                if (ev.Player.Role.Side != Side.Scp)
                {
                    ev.IsAllowed = false;
                }
            }
        }

        public void OnRoundStarted()
        {
            if (!Plugin.Instance.Config.AlwaysActive && Plugin.Instance.IsActiveForRound)
                Plugin.Instance.IsActiveForRound = false;
        }

        public void OnHandcuffing(HandcuffingEventArgs ev)
        {
            if (ev.Player == null || ev.Target == null || ev.Target == ev.Player)
                return;

            if (Plugin.Instance.Config.AlwaysActive || Plugin.Instance.IsActiveForRound)
                ev.IsAllowed = false;
        }
    }
}