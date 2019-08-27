using UnityEngine;
using UnityEngine.SceneManagement;

public class Sample : MonoBehaviour
{
    private void OnGUI()
    {
        GUI.Label(new Rect(100, 100, 500, 500), SceneManager.GetActiveScene().name);
    }
}