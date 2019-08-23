using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetObjectActive : MonoBehaviour
{
    public GameObject[] objeto;
    public bool[] boolValue;

    public void SetObjecActive()
    {
        int i = 0;
        do
        {
            objeto[i].SetActive(boolValue[i]);
            i++;
        } while (objeto[i] != null);
    }
}