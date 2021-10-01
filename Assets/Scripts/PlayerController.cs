using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour {
	
	public float speed;
	public TextMeshProUGUI countText;
    public TextMeshProUGUI leftText;
	public GameObject winTextObject;
    public GameObject celebrationObject;
    public Vector3 currentVelocity;
    public AudioSource audioSource;
    public AudioClip soundEffect;


    private Rigidbody rb;
    private float movementX;
    private float movementY;
	private int count;
    private bool moved;

	void Start ()
	{
		rb = GetComponent<Rigidbody>();

		count = 0;

		SetCountText ();
        
        winTextObject.SetActive(false);
        celebrationObject.SetActive(false);
        moved = false;
	}

	void FixedUpdate ()
	{
		Vector3 movement = new Vector3(movementX, 0.0f, movementY);
		rb.AddForce(movement * speed);
        if (moved) 
        {
            currentVelocity = rb.velocity;
        }
        else
        {
            currentVelocity = new Vector3(0, 0, 1.0f);
        }
        rb.transform.rotation = Quaternion.LookRotation(currentVelocity, Vector3.right);
	}

	void OnTriggerEnter(Collider other) 
	{
		if (other.gameObject.CompareTag("PickUp"))
		{
			other.gameObject.SetActive(false);
			count = count + 1;
			SetCountText();
            StartCoroutine("FlashRed");
		}
	}

    void OnCollisionEnter(Collision other) 
	{
        if (other.gameObject.CompareTag("wall"))
        {
            audioSource.PlayOneShot(soundEffect, 1.0f);
        }
	}

    void OnMove(InputValue value)
    {
        Vector2 v = value.Get<Vector2>();

        movementX = v.x;
        movementY = v.y;

        if (moved == false) 
        {
            moved = true;
        }
    }

    void SetCountText()
	{
		countText.text = "Count: " + count.ToString();
        leftText.text = (12 - count).ToString() + " more to go!";

		if (count >= 12) 
		{
            winTextObject.SetActive(true);
            celebrationObject.SetActive(true);
		}
	}

    IEnumerator FlashRed() 
    {
        for (int i = 0; i < 2; i++)
        {
            GetComponent<Renderer>().material.color = Color.red;
            yield return new WaitForSeconds(0.2f);
            GetComponent<Renderer>().material.color = new Color(0, 220, 255);
            yield return new WaitForSeconds(0.2f);
        }
    }
}

