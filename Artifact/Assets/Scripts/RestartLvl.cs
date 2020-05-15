using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartLvl : MonoBehaviour
{

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            SceneManager.LoadScene(2);
        }    
    }
}
