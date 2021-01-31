using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class healthPack : Collectable
{
    
    public PlayerInfo info;

    
    // Update is called once per frame
    void Start()
    {
        collectableName = "Health";
        description = "increase health by 30";
        DontDestroyOnLoad(this.gameObject);
        
    }

    override public void Use() {
        
        player.GetComponent<playerManager>().ChangeHealth(30);
        Destroy(this.gameObject);
        
        
    }
}
