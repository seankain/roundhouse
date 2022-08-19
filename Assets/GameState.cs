using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{

    private PlayerController player;
    private TurnTaker playerTurnTaker;
    private List<TurnTaker> enemyTurnTakers;
    private TurnTaker[] allTurnTakers;

    private int currentTurnTaker = 0;

    void Awake()
    {
        allTurnTakers = FindObjectsOfType<TurnTaker>();
        enemyTurnTakers = new List<TurnTaker>();
        for(var i = 0;i<allTurnTakers.Length;i++)
        {
            var turnTaker = allTurnTakers[i];
           if(turnTaker.gameObject.tag == "Player")
            {
                player = turnTaker.GetComponent<PlayerController>();
                playerTurnTaker = turnTaker;
                //player goes first
                currentTurnTaker = i;
            }
            else
            {
                enemyTurnTakers.Add(turnTaker);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
