using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    GameManager gameManager;
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }
   void OnTriggerEnter2D(Collider2D other)
   {
        if(other.tag == "box")
        {
            gameManager.UpdateGoals(1);
        }
   }

    void OnTriggerExit2D(Collider2D other)
   {
        if(other.tag == "box")
        {
            gameManager.UpdateGoals(-1);
        }
   }
}
