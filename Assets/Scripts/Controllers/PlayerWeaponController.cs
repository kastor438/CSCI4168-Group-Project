using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWeaponController : MonoBehaviour, IWeapon
{
    private PlayerStats playerStats;
    private PlayerInput playerInput;
    private GameObject weaponGameObject;
    private Animator weaponAnimator;
    private List<GameObject> spawnedProjectiles;

    public Weapon thisWeapon { get; private set; }
    public bool isAttacking;

    public void Start()
    {
        spawnedProjectiles = new List<GameObject>();
        playerStats = GetComponentInParent<PlayerStats>();
        playerInput = GameManager.Instance.GetComponent<PlayerInput>();
    }

    public void SetWeapon(Weapon weapon)
    {
        thisWeapon = weapon;
        weaponGameObject = Instantiate(weapon.equipmentPrefab, transform);
        weaponAnimator = weaponGameObject.GetComponent<Animator>();
    }

    public void Update()
    {
        if (!playerInput || !thisWeapon || !weaponGameObject || !weaponAnimator)
            return;

        if (playerInput.actions["BaseAttack"].IsPressed() && !isAttacking)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(playerInput.actions["MousePos"].ReadValue<Vector2>());
            mousePos.z = 0;
            Vector3 direction = Vector3.Normalize(mousePos - transform.position);
            if (thisWeapon.GetType() == typeof(RangedWeapon))
            {
                GameObject newProjectile = Instantiate(((RangedWeapon)thisWeapon).projectilePrefab, transform.position + direction, Quaternion.identity);

                newProjectile.GetComponent<ProjectileController>().SetProjectileInfo(direction, ((RangedWeapon)thisWeapon).projectileSpeed, ((RangedWeapon)thisWeapon).projectileLifetime);
                spawnedProjectiles.Add(newProjectile);
            }
            StartCoroutine(PerformAttack());
        }
        else if (thisWeapon.hasSpecialAttack && playerInput.actions["SpecialAttack"].WasPerformedThisFrame() && !isAttacking)
        {
            if (thisWeapon.GetType() == typeof(RangedWeapon))
            {

            }
            StartCoroutine(PerformSpecialAttack());
        }

/*        // Check if weapon attacks are so spaced out that the weapon reverts to first attack animation.
        if (Time.unscaledTime > lastAttackTime + maxAttackSpacingTime)
        {
            animator.SetInteger("attackCount", 0);
        }
        // Queue base attacks;
        if (playerInput.actions["BaseAttack"].WasPerformedThisFrame() && !attackQueued)
        {
            attackQueued = true;
        }*/

        
    }

    public IEnumerator PerformAttack()
    {
        isAttacking = true;
        weaponAnimator.SetTrigger("Attack");
        yield return new WaitForSeconds(1 / thisWeapon.attackSpeed);
        isAttacking = false;
    }

    public IEnumerator PerformSpecialAttack()
    {
        isAttacking = true;
        weaponAnimator.SetTrigger("SpecialAttack");
        yield return new WaitForSeconds(1 / thisWeapon.attackSpeed);
        isAttacking = false;
    }
}
