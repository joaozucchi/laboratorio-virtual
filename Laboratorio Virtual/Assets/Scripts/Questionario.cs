using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class Questionario : MonoBehaviour
{   
    Fase[] fases;
    public TextMeshProUGUI contador;
    public TextMeshProUGUI numeroQuestoes;
    public TextMeshProUGUI pergunta;
    public GameObject lista;
    public Button[] buttonsVidrarias;
    public Button[] buttonsAlternativas;
    public Text[] buttonText;

    public Button anterior, proximo;
    
    public Toggle t1, t2, t3;
    
    int x = 0;
    int y = 0; 
    private void Start()
    {
        
        fases = new Fase[int.Parse(numeroQuestoes.text)];
        
        for (int i = 0; i < buttonsAlternativas.Length; i++)
        {
            int closureIndex = i;
            buttonsAlternativas[closureIndex].onClick.AddListener(() => EscolherBotao(closureIndex));
        }
            
        for (int i = 0; i < buttonsVidrarias.Length; i++)
        {
            int closureIndex = i;
            buttonsVidrarias[closureIndex].onClick.AddListener(() => EscolherVidraria(closureIndex));
        }
    }
    public void EscolherBotao(int buttonIndex)
    {
        Debug.Log("You have clicked the button #" + buttonIndex, buttonsAlternativas[buttonIndex]);
        x = buttonIndex;
        lista.SetActive(true);
    }

    public void EscolherVidraria(int buttonIndex)
    {
        Debug.Log("You have clicked the button #" + buttonIndex, buttonsVidrarias[buttonIndex]);  

        Text t = buttonsVidrarias[buttonIndex].GetComponentInChildren<Text>();
        buttonText[x].text = t.text;
        lista.SetActive(false);
    }

    public void MudaNumero()
    {
        fases = new Fase[int.Parse(numeroQuestoes.text)];
    }

    public void ProximaQuestao()
    {
        fases[y].pergunta = pergunta.text;
        fases[y].v1 = buttonText[0].text;
        fases[y].v2 = buttonText[1].text;
        fases[y].v3 = buttonText[2].text;

        if (t1.enabled)
        {
            fases[y].vCorreta = buttonText[0].text;
        } 
        else if (t2.enabled)
        {
            fases[y].vCorreta = buttonText[2].text;
        }
        else
        {
            fases[y].vCorreta = buttonText[3].text;
        }
        y++;
        contador.text = "Questão " + y+1;
        pergunta.text = "";

        for (int i = 0; i < buttonText.Length; i++)
        {
            buttonText[i].text = "Clique para escolher";
        }

        anterior.interactable = true;

        if(y == fases.Length)
        {
            proximo.interactable = false;
        }
    }

    public void QuestaoAnterior()
    {
        fases[y].pergunta = pergunta.text;
        fases[y].v1 = buttonText[0].text;
        fases[y].v2 = buttonText[1].text;
        fases[y].v3 = buttonText[2].text;

        if (t1.enabled)
        {
            fases[y].vCorreta = buttonText[0].text;
        }
        else if (t2.enabled)
        {
            fases[y].vCorreta = buttonText[2].text;
        }
        else
        {
            fases[y].vCorreta = buttonText[3].text;
        }
        y--;
        contador.text = "Questão " + y + 1;
        pergunta.text = "";

        for (int i = 0; i < buttonText.Length; i++)
        {
            buttonText[i].text = "Clique para escolher";
        }

        proximo.interactable = true;
        if(y == 0)
        {
            anterior.interactable = false;
        }
    }
}
