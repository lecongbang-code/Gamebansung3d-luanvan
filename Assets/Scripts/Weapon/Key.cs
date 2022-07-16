using UnityEngine;

public class Key : MonoBehaviour
{
    public PlayerController playerController;

    AudioManager audioManager;

    BoxCollider boxCollider;

    void Start()
    {
        audioManager = GetComponent<AudioManager>();
        boxCollider = GetComponent<BoxCollider>();
    }

    void AddKey()
    {
        audioManager.Play("LootKey");
        
        boxCollider.enabled = false;

        playerController.key += 1;

        Destroy(gameObject, 0.3f);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            AddKey();
        }
    }
}
