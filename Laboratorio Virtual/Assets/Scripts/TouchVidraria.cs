using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchVidraria : MonoBehaviour
{

    public GameObject botaoCorreto, botaoIncorreto, seta;
    public string VidrariaCorreta;
    string vidraria;
    public string[] vidrarias;
    public float[] alturaVidrarias;


    void Update()
    {
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit Hit;
            if (Physics.Raycast(ray, out Hit))
            {
                vidraria = Hit.transform.name;
                GameObject objeto = GameObject.FindGameObjectWithTag(vidraria);
                seta.transform.parent = objeto.transform;

                for (int i = 0; i < vidrarias.Length; i++)
                {
                    if (vidraria.Equals(vidrarias[i]))
                    {
                        seta.transform.localPosition = new Vector3(0, alturaVidrarias[i], 0);
                        seta.SetActive(true);
                    }
                }

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
}
//https://www.youtube.com/watch?v=hi_KDpC1nzk