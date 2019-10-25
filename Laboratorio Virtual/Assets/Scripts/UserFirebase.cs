using System.Collections;
using System.Collections.Generic;
using FullSerializer;
using Proyecto26;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using TMPro;

public class UserFirebase : MonoBehaviour
{
    public TextMeshProUGUI emailText;
    public TextMeshProUGUI nomeText;
    public TextMeshProUGUI senhaText;
    public Text logCadastroText;
    public Toggle aluno;
    public Toggle professor;

    public GameObject CanvasLogin, CanvasCadastro;

    private string idToken;
    public static string localId;
    public static string nome;
    public static string papel;

    private string databaseURL = "https://ar-lab-88ab2.firebaseio.com/users";
    private string AuthKey = "AIzaSyCh6lnBC4pF_CBu1qmNDJOsaxnJAndh6co";

    public static fsSerializer serializer = new fsSerializer();

    //PARTE DO CADASTRO

    //MÉTODO DO BOTAO
    public void SignUpUserButton()
    {
        if (aluno.isOn)
        {
            papel = "aluno";
        }
        else
        {
            papel = "professor";
        }
        SignUpUser(emailText.text, nomeText.text, senhaText.text);
    }

    //CADASTRO DE USUARIO 
    private void SignUpUser(string emailLido, string nomeLido, string senhaLida)
    {
        string userData = "{\"email\":\"" + emailLido + "\",\"password\":\"" + senhaLida + "\",\"returnSecureToken\":true}";
        RestClient.Post<SignResponse>("https://www.googleapis.com/identitytoolkit/v3/relyingparty/signupNewUser?key=" + AuthKey, userData).Then(
            response =>
            {
                idToken = response.idToken;
                localId = response.localId;
                nome = nomeLido;

                PostUserToDatabase();

                CanvasCadastro.SetActive(false);
                CanvasLogin.SetActive(true);
                logCadastroText.text = null;
            }).Catch(error =>
            {
                Debug.Log(error);
                logCadastroText.text = "Houve um erro no cadastro. Confira os dados e tente novamente.";
            });
    }

    //ADICIONAR USUARIO NO BANCO
    private void PostUserToDatabase()
    {
        User user = new User();
        RestClient.Put("https://ar-lab-88ab2.firebaseio.com/users" + "/" + localId + ".json?auth=" + idToken, user);
    }
    
    //public void SignInUserButton()
    //{
    //    SignInUser(emailText.text, senhaText.text);
    //}

    //private void SignInUser(string email, string password)
    //{
    //    string userData = "{\"email\":\"" + email + "\",\"password\":\"" + password + "\",\"returnSecureToken\":true}";
    //    RestClient.Post<SignResponse>("https://www.googleapis.com/identitytoolkit/v3/relyingparty/verifyPassword?key=" + AuthKey, userData).Then(
    //        response =>
    //        {
    //            idToken = response.idToken;
    //            localId = response.localId;
    //        }).Catch(error =>
    //        {
    //            Debug.Log(error);
    //        });

    //    RestClient.Get<User>("https://ar-lab-88ab2.firebaseio.com/users" + "/" + localId + ".json?auth=" + idToken).Then(response =>
    //    {
    //        papel = response.role;
    //        SetRole(papel);
    //        SetLogText();
    //    });
    //}

    //private void SetRole(string papel)
    //{
    //    if (papel.Equals("professor"))
    //    {
    //        canvasLogin.SetActive(false);
    //        canvasProfessor.SetActive(true);
    //    }
    //    else
    //    {
    //        canvasLogin.SetActive(false);
    //        canvasAluno.SetActive(true);
    //    }
    //}

    //private void SetLogText()
    //{
    //    RestClient.Get<User>("https://ar-lab-88ab2.firebaseio.com/users" + "/" + localId + ".json?auth=" + idToken).Then(response =>
    //    {
    //        nome = response.userName;
    //        papel = response.role;
    //        logText.text = nome + "\n" + papel;
    //        logText1.text = nome + "\n" + papel;
    //    });
    //}
}