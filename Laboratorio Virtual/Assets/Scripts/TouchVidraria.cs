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
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit Hit;
            if (Physics.Raycast(ray, out Hit))
            {

                vidraria = Hit.transform.name;
                GameObject objeto = GameObject.FindGameObjectWithTag(vidraria);
                seta.transform.parent = objeto.transform;
                switch (vidraria)
                {
                    case "bequer":
                        seta.transform.localPosition = new Vector3(0, 0.15f, 0);
                        break;

                    case "erlenmeyer":
                        seta.transform.localPosition = new Vector3(0, 0.19f, 0);
                        break;

                    case "balaodefundoredondo":
                        Debug.Log("BALAO SELECIONADO");
                        seta.transform.localPosition = new Vector3(0, 1.15f, 0);
                        break;

                    case "provetagraduada":
                        seta.transform.localPosition = new Vector3(0, 0.57f, 0);
                        break;
                }
                seta.SetActive(true);

                Debug.Log(vidraria+"  "+VidrariaCorreta);
                if (Equals(vidraria, VidrariaCorreta))
                {
                    botaoCorreto.SetActive(true);
                    botaoIncorreto.SetActive(false);
                    Debug.Log("CORRETO");
                }
                else
                {
                    botaoIncorreto.SetActive(true);
                    botaoCorreto.SetActive(false);
                    Debug.Log("INCORRETO");
                }

            }

        }
    }
}
//https://www.youtube.com/watch?v=hi_KDpC1nzk