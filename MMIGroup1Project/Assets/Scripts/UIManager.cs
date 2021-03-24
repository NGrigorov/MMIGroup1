using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public Animator myAnim;
    private bool isShown = false;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F1))
        {
            triggerAnimation();
        }
    }


    void triggerAnimation()
    {
        if(isShown)
        {
            myAnim.SetTrigger("Hide");
            isShown = !isShown;
        }
        else
        {
            myAnim.SetTrigger("Show");
            isShown = !isShown;
        }
    }
}
