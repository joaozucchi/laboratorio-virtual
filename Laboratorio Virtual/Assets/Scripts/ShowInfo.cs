using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowInfo : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject info;

    public void ShowInformation()
    {
        info.SetActive(!info.activeInHierarchy);
    }
    
}
