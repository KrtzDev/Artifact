using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerx : MonoBehaviour
{

    public static SceneManagerx instance = null;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }

    public void ChangeSceneStartGame()
    {
        SceneManager.LoadScene(2);
    }

    public void ChangeSceneCredits()
    {
        SceneManager.LoadScene(1);
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
