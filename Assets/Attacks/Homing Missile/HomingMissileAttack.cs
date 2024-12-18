using UnityEngine;

[CreateAssetMenu(fileName = "HomingMissileAttack", menuName = "Attacks/Homing Missile Attack")]
public class HomingMissileAttack : Attack
{
    public GameObject homingMissilePrefab;

    public override void Use(GameObject owner)
    {
        if (Time.time - lastUsedTime >= GetStat(StatType.Cooldown))
        {
            lastUsedTime = Time.time;

            Vector3 direction;
            if (autoAim)
            {
                // Auto-aim towards the closest enemy
                Vector3 enemyDirection = HomingMissileBehaviour.FindClosestEnemyDirection(owner.transform, GetStat(StatType.MaxHomingDistance));
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

            // Instantiate the homing missile and initialize it with stats
            GameObject missile = Instantiate(homingMissilePrefab, owner.transform.position, Quaternion.identity);
            HomingMissileBehaviour missileBehaviour = missile.GetComponent<HomingMissileBehaviour>();
            missileBehaviour.Initialize(this, direction);
        }
    }
}
