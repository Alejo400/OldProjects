using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    GameObject StoreInventory, CabinetInventory, Instructions, PauseMenu;
    public static UIManager _sharedIntance;
    public TextMeshProUGUI valueBalance,totalprice;
    [SerializeField]
    PlayerController _player;
    [SerializeField]
    List<GameObject> checkMarks = new List<GameObject>();
    private void Awake()
    {
        if (_sharedIntance == null)
            _sharedIntance = this;
        else
            Destroy(this);
    }
    private void Start()
    {
        ChangeTextOfBalancePlayer();
    }
    //Show UI Inventory to buy in store
    public void ShowStoreInventory() 
    {
        GameManager._sharedInstance.playerMove(false);
        StoreInventory.SetActive(true);
    }
    //Hide UI Inventory and remove selected items
    public void HideStoreInventory()
    {
        GameManager._sharedInstance.playerMove(true);
        StoreInventory.SetActive(false);
        foreach (var item in checkMarks)
        {
            item.SetActive(false);
        }
        StoreManager._sharedIntance.RemoveAllItemsOfInventory();
    }
    //Show Cabinet UI Inventory to change clothes
    public void ShowCabinetInventory()
    {
        GameManager._sharedInstance.playerMove(false);
        CabinetInventory.SetActive(true);
    }
    //Hide UI Cabinet
    public void HideCabinet()
    {
        GameManager._sharedInstance.playerMove(true);
        CabinetInventory.SetActive(false);
    }
    /// <summary>
    /// //Show or Hide checkmark in images items. Add or Remove Item of Inventory to Buy
    /// </summary>
    /// <param name="checkMark">selected item</param>
    public void SelectedItem(GameObject checkMark, GameObject[] item, int priceItem)
    {
        checkMark.SetActive(!checkMark.activeInHierarchy);
        if (checkMark.activeInHierarchy)
        {
            StoreManager._sharedIntance.AddItemToInventory(item, priceItem);
            StoreManager._sharedIntance.InteractablePurchasedBotton();
            ChangeTotalPrice();
        }
        else
        {
            StoreManager._sharedIntance.RemoveItemOfInventory(item, priceItem);
            StoreManager._sharedIntance.InteractablePurchasedBotton();
            ChangeTotalPrice();
        }
    }
    /// <summary>
    /// Active or desactive a dialog
    /// </summary>
    /// <param name="dialog"></param>
    public void showDialog(GameObject dialog)
    {
        dialog.SetActive(!dialog.activeInHierarchy);
    }
    public void ChangeTextOfBalancePlayer()
    {
        valueBalance.text = $"{_player.Money}$";
    }
    public void ChangeTotalPrice()
    {
        totalprice.text = $"{StoreManager._sharedIntance.TotalPriceItems}$";
    }
    public void HideInstructions()
    {
        Instructions.SetActive(false);
        ResumeGame();
    }
    public void PauseGame()
    {
        PauseMenu.SetActive(true);
        Time.timeScale = 0;
    }
    public void ResumeGame()
    {
        PauseMenu.SetActive(false);
        Time.timeScale = 1;
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
