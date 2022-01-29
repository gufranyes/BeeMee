using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class cameraControl : MonoBehaviour
{
    public playerController player;
    public float speedStart;
    public float increasePerSecond;
    private float secondsElapsed = 0;
    public bool isStarted = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isStarted = true;
        }
        if (!player.gameOver && isStarted)
        {
            transform.Translate(Vector3.right * Time.deltaTime * (increasePerSecond * secondsElapsed + speedStart));
            secondsElapsed += Time.deltaTime;
        }
    }
}
