using System.IO;
using System.Text;

public static class DataContractJsonSerializer
{
    // NOTE:
    // [DataMember(Name="xxx")] and [IgnoreDataMember] attribute is valid.

    #region Method

    public static string Serialize<T>(T obj)
    {
        var memoryStream = new MemoryStream();
        var streamReader = new StreamReader(memoryStream);

        var serializer = new System.Runtime.Serialization.Json.DataContractJsonSerializer(typeof(T));
            serializer.WriteObject(memoryStream, obj);

        memoryStream.Position = 0;

        string json = streamReader.ReadToEnd();

        memoryStream.Dispose();
        streamReader.Dispose();

        return json;
    }

    public static T Deserialize<T>(string json)
    {
        var memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(json));
            memoryStream.Position = 0;

        var serializer = new System.Runtime.Serialization.Json.DataContractJsonSerializer(typeof(T));
        var serializedObject = (T)serializer.ReadObject(memoryStream);

        memoryStream.Dispose();

        return serializedObject;
    }

    #endregion Method
}