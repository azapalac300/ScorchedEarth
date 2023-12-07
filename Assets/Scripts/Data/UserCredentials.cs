using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "User Credentials", menuName = "SE Assets/User Credentials", order = 0)]
public class UserCredentials : ScriptableObject
{
    public string username;
    public string password;
    public string id;

    public bool canJoinGame;
   
}
