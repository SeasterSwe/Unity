using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Net;
using System.Text;
using Firebase;
using Firebase.Auth;
using Firebase.Extensions;
using Firebase.Database;

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
        SaveToFile(fileName, jsonString);
    }

    public void SaveToFile(string fileName, string jsonString)
    {
        var db = FirebaseDatabase.DefaultInstance;
        var userId = FirebaseAuth.DefaultInstance.CurrentUser.UserId;
        //puts the json data in the "users/userId" part of the database.
        db.RootReference.Child("users").Child(userId).Child(fileName).SetRawJsonValueAsync(jsonString);
    }
    public AutoInfo Load(string fileName)
    {
        AutoInfo info = new AutoInfo();
        var db = FirebaseDatabase.DefaultInstance;
        var userId = FirebaseAuth.DefaultInstance.CurrentUser.UserId;
        var dataTask = db.RootReference.Child("users").Child(userId).Child(fileName).GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.Exception != null)
            {
                Debug.LogError(task.Exception);
            }

            //here we get the result from our database.
            DataSnapshot snap = task.Result;

            //And send the json data to a function that can update our game.
            var abo = snap.GetRawJsonValue();
            info = JsonUtility.FromJson<AutoInfo>(abo);
            GameObject.Find(fileName).GetComponent<AutoMiner>().LoadData(info);
            return info;
        });
        return info;
    }
    public CoockieData LoadCoockies(string fileName)
    {
        CoockieData info = new CoockieData();
        var db = FirebaseDatabase.DefaultInstance;
        var userId = FirebaseAuth.DefaultInstance.CurrentUser.UserId;
        var dataTask = db.RootReference.Child("users").Child(userId).Child(fileName).GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.Exception != null)
            {
                Debug.LogError(task.Exception);
            }

            //here we get the result from our database.
            DataSnapshot snap = task.Result;

            //And send the json data to a function that can update our game.
            var coockieData = snap.GetRawJsonValue();
            info = JsonUtility.FromJson<CoockieData>(coockieData);
            GameObject.FindGameObjectWithTag("Player").GetComponent<Coockie>().LoadCoockies(info);
            return info;
        });
        return info;
    }

    public void SaveInfos(CoockieData autoInfo, string fileName)
    {
        var info = new CoockieData();
        var comp = autoInfo;

        info.coockieAmount = comp.coockieAmount;
        info.coockiesPerSec = comp.coockiesPerSec;

        string jsonString = JsonUtility.ToJson(info);
        SaveToFile(fileName, jsonString);
    }
}
