using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    private Vector2 velocity;
    private void Update()
    {
        // Move Player
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector2 movement = new Vector2(horizontal, vertical).normalized;
        Move(movement * (Time.deltaTime * speed));
        Move(velocity * Time.deltaTime, isVelocity:true);
    }

    public void Move(Vector2 movement, bool isVelocity = false, bool setVelocity = false)
    {
        // Move the player
        transform.Translate(movement);
        if (setVelocity) { velocity = movement; }
        if (isVelocity) { velocity -= movement; }
    }

    public void SetSpeed(float speed)
    {
        // Set the speed of the player
        this.speed = speed;
    }
}
