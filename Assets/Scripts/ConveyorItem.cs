using UnityEngine;

public class ConveyorItem : MonoBehaviour
{
    public float moveSpeed = 2f;
    void Start()
    {
        
    }

    void Update()
    {
        transform.position += Vector3.right * moveSpeed * Time.deltaTime;
    }
}
