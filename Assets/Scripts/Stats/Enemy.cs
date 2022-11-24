using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemy", menuName = "Enemy")]
public class Enemy : ScriptableObject
{
    public string enemyName;
    public int maxHealth;
    public int damage;
    public float attackSpeed;
    public float movementSpeed;
    public float attackRange;
    public float chaseRange;
}