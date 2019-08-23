using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pontuar : MonoBehaviour
{
    public void PontuarQuestao()
    {
        PlayerPrefs.SetInt("Pontos", PlayerPrefs.GetInt("Pontos") + 1);
    }

    public void ZerarPontuacao()
    {
        PlayerPrefs.SetInt("Pontos", 0);
    }

}
