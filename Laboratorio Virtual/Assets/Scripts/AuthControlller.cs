using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;
using UnityEngine.UI;
using TMPro;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;

public class AuthControlller : MonoBehaviour
{
    public TextMeshProUGUI RegisteremailInput, RegisterpasswordInput, usernameInput, RegisterConsole;
    public TextMeshProUGUI LoginemailInput, LoginpasswordInput, LoginConsole;

    public Toggle aluno, professor;
    private string uid, role = "aluno";

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
        print("aaa");
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

                    foreach (var child in snapshot.Children)
                    {
                        string t = child.GetRawJsonValue();
                        User extractedData = JsonUtility.FromJson<User>(t);
                    }
                }

            }));
    }

    public void Login(){
        FirebaseAuth.DefaultInstance.SignInWithEmailAndPasswordAsync(LoginemailInput.text, LoginpasswordInput.text)
            .ContinueWith(( task =>{

            if(task.IsCanceled){
                Firebase.FirebaseException e = task.Exception.Flatten().InnerExceptions[0] as Firebase.FirebaseException;
                GetErrorMessage((AuthError)e.ErrorCode);
                return;
            }

            if(task.IsFaulted){
                Firebase.FirebaseException e = task.Exception.Flatten().InnerExceptions[0] as Firebase.FirebaseException;
                GetErrorMessage((AuthError)e.ErrorCode);
                return;
            }
            if (task.IsCompleted)
            {
                print("Login COMPLETE");
                GetUid();
            }
        }));
    }

    public void Register(){

        FirebaseAuth.DefaultInstance.CreateUserWithEmailAndPasswordAsync(RegisteremailInput.text, RegisterpasswordInput.text)
            .ContinueWith((task =>{

            if(task.IsCanceled){
                Firebase.FirebaseException e = task.Exception.Flatten().InnerExceptions[0] as Firebase.FirebaseException;
                GetErrorMessage((AuthError)e.ErrorCode);
                return;
            }

            if(task.IsFaulted){
                Firebase.FirebaseException e = task.Exception.Flatten().InnerExceptions[0] as Firebase.FirebaseException;
                GetErrorMessage((AuthError)e.ErrorCode);
                return;
            }

            if(task.IsCompleted){
                print("Registration COMPLETE");
                uid = task.Result.UserId;
                print("aaaa");
                SaveUser(usernameInput.text, role, uid);
            }

        }));
    }

    public void Logout(){
        if(FirebaseAuth.DefaultInstance.CurrentUser != null)
        {
            FirebaseAuth.DefaultInstance.SignOut();
        }
    }

    private void GetUid()
    {
        uid = FirebaseAuth.DefaultInstance.CurrentUser.UserId;
        print(uid);
    }

    void GetErrorMessage (AuthError errorCode){

        string msg = "";
        msg = errorCode.ToString();
        print(msg);

        switch (errorCode)
        {
            case AuthError.AccountExistsWithDifferentCredentials:
                RegisterConsole.text = "Email já foi utilizado anteriormente";
                break;
            case AuthError.MissingEmail:
                RegisterConsole.text = "Insira o email";
                LoginConsole.text = "Insira o email";
                break;
            case AuthError.MissingPassword:
                RegisterConsole.text = "Insira a senha";
                LoginConsole.text = "Insira a senha";
                break;
            case AuthError.WrongPassword:
                LoginConsole.text = "Senha incorreta";
                break;
            case AuthError.InvalidEmail:
                RegisterConsole.text = "Email inválido";
                break;
        }
    }

    public void Role()
    {
        if (aluno.isOn)
        {
            role = "aluno";
        }
        else{
            role = "professor";
        }
    }
}
