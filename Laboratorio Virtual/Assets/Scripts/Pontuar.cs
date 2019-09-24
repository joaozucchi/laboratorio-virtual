using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;


public class Pontuar : MonoBehaviour
{
    public TextMeshProUGUI texto;
    public string[] nomeFases;
    
    public void PontuarQuestao()
    {
        PlayerPrefs.SetInt("Pontos", PlayerPrefs.GetInt("Pontos") + 1);
        string name = SceneManager.GetActiveScene().name;
        PlayerPrefs.SetInt(name, 1);
    }

    public void ZerarPontuacao()
    {
        PlayerPrefs.DeleteAll();
    }
    
    public void MostrarPlacar()
    {

        for (int i = 0; i < nomeFases.Length; i++)
        {
            string a;
            if(PlayerPrefs.GetInt(nomeFases[i]) == 1)
            {
                a = "Correta";
            }
            else
            {
                a = "Incorreta";
            }
            int x = i + 1;
            texto.text += "Questão " + x + ": " + a +"<br>";
        }
        texto.text += "Você acertou " + PlayerPrefs.GetInt("Pontos") + " de 10 questões! <br>";
    }
}
