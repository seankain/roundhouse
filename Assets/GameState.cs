using System;
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
            turnTaker.TurnEnded += TurnTransition;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        allTurnTakers[currentTurnTaker].OnTurnStarted(new EventArgs());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void TurnTransition(object sender,EventArgs e)
    {
        currentTurnTaker++;
        if(currentTurnTaker >= allTurnTakers.Length)
        {
            currentTurnTaker = 0;
        }
        Debug.Log($"{allTurnTakers[currentTurnTaker].gameObject.name}'s turn");
        allTurnTakers[currentTurnTaker].OnTurnStarted(new EventArgs());
    }
}
