using UnityEngine;
using UnityEngine.SceneManagement;

public class ExhibitionSceneLoader : SingletonMonoBehaviour<ExhibitionSceneLoader>
{
    #region Field

    public string settingFile = "ExhibitionSceneLoaderSettings.txt";
    public int    sceneIndex  = 1;

    public  float waitTimeSec = 30;
    private float prevTimeSec = 0;

    public Rect fieldSize = new Rect(100, 100, 500, 500);

    protected string[] scenes;

    #endregion Field

    #region Method

    protected virtual void Start()
    {
        // NOTE:
        // Ignore SceneLoader scene.

        int sceneCount = SceneManager.sceneCountInBuildSettings;
        this.scenes = new string[sceneCount - 1];

        for (int i = 1; i < sceneCount; i++)
        {
            this.scenes[i - 1] = " " + SceneUtility.GetScenePathByBuildIndex(i);
        }

        string sceneIndex = FileReadWriter.ReadTextFromFile(this.settingFile) ?? "1";
        int.TryParse(sceneIndex, out this.sceneIndex);
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

        GUILayout.BeginArea(this.fieldSize);

        GUILayout.Label("Scenes : ");
        this.sceneIndex = GUILayout.SelectionGrid(this.sceneIndex, this.scenes, 1, GUI.skin.toggle);

        GUILayout.Label("Setting file path : ");
        GUILayout.Label(this.settingFile);

        if (GUILayout.Button("Start (" + count + ")"))
        {
            TrySaveSettings();
            TrySceneLoad();
        };

        GUILayout.EndArea();
    }

    protected virtual void TrySaveSettings()
    {
        try
        {
            FileReadWriter.WriteTextToFile(this.settingFile, this.sceneIndex.ToString());
        }
        catch
        {
            FileReadWriter.WriteTextToStreamingAssets(this.settingFile, this.sceneIndex.ToString());
        }
    }

    protected virtual void TrySceneLoad()
    {
        // NOTE:
        // this.sceneIndex ignores SceneLoader.

        try
        {
            SceneManager.LoadScene(this.sceneIndex + 1, LoadSceneMode.Single);
        }
        catch
        {
            this.prevTimeSec = Time.timeSinceLevelLoad;
        }
    }

    #endregion Method
}