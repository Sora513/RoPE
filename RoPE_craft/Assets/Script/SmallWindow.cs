using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallWindow : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject AddedBlock;//追加するobject

    private GameObject AddedBlock_sub;//追加する予定のobject
    private GameObject InsObj;//インスタンス化されたobject

    private bool onWindow,dragMode;
    private Vector3 preMousePosition;
    public float RotationSpeed = 0.1f;

    private float AddedBlockSize = 1f;
    public Quaternion InsObjQ = Quaternion.identity;
    void Start()
    {
        AddedBlock_sub = AddedBlock;
        show();
    }

    // Update is called once per frame
    void Update()
    {
        
        if(AddedBlock == null){
            Destroy(InsObj);
            AddedBlock_sub = AddedBlock;
            return;
        }
        if(AddedBlock_sub != AddedBlock)
       {
           Destroy(InsObj);
           AddedBlock_sub = AddedBlock;
           show();
           InsObjQ = Quaternion.identity;
       }

       // Debug.Log(Camera.main.transform.position);
       InsObj.transform.rotation = InsObjQ;



        //Windowないブロックの回転
    
        if(Input.mousePosition.x < (200f*(float)Screen.width / 1138f) && Input.mousePosition.y < (160f * (float)Screen.height / 530f))
        {
            onWindow = true;
        }
        else{
            onWindow = false;
        }    


        if(dragMode){
            InsObjRotation();
        }
        if(Input.GetMouseButtonDown(0) && onWindow){
            dragMode = true;
            preMousePosition = Input.mousePosition;
        }

        if(Input.GetMouseButtonUp(0))
        {
   
            Vector3 Angle = InsObj.transform.rotation.eulerAngles;
            Angle = Angle / 90f;
            
            Angle.x = Mathf.Round(Angle.x);
            Angle.y = Mathf.Round(Angle.y);
            Angle.z = Mathf.Round(Angle.z);

            Angle = Angle * 90f;

            InsObjQ = Quaternion.Euler(Angle);
           
            dragMode = false;
        }
    }



    void show(){
        Vector3 CamPos = Camera.main.transform.position;
        Quaternion CamQ = Camera.main.transform.rotation;

        //画面アスペクト比に応じて位置を変える
        Vector3 setPosbuf = new Vector3(-3f,-1.2f,3f) * 0.6f;

        setPosbuf.x = setPosbuf.x * (((float)Screen.width / (float)Screen.height) / (1138f/530f));

        Vector3 setPos = CamPos + CamQ*(setPosbuf);
        InsObj  = Instantiate(AddedBlock_sub,setPos,Quaternion.identity);
        
        Data script = InsObj.GetComponent<Data>();
        
        AddedBlockSize = Vector3MAX(script.size);

        InsObj.transform.localScale = new Vector3(0.3f,0.3f,0.3f) / AddedBlockSize;
        InsObj.transform.parent = Camera.main.transform;
    }

    void InsObjRotation(){
        Vector3 dif = Input.mousePosition - preMousePosition;
        float deltaAngle = dif.magnitude * RotationSpeed;

        Transform cameraTransform = Camera.main.transform;
        Vector3 deltaWorld = cameraTransform.right * dif.x + cameraTransform.up * dif.y;
        Vector3 axisWorld = Vector3.Cross(deltaWorld, cameraTransform.forward).normalized;

        InsObj.transform.Rotate(axisWorld, deltaAngle, Space.World);
    }

    private float Vector3MAX(Vector3 SizeInfo)
    {
        float MaxSize = SizeInfo.x;
        if(SizeInfo.y > MaxSize)MaxSize = SizeInfo.y;
        if(SizeInfo.z > MaxSize)MaxSize = SizeInfo.z;
        return MaxSize;
    }
}
