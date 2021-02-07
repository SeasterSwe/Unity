using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Net;
using System.Text;


public class Save : MonoBehaviour
{
    public static string path;
    private void Awake()
    {
        path = Application.persistentDataPath + "//";
        print(path);
    }
    public void SaveInfos(AutoInfo autoInfo, string fileName)
    {
        var info = new AutoInfo();
        var comp = autoInfo;
        
        info.cost = comp.cost;
        info.amount = comp.amount;

        string jsonString = JsonUtility.ToJson(info);
        SaveToFile(path + fileName, jsonString);
    }

    public void SaveToFile(string fileName, string jsonString)
    {
        using (var stream = File.OpenWrite(fileName))
        {
            stream.SetLength(0);

            var bytes = Encoding.UTF8.GetBytes(jsonString);

            stream.Write(bytes, 0, bytes.Length);

        }
    }
    public AutoInfo Load(string fileName)
    {
        string load = path + fileName;
        using (var stream = File.OpenText(load))
        {
            AutoInfo info = new AutoInfo();
            info = JsonUtility.FromJson<AutoInfo>(stream.ReadToEnd());
            return info;
        }
    }
    public CoockieData LoadCoockies(string fileName)
    {
        string load = path + fileName;
        using (var stream = File.OpenText(load))
        {
            CoockieData info = new CoockieData();
            info = JsonUtility.FromJson<CoockieData>(stream.ReadToEnd());
            return info;
        }
    }

    public void SaveInfos(CoockieData autoInfo, string fileName)
    {
        var info = new CoockieData();
        var comp = autoInfo;

        info.coockieAmount = comp.coockieAmount;
        info.coockiesPerSec = comp.coockiesPerSec;

        string jsonString = JsonUtility.ToJson(info);
        SaveToFile(path + fileName, jsonString);
    }
}
