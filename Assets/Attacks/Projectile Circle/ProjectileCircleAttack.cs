using UnityEngine;

[CreateAssetMenu(fileName = "ProjectileCircleAttack", menuName = "Attacks/Projectile Circle Attack")]
public class ProjectileCircleAttack : Attack
{
    public GameObject projectilePrefab;

    public override void Initialize(GameObject owner)
    {
        int projectileCount = (int)GetStat(StatType.ProjectileCount);
        float radius = GetStat(StatType.Radius);

        for (int i = 0; i < projectileCount; i++)
        {
            GameObject proj = Instantiate(projectilePrefab, owner.transform.position, Quaternion.identity, owner.transform);
            ProjectileCircleBehaviour projBehaviour = proj.GetComponent<ProjectileCircleBehaviour>();
            projBehaviour.Initialize(this, owner.transform, radius, i, projectileCount);
        }
    }
}