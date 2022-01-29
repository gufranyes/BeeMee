using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using TextMeshProUGUI = TMPro.TextMeshProUGUI;

public class uiController : MonoBehaviour
{
    public cameraControl camControl;
    public playerController player;
    public TextMeshProUGUI _scoreText;
    public TextMeshProUGUI _messageText;
    public GameObject _lives;
    private string score;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (camControl.isStarted)
        {
            _messageText.gameObject.GetComponent<TextMeshProUGUI>().enabled = false;
        }
        if (player.gameOver)
        {
            Debug.Log("Game Over");
            _messageText.gameObject.GetComponent<TextMeshProUGUI>().enabled = true;
        }
        score = ((int)camControl.transform.position.x).ToString();
        _scoreText.text = "Score: " + score;
        switch (player.lives)
        {
            case 3:
                _lives.transform.GetChild(2).gameObject.SetActive(true);
                _lives.transform.GetChild(1).gameObject.SetActive(true);
                _lives.transform.GetChild(0).gameObject.SetActive(true);
                break;
            case 2:
                _lives.transform.GetChild(2).gameObject.SetActive(false);
                _lives.transform.GetChild(1).gameObject.SetActive(true);
                _lives.transform.GetChild(0).gameObject.SetActive(true);
                break;
            case 1:
                _lives.transform.GetChild(2).gameObject.SetActive(false);
                _lives.transform.GetChild(1).gameObject.SetActive(false);
                _lives.transform.GetChild(0).gameObject.SetActive(true);
                break;
            case 0:
                _lives.gameObject.SetActive(false);
                break;
            default:
                _lives.gameObject.SetActive(false);
                break;
        }
    }
}
