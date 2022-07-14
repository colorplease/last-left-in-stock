using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]Push[] boxes;
    [SerializeField]PlayerController[] players;
     GameObject[] playersObjects;
     public Vector2 player1Start;
     public Vector2 player2Start;
     [SerializeField]Transform Player1;
     [SerializeField]Transform Player2;
     [SerializeField]int currentGoalsDone;
     public int peakGoals;
     public GameObject World;
     public GameObject youWin;
     public GameObject[] levels;
     [SerializeField]int currentLevelNumber;
     [SerializeField]GameObject currentLevel;
    // Update is called once per frame

    void Start()
    {
        playersObjects = GameObject.FindGameObjectsWithTag("Player");
    }

   public void UpdateGoals(int updateValue)
    {
        
        Mathf.Clamp(currentGoalsDone += updateValue, 0, peakGoals);
        if(peakGoals == currentGoalsDone)
        {
            StartCoroutine(NextLevel());
        }
    }

    IEnumerator NextLevel()
    {
        yield return new WaitForSeconds(0.5f);
        World.SetActive(false);
        Destroy(currentLevel);
        youWin.SetActive(true);
        yield return new WaitForSeconds(2);
        currentLevelNumber++;
        GameObject newLevel = Instantiate(levels[currentLevelNumber], new Vector2(0, 0), Quaternion.identity);
        newLevel.transform.parent = World.transform;
        currentLevel = newLevel;
        World.SetActive(true);
        youWin.SetActive(false);
        UpdateEverything();
        currentGoalsDone = 0;
    }

    void UpdateEverything()
    {
            for(int i = 0; i < playersObjects.Length; i++)
            {
                players[i].ReCallObjects();
            }
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            GameObject[] boxesObjects = GameObject.FindGameObjectsWithTag("box");
            boxes = new Push[boxesObjects.Length];
            for(int i = 0; i < boxesObjects.Length; i++)
            {
                boxes[i] = boxesObjects[i].GetComponent<Push>();
                boxes[i].ResetPos();
            }
            players = new PlayerController[playersObjects.Length];
            for(int i = 0; i < playersObjects.Length; i++)
            {
                players[i] = playersObjects[i].GetComponentInParent<PlayerController>();
                players[i].ResetPos();
            }
        }
    }

    public void ResetPlayerPos()
    {
        Player1.position = player1Start;
        Player2.position = player2Start;
        playersObjects = GameObject.FindGameObjectsWithTag("Player");
         players = new PlayerController[playersObjects.Length];
            for(int i = 0; i < playersObjects.Length; i++)
            {
                players[i] = playersObjects[i].GetComponentInParent<PlayerController>();
                players[i].SetResetPos();
            }
    }
}
