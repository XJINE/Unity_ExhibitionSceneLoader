using UnityEngine;
using UnityEngine.SceneManagement;

public class ExhibitionSceneLoader : SingletonMonoBehaviour<ExhibitionSceneLoader>
{
    #region Field

    public string absoluteFilePath = "ExhibitionSceneLoaderSettings.txt";
    public string sceneName = "ExhibitionSceneLoader";

    public  float timeLimitSec = 30;
    private float previousLoadTimeSec = 0;

    #endregion Field

    #region Method

    protected virtual void Start()
    {
        this.sceneName = FileReadWriter.ReadTextFromFile(this.absoluteFilePath);
    }

    protected virtual void Update()
    {
        if (Time.timeSinceLevelLoad - this.previousLoadTimeSec > this.timeLimitSec)
        {
            FileReadWriter.WriteTextToFile(this.absoluteFilePath, this.sceneName);
            TrySceneLoad();
        }
    }

    protected virtual void OnGUI()
    {
        int count = (int)(this.timeLimitSec - (Time.timeSinceLevelLoad - this.previousLoadTimeSec));

        GUILayout.BeginArea(new Rect(100, 100, 300, 300));
        GUILayout.Label("This scene must be load first.");
        GUILayout.Label("Input scene name (" + count + ") : ");
        this.sceneName = GUILayout.TextField(this.sceneName);
        GUILayout.Label("Scene name will be saved automatically.");
        GUILayout.EndArea();
    }

    protected virtual void TrySceneLoad()
    {
        try
        {
            SceneManager.LoadScene(this.sceneName, LoadSceneMode.Single);
        }
        catch
        {
            this.previousLoadTimeSec = Time.timeSinceLevelLoad;
        }
    }

    #endregion Method
}