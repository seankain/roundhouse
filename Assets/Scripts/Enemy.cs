using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public float Health = 100;
    public float Lookup = 0.1f;
    public float MaxMoveDistance = 1.0f;
    public float AttackDistance = 1.0f;
    public float MaxMoveTime = 5.0f;
    public float MoveSpeed = 1.0f;
    private Animator anim;
    private Ragdoller ragdoller;
    private Rigidbody rb;
    private PlayerController player;
    private TurnTaker turnTaker;
    private UnityEngine.AI.NavMeshAgent nav;
    private Camera mainCamera;
    private bool down = false;
    private float downCount = 5;
    private float downElapsed = 0;
    private float moveElapsed = 0;
    private bool isTurn = false;
    private bool moving = false;
    private bool isWithinAttack = false;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        ragdoller = GetComponent<Ragdoller>();
        turnTaker = GetComponent<TurnTaker>();
        nav = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        player = FindObjectOfType<PlayerController>();
        mainCamera = Camera.main;
        turnTaker.TurnStarted += TurnTaker_TurnStarted;
        nav.stoppingDistance = AttackDistance;
        nav.speed = MoveSpeed;
    }

    private void TurnTaker_TurnStarted(object sender, System.EventArgs e)
    {
        isTurn = true;
        moveElapsed = 0;
        //if (Vector3.Distance(player.gameObject.transform.position, gameObject.transform.position) > MaxMoveDistance)
        //{
        //    var destination = Vector3.MoveTowards(gameObject.transform.position, player.gameObject.transform.position, MaxMoveDistance);
        //    StartCoroutine(Move(destination));
        //    Attack();
        //}
    }

    private IEnumerator Move(Vector3 destination)
    {
        var elapsed = 0f;
        while (Vector3.Distance(this.gameObject.transform.position, destination) > 0.1f && elapsed < MaxMoveTime)
        {
            this.gameObject.transform.position = Vector3.Lerp(this.gameObject.transform.position, destination, Time.deltaTime * MoveSpeed);
            elapsed += Time.deltaTime;
            yield return null;
        }
    }
    private void Attack()
    {
        anim.SetTrigger("Punch");
        isTurn = false;
        turnTaker.OnTurnEnded(new EventArgs());
        //StartCoroutine(WaitAttack());
    }

    private IEnumerator WaitAttack()
    {
        //Debug.Log(anim.GetCurrentAnimatorClipInfo(0)[0].clip.name);
        var state = anim.GetCurrentAnimatorStateInfo(0);
        //Debug.Log(state);
        var elapsed = 0f;
        while (state.IsName("Punch"))
        {
            yield return null;
        }
        while (elapsed < 2)
        {
            elapsed += Time.deltaTime;
            yield return null;
        }

    }

    // Update is called once per frame
    void Update()
    {
        //this.transform.LookAt(player.gameObject.transform.position + Vector3.up * Lookup);

        if (down)
        {
            if(Health <= 0) { return; }
            downElapsed += Time.deltaTime;
            if (downElapsed >= downCount)
            {
                downElapsed = 0;
                Getup();
            }
            return;
        }
        if (isTurn)
        {
            //Check distance from player
            isWithinAttack = Vector3.Distance(this.transform.position, player.gameObject.transform.position) <= AttackDistance;
            Debug.Log($"{gameObject.name} is {isWithinAttack} from player");
            if (isWithinAttack)
            {
                Debug.Log($"{gameObject.name} is attacking");
                Attack();
            }
            else
            {
                Debug.Log($"{gameObject.name} is moving");
                if (moveElapsed < MaxMoveTime)
                {
                    nav.isStopped = false;
                    //Move closer
                    nav.SetDestination(player.transform.position);
                    moveElapsed += Time.deltaTime;
                    anim.SetFloat("Movement", 1);
                }
                else
                {
                    nav.isStopped = true;
                    anim.SetFloat("Movement", 0);
                    isTurn = false;
                    turnTaker.OnTurnEnded(new EventArgs());
                }

            }

        }
    }

    public void Hit(float damage, Vector3 direction,Vector3 worldPosition)
    {
        Health -= damage;
        Debug.Log($"{gameObject.name} hit for {damage} damage, {Health} health remaining");
        if (Health <= 0)
        {
            down = true;
            nav.enabled = false;
            anim.SetBool("Down", true);
            ragdoller.SetRagdoll(true);
            //rb.AddForceAtPosition(direction, worldPosition, ForceMode.Impulse);
        }
        else
        {
            anim.SetTrigger("Struck");
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        //Debug.Log(collision.gameObject);
        //if(collision.gameObject.tag == "Foot")
        //{
        //    down = true;
        //    anim.SetBool("Down", true);
        //    ragdoller.SetRagdoll(true);
        //}
    }

    void Getup()
    {
        if (Health > 0)
        {
            down = false;
            //anim.enabled = true;
            nav.enabled = true;
            anim.SetBool("Down", false);
            ragdoller.SetRagdoll(false);
        }
    }


}
