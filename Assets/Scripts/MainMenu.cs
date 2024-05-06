using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void LoadScene(string scene)
    {
        GameManager.Instance.LoadScene(scene);
    }
}
