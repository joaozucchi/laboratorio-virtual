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
public class AuthControlller : MonoBehaviour
{
    public TextMeshProUGUI RegisteremailInput, RegisterpasswordInput, usernameInput;
    public TextMeshProUGUI LoginemailInput, LoginpasswordInput;

    public Toggle aluno, professor;
    private string uid, role = "aluno", roleLida, userNameLido;

    private string DATA_URL = "https://ar-lab-88ab2.firebaseio.com/";
    private User user;
    private DatabaseReference databaseReference;

    public GameObject canvasMenu, canvasLogin, canvasRegister;
    public Text textButton, textBemVindo;
    private void Start()
    {
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl(DATA_URL);
        databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
    }
    public void SaveUser(string username, string role, string uid)
    {
        user = new User(username, role, uid);
        string jsonData = JsonUtility.ToJson(user);

        databaseReference.Child("Users").Child(uid).SetRawJsonValueAsync(jsonData);
    }
    public void Load()
    {
        FirebaseDatabase.DefaultInstance.GetReference("Users").OrderByChild("Uid").EqualTo(uid).GetValueAsync()
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
                    string userData = snapshot.GetRawJsonValue();

                    foreach (var child in snapshot.Children)
                    {
                        string t = child.GetRawJsonValue();
                        User extractedData = JsonUtility.FromJson<User>(t);
                        //print("Nome: " + extractedData.Username);
                        //print("Papel: " + extractedData.Role);
                        userNameLido = extractedData.Username;
                        roleLida = extractedData.Role;
                        textBemVindo.text = "Bem vindo, " + userNameLido;
                        textButton.text = "Entrar na área do " + roleLida;
                        canvasLogin.SetActive(false);
                        canvasRegister.SetActive(false);
                        canvasMenu.SetActive(true);
                    }
                }
            }
            ));

        //FirebaseDatabase.DefaultInstance.GetReferenceFromUrl(DATA_URL).GetValueAsync()
        //    .ContinueWith((task =>
        //    {
        //        if (task.IsFaulted)
        //        {
        //            print("faluted");
        //            return;
        //        }
        //        if (task.IsCanceled)
        //        {
        //            print("canceled");
        //            return;
        //        }
        //        if (task.IsCompleted)
        //        {
        //            DataSnapshot snapshot = task.Result;

        //            string userData = snapshot.GetRawJsonValue();

        //            foreach (var child in snapshot.Children)
        //            {
        //                string t = child.GetRawJsonValue();
        //                User extractedData = JsonUtility.FromJson<User>(t);
        //                print(extractedData.Username);
        //            }
        //        }

        //    }));
    }


    public void Login()
    {
        //Console("");
        FirebaseAuth.DefaultInstance.SignInWithEmailAndPasswordAsync(LoginemailInput.text, LoginpasswordInput.text)
            .ContinueWith((task =>
            {

                if (task.IsCanceled)
                {
                    Firebase.FirebaseException e = task.Exception.Flatten().InnerExceptions[0] as Firebase.FirebaseException;
                    GetErrorMessage((AuthError)e.ErrorCode);
                    return;
                }

                if (task.IsFaulted)
                {
                    Firebase.FirebaseException e = task.Exception.Flatten().InnerExceptions[0] as Firebase.FirebaseException;
                    GetErrorMessage((AuthError)e.ErrorCode);
                    return;
                }
                if (task.IsCompleted)
                {
                    print("Login COMPLETE");
                    GetUid();
                    Load();
                }
            }));
    }

    public void Register()
    {
        if (aluno.isOn)
        {
            role = "aluno";
        }
        else
        {
            role = "professor";
        }
        //Console("");
        FirebaseAuth.DefaultInstance.CreateUserWithEmailAndPasswordAsync(RegisteremailInput.text, RegisterpasswordInput.text)
            .ContinueWith((task =>
            {

                if (task.IsCanceled)
                {
                    Firebase.FirebaseException e = task.Exception.Flatten().InnerExceptions[0] as Firebase.FirebaseException;
                    GetErrorMessage((AuthError)e.ErrorCode);
                    return;
                }

                if (task.IsFaulted)
                {
                    Firebase.FirebaseException e = task.Exception.Flatten().InnerExceptions[0] as Firebase.FirebaseException;
                    GetErrorMessage((AuthError)e.ErrorCode);
                    return;
                }

                if (task.IsCompleted)
                {
                    print("Registration COMPLETE");
                    uid = task.Result.UserId;
                    SaveUser(usernameInput.text, role, uid);
                    Load();
                }

            }));
    }

    public void Logout()
    {
        if (FirebaseAuth.DefaultInstance.CurrentUser != null)
        {
            FirebaseAuth.DefaultInstance.SignOut();
        }
    }

    private void GetUid()
    {
        uid = FirebaseAuth.DefaultInstance.CurrentUser.UserId;
    }

    void GetErrorMessage(AuthError errorCode)
    {

        string msg = "";
        msg = errorCode.ToString();
        print(msg);

        //switch (errorCode)
        //{
        //    case AuthError.AccountExistsWithDifferentCredentials:
        //        Console("Email já foi utilizado anteriormente");
        //        break;
        //    case AuthError.MissingEmail:
        //        Console("Insira o email");
        //        break;
        //    case AuthError.MissingPassword:
        //        Console("Insira a senha");
        //        break;
        //    case AuthError.WrongPassword:
        //        Console("Senha incorreta");
        //        break;
        //    case AuthError.InvalidEmail:
        //        Console("Email inválido");
        //        break;
        //    case AuthError.WeakPassword:
        //        Console("A senha deve ter no mínimo seis digitos");
        //        break;
        //}
    }

    public void Role()
    {
        if (aluno.isOn)
        {
            role = "aluno";
        }
        else
        {
            role = "professor";
        }
    }

    public void Console(string msg)
    {
        Text LoginConsole = GameObject.Find("LoginConsole").GetComponent<Text>();
        LoginConsole.text = msg;
        Text RegisterConsole = GameObject.Find("RegisterConsole").GetComponent<Text>();
        RegisterConsole.text = msg;
    }

}
