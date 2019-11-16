using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class Questionario : MonoBehaviour
{   
    Fase[] fases;
    public TextMeshProUGUI contador;
    public Text numeroQuestoes;
    public TMP_InputField pergunta;
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
        MudaNumero();
        
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
        for (int i = 0; i < fases.Length; i++)
        {
            fases[i] = new Fase();
            fases[i].v1 = "Clique para escolher";
            fases[i].v2 = "Clique para escolher";
            fases[i].v3 = "Clique para escolher";
            fases[i].vCorreta = "null";
        }
        y = 0;
        contador.text = "Questão 1";
        anterior.interactable = false;
    }

    public void ProximaQuestao()
    {
        SalvarResultados();        
        y++;
        AtualizarTela();
        anterior.interactable = true;

        if(y == fases.Length-1)
        {
            proximo.interactable = false;
        }
    }
   
    public void QuestaoAnterior()
    {
        SalvarResultados();
        y--;
        AtualizarTela();

        proximo.interactable = true;

        if(y == 0)
        {
            anterior.interactable = false;
        }
    }
    private void SalvarResultados()
    {
        fases[y].pergunta = pergunta.text;
        fases[y].v1 = buttonText[0].text;
        fases[y].v2 = buttonText[1].text;
        fases[y].v3 = buttonText[2].text;
        if (t1.isOn)
        {
            fases[y].vCorreta = buttonText[0].text;
        }
        else if (t2.isOn)
        {
            fases[y].vCorreta = buttonText[1].text;
        }
        else
        {
            fases[y].vCorreta = buttonText[2].text;
        }
    }

    private void AtualizarTela()
    {
        contador.text = "Questão " + y;
        pergunta.text = fases[y].pergunta;
                                            
        buttonText[0].text = fases[y].v1;
        buttonText[1].text = fases[y].v2;
        buttonText[2].text = fases[y].v3;

        if (fases[y].vCorreta.Equals(fases[y].v1))
        {
            t1.isOn = true;
        }
        else if (fases[y].vCorreta.Equals(fases[y].v2))
        {
            t2.isOn = true;
        }
        else
        {
            t3.isOn = true;
        }
    }
}
