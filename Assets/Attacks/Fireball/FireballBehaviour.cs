using UnityEngine;

public class FireballBehaviour : MonoBehaviour
{
    private Vector3 direction;
    private float speed = 5f; // Default speed
    private int damage = 10; // Default damage
    private int pierce = 1; // Default pierce

    private Transform _transform;

    void Awake()
    {
        _transform = transform;
    }

    void Update()
    {
        _transform.position += direction.normalized * (Time.deltaTime * speed);
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

    public void Initialize(Attack attack, Vector3 direction)
    {
        speed = attack.GetStat(StatType.Speed);
        damage = (int)attack.GetStat(StatType.Damage);
        pierce = (int)attack.GetStat(StatType.Pierce);
        float lifespan = attack.GetStat(StatType.Lifespan);
        Destroy(gameObject, lifespan);

        this.direction = direction.normalized;

        // Set the initial rotation to face the given direction
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    public static Vector3 FindClosestEnemyDirection(Transform origin)
    {
        Transform closest = null;
        float closestDistance = float.MaxValue;

        foreach (var enemy in EnemySpawner.Instance.enemies)
        {
            float distance = Vector3.Distance(origin.position, enemy.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closest = enemy.transform;
            }
        }

        if (closest != null)
        {
            return closest.position - origin.position;
        }
        else
        {
            return Vector3.zero; // No enemies found
        }
    }
}