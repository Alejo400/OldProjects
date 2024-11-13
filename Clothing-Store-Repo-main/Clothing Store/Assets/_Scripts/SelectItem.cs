using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectItem : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    GameObject checkMarkItem;
    [SerializeField]
    GameObject[] item;
    [SerializeField]
    int price;
    public void OnPointerClick(PointerEventData pointerEventData)
    {
        //The selected image will execute a specific action depending of the selected structure
        if (GameManager._sharedInstance._selectedStructure == SelectedStructure.ClothingStore)
        {
            //Selected the item on inventory
            UIManager._sharedIntance.SelectedItem(checkMarkItem, item, price);
        }
        if (GameManager._sharedInstance._selectedStructure == SelectedStructure.Cabinet)
        {
            //player change clothes
            GameManager._sharedInstance._playerDetected.GetComponent<PlayerController>().changeClothes(gameObject.name);
        }
    }
}
