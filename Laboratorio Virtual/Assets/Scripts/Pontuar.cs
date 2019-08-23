using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Pontuar : MonoBehaviour
{
    public Text texto;
    public void PontuarQuestao()
    {
        PlayerPrefs.SetInt("Pontos", PlayerPrefs.GetInt("Pontos") + 1);
    }

    public void ZerarPontuacao()
    {
        PlayerPrefs.SetInt("Pontos", 0);
    }
    
    public void MostrarPlacar()
    {
        texto.text = "Você acertou" + PlayerPrefs.GetInt("Pontos") + " de 3 questões!";
    }

}
