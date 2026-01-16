using UnityEngine;

public class DuckSound : MonoBehaviour
{
    private AudioSource a;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        a = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GetComponent<AudioSource>().PlayOneShot(GetComponent<AudioSource>().clip);
            a.PlayOneShot(a.clip);

        }
    }
}
