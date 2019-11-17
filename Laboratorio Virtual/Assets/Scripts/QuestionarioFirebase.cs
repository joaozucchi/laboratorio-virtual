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
public class QuestionarioFirebase : MonoBehaviour
{
    // Start is called before the first frame update
    private string DATA_URL = "https://ar-lab-88ab2.firebaseio.com/";
    private DatabaseReference databaseReference;
    private void Start()
    {
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl(DATA_URL);
        databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    public void SaveQuestionario(Fase[] fases,string nomeQuestionario) 
    {
        for(int i = 0; i < fases.Length; i++)
        {
            fases[i].pergunta = "aaa";
            string jsonData = JsonUtility.ToJson(fases[i]);
            print(jsonData);
            databaseReference.Child("Questionarios").Child(nomeQuestionario).Child("fase"+i).SetValueAsync(jsonData);
        }
        
    }
}
//user = new User(username, role, uid);
//string jsonData = JsonUtility.ToJson(user);
//databaseReference.Child("Users").Child(uid).SetRawJsonValueAsync(jsonData);
