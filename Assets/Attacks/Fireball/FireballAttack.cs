using UnityEngine;

[CreateAssetMenu(fileName = "FireballAttack", menuName = "Attacks/Fireball Attack")]
public class FireballAttack : Attack
{
    public GameObject fireballPrefab;

    public override void Use(GameObject owner)
    {
        if (Time.time - lastUsedTime >= GetStat(StatType.Cooldown))
        {
            lastUsedTime = Time.time;

            Vector3 direction;
            if (autoAim)
            {
                // Auto-aim towards the closest enemy
                Vector3 enemyDirection = FireballBehaviour.FindClosestEnemyDirection(owner.transform);
                direction = enemyDirection != Vector3.zero ? enemyDirection.normalized : owner.transform.right;
            }
            else
            {
                // Get the mouse position in world space
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePos.z = 0f; // Ensure z is zero for 2D

                // Calculate direction from player to mouse
                direction = (mousePos - owner.transform.position).normalized;
            }

            // Instantiate the fireball and initialize it with stats
            GameObject fireball = Instantiate(fireballPrefab, owner.transform.position, Quaternion.identity);
            FireballBehaviour fireballBehaviour = fireball.GetComponent<FireballBehaviour>();
            fireballBehaviour.Initialize(this, direction);
        }
    }
}
