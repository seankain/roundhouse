using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameState : MonoBehaviour, ISelectionNotifier
{

    public float TurnEndWaitTime = 2f;
    public float TotalScore = 0;
    private PlayerController player;
    private TurnTaker playerTurnTaker;
    private List<TurnTaker> enemyTurnTakers;
    private TurnTaker[] allTurnTakers;
    private int currentTurnTaker = 0;
    private Scorable[] scorableItems;

    public GameObject SelectedGameObject { get { return FindObjectsOfType<Selectable>().FirstOrDefault(s => s.Selected).gameObject; } }

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
        scorableItems = FindObjectsOfType<Scorable>();
    }

    // Start is called before the first frame update
    void Start()
    {
        allTurnTakers[currentTurnTaker].OnTurnStarted(new EventArgs());
    }

    // Update is called once per frame
    void Update()
    {
        foreach(var s in scorableItems)
        {
            TotalScore += s.Score;
        }
    }

    private IEnumerator FireNextTurn()
    {
        var elapsed = 0f;
        while(elapsed < TurnEndWaitTime)
        {
            elapsed += Time.deltaTime;
            yield return null;
        }
        Debug.Log($"{allTurnTakers[currentTurnTaker].gameObject.name}'s turn");
        allTurnTakers[currentTurnTaker].OnTurnStarted(new EventArgs());
    }

    void TurnTransition(object sender,EventArgs e)
    {
        currentTurnTaker++;
        if(currentTurnTaker >= allTurnTakers.Length)
        {
            currentTurnTaker = 0;
        }
        StartCoroutine(FireNextTurn());
    }

    public void NotifyOfSelection(Selectable selectable)
    {
        Debug.Log($"{selectable.gameObject.name} selected");
    }
}
