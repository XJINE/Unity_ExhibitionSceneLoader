using UnityEngine;

public static class FileReadWriter
{
    // NOTE:
    // ~X function use DataContractJsonSerializer.

    #region JSON

    #region Write

    public static bool WriteJsonToStreamingAssets<T>(T obj, string file = null, string dir = null)
    {
        return WriteTextToStreamingAssets(GenerateReadWriteJsonPath(obj.GetType(), file, dir), JsonUtility.ToJson(obj));
    }

    public static bool WriteJsonToStreamingAssetsX<T>(T obj, string file = null, string dir = null)
    {
        return WriteTextToStreamingAssets(GenerateReadWriteJsonPath(obj.GetType(), file, dir), DataContractJsonSerializer.Serialize<T>(obj));
    }

    public static bool WriteJsonToFile<T>(T obj, string file = null, string dir = null)
    {
        return WriteTextToFile(GenerateReadWriteJsonPath(obj.GetType(), file, dir), JsonUtility.ToJson(obj));
    }

    public static bool WriteJsonToFileX<T>(T obj, string file = null, string dir = null)
    {
        return WriteTextToFile(GenerateReadWriteJsonPath(obj.GetType(), file, dir), DataContractJsonSerializer.Serialize<T>(obj));
    }

    #endregion Write

    #region Read

    public static T ReadJsonFromStreamingAssets<T>(string file = null, string dir = null)
    {
        string json = ReadTextFromStreamingAssets(GenerateReadWriteJsonPath(typeof(T), file, dir));

        return json == null ? default : JsonUtility.FromJson<T>(json);
    }

    public static T ReadJsonFromStreamingAssetsX<T>(string file = null, string dir = null)
    {
        string json = ReadTextFromStreamingAssets(GenerateReadWriteJsonPath(typeof(T), file, dir));

        return json == null ? default : DataContractJsonSerializer.Deserialize<T>(json);
    }

    public static void ReadJsonFromStreamingAssets<T>(T obj, string file = null, string dir = null)
    {
        string json = ReadTextFromStreamingAssets(GenerateReadWriteJsonPath(typeof(T), file, dir));

        if (json != null)
        {
            JsonUtility.FromJsonOverwrite(json, obj);
        }
    }

    public static T ReadJsonFromFile<T>(string file = null, string dir = null)
    {
        string json = ReadTextFromFile(GenerateReadWriteJsonPath(typeof(T), file, dir));

        return json == null ? default : JsonUtility.FromJson<T>(json);
    }

    public static T ReadJsonFromFileX<T>(string file = null, string dir = null)
    {
        string json = ReadTextFromFile(GenerateReadWriteJsonPath(typeof(T), file, dir));

        return json == null ? default : DataContractJsonSerializer.Deserialize<T>(json);
    }

    public static void ReadJsonFromFile<T>(T obj, string file = null, string dir = null)
    {
        string json = ReadTextFromFile(GenerateReadWriteJsonPath(typeof(T), file, dir));

        if (json != null)
        {
            JsonUtility.FromJsonOverwrite(json, obj);
        }
    }

    #endregion Read

    #endregion JSON

    #region Text

    public static string ReadTextFromAssets(string relativePath)
    {
        return ReadTextFromFile(Application.dataPath + System.IO.FileReadWriter.ToRelativePath(relativePath));
    }

    public static string ReadTextFromStreamingAssets(string relativePath)
    {
        return ReadTextFromFile(Application.streamingAssetsPath + System.IO.FileReadWriter.ToRelativePath(relativePath));
    }

    public static string ReadTextFromFile(string path)
    {
        System.IO.FileReadWriter.Result result = System.IO.FileReadWriter.ReadTextFromFile(path);

        if (!result.success)
        {
            Debug.LogWarning(result.content);
            return null;
        }

        return result.content;
    }

    public static bool WriteTextToAssets(string relativePath, string text)
    {
        return WriteTextToFile(Application.dataPath + System.IO.FileReadWriter.ToRelativePath(relativePath), text);
    }

    public static bool WriteTextToStreamingAssets(string relativePath, string text)
    {
        return WriteTextToFile(Application.streamingAssetsPath + System.IO.FileReadWriter.ToRelativePath(relativePath), text);
    }

    public static bool WriteTextToFile(string path, string content)
    {
        System.IO.FileReadWriter.Result result = System.IO.FileReadWriter.WriteTextToFile(path, content);

        if (!result.success)
        {
            Debug.LogWarning(result.content);
        }

        return result.success;
    }

    #endregion Text

    #region GetPath

    public static string GenerateReadWriteJsonPath(System.Type type, string file, string dir)
    {
        return GenerateReadWriteFilePath(type, file, dir) + ".json";
    }

    public static string GenerateReadWriteFilePath(System.Type type, string file, string dir)
    {
        string path = "";

        if (dir == null || dir.Length == 0)
        {
            path = "/";
        }
        else
        {
            if (dir[dir.Length - 1] != '/')
            {
                dir += "/";
            }

            path = dir;
        }

        if (file != null)
        {
            path = path + file;
        }
        else
        {
            path = path + type.Name;
        }

        return path;
    }

    #endregion GetPath
}