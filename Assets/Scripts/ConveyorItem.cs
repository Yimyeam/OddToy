using UnityEngine;

public enum ToyType
{
    Normal,
    Anomaly
}

public class ConveyorItem : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 1.5f;

    [SerializeField]
    private ToyType toyType;

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

    public ToyType GetToyType()
    {
        return toyType;
    }
}