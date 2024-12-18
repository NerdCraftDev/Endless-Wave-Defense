using UnityEngine;

[CreateAssetMenu(fileName = "FireballAttack", menuName = "Attacks/Fireball Attack")]
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
            GameObject fireball = Instantiate(projectilePrefab, owner.transform.position, Quaternion.identity);
            if (fireball.TryGetComponent<ProjectileShotBehaviour>(out var fireballBehaviour))
            {
                if (owner.TryGetComponent<Player>(out var player))
                {
                    fireballBehaviour.Initialize(this, direction, player.data);
                }
            }
        }
    }
}
