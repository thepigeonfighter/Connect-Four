using ConnectFour;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerPicker : MonoBehaviour
{

    public GameObject[] Players;
    public InputField p1NameInputField, p2NameInputField;

    private static PlayerPicker _instance;
    private int _p1;
    private int _p2;
    private string _p1Name;
    private string _p2Name;
    // Start is called before the first frame update

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode arg1)
    {
        if (scene.name == "Game")
        {
            if (_p1 == 0 && !string.IsNullOrEmpty(_p1Name))
            {
                Players[_p1].GetComponent<HumanPlayer_Base>()._myName = _p1Name;
            }
            Instantiate(Players[_p1]);
            if (_p2 == 0 && !string.IsNullOrEmpty(_p2Name))
            {
                Players[_p2].GetComponent<HumanPlayer_Base>()._myName = _p2Name;
            }
            Instantiate(Players[_p2]);
        }
    }

    void Start()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(_instance.gameObject);
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnPlayerOneChanged(int playerNumber)
    {
        _p1 = playerNumber;
        if (playerNumber != 0)
        {
            p1NameInputField.readOnly = true;
            p1NameInputField.text = Players[playerNumber].GetComponent<AI_Base>()._myName;
        }
        else
        {
            p1NameInputField.readOnly = false;
            p1NameInputField.text = "";
            p1NameInputField.placeholder.enabled = true;
        }
    }
    public void OnPlayerTwoChanged(int playerNumber)
    {
        _p2 = playerNumber;
        if (playerNumber != 0)
        {
            p2NameInputField.readOnly = true;
            p2NameInputField.text = Players[playerNumber].GetComponent<AI_Base>()._myName;
        }
        else
        {
            p2NameInputField.readOnly = false;
            p2NameInputField.text = "";
            p2NameInputField.placeholder.enabled = true;
        }
    }
    public void OnPlayerOneNameChanged(string name)
    {
        _p1Name = name;
    }
    public void OnPlayerTwoNameChanged(string name)
    {
        _p2Name = name;
    }
    public void OnStartGame()
    {
        SceneManager.LoadScene("Game");

    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
