using UnityEngine;

public class ProjectileCircleBehaviour : MonoBehaviour
{
    private float speed = 5f; // Default speed
    private float radius = 5f; // Default radius
    private int damage = 10; // Default damage
    private Transform origin;
    private Transform _transform;
    private float angle;

    void Awake()
    {
        _transform = transform;
    }

    void Update()
    {
        if (origin != null)
        {
            angle += speed * Time.deltaTime;
            float x = origin.position.x + Mathf.Cos(angle) * radius;
            float y = origin.position.y + Mathf.Sin(angle) * radius;
            _transform.position = new Vector3(x, y, _transform.position.z);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Enemy enemy))
        {
            enemy.Damage(damage);
        }
    }

    public void Initialize(float speed, float radius, int damage, Transform origin, int projectileCount, int index)
    {
        this.speed = speed;
        this.radius = radius;
        this.damage = damage;
        this.origin = origin;
        angle = Mathf.Deg2Rad * (360f / projectileCount * index); // Convert degrees to radians
    }
}
