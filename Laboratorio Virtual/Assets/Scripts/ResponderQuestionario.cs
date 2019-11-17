using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;
using UnityEngine.UI;
using TMPro;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using UnityEngine.SceneManagement;
public class ResponderQuestionario : MonoBehaviour
{
    public TMP_InputField nomeSala;
    private int n;
    private Fase[] fases;

    private string DATA_URL = "https://ar-lab-88ab2.firebaseio.com/";
    private DatabaseReference databaseReference;
    private void Start()
    {
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl(DATA_URL);
        databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    public void LoadQuestionario()
    {
        FirebaseDatabase.DefaultInstance.GetReference("Questionarios").Child(nomeSala.text)/*.Child("info").*/.OrderByChild("nomeQuestionario").EqualTo(nomeSala.text).GetValueAsync()
            .ContinueWith((task =>
            {
                if (task.IsFaulted)
                {
                    print("faulted");
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
                    InfoQuestionario extractedData = JsonUtility.FromJson<InfoQuestionario>(snapshot.GetRawJsonValue());
                    n = extractedData.n;
                    print(n);
                    print(extractedData.uidAutor);
                    //foreach (var child in snapshot.Children)
                    //{
                    //    string t = child.GetRawJsonValue();
                    //    User extractedData = JsonUtility.FromJson<User>(t);
                    //}
                }
            }
            ));

        fases = new Fase[n];
        for (int i = 0; i < n; i++)
        {
            FirebaseDatabase.DefaultInstance.GetReference("Questionarios").Child(nomeSala.text).Child("fase" + i).GetValueAsync()
                        .ContinueWith((task =>
                        {
                            if (task.IsFaulted)
                            {
                                print("faulted");
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
                                Fase extractedData = JsonUtility.FromJson<Fase>(snapshot.GetRawJsonValue());
                                print(extractedData.vCorreta);

                            }
                        }
                        ));
        }

    }
}
