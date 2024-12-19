using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ProjectileCircleAttack", menuName = "Attacks/Projectile Circle Attack")]
public class ProjectileCircleAttack : Attack
{
    public GameObject projectilePrefab;
    private List<GameObject> projectiles = new List<GameObject>();

    public override void Initialize(GameObject owner)
    {
        CalculateProjectiles(owner);
    }

    public void CalculateProjectiles(GameObject owner)
    {
        if (owner.TryGetComponent<Player>(out var player))
        {
            int projectileCount = (int)(GetStat(StatType.ProjectileCount) + player.data.GetStatValue(StatType.ProjectileCount));

            // Destroy existing projectiles
            foreach (var proj in projectiles)
            {
                Destroy(proj);
            }
            projectiles.Clear();

            // Recreate projectiles with updated count
            for (int i = 0; i < projectileCount; i++)
            {
                GameObject proj = Instantiate(projectilePrefab, owner.transform.position, Quaternion.identity, owner.transform);
                ProjectileCircleBehaviour projBehaviour = proj.GetComponent<ProjectileCircleBehaviour>();
                projBehaviour.Initialize(this, owner.transform, i, projectileCount, player.data);
                projectiles.Add(proj);
            }
        }
    }
}