using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "ProjectileShotAttack", menuName = "Attacks/Projectile Shot Attack")]
public class ProjectileShotAttack : Attack
{
    public GameObject projectilePrefab;

    public override void Use(GameObject owner)
    {
        if (Time.time - lastUsedTime >= GetStat(StatType.Cooldown))
        {
            lastUsedTime = Time.time;

            Vector3 direction;
            // Get the mouse position in world space
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0f; // Ensure z is zero for 2D

            // Calculate direction from player to mouse
            Vector3 mouseDirection = (mousePos - owner.transform.position).normalized;
            if (autoAim)
            {
                // Auto-aim towards the closest enemy
                Vector3 enemyDirection = ProjectileShotBehaviour.FindClosestEnemyDirection(owner.transform);
                direction = enemyDirection != Vector3.zero ? enemyDirection.normalized : mouseDirection;
            }
            else
            {
                direction = mouseDirection;
            }

            // Instantiate the fireball and initialize it with stats
            if (owner.TryGetComponent<Player>(out var player))
            {
                int projectileCount = (int)(GetStat(StatType.ProjectileCount) + player.data.GetStatValue(StatType.ProjectileCount));

                GameObject fireball = Instantiate(projectilePrefab, owner.transform.position, Quaternion.identity);
                if (fireball.TryGetComponent<ProjectileShotBehaviour>(out var fireballBehaviour))
                {
                    fireballBehaviour.Initialize(this, direction, player.data);
                }
                SpawnExtraProjectiles(projectileCount-1, direction, owner, player);
            }
        }
    }

    private IEnumerator SpawnExtraProjectiles(int projectileCount, Vector3 direction, GameObject owner, Player player) {
        yield return new WaitForSeconds(0.1f);
        float angleStep = 15f;
        float startAngle = -(projectileCount - 1) / 2f * angleStep;
        for (int i = 0; i < projectileCount; i++)
        {
            float angle = startAngle + i * angleStep;
            Vector3 spreadDirection = Quaternion.Euler(0, 0, angle) * direction;

            GameObject fireball = Instantiate(projectilePrefab, owner.transform.position, Quaternion.identity);
            if (fireball.TryGetComponent<ProjectileShotBehaviour>(out var fireballBehaviour))
            {
                fireballBehaviour.Initialize(this, spreadDirection, player.data);
            }
        }
    }
}
