using UnityEngine;

public class DoorTriggers : MonoBehaviour
{
    private bool playerInRange = false;
    public Door door;

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            door.Interact();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        playerInRange = other.CompareTag("Player");

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }

    }

}
