using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Knife : MonoBehaviour {
    public Rigidbody rb;
    private Vector2 startSwipe;
    private Vector2 endSwipe;

    public float force = 5f;
    public float torque = 20f;

    private float flyTime;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (!rb.isKinematic)
            return;
        if (Input.GetMouseButtonDown(0))
        {
            startSwipe = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        }

        if (Input.GetMouseButtonUp(0))
        {
            endSwipe = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            Swipe();
        }
	}

    void Swipe()
    {
        rb.isKinematic = false;
        flyTime = Time.time;
        Vector2 swipe = endSwipe - startSwipe;
        rb.AddForce(swipe * force, ForceMode.Impulse);
        rb.AddTorque(0f, 0f, -torque, ForceMode.Impulse);
    }

    void OnTriggerEnter(Collider collider)
    {
        if(collider.tag == "WoodenBlock")
        {
            rb.isKinematic = true;
        }
        else
        {
            Restart();
        }
    }

    void OnCollisionEnter()
    {
        float timeInAir = Time.time - flyTime;
        if(!rb.isKinematic && timeInAir > 0.05f)
        {
            Restart();
        }
    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
