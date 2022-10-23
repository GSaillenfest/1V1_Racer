using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Rigidbody playerRb;
    [SerializeField] static float speed = 150f;
    [SerializeField] static float attackForce = 100f;
    [SerializeField] static float xRange = 6f;
    [SerializeField] ParticleSystem contactFX;
    [SerializeField] BackgroundMovement bGmovement;
    [SerializeField] Animator animator;

    Joint joint;
    bool attack;
    public bool stop;
    float horizontalInput;
    float startXPos;
    float jointXPos;
    float timer;
    bool isDestroyed;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        startXPos = transform.position.x;
        jointXPos = startXPos;
        joint = GetComponent<Joint>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDestroyed)
        {
            GetInputs();
            Move();
        }
    }

    private void Move()
    {
        if (timer > 1f)
        {
            jointXPos += horizontalInput * speed * Time.deltaTime;
            jointXPos = Mathf.Clamp(jointXPos, startXPos - xRange, startXPos + xRange);
            joint.connectedAnchor = new Vector3(jointXPos, joint.connectedAnchor.y, joint.connectedAnchor.z);
        }
    }

    private void GetInputs()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        timer += Time.deltaTime;
        if (timer > 3f & horizontalInput != 0 & Input.GetButtonDown("Fire1")) attack = true;
    }

    private void FixedUpdate()
    {
        Attack();
    }

    private void Attack()
    {
        if (horizontalInput != 0 & attack)
        {
            timer = 0f;
            playerRb.AddForce(attackForce * horizontalInput / Mathf.Abs(horizontalInput) * Vector3.right, ForceMode.Impulse);
            attack = false;
        }


    }

    private void OnCollisionEnter(Collision collision)
    {
        contactFX.Play();
        if (collision.gameObject.CompareTag("Danger"))
        {
            DestroyPlayer();
            collision.gameObject.GetComponent<Collider>().enabled = false;
        }
    }

    private void DestroyPlayer()
    {
        animator.SetTrigger("isDestroyed");
        //isDestroyed = true;
        bGmovement.StopAndRestart();
    }
}
