using UnityEngine;

public class IncreasedBlood : MonoBehaviour
{
    public InterFace interFace;

    AudioManager audioManager;

    MeshRenderer meshRenderer;

    BoxCollider boxCollider;

    public int health;

    void Awake()
    {
        audioManager = GetComponent<AudioManager>();
        meshRenderer = GetComponent<MeshRenderer>();
        boxCollider = GetComponent<BoxCollider>();
    }

    void Blood()
    {
        audioManager.Play("Loot");

        interFace.health += health;

        if(interFace.health > 100)
        {
            interFace.health = 100;
        }
        meshRenderer.enabled = false;

        boxCollider.enabled = false;

        Destroy(gameObject, 0.7f);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Blood();
        }
    }
}
