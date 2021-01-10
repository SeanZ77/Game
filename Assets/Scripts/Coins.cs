using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : Collectable
{
    // Start is called before the first frame update
    void Start()
    {
        collectableName = "coin";
        description = "increase score by 10";
        DontDestroyOnLoad(this.gameObject);
    }

    override public void Use() {
        player.GetComponent<playerManager>().ChangeScore(10);
        Destroy(this.gameObject);
    }
}
