using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchVidraria : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject botaoCorreto,botaoIncorreto;
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
                switch (vidraria)
                {
                    case "bequer":
                        botaoCorreto.SetActive(true);
                        botaoIncorreto.SetActive(false);
                        break;

                    case "erlenmeyer":
                        botaoIncorreto.SetActive(true);
                        botaoCorreto.SetActive(false);
                        break;
                }
            }
        }
    }
}
//https://www.youtube.com/watch?v=hi_KDpC1nzk