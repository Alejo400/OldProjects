using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager _sharedInstance;
    public SelectedStructure _selectedStructure;
    public GameObject _playerDetected;
    private void Awake()
    {
        if(_sharedInstance == null)
        {
            _sharedInstance = this;
        }
    }
    private void Start()
    {
        //Pause the game while the player see the instructions
        Time.timeScale = 0;
    }
    /// <summary>
    /// Used to prevent player movement if it's performing an action. For example: buying
    /// </summary>
    public void playerMove(bool move)
    {
        PlayerController _player = _playerDetected.GetComponent<PlayerController>();
        if (move)
            _player.speed = _player.DefaultSpeed;
        else
            _player.speed = 0;
    }
}
