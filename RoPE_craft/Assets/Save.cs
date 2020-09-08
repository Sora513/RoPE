using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class Save : MonoBehaviour
{
    private GameObject Robot;
    public string Name;
    public void SaveJson()
    {
        //Name = "robot1";
        Robot = GameObject.Find("Robot");
        RobotData robot = new RobotData();

        robot.RobotName = Name;
        robot.version = "1";
        robot.CraftDate = DateTime.Now;
        
        Transform objList = Robot.GetComponentInChildren<Transform> ();
        robot.Objs = new GOData[500];

        int i = 0;
        foreach( Transform child in objList ) {
            Data script = child.gameObject.GetComponent<Data>();
            robot.Objs[i].name = script.Name;
            robot.Objs[i].LocalPosition = RoundVector3(child.localPosition);
            robot.Objs[i].rotation = RoundQuaternion(child.rotation);

            i++;
        }

        string jsonstr = JsonUtility.ToJson(robot);
        savePlayerData(jsonstr);
    }


    private void savePlayerData(string jsonstr)
    {
        StreamWriter writer;
    
        writer = new StreamWriter(Application.dataPath + "/RobotSave/" + Name + ".json", false);
        writer.Write (jsonstr);
        writer.Flush ();
        writer.Close ();
    }

    private Vector3 RoundVector3(Vector3 self){
        Vector3 out_data = self;    
        out_data.x = Mathf.Round( out_data.x );
        out_data.y = Mathf.Round( out_data.y );
        out_data.z = Mathf.Round( out_data.z );
        return out_data;
    }

    private Quaternion RoundQuaternion(Quaternion self){
        Quaternion out_data = self;    
        out_data.x = Mathf.Round( out_data.x );
        out_data.y = Mathf.Round( out_data.y );
        out_data.z = Mathf.Round( out_data.z );
        out_data.w = Mathf.Round( out_data.w );
        return out_data;
    }
}

[System.Serializable]
public class RobotData
{
    public string RobotName;
    public string version;
    public DateTime CraftDate;

    public GOData[] Objs;
}

[System.Serializable]
public struct GOData
{
    public string name;
    public Vector3 LocalPosition;
    public Quaternion rotation;
    public string MetaData;
}