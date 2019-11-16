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
    public TextMeshProUGUI RegisteremailInput, RegisterpasswordInput, usernameInput;
    public TextMeshProUGUI LoginemailInput, LoginpasswordInput;
    public Toggle aluno, professor;
    private string uid, role = "aluno";
    private DataBridge db;

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
                db.SaveUser(usernameInput.text, role, uid);
                db.Load();
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
                break;
            case AuthError.MissingEmail:
                break;
            case AuthError.MissingPassword:
                break;
            case AuthError.WrongPassword:
                break;
            case AuthError.InvalidEmail:
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
