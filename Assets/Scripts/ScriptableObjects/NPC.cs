using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewNPC", menuName = "NPC")]
public class NPC : ScriptableObject
{
    public string npcName;
    public bool hasDialog;
    public string[] npcDialog;
    public float maxHealth;
    public float characterSpeed;
}
