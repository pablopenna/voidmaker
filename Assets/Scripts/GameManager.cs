using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    
    void Start()
    {
        if(GameManager.instance == null) {
            GameManager.instance = this;
        } else {
            Destroy(gameObject);
        }
        print("singleton: " + instance);
    }

    public void GameOver() {
        print("GAME OVER!");
    }


}
