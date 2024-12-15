using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 1f;
    public int health = 1;
    private Transform target;

    private void Update()
    {
        // Move towards the player
        Vector2 direction = (target.position - transform.position).normalized;
        Move(direction * (Time.deltaTime * speed));
    }

    public void Move(Vector2 direction)
    {
        // Move the enemy
        transform.Translate(direction);
    }

    public void SetTarget(Transform target)
    {
        // Set the target of the enemy
        this.target = target;
    }

    public void Damage(int damage)
    {
        // Reduce the enemy's health by damage
        health -= damage;
        // If the enemy's health is less than or equal to 0, call Die()
        if (health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        // Destroy the enemy
        Destroy(gameObject);
        EnemySpawner.Instance.enemies.Remove(this);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the enemy collided with the player
        if (other.TryGetComponent<Player>(out var player))
        {
            player.Damage(1);
        }
    }
}