using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game: MonoBehaviour
{

    public GameObject defeatEffect;
    private bool isDefeat = false;
    public void onEnimyPass() {
        if (!isDefeat) {
            isDefeat = true;
            defeat();
        }
    }
    private void defeat() {
        defeatEffect.SetActive(true);
    }
}
