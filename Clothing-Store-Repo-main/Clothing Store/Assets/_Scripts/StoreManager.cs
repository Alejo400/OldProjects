using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StoreManager : MonoBehaviour
{

    public static StoreManager _sharedIntance;
    Dictionary<string, GameObject[]> listSelectedItems = new Dictionary<string, GameObject[]>();
    GameObject buyerPerson;
    public List<GameObject> purchasedItems = new List<GameObject>();
    public Button PurchasedButtom;
    int totalPriceItems;
    public int TotalPriceItems { get => totalPriceItems; 
                                 set => totalPriceItems = value; }
    public TextMeshProUGUI insufficientMoney;
    private void Awake()
    {
        if (_sharedIntance == null)
            _sharedIntance = this;
        else
            Destroy(this);
    }
    /// <summary>
    /// add selected item of the store and sum price of items
    /// </summary>
    /// <param name="item">Prefab of selected item</param>
    public void AddItemToInventory(GameObject[] item, int priceItem)
    {
        listSelectedItems.Add(item[0].name, item);
        totalPriceItems += priceItem;
    }
    public void RemoveItemOfInventory(GameObject[] item, int priceItem)
    {
        listSelectedItems.Remove(item[0].name);
        totalPriceItems -= priceItem;
    }
    public void RemoveAllItemsOfInventory()
    {
        listSelectedItems.Clear();
        InteractablePurchasedBotton();
        totalPriceItems = 0;
        insufficientMoney.enabled = false;
        UIManager._sharedIntance.ChangeTotalPrice();
    }
    /// <summary>
    /// Enabled the botton to buy if the dictionary have 1 key or more
    /// </summary>
    public void InteractablePurchasedBotton()
    {
        int moneyOfPlayer = GameManager._sharedInstance._playerDetected.GetComponent<PlayerController>().Money;
        if (listSelectedItems.Count <= 0)
        {
            PurchasedButtom.interactable = false;
        }
        else
        {
            if(moneyOfPlayer < totalPriceItems)
            {
                PurchasedButtom.interactable = false;
                insufficientMoney.enabled = true;
            }
            else
            {
                PurchasedButtom.interactable = true;
                insufficientMoney.enabled = false;
            }
        }
            
    }
    public void BuySelectedItems()
    {
        buyerPerson = GameManager._sharedInstance._playerDetected;
        foreach (var items in listSelectedItems)
        {
            PlayerController _player = buyerPerson.GetComponent<PlayerController>();
            if (!_player.myPurchasedItems.ContainsKey(items.Key + "(Clone)"))
            {
                //Add keys and items of dictionary in the dictionary items of the player
                //instantiate and add prefabs in the transform of player category clothes (object empty)

                _player.myPurchasedItems.Add(items.Key + "(Clone)",
                    Instantiate(items.Value[0], buyerPerson.transform.Find("Clothes")));

                //instantiate and add images of items in the transform of player clothes (UI container)
                Instantiate(items.Value[1], _player.myClothes.transform);
            }
            else
            {
                //give a random number to buy similar clothes
                int randomNumber = Random.Range(0, 999);
                //create new key to find the item
                string newKey = $"{items.Key}(Clone){randomNumber}";

                _player.myPurchasedItems.Add(newKey, Instantiate(items.Value[0], buyerPerson.transform.Find("Clothes")));

                GameObject imageItem = Instantiate(items.Value[1], _player.myClothes.transform);
                imageItem.name += randomNumber;
            }
        }
        //rest money after buy and change text of value balance 
        buyerPerson.GetComponent<PlayerController>().Money -= totalPriceItems;
        UIManager._sharedIntance.ChangeTextOfBalancePlayer();

        UIManager._sharedIntance.HideStoreInventory();
        PurchasedButtom.interactable = false;
    }
}
