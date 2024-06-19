using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5.0f;
    [SerializeField] private Joystick joystick;

    public void HandleMovement()
    {
        float moveHorizontal =  joystick.Horizontal;
        float moveVertical =  joystick.Vertical;

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical) * moveSpeed * Time.deltaTime;
        transform.Translate(movement, Space.World);
    }
}