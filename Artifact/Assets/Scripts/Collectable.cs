using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField] private ThrowGrapplingHook throwGrapplingHook;

    void Start()
    {
        throwGrapplingHook = FindObjectOfType<ThrowGrapplingHook>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            throwGrapplingHook.raycastLenght ++;
            GetComponent<MeshRenderer>().enabled = false;
            GetComponentInChildren<ParticleSystem>().Play();
            Destroy(gameObject,2f);
        }    
    }
}
