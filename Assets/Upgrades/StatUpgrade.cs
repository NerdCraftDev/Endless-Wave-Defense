using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "StatUpgrade", menuName = "Upgrades/Stat Upgrade")]
public class StatUpgrade : Upgrade
{
    public List<AttackStat> stats = new List<AttackStat>();

    public override void ApplyUpgrade(GameObject target)
    {
        base.ApplyUpgrade(target);
        if (target.TryGetComponent<Player>(out var player))
        {
            foreach (var stat in stats)
            {
                player.IncreaseStat(stat.statType, stat.value);
            }
        }
    }
}
