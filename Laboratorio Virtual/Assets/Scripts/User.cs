using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class User : MonoBehaviour
{
    public string Username, Role;

    public User() { }

    public User(string username, string role)
    {
        Username = username;
        Role = role;
    }
}
