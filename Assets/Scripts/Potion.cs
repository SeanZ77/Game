using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : Collectable
{
    // Start is called before the first frame update
    void Start()
    {
        collectableName = "Potion";
        description = "increase score and decrease health";
        DontDestroyOnLoad(this.gameObject);

    }

    override public void Use() {
        player.GetComponent<playerManager>().ChangeScore(30);
        player.GetComponent<playerManager>().ChangeHealth(-30);
        Destroy(this.gameObject);
    }
}
