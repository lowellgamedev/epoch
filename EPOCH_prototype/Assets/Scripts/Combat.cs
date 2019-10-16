using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour {

    private float timeBtwAttack;
    public float startTimeBtwAttack;

    public Transform attackPos;
    public Transform ultPos;
    public LayerMask whatIsEnemies;
    //public Animator camAnim;
    public Animator playerAnim;
    public float attackRange;
    //public float ultRange;
    public int damage;
    //public int ult_damage;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        if (timeBtwAttack <= 0) {
            if (Input.GetKeyDown("j")) {
                //camAnim.SetTrigger("shake");
                playerAnim.SetTrigger("Attack1");
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies);
                for (int i = 0; i < enemiesToDamage.Length; i++) {
                    enemiesToDamage[i].GetComponent<Enemy>().TakeDamage(damage);
                }
            } else if (Input.GetKeyDown("u")) {
                playerAnim.SetTrigger("Attack2");
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(ultPos.position, attackRange, whatIsEnemies);
                for (int i = 0; i < enemiesToDamage.Length; i++) {
                    enemiesToDamage[i].GetComponent<Enemy>().TakeDamage(damage);
                }
            }
            timeBtwAttack = startTimeBtwAttack;
        } else {
            timeBtwAttack -= Time.deltaTime;
        }
    }

    void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}
