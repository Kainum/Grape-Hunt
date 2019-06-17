using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRespawn : MonoBehaviour
{
    public bool Death;
    public float Timer;
    public float Cooldown;
    public GameObject Item;
    public string ItemName;
    GameObject LastItem;

    void Start () {
        Death = true;
        Timer = Cooldown - 0.1f;
        gameObject.name = ItemName + "spawn point";
    }

    void Update () {
        if(Death == true) {
            Timer += Time.deltaTime;
        }
        if(Timer >= Cooldown) {
            Item.transform.position = transform.position;
            Item item = Instantiate(Item).GetComponent<Item>();
            item.respawn = true;
            LastItem = GameObject.Find(Item.name + "(Clone)");
            LastItem.name = ItemName;
            Death = false;
            Timer = 0;
        }
    }
}
