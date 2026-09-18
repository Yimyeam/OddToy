using UnityEngine;

public class ExitZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        ConveyorItem item = other.GetComponent<ConveyorItem>();

        if (item == null)
            return;

        Destroy(item.gameObject);
    }
}