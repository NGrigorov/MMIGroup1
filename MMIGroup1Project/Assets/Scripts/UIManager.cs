using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public Animator myAnim;

    private bool isShownUI = false;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1)){
            triggerAnimation();
        }

    }

    void triggerAnimation()
    {
        if (!isShownUI)
        {
            myAnim.SetTrigger("Show");
            isShownUI = !isShownUI;
        }
        else
        {
            myAnim.SetTrigger("Hide");
            isShownUI = !isShownUI;
        }

        
    }
}
