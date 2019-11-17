using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class User 
{
    public string Username, Role, Uid;

    public User() { }

    public User(string username, string role, string uid)
    {
        Username = username;
        Role = role;
        Uid = uid;
    }
}
