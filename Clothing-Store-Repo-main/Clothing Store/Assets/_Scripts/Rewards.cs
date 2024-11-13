using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rewards : MonoBehaviour
{
    [SerializeField]
    int value;
    [SerializeField]
    GameObject item, dialog;
    public void GiveStartMoney()
    {
        GameManager._sharedInstance._playerDetected.GetComponent<PlayerController>().Money += value;
        item.SetActive(false);
        UIManager._sharedIntance.ChangeTextOfBalancePlayer();
        UIManager._sharedIntance.showDialog(dialog);
    }
}
