using UnityEngine;
using System.Collections;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using Firebase.Extensions;
using Firebase.Database;

public class FirebaseTest : MonoBehaviour
{
    FirebaseDatabase db;
    void Start()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            if (task.Exception != null)
            {
                Debug.LogError(task.Exception);
            }

            //db = FirebaseDatabase.DefaultInstance;
            //db.RootReference.Child("Hello").SetValueAsync("World");
            StartCoroutine(SignIn("test@test.test", "test123!"));
        });
    }

    IEnumerator RegUser(string email, string password)
    {
        Debug.Log("Starting Registration");
        var auth = FirebaseAuth.DefaultInstance;
        var regTask = auth.CreateUserWithEmailAndPasswordAsync(email, password);
        yield return new WaitUntil(() => regTask.IsCompleted);

        if (regTask.Exception != null)
            Debug.LogWarning(regTask.Exception);
        else
            Debug.Log("Logged in");
    }

    private IEnumerator SignIn(string email, string password)
    {
        Debug.Log("Logging in");
        var auth = FirebaseAuth.DefaultInstance;
        var regTask = auth.SignInWithEmailAndPasswordAsync(email, password);
        yield return new WaitUntil(() => regTask.IsCompleted);

        if (regTask.Exception != null)
            Debug.LogWarning(regTask.Exception);
        else
            Debug.Log("Logged in");

       // StartCoroutine(DataTest(FirebaseAuth.DefaultInstance.CurrentUser.UserId, "TestWrite"));
    }

    private IEnumerator DataTest(string userID, string data)
    {
        Debug.Log("Trying to write data");
        var db = FirebaseDatabase.DefaultInstance;
        var dataTask = db.RootReference.Child("users").Child(userID).SetValueAsync(data);

        yield return new WaitUntil(() => dataTask.IsCompleted);

        if (dataTask.Exception != null)
            Debug.LogWarning(dataTask.Exception);
        else
            Debug.Log("DataTestWrite: Complete");
    }

}