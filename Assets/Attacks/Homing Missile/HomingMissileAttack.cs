using UnityEngine;

[CreateAssetMenu(fileName = "HomingMissileAttack", menuName = "Attacks/Homing Missile")]
public class HomingMissileAttack : Attack
{
    public float cooldown;
    public int pierce;
    public int lifespan;
    public float speed;
    public float rotationSpeed;

    public GameObject missilePrefab;

    public override void Use(GameObject owner)
    {
        if (Time.time < lastUsedTime + cooldown) return;

        HomingMissileBehaviour missile = Instantiate(missilePrefab, owner.transform.position, Quaternion.identity, owner.transform).GetComponent<HomingMissileBehaviour>();
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePosition - owner.transform.position).normalized;
        missile.Initialize(speed, rotationSpeed, damage, pierce, lifespan, direction);

        lastUsedTime = Time.time;
    }
}
