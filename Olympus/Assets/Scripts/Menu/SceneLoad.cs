using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneLoad : MonoBehaviour
{
    public void LoadScene(int level)
    {
        SceneManager.LoadSceneAsync(level);
    }
}
