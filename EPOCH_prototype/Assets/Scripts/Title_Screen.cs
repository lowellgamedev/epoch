using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title_Screen : MonoBehaviour {

    public void Update() {
        if (Input.anyKeyDown) {
            Start_Game();
        }
    }

    public void Start_Game() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
