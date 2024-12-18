using UnityEngine;

public class ProjectileShotBehaviour : MonoBehaviour
{
    private Vector3 direction;
    private float speed = 5f; // Default speed
    private int damage = 10; // Default damage
    private int pierce = 1; // Default pierce
    private float rotationSpeed;
    private float maxDistance;

    private Transform _transform;

    void Awake()
    {
        _transform = transform;
    }

    void Update()
    {
        Vector2 targetDirection = FindClosestEnemyDirection(_transform, maxDistance);
        if (targetDirection != Vector2.zero)
        {
            float step = rotationSpeed * Time.deltaTime;
            Vector2 newDirection = Vector2.Lerp(_transform.right, targetDirection, step).normalized;
            float angle = Mathf.Atan2(newDirection.y, newDirection.x) * Mathf.Rad2Deg;
            _transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            _transform.position += (Vector3)newDirection * (Time.deltaTime * speed);
        }
        else
        {
            _transform.position += direction * (Time.deltaTime * speed);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Enemy enemy))
        {
            enemy.Damage(damage);
            pierce--;
            if (pierce <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    public void Initialize(Attack attack, Vector3 direction, PlayerData playerData)
    {
        speed = attack.GetStat(StatType.Speed) + playerData.GetStatValue(StatType.Speed);
        damage = (int)(attack.GetStat(StatType.Damage) + playerData.GetStatValue(StatType.Damage));
        pierce = (int)attack.GetStat(StatType.Pierce) + (int)playerData.GetStatValue(StatType.Pierce);
        float lifespan = attack.GetStat(StatType.Lifespan) + playerData.GetStatValue(StatType.Lifespan);
        rotationSpeed = attack.GetStat(StatType.RotationSpeed) + playerData.GetStatValue(StatType.RotationSpeed);
        maxDistance = attack.GetStat(StatType.MaxHomingDistance) + playerData.GetStatValue(StatType.MaxHomingDistance);
        Destroy(gameObject, lifespan);

        // Set the initial rotation to face the given direction
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        this.direction = direction;
    }

    public static Vector3 FindClosestEnemyDirection(Transform origin, float maxDistance = 100f)
    {
        Transform closest = null;
        float closestDistanceSqr = maxDistance * maxDistance;

        foreach (var enemy in EnemySpawner.Instance.enemies)
        {
            if (enemy != null)
            {
                float distanceSqr = (enemy.transform.position - origin.position).sqrMagnitude;
                if (distanceSqr < closestDistanceSqr)
                {
                    closestDistanceSqr = distanceSqr;
                    closest = enemy.transform;
                }
            }
        }

        return closest != null ? (closest.position - origin.position).normalized : Vector2.zero;
    }
}