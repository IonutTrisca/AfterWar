using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleCard : MonoBehaviour
{
    public void ChecKeyPressed()
    {
        if (UIAnimator.keyHasBeenPressed)
        {
            UIAnimator.animationFinished = true;
        }
    }
}
