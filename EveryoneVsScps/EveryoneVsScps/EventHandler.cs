using System.Linq;
using Exiled.API.Features;
using Exiled.API.Enums;
using Exiled.Events.EventArgs.Player;
using Exiled.Events.EventArgs.Server;

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
            if (ev.Attacker.Role.Side == Side.Scp)
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

        public void OnRoundEnding(EndingRoundEventArgs ev)
        {
            if (!(Plugin.Instance.Config.AlwaysActive || Plugin.Instance.IsActiveForRound))
                return;

            var alive = Player.List.Where(p => p.IsAlive).ToList();

            if (alive.Count == 0)
                return;

            var allScps = alive.All(p => p.Role.Side == Side.Scp);
            var allNonScps = alive.All(p => p.Role.Side != Side.Scp);

            if (allScps)
            {
                ev.IsAllowed = true;
                ev.LeadingTeam = LeadingTeam.Anomalies;
                Round.EndRound();
                return;
            }
            if (allNonScps)
            {
                ev.IsAllowed = true;
                ev.LeadingTeam = LeadingTeam.FacilityForces;
                Round.EndRound();
                return;
            }
            ev.IsAllowed = false;
        }
    }
}