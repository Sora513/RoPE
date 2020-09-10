using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveButton : MonoBehaviour
{
    GameObject Director;
    BlockAdder script;

    private bool mode;

    void Start()
    {
        Director = GameObject.Find("GameDirector");
        script = Director.GetComponent<BlockAdder>();
        mode  = true;
    }
    public void OnClick() {
        Debug.Log("Fuck!!");
        if(mode){
            script.modeAdd = false;
            script.modeRemove = true;
            mode = false;
        }
        else
        {
            script.modeAdd = true;
            script.modeRemove = false;
            mode = true;
        }
        

    }
}
