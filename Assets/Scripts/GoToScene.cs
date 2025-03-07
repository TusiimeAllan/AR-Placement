using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToScene : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;

    public void GoTo() {
        SceneManager.LoadScene(sceneToLoad);
    }
}
