using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCharacter", menuName = "Character")]
public class CharacterClass : ScriptableObject
{
    public string characterName;
    public GameObject characterPrefab;
    public Sprite characterSprite;
    public int maxHealth;
    public float characterSpeed;
    public float characterWeight;
    public float meleeAttackSpeedMultiplier;
    public float rangedAttackSpeedMultiplier;
    public float oxygenConsumptionRate;
    public float maxCarryWeight;
    public bool suffersFromRecoil;
}