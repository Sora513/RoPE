using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockAdder : MonoBehaviour
{
    // Start is called before the first frame update
    public bool modeAdd,modeRemove;
    public GameObject BlockPrefab;
    public GameObject Robot;
    private float clicktime;
    private bool HitObj;

    private Ray ray;
    private RaycastHit hit;

    private SmallWindow script;
    private bool onWindow;

    private float TotallMass;
    public Vector3 PosMass;
    void Start()
    {
        Robot = GameObject.Find("Robot");
        script = this.GetComponent<SmallWindow>();
        modeAdd = true;
        modeRemove = false;

        PosMass = new Vector3(0f,0f,0f);
        TotallMass = GameObject.Find("Root").GetComponent<Data>().Mass;
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.mousePosition.x < (200f*(float)Screen.width / 1138f) && Input.mousePosition.y < (160f * (float)Screen.height / 530f))
        {
            onWindow = true;
        }
        else{
            onWindow = false;
        }    

        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        

        HitObj = Physics.Raycast(ray,out hit);
        if(Input.GetMouseButtonDown(0)){
            clicktime = 0;
        }
        if(Input.GetMouseButton(0))
        {
            clicktime += Time.deltaTime;
        }
        if(Input.GetMouseButtonUp(0) && !onWindow)
        {
            if(clicktime < 0.5f)
            {
                if(modeAdd)
                {
                    AddBlock();
                }
                if(modeRemove)
                {
                    RemoveBlock();
                }
                
            }
        }
    }

    private void AddBlock()
    {
        if(HitObj)
        {
            GameObject target = hit.collider.gameObject.transform.parent.gameObject;
            GameObject targetBody  = hit.collider.gameObject;
            Vector3 targetSize = target.GetComponent<Data>().size;
            Vector3 InsObjSize = BlockPrefab.GetComponent<Data>().size;

            Vector3 posL = target.transform.InverseTransformPoint(hit.point);
            
            posL = Round(posL);
            
            Vector3 margen = target.transform.rotation * hit.normal;
            margen = Vector3.Scale(margen,script.InsObjQ*InsObjSize) / 2f;
            float margenSize = Vector3MAXabso(margen) + 0.5f;
            
            margen = target.transform.rotation * margen;

            Vector3 posW = target.transform.TransformPoint(posL) + hit.normal * margenSize;
            BlockPrefab = script.AddedBlock;
            GameObject InsObj = Instantiate(BlockPrefab, posW,script.InsObjQ);
            InsObj.transform.parent = Robot.transform;

            //重心計算
            TotallMass += InsObj.GetComponent<Data>().Mass;
            PosMass += InsObj.transform.localPosition * InsObj.GetComponent<Data>().Mass;
            Robot.GetComponent<Rigidbody>().centerOfMass = PosMass/TotallMass;
            Robot.GetComponent<Rigidbody>().mass = TotallMass;
        }
    }
    private void RemoveBlock(){
        if(HitObj){
            GameObject target = hit.collider.gameObject.transform.parent.gameObject;

            //重心計算
            TotallMass -= target.GetComponent<Data>().Mass;
            PosMass -= target.transform.localPosition * target.GetComponent<Data>().Mass;
            Robot.GetComponent<Rigidbody>().centerOfMass = PosMass/TotallMass;
            Robot.GetComponent<Rigidbody>().mass = TotallMass;

            Destroy(target);
        }
    }
   
    private Vector3 Round(Vector3 self){
        Vector3 out_data = self;    
        if(out_data.x < 0)
        {
            out_data.x = out_data.x + 0.1f;
        }
        else
        {
            out_data.x = out_data.x - 0.1f;
        }

        if(out_data.y < 0)
        {
            out_data.y += 0.1f;
        }
        else
        {
            out_data.y -= 0.1f;
        }

        if(out_data.z < 0)
        {
            out_data.z += 0.1f;
        }
        else
        {
            out_data.z -= 0.1f;
        }
        out_data.x = Mathf.Round( out_data.x );
        out_data.y = Mathf.Round( out_data.y );
        out_data.z = Mathf.Round( out_data.z );
        return out_data;
    }
    
    private float Vector3MAXabso(Vector3 SizeInfo)
    {

        if(SizeInfo.x < 0)SizeInfo.x *= -1;
        if(SizeInfo.y < 0)SizeInfo.y *= -1;
        if(SizeInfo.z < 0)SizeInfo.z *= -1;
        
        float MaxSize = SizeInfo.x;
        if(SizeInfo.y > MaxSize)MaxSize = SizeInfo.y;
        if(SizeInfo.z > MaxSize)MaxSize = SizeInfo.z;
        return MaxSize;
    }
}
