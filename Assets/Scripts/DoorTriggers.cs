using UnityEngine;

public class DoorTriggers : MonoBehaviour
{
    private bool playerInRange = false;
    private FPController player;
    public Door door;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<FPController>();
        
    }
    private void Update()
    {
        if (playerInRange && DoorIsInteractable())
        {
            if (Input.GetKeyDown(player.interactKey) || Input.GetMouseButtonDown(0))
            {
                door.Interact();
            }
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

    bool DoorIsInteractable()
    {
        var d = player.ObjectInFocus.GetComponent<Door>();
        var playerLookingAtDoor = d != null;
        return playerLookingAtDoor && d == door; 
    }
}
