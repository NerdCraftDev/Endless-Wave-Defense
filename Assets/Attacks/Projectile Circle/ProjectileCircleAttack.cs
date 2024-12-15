using UnityEngine;

[CreateAssetMenu(fileName = "ProjectileCircleAttack", menuName = "Attacks/Projectile Circle")]
public class ProjectileCircleAttack : Attack
{
    public float speed;
    public float radius;
    public int projectileCount;
    
    public GameObject projectilePrefab;

    public override void Initialize(GameObject owner)
    {

        for (int i = 0; i < projectileCount; i++)
        {
            ProjectileCircleBehaviour projectile = Instantiate(projectilePrefab, owner.transform.position, Quaternion.identity).GetComponent<ProjectileCircleBehaviour>();
            projectile.Initialize(speed, radius, damage, owner.transform, projectileCount, i);
        }
    }
}