using UnityEngine;

public class ConveyorItem : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 1.5f;

    void Start()
    {

    }

    void Update()
    {
        MoveItem();
    }

    private void MoveItem()
    {
        transform.position += Vector3.right * moveSpeed * Time.deltaTime;
    }
}