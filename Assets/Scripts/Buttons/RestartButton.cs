using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartButton : InteractableButton
{
    protected override void Perform()
    {
        GameManager.instance.Restart();
    }
}
