using UnityEngine;

public class ProjectileCircleBehaviour : MonoBehaviour
{
    private float speed;
    private int damage;
    private int pierce;
    private float radius;
    private Transform owner;
    private float angleStep;
    private float currentAngle;

    void Update()
    {
        // Calculate the new position based on the current angle
        float posX = owner.position.x + Mathf.Cos(currentAngle) * radius;
        float posY = owner.position.y + Mathf.Sin(currentAngle) * radius;
        transform.position = new Vector3(posX, posY, 0f);

        // Rotate the projectile around the owner
        currentAngle += speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Enemy enemy))
        {
            enemy.Damage(damage);
        }
    }

    public void Initialize(Attack attack, Transform owner, float radius, int index, int totalProjectiles)
    {
        speed = attack.GetStat(StatType.Speed);
        damage = (int)attack.GetStat(StatType.Damage);
        pierce = (int)attack.GetStat(StatType.Pierce);

        this.owner = owner;
        this.radius = radius;

        // Calculate the initial angle for this projectile
        angleStep = 2 * Mathf.PI / totalProjectiles;
        currentAngle = index * angleStep;
    }
}
