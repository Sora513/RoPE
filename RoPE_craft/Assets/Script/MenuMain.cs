using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

public class MenuMain : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject robot;
    void Start()
    {

        robot = GameObject.Find("Robot");
        var files = new DirectoryInfo(Application.dataPath + "/RobotSave/")
                        .GetFiles("*.json")
                        .OrderBy(fi => fi.CreationTime)
                        .Select(fi => fi.FullName)
                        .ToArray();

        
        foreach(var f in files)
        {
            Debug.Log(f);
        }


        Load loadscript = GetComponent<Load>();
        if(files != null)
        {
            loadscript.filename = files[files.Count()-1];
            loadscript.LoadJson(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        robot.transform.Rotate(0f,0.3f,0f);   
    }
}
