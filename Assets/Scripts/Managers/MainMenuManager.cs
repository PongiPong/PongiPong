using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void SceneChange(int index)
    {
        SceneManager.LoadScene(index);
    }
}