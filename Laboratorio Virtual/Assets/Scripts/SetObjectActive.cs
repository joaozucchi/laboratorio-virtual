using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetObjectActive : MonoBehaviour
{
    public GameObject[] objeto;
    public bool[] boolValue;

    public void SetObjecActive()
    {
        for (int i = 0; i < objeto.Length; i++)
        {
            objeto[i].SetActive(boolValue[i]);
        }
    }
}