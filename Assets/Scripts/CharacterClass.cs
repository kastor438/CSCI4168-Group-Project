using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCharacter", menuName = "Character")]
public class CharacterClass : ScriptableObject
{
    public string characterName;
    public GameObject characterPrefab;
    public int maxHealth;
    public float characterSpeed;
}