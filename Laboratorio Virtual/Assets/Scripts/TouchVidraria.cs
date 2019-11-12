using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TouchVidraria : MonoBehaviour
{
    public GameObject p1, p2, p3;
    public GameObject botaoCorreto, botaoIncorreto, seta;
    string VidrariaCorreta = Fase.vCorreta;
    string vidraria;
    string pergunta = Fase.pergunta;
    public TextMeshProUGUI perguntaText;
    public string[] vidrarias;
    public float[] alturaVidrarias;

    private void Start()
    {
        perguntaText.text = pergunta;

        GameObject a = Instantiate(Resources.Load("Vidrarias/"+Fase.v1), new Vector3(0,0,0), Quaternion.identity) as GameObject;
        GameObject b = Instantiate(Resources.Load("Vidrarias/" + Fase.v2), new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        GameObject c = Instantiate(Resources.Load("Vidrarias/" + Fase.v3), new Vector3(0, 0, 0), Quaternion.identity) as GameObject;

        Vector3 aScale = new Vector3 (a.transform.localScale.x, a.transform.localScale.y, a.transform.localScale.z);
        Vector3 bScale = new Vector3(b.transform.localScale.x, b.transform.localScale.y, b.transform.localScale.z);
        Vector3 cScale = new Vector3(c.transform.localScale.x, c.transform.localScale.y, c.transform.localScale.z);

        a.transform.parent = p1.transform;
        b.transform.parent = p2.transform;
        c.transform.parent = p3.transform;

        a.transform.localPosition = new Vector3(0,0,0);
        b.transform.localPosition = new Vector3(0, 0, 0);
        c.transform.localPosition = new Vector3(0, 0, 0);

        a.transform.localScale = aScale;
        b.transform.localScale = bScale;
        c.transform.localScale = cScale;
    }

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