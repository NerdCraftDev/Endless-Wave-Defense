using UnityEngine;

[CreateAssetMenu(fileName = "FireballAttack", menuName = "Attacks/Fireball")]
public class FireballAttack : Attack
{
    public float cooldown;
    public int pierce;
    public int lifespan;
    public float speed;
    public GameObject fireballPrefab;

    public override void Use(GameObject owner)
    {
        if (Time.time < lastUsedTime + cooldown) return;

        FireballBehaviour fireball = Instantiate(fireballPrefab, owner.transform.position, Quaternion.identity).GetComponent<FireballBehaviour>();
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePosition - owner.transform.position).normalized;
        fireball.Initialize(speed, damage, pierce, lifespan, direction, false);

        lastUsedTime = Time.time;
    }
}
