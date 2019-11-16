using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;

public class DataBridge : MonoBehaviour
{
    private string DATA_URL = "https://ar-lab-88ab2.firebaseio.com/";

    private User user;

    private DatabaseReference databaseReference;
    private void Start()
    {
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl(DATA_URL);
        databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    public void SaveUser(string username, string role, string uid)
    {
        user = new User(username, role);

        string jsonData = JsonUtility.ToJson(user);

        databaseReference.Child("Users").Child(uid).SetRawJsonValueAsync(jsonData);
    }

    public void Load()
    {
        FirebaseDatabase.DefaultInstance.GetReferenceFromUrl(DATA_URL).GetValueAsync()
            .ContinueWith((task => {

                if (task.IsFaulted)
                {
                    print("faluted");
                    return;
                }
                if (task.IsCanceled)
                {
                    print("canceled");
                    return;
                }
                if (task.IsCompleted)
                {
                    DataSnapshot snapshot = task.Result;

                    string userData = snapshot.GetRawJsonValue();
                    
                    foreach(var child in snapshot.Children)
                    {
                        string t = child.GetRawJsonValue();
                        User extractedData = JsonUtility.FromJson<User>(t);
                    }
                }

            }));
    }
}
