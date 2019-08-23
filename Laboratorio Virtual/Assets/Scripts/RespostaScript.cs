using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespostaScript : MonoBehaviour
{
    public GameObject button;
 
    public void responder()
    {
        button.SetActive(true);
    }
    
}
