using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StoreController : MonoBehaviour
{
    [SerializeField]
    float sellerHourStartJob,eachHourToClose;
    [SerializeField]
    GameObject seller, closeWindow;
    public bool sellerInStore;
    [SerializeField]
    TextMeshProUGUI closedMessage;
    //value to set hours with the clock
    const float multiplierHours = 2.5f;
    private void Start()
    {
        closedMessage.text = $"open at {sellerHourStartJob}:00";
        sellerHourStartJob *= multiplierHours;
        StartCoroutine(StartJob());
    }
    IEnumerator StartJob()
    {
        yield return new WaitForSeconds(sellerHourStartJob);
        while (true)
        {
            seller.SetActive(!seller.activeInHierarchy);
            sellerInStore = !sellerInStore;
            closeWindow.SetActive(!closeWindow.activeInHierarchy);
            //if the seller leave, the inventory need close (dont sell items)
            if (!sellerInStore)
            {
                UIManager._sharedIntance.HideStoreInventory();
            }
            yield return new WaitForSeconds(eachHourToClose * multiplierHours);
        }
    }
}
