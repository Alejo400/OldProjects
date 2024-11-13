using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public enum SelectedStructure
{
    ClothingStore,
    Cabinet,
    Reward
}
public class TouchController : MonoBehaviour
{
    public UnityEvent onTouch;
    public SelectedStructure _selectedStructure;
    public bool playerInZone;
    private void OnMouseDown()
    {
        GameManager._sharedInstance._selectedStructure = _selectedStructure;
        if (playerInZone)
        {
            if (_selectedStructure == SelectedStructure.ClothingStore)
            {
                if (GetComponent<StoreController>().sellerInStore)
                    onTouch.Invoke();
            }
            else
            {
                onTouch.Invoke();
            }
        }
    }
}
