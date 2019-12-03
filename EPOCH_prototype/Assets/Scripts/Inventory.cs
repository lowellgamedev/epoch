using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

    private bool InvEnabled;
    public GameObject inventory;

    private int allSlots;
    private int enabledSlots;
    private GameObject[] slots;

    public GameObject slotHolder;

    void Start() {
        allSlots = 6;
        slots = new GameObject[allSlots];

        for (int i = 0; i < allSlots; i++) {
            slots[i] = slotHolder.transform.GetChild(i).gameObject;
        }
    }
    void Update() {
        if (Input.GetKeyDown(KeyCode.Tab)) {
            InvEnabled = true;
        }
        if (Input.GetKeyUp(KeyCode.Tab)) {
            InvEnabled = false;
        }

        if (InvEnabled) {
            inventory.SetActive(true);
        }
        else {
            inventory.SetActive(false);
        }
    }
}
