using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void Onclick()
    {
        Save();
    }
    public void Save()
    {
        SaveData save1 = new SaveData();
        save1.HP  = 100f;
        save1.name = "takayasu";
       // save1.pd = new PhyiscalData;
        save1.pd = new PhyiscalData[100];
        save1.pd[0].position = new Vector3(0f,1f,4f);
        save1.pd[0].rotation = Quaternion.identity;
        save1.pd[0].Mass = 10f;

        string jsonstr = JsonUtility.ToJson(save1);

        //Debug.Log(jsonstr);

        SaveData save2 = JsonUtility.FromJson<SaveData> (jsonstr);
        //Debug.Log(save2.pd[0].position.z);

        GameObject Robot = GameObject.Find("Robot");
        Transform objList = Robot.GetComponentInChildren<Transform> ();
        foreach( Transform child in objList ) {
            Debug.Log(child.gameObject.name);
        }
        
    }
}

[System.Serializable]
public class SaveData
{
    public float HP;
    public string name;
    public PhyiscalData[] pd;
}

[System.Serializable]
public struct PhyiscalData
{
    public Vector3 position;
    public Quaternion rotation;
    public float Mass;
}