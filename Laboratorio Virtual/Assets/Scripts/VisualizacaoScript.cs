using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VisualizacaoScript : MonoBehaviour
{

    public GameObject[] vidrarias;
    public string[] nomeVidrarias;
    public TextMeshProUGUI texto;
    public Button botaoAnterior, botaoProximo;
    int x = 0;

    public void VidrariaProximo()
    {
        if(x < vidrarias.Length - 1)
        {
            vidrarias[x].SetActive(false);
            x++;
            vidrarias[x].SetActive(true);
            texto.text = nomeVidrarias[x];
            botaoAnterior.interactable = true;
        }
        
        if(x == vidrarias.Length - 1)
        {
            botaoProximo.interactable = false;
        }
    }

    public void VidrariaAnterior()
    {
        vidrarias[x].SetActive(false);
        x--;
        vidrarias[x].SetActive(true);
        texto.text = nomeVidrarias[x];
        botaoProximo.interactable = true;

        if(x == 0)
        {
            botaoAnterior.interactable = false;
        }
    }
}
