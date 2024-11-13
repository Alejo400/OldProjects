using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class PlayerController : MonoBehaviour
{
    Rigidbody2D _rigibody;
    [Range(2, 6)]
    public float speed = 4;
    float defaultSpeed;
    public float DefaultSpeed {
        get => defaultSpeed;
        set => defaultSpeed = value; }
    [SerializeField]
    int money;
    public int Money
    {
        get => money;
        set => money = value;
    }

    float horizontalMove, verticalMove;
    Vector2 movement;
    public Dictionary<string, GameObject> myPurchasedItems = new Dictionary<string, GameObject>();
    public GameObject myClothes; //specific clothes of Mary Sky
    //remove Image and Clone
    const int nCharactersStartRemove = 5;
    [SerializeField]
    List<GameObject> defaultClothes = new List<GameObject>();

    private void Awake()
    {
        _rigibody = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        defaultSpeed = speed;
        //Save the default clothes in the dictionary
        foreach (var item in defaultClothes)
        {
            myPurchasedItems.Add(item.name,item);
        }
    }
    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxis("Horizontal");
        verticalMove = Input.GetAxis("Vertical");
        movement = new Vector2(horizontalMove, verticalMove);
        _rigibody.velocity = movement.normalized * speed;

        if (Input.GetButtonDown("Fire2"))
            UIManager._sharedIntance.PauseGame();
    }
    public void changeClothes(string name)
    {
        //remove word "image" to find the name of Prefabs
        name = name.Remove(0, nCharactersStartRemove);
        GameObject clothes = myPurchasedItems[name];
        //know the type of clothes
        TypeClothing clothesType = clothes.GetComponent<clothingCharacteristics>().typeClothing;
        foreach (var item in myPurchasedItems.Values)
        {
            //find which clothes have the same type of the selected clothes to use. Example "shirt"
            if (item.GetComponent<clothingCharacteristics>().typeClothing == clothesType 
                && item.name != name)
            {
                //Disabled the sprite renderer
                item.GetComponent<SpriteRenderer>().enabled = false;
            }
        }
        clothes.GetComponent<SpriteRenderer>().enabled = true;
    }
}
