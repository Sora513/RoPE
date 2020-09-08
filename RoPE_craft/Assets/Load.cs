using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class Load : MonoBehaviour
{
    private GameObject Robot;
    public string Name;
    public void LoadJson()
    {
        
        //現在あるobjectの消去
        Robot = GameObject.Find("Robot");
        Transform objList = Robot.GetComponentInChildren<Transform>();
       
        foreach(Transform child in objList){
            Destroy(child.gameObject);
        }

        //読み込み
        float TotallMass = 0;
        Vector3 PosMass = new Vector3(0f,0f,0f);
        RobotData robot = loadPlayerData();
        foreach(GOData obj in robot.Objs)
        {
            if(obj.name != "")
            {
                GameObject Prefab = Resources.Load(obj.name) as GameObject;
                //GameObject Prefab = GameObject.Find("BasicBlock");
                Debug.Log(obj.name);
                Vector3 pos = Robot.transform.TransformPoint(obj.LocalPosition);
                GameObject InsObj = Instantiate(Prefab, pos,obj.rotation);
                InsObj.transform.parent = Robot.transform;

                //重心計算
                TotallMass += InsObj.GetComponent<Data>().Mass;
                PosMass += InsObj.transform.localPosition * InsObj.GetComponent<Data>().Mass;
            }
        }
        
        Robot.GetComponent<Rigidbody>().centerOfMass = PosMass/TotallMass;
        Robot.GetComponent<Rigidbody>().mass = TotallMass;
        return;
        
    }
    public RobotData loadPlayerData()
    {
        string datastr = "";
        StreamReader reader;
        reader = new StreamReader (Application.dataPath + "/RobotSave/" + Name + ".json");
        datastr = reader.ReadToEnd ();
        reader.Close ();
 
        return JsonUtility.FromJson<RobotData> (datastr);
    }
}
