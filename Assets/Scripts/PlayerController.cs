using NUnit.Framework;
using System;
using System.Collections;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float speed = 200.0f;
    public float bound = 13f;
    public float reloadTime = 10.0f;
    public float splashtime = 3.0f;
    public bool shootAble = true;
    public GameObject splash;
    private Rigidbody playerRb;
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        if (Input.GetKey(KeyCode.Space))
        {
            if (shootAble)
            {
                Shoot();
                shootAble = false;
                StartCoroutine(Reload(reloadTime));
            }
        }
    }

    // Player movement
    void Shoot()
    {
        splash.SetActive(true);
        StartCoroutine(SplashDisappear(splashtime));
    }

    IEnumerator SplashDisappear(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        splash.SetActive(false);
    }

    IEnumerator Reload(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        shootAble = true;
    }

    void MovePlayer()
    {
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");
        //transform.Translate(Vector3.forward * speed * verticalInput * Time.deltaTime);
        //transform.Translate(Vector3.right * speed * horizontalInput * Time.deltaTime);
        playerRb.AddForce(Vector3.forward * speed * verticalInput);
        playerRb.AddForce(Vector3.right * speed * horizontalInput);
    }

    // Walls will be walls
    void CheckBoundaries()
    {
        if (transform.position.z > bound)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, bound);
        }
        if (transform.position.z < -bound)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -bound);
        }
        if (transform.position.x > bound)
        {
            transform.position = new Vector3(bound, transform.position.y, transform.position.z);
        }
        if (transform.position.x < -bound)
        {
            transform.position = new Vector3(-bound, transform.position.y, transform.position.z);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            Destroy(gameObject);
        }
    }
}
