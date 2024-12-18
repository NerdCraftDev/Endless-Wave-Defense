using UnityEngine;

public class HomingMissileBehaviour : MonoBehaviour
{
    private float speed = 5f; // Default speed
    private float rotationSpeed = 20f; // Default rotation speed
    private int damage = 10; // Default damage
    private int pierce = 1; // Default pierce
    public float maxDistance = 100f; // Default max distance to search for enemies

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
            _transform.position += _transform.right * (Time.deltaTime * speed);
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

    public void Initialize(float speed, float rotationSpeed, int damage, int pierce, float lifespan, Vector3 direction, float maxHomingDistance)
    {
        this.speed = speed;
        this.rotationSpeed = rotationSpeed;
        this.damage = damage;
        this.pierce = pierce;
        maxDistance = maxHomingDistance;
        Destroy(gameObject, lifespan);

        // Set the initial rotation to face the given direction
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        _transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    public static Vector3 FindClosestEnemyDirection(Transform origin, float maxDistance = 100f)
    {
        Transform closest = null;
        float closestDistance = float.MaxValue;

        foreach (var enemy in EnemySpawner.Instance.enemies)
        {
            float distance = Vector3.Distance(origin.position, enemy.transform.position);
            if (distance < closestDistance && distance <= maxDistance)
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
            return Vector3.zero; // No enemies found within maxDistance
        }
    }
}
