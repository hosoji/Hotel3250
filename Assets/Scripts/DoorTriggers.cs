using UnityEngine;

public class DoorTriggers : MonoBehaviour
{
    private bool playerInRange = false;

    private BoxCollider[] triggers = new BoxCollider[2];
    private FPController player;
    private LayerMask playerLayer;
    public Door door;
    private Light spotLight;

    private Material slMat;
    public MeshRenderer slRend;

    private float autoShutDelay = 1;
    private float timeUntilAutoShut = 0;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<FPController>();

        slMat = slRend.material;
        spotLight = slRend.gameObject.GetComponentInChildren<Light>();
        spotLight.enabled = false;

        triggers = GetComponentsInChildren<BoxCollider>();

        playerLayer = LayerMask.GetMask("Player");

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

        AutoShutDoor();
    }

    private void AutoShutDoor()
    {
        if (door.doorOpen && !playerInRange && !PlayerInDoorway())
        {
            if (timeUntilAutoShut < autoShutDelay)
            {
                timeUntilAutoShut += Time.deltaTime;
            }
            else
            {
                door.Interact();
            }

        }
        else {timeUntilAutoShut = 0;}


    }

    private void OnTriggerEnter(Collider other)
    {
        playerInRange = other.CompareTag("Player");
        if (playerInRange) ActivateLight(true);


    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            ActivateLight(false);
        }

    }

    bool DoorIsInteractable()
    {
        Door d = null;

        if (player.ObjectInFocus.GetComponent<Door>() != null)
        {
            d = player.ObjectInFocus.GetComponent<Door>();
        }

        var playerLookingAtDoor = d != null;
        return playerLookingAtDoor && d == door; 
    }

    void ActivateLight(bool on)
    {
        if (on)
        {
            if (!slMat.IsKeywordEnabled("_EMISSION")) slMat.EnableKeyword("_EMISSION");
            spotLight.enabled = true;

        }
        else 
        {
            if (slMat.IsKeywordEnabled("_EMISSION")) slMat.DisableKeyword("_EMISSION");
            spotLight.enabled = false;
        }

    }

    bool PlayerInDoorway()
    {
        bool result = false;
        Vector3 direction =  (triggers[1].transform.position - triggers[0].transform.position).normalized;
        float dist = Vector3.Distance(triggers[0].transform.position,triggers[1].transform.position);

        RaycastHit hit;
        if (Physics.Raycast(triggers[0].transform.position,direction, out hit, dist, playerLayer))
        {
            result = true;       
        }

        return result;
    }


}
