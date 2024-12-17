using UnityEngine;

[CreateAssetMenu(fileName = "Attack Upgrade", menuName = "Upgrades/Attack Upgrade")]
public class AttackUpgrade : Upgrade
{
    public Attack attack;

    public override void ApplyUpgrade(GameObject target)
    {
        Debug.Log($"Applying {upgradeName} to {target.name}");
        if (target.TryGetComponent(out Player player))
        {
            player.AddAttack(attack);
        }
    }
}
