using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchVidraria : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject botaoCorreto, botaoIncorreto, seta;
    public string VidrariaCorreta;
    string vidraria;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       if(Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit Hit;
            if(Physics.Raycast(ray, out Hit))
            {
                
                vidraria = Hit.transform.name;
                GameObject objeto = GameObject.FindGameObjectWithTag(vidraria);
                seta.transform.position = new Vector3(objeto.transform.position.x, seta.transform.position.y, objeto.transform.position.z);
                seta.SetActive(true);

                if (Equals(vidraria, VidrariaCorreta))
                {
                    botaoCorreto.SetActive(true);
                    botaoIncorreto.SetActive(false);
                }
                else
                {
                    botaoIncorreto.SetActive(true);
                    botaoCorreto.SetActive(false);
                }
            }
        }
    }

    /*public void teste()
    {
        string v = "BalaodefundoRedondo";
        GameObject objeto = GameObject.FindGameObjectWithTag(v);
        seta.transform.position = new Vector3(objeto.transform.position.x, seta.transform.position.y, objeto.transform.position.z);
        seta.SetActive(true);

        if (Equals(v, VidrariaCorreta))
        {
            botaoCorreto.SetActive(true);
            botaoIncorreto.SetActive(false);
        }
        else
        {
            botaoIncorreto.SetActive(true);
            botaoCorreto.SetActive(false);
        }
    }*/


}
//https://www.youtube.com/watch?v=hi_KDpC1nzk