using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    public float PowerMeterSpeed = 10f;
    public float TurnSpeed = 25f;
    public float KickForceMultiplier = 500f;
    private Animator anim;
    private KickArc kickArc;
    private TurnTaker turnTaker;
    private bool isTurn = false;
    private PlayerUI ui;
    //-1 for coming back, 1 for going forward, 0 for not cycling
    private int cycleDirection = 0;
    // Start is called before the first frame update
    private PowerMeter powerMeter;
    private float power = 0;
    private bool settingPower = false;

    void Awake() {
        anim = GetComponentInChildren<Animator>();
        ui = GetComponent<PlayerUI>();
        kickArc = GetComponent<KickArc>();
        powerMeter = FindObjectOfType<PowerMeter>();
        turnTaker = GetComponent<TurnTaker>();
        turnTaker.TurnStarted += TurnTaker_TurnStarted;
    }

    void Start()
    {

    }

    private void TurnTaker_TurnStarted(object sender, EventArgs e)
    {
        //ui.FadePowerMeter(true, 10);
        isTurn = true;
        ui.ShowPowerMeter();
        ui.ShowMessage("Your turn!", 10);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isTurn) { return; }
        var kickInputPressed = Input.GetKeyDown(KeyCode.Space);
        // if (Input.GetKeyDown(KeyCode.Space))
        //{
        switch (cycleDirection)
        {
            case -1:
                if (kickInputPressed)
                {
                    cycleDirection = 0;
                    //Kick now
                    anim.SetTrigger("Kick");
                    StartCoroutine(WaitKick());
                    break;
                }
                power -= (PowerMeterSpeed * Time.deltaTime);
                //If power is all the way down without input, end cycle
                if (power <= 0)
                {
                    power = 0;
                    cycleDirection = 0;
                    break;
                }
                break;
            case 0:
                //initiate power setting
                if (kickInputPressed)
                {
                    cycleDirection = 1;
                }
                //but if not inputting kick just dont do anything
                break;
            case 1:
                power += PowerMeterSpeed * Time.deltaTime;
                if (power >= 100)
                {
                    power = 100;
                    cycleDirection = -1;
                    break;
                }
                if (kickInputPressed)
                {
                    cycleDirection = -1;
                    break;
                }
                break;
            default:
                cycleDirection = 0;
                break;
                //  }

                //anim.SetTrigger("Kick");
                //kickArc.Draw(1.5f);
        }
        powerMeter.SetPowerLevel(power);
    }


    IEnumerator WaitKick()
    {
        //Debug.Log(anim.GetCurrentAnimatorClipInfo(0)[0].clip.name);
        var state = anim.GetCurrentAnimatorStateInfo(0);
        //Debug.Log(state);
        var elapsed = 0f;
        while (state.IsName("Kick"))
        {
            yield return null;
        }
        while (elapsed < 2)
        {
            elapsed += Time.deltaTime;
            yield return null;
        }
        power = 0;
        Debug.Log("player turn ended");
        //ui.FadePowerMeter(false, 10);
        ui.HidePowerMeter();
        ui.HideMessage();
        isTurn = false;
        turnTaker.OnTurnEnded(new EventArgs());
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision);
        var enemy = collision.gameObject.GetComponent<Enemy>();
        if(enemy != null)
        {
            //I think I made forward backward on the player gameobject because of course i did, TODO fix that
            //enemy.Hit(100, -gameObject.transform.forward * KickForceMultiplier,collision.GetContact(0).point);
            enemy.Hit(100, Vector3.zero,collision.GetContact(0).point);
        }
    }

    public void LookLeft() { this.gameObject.transform.Rotate(0, -1 * Time.deltaTime *TurnSpeed, 0); }
    public void LookRight() { this.gameObject.transform.Rotate(0, 1 * Time.deltaTime * TurnSpeed, 0); }

}
