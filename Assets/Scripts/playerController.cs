using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;

public class playerController : MonoBehaviour
{
    public Rigidbody2D r2d;
    private Animator _anim;
    public cameraControl camController;
    private AudioSource _audio;
    public bool gameOver;
    public int lives = 3;

    // Start is called before the first frame update
    void Start()
    {
        r2d = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _audio = GetComponent<AudioSource>();
        gameOver = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey("space"))
        {
            r2d.AddForce(new Vector2(0,25f),ForceMode2D.Force);
            _anim.SetBool("isGrounded", false);
        } 
        else if (Input.GetKeyUp("space"))
        {
            r2d.velocity *= 0.3f;
            r2d.AddForce(new Vector2(0, -10f), ForceMode2D.Force);
            _anim.SetBool("isGrounded", true);
        }
    }

    void Update()
    {
        if (gameOver)
        {
            if (Input.GetMouseButtonDown(0))
            {
                SceneManager.LoadScene("Game");
            }
            return;
        }
        if (camController.isStarted)
        {
            _anim.SetBool("isStarted", true);
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Obstacle"))
        {
            lives--;
        }
        else if (collider.CompareTag("Collectible") && lives != 3)
        {
            lives++;
            _audio.Play();
        }
        if (lives == 0)
        {
            gameOver = true;
            _anim.enabled = false;
            r2d.velocity = new Vector2(0, 0);
            r2d.isKinematic = true;
        }
    }
}
