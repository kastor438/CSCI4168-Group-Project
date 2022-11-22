using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemy", menuName = "Enemy")]
public class Enemy : ScriptableObject
{
    public string enemyName;
    public int maxHealth;
    public int damage;
    public int attackSpeed;
    public int speed;
    public EnemyType enemyType;
    public GameObject enemyAttack;
}

public enum EnemyType { Melee, Ranged }