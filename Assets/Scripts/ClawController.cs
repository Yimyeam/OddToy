using UnityEngine;
using UnityEngine.InputSystem;

public class ClawController : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 5f;

    [SerializeField]
    private float leftLimit = -5f;

    [SerializeField]
    private float rightLimit = 5f;

    void Start()
    {

    }

    void Update()
    {
        if (Keyboard.current.dKey.isPressed)
            MoveRight();

        if (Keyboard.current.aKey.isPressed)
            MoveLeft();
    }

    private void MoveRight()
    {
        transform.position += Vector3.right * moveSpeed * Time.deltaTime;

        if (transform.position.x > rightLimit)
            transform.position = new Vector3(rightLimit, transform.position.y, transform.position.z);
    }

    private void MoveLeft()
    {
        transform.position += Vector3.left * moveSpeed * Time.deltaTime;

        if (transform.position.x < leftLimit)
            transform.position = new Vector3(leftLimit, transform.position.y, transform.position.z);
    }
}