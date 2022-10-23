using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMovement : MonoBehaviour
{
    [SerializeField] static float maxSpeed = 50f;
    [SerializeField] PlayerController playerController;

    Vector3 position;
    float timer;
    float speed;
    float acceleration;
    float accelerationTime = 1.2f;

    

    // Start is called before the first frame update
    void Start()
    {
        position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (speed < maxSpeed)
        {
            speed = Mathf.SmoothDamp(speed, maxSpeed, ref acceleration, accelerationTime);
        }
        position -= speed * Time.deltaTime * Vector3.forward;
        transform.position = position;
        
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            position += 300 * Vector3.forward;
        }
    }
     public void StopAndRestart()
    {
        timer = 0f;
        speed = 0f;
    }
}
