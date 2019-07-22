using UnityEngine;
using UnityEngine.SceneManagement;

public class ExhibitionSceneLoader : SingletonMonoBehaviour<ExhibitionSceneLoader>
{
    #region Field

    public string settingFile = "ExhibitionSceneLoaderSettings.txt";
    public string sceneName   = "ExhibitionSceneLoader";

    public  float waitTimeSec = 30;
    private float prevTimeSec = 0;

    #endregion Field

    #region Method

    protected virtual void Start()
    {
            this.sceneName = FileReadWriter.ReadTextFromFile(this.settingFile) ?? this.sceneName;
    }

    protected virtual void Update()
    {
        if (Time.timeSinceLevelLoad - this.prevTimeSec > this.waitTimeSec)
        {
            TrySaveSettings();
            TrySceneLoad();
        }
    }

    protected virtual void OnGUI()
    {
        int count = (int)(this.waitTimeSec - (Time.timeSinceLevelLoad - this.prevTimeSec));

        GUILayout.BeginArea(new Rect(100, 100, 300, 300));
        GUILayout.Label("This scene must be load first.");
        GUILayout.Label("Input scene name (" + count + ") : ");
        this.sceneName = GUILayout.TextField(this.sceneName);
        GUILayout.Label("Scene name will be saved automatically.");
        GUILayout.EndArea();
    }

    protected virtual void TrySaveSettings()
    {
        try
        {
            FileReadWriter.WriteTextToFile(this.settingFile, this.sceneName);
        }
        catch
        {
            FileReadWriter.WriteTextToStreamingAssets(this.settingFile, this.sceneName);
        }
    }

    protected virtual void TrySceneLoad()
    {
        try
        {
            SceneManager.LoadScene(this.sceneName, LoadSceneMode.Single);
        }
        catch
        {
            this.prevTimeSec = Time.timeSinceLevelLoad;
        }
    }

    #endregion Method
}