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

    public void Initialize(float speed, int damage, int pierce, float lifespan, Vector3 direction, bool autoAim)
    {
        if (autoAim)
        {
            Vector3 closestEnemyDirection = FindClosestEnemyDirection(_transform);
            this.direction = closestEnemyDirection == Vector3.zero ? direction : closestEnemyDirection;
        }
        else
        {
            this.direction = direction;
        }

        this.speed = speed;
        this.damage = damage;
        this.pierce = pierce;
        Destroy(gameObject, lifespan);
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