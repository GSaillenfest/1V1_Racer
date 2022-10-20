using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Rigidbody playerRb;
    [SerializeField] static float speed = 100f;
    [SerializeField] static float attackForce = 150f;
    [SerializeField] static float xRange = 6f;
    [SerializeField] ParticleSystem contactFX;

    Joint joint;
    bool attack;
    float horizontalInput;
    float startXPos;
    float jointXPos;

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
        horizontalInput = Input.GetAxis("Horizontal");
        if (horizontalInput != 0 & Input.GetButtonDown("Fire1")) attack = true;

        jointXPos += horizontalInput * speed * Time.deltaTime;
        jointXPos = Mathf.Clamp(jointXPos, startXPos - xRange, startXPos + xRange);
        joint.connectedAnchor = new Vector3(jointXPos, joint.connectedAnchor.y, joint.connectedAnchor.z);
    }

    private void FixedUpdate()
    {
        //playerRb.AddForce(speed * Vector3.forward, ForceMode.VelocityChange);
        
        if (horizontalInput != 0 & attack)
        {
            playerRb.AddForce(attackForce * horizontalInput/Mathf.Abs(horizontalInput) * Vector3.right, ForceMode.Impulse);
            attack = false;
        }
        //playerRb.AddForce(horizontalInput * force * Vector3.right, ForceMode.Force);
    }

    private void OnCollisionEnter(Collision collision)
    {
        contactFX.Play();
    }
}
