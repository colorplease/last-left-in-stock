using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelInitilaize : MonoBehaviour
{
[SerializeField]Vector2 player1Start;
[SerializeField]Vector2 player2Start;
[SerializeField]int peakGoals;
public GameManager gameManager;

  void OnEnable()
  {
    gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    gameManager.player1Start = player1Start;
    gameManager.player2Start = player2Start;
    gameManager.peakGoals = peakGoals;
    gameManager.ResetPlayerPos();
  }
}
