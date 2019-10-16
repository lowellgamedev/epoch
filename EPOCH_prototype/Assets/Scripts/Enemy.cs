using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public int health;
    public int armor;


    private Animator anim;
    public GameObject bloodSplash;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    public void TakeDamage(int damage) {
        Instantiate(bloodSplash, transform.position, Quaternion.identity);
        health -= damage;
        Debug.Log("Dummy hit!");
    }
}
