using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class User
{
    public string userName;
    public string localId;
    public string role;

    public User()
    {
        userName = UserFirebase.nome;
        localId = UserFirebase.localId;
        role = UserFirebase.papel;
    }
}