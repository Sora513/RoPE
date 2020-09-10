using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Direcotr : MonoBehaviour
{
    public GameObject select;
    // Start is called before the first frame update
    void Start()
    {
        //select = GameObject.Find("BasicBlock");
    }

    // Update is called once per frame
    void Update()
    {
        this.GetComponent<SmallWindow>().AddedBlock = select;
        this.GetComponent<BlockAdder>().BlockPrefab = select;
    }
}
