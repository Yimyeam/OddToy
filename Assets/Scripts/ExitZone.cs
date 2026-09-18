using UnityEngine;

public class ExitZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        ConveyorItem item = other.GetComponent<ConveyorItem>();

        if (item == null)
            return;

        if (item.GetToyType() == ToyType.Normal)
        {
            GameManager.instance.AddScore(1);
        }
        else
        {
            GameManager.instance.AddScore(-3);
        }

        Destroy(item.gameObject);
    }
}