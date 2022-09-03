using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scorable : MonoBehaviour
{

    public bool Breakable = false;
    public float Score = 0;
    private Vector3 LastPosition;
    private Quaternion OriginalRotation;
    private Rigidbody rb;
    private GameState gameState;
    
    private bool scoring = false;
    

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        gameState = FindObjectOfType<GameState>();
        LastPosition = gameObject.transform.position;
        OriginalRotation = gameObject.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (!scoring) { return; }
        if (rb.velocity.sqrMagnitude > 0)
        {
            //Score += Vector3.Distance(LastPosition, gameObject.transform.position);
            gameState.TotalScore += Mathf.Ceil(Vector3.Distance(LastPosition, gameObject.transform.position));
            LastPosition = gameObject.transform.position;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        var enemy = collision.gameObject.GetComponent<Enemy>();
        if (enemy != null)
        {
            scoring = true;
        }
    }
}
