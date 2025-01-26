using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    public void OpenGameLevel() 
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1); //SceneManager
    }
    
    public void OpenMainMenu() 
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0); //SceneManager
    }
}
