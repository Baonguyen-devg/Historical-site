using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Player
{
    public string ID { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public int Score { get; set; }
    public Vector3 CurrentLocation { get; set; } 
}
