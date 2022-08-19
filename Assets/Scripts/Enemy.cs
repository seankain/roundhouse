using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float Health = 100;
    public float Lookup = 0.1f;
    private Animator anim;
    private Ragdoller ragdoller;
    private Rigidbody rb;
    private PlayerController player;
    private Camera mainCamera;
    private bool down = false;
    private float downCount = 5;
    private float downElapsed = 0;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        ragdoller = GetComponent<Ragdoller>();
        rb = GetComponent<Rigidbody>();
        player = FindObjectOfType<PlayerController>();
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.LookAt(player.gameObject.transform.position + Vector3.up * Lookup);
        if (down)
        {
            downElapsed += Time.deltaTime;
            if(downElapsed >= downCount)
            {
                downElapsed = 0;
                Getup();
            }
        }
    }

    public void Hit(float damage,Vector3 direction)
    {
        Health -= damage;
        if(Health <= 0)
        {
            down = true;
            anim.SetBool("Down", true);
            ragdoller.SetRagdoll(true);
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
            anim.SetBool("Down", false);
            ragdoller.SetRagdoll(false);
        }
    }


}
