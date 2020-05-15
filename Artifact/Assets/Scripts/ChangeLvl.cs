using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeLvl : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            SceneManager.LoadScene(1);
        }    
    }
}
