using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerControl))]
[RequireComponent(typeof(CameraControl))]

public class Droid : MonoBehaviour {


    #region Singleton
    private static Droid droid;
    public static Droid instance
    {
        get
        {
            if (droid == null)
            {
                droid = FindObjectOfType<Droid>();
            }
            return droid;
        }
    }

    #endregion Singleton

    public PlayerControl playerControl;
    public CameraControl cameraControl;
    public Transform mirillaSelect;
    //public List<IA> join = new List<IA>();
    public GameObject[] paneles;
    public float rangeImp;
    public int state = 0;

    public float velocity;
    public float timeMove;
    public float distance;
    [HideInInspector]
    public Dictionary<int, Transform> targets = new Dictionary<int, Transform>();
    [HideInInspector]
    public bool isAutoimp;

    private bool triggerPush;
    private bool padPush;
    private int posX=0;
    private int posY=0;
    private int indexPanel=0;
    private int IDtarget;
    private Dictionary<int,GameObject> globalElements=new Dictionary<int,GameObject>();
    private Dictionary<int, GameObject> groupElements=new Dictionary<int, GameObject>();
    private Dictionary<Vector2, GameObject> singleElements =new  Dictionary<Vector2, GameObject>();

    // Use this for initialization
    void Start () {
        // rg = GetComponent<Rigidbody>();
        Debug.Log(mirillaSelect);
        mirillaSelect.position = playerControl.transform.position;
        settingElements();
    }

   
    // Update is called once per frame
    void LateUpdate () {

        if (playerControl!=null) {
            if (state == 0)
            {
                // followPlayer();
            } else if (state == 1) {

                inputControlPanelItems();

            }else if (state == 2)
            {
                activeAreaAutoImp();
                fxTargetSelect();
            }
            else 
            {
                
            }
           // transform.rotation = Quaternion.LookRotation(Vector3.down, playerControl.transform.position - transform.position);
        }
    }

    void settingElements()
    {
        if (paneles!=null && paneles.Length>0) {

            Transform[] elementsGlobal = paneles[0].transform.GetComponentsInChildren<Transform>(true);
            int indexGlobal = 0;
            for (int i = 0; i < elementsGlobal.Length; i++)
            {
                if (elementsGlobal[i].tag == "elementPanel")
                {
                    globalElements.Add(indexGlobal, elementsGlobal[i].gameObject);
                    indexGlobal++;
                }
            }

            Transform[] elementsGroup = paneles[1].transform.GetComponentsInChildren<Transform>(true);

            int indexGroup = 0;
            for (int i = 0; i < elementsGroup.Length; i++)
            {
                if (elementsGroup[i].tag == "elementPanel")
                {
                    groupElements.Add(indexGroup, elementsGroup[i].gameObject);
                    indexGroup++;
                }
            }

            Transform[] elementsSingle = paneles[2].transform.GetComponentsInChildren<Transform>(true);
            int indexSingleX = 0;
            int indexSingleY = 0;
            for (int i = 0; i < elementsSingle.Length; i++)
            {
                if (elementsSingle[i].tag == "elementPanel")
                {
                    singleElements.Add(new Vector2(indexSingleX, indexSingleY), elementsSingle[i].gameObject);
                    //indexGlobal++;

                    if (indexSingleX < paneles[2].GetComponent<SetPanel>().filas)
                    {

                        indexSingleX++;
                    }
                    else
                    {

                        indexSingleX = 0;
                        indexSingleY++;
                    }


                }
            }
        }

    }

    void inputControlPanelItems()
    {

        if (Input.GetAxis("buttonRT") > 0)
        {

            /* Vector3 posCursor = Util.getMousePointWorld(true);
             triggerPush = true;
             //mirilla.transform.localPosition = transform.position;
             if (posCursor.sqrMagnitude > 0.0f)
             {
                 Vector3 newDir = new Vector3(transform.position.x + posCursor.x * timeMove, 1, transform.position.z + posCursor.z * timeMove);
                 //transform.position = new Vector3(transform.position.x + posCursor.x * 20f * Time.deltaTime, 20, transform.position.z + posCursor.z * 20f * Time.deltaTime);
                 transform.position = Vector3.Lerp(transform.position, newDir, Time.deltaTime);
             }

             state = 1;
             playerControl.setFullStop(true);
             cameraControl.setTarget(transform);*/
            triggerPush = true;
            paneles[indexPanel].SetActive(true);
           // playerControl.setCanMove(false);

            float verticalAxes = Input.GetAxisRaw("PadAxisV");
            float horizontalAxes = Input.GetAxisRaw("PadAxisH");
            if (horizontalAxes==0 && verticalAxes==0)
            {
                padPush = false;
            }

            if (horizontalAxes > 0 && !padPush)
            {

                if (posX < paneles[indexPanel].GetComponent<SetPanel>().filas)
                {
                    posX++;

                }
                padPush = true;

            }
            else if (horizontalAxes < 0 && !padPush)
            {
                if (posX > 0)
                {
                    posX--;
                }
                padPush = true;
            }
            else if (verticalAxes > 0 && !padPush)
            {
                if (posY < paneles[indexPanel].GetComponent<SetPanel>().columnas)
                {
                    posY++;

                }
                padPush = true;
            }
            else if (verticalAxes < 0 && !padPush)
            {
                if (posY > 0)
                {
                    posY--;
                }
                padPush = true;
            }

            GameObject gameObject = getElement(posX, posY);

        }
        else if (Input.GetAxis("buttonRT") == 0 && triggerPush)
        {
            if (paneles!=null && paneles.Length>0) {

               // playerControl.setCanMove(true);
                cameraControl.setTarget(playerControl.transform);
                paneles[indexPanel].SetActive(false);
                state = 0;
            }


            triggerPush = false;

        }

    }

    void followPlayer()
    {
            Vector3 miPos = new Vector3(transform.position.x, 1, transform.position.z);
            Vector3 targetPos = new Vector3(playerControl.transform.position.x, 1, playerControl.transform.position.z);

            if (Vector3.Distance(miPos, targetPos) > distance)
            {
                transform.position = Vector3.Slerp(miPos, targetPos, Time.deltaTime * velocity);
                //Debug.Log(Vector3.Distance(miPos, targetPos) + "/ player:" + playerControl.transform.position);

            }
            else
            {
               // rg.velocity = Vector3.zero;
                // Debug.Log("distancia adecuada");

            }
        

    }

    void activeAreaAutoImp()
    {
        Collider[] colliders = Physics.OverlapSphere(playerControl.transform.position, rangeImp, 1 << LayerMask.NameToLayer("enemigo"));

        if (colliders.Length > 0)
        {
            foreach (Collider col in colliders)
            {
                if (!targets.ContainsKey(col.gameObject.GetInstanceID()))
                {
                    targets.Add(col.gameObject.GetInstanceID(), col.transform);

                }
            }

        }
        else
        {
            
            isAutoimp = false;
            targets.Clear();

        }
       
        if (Input.GetAxis("buttonRT") > 0)
        {
            targets.Clear();
            if (colliders.Length>0)
            {
                isAutoimp = true;
            }
            
        }
        
    }

    void fxTargetSelect()
    {
        if (mirillaSelect!=null)
        {

            if (isAutoimp && targets != null && targets.Count > 0)
            {
                if (!mirillaSelect.gameObject.activeSelf)
                {
                    mirillaSelect.gameObject.SetActive(true);
                }

                Vector3 posMirilla = new Vector3(mirillaSelect.position.x, 3, mirillaSelect.position.z);
                Vector3 posTarget = new Vector3(getTarget().position.x, 3, getTarget().position.z);
                mirillaSelect.transform.position = Vector3.MoveTowards(posMirilla, posTarget, Time.deltaTime * 20f);
            }
            else
            {

                mirillaSelect.position = playerControl.transform.position;
                mirillaSelect.gameObject.SetActive(false);
            }
        }
    }

    void changeTarget()
    {
        if (isAutoimp && targets!=null && targets.Count>0)
        {
            float verticalAxes = Input.GetAxisRaw("PadAxisV");
            float horizontalAxes = Input.GetAxisRaw("PadAxisH");

            if (horizontalAxes == 0 && verticalAxes == 0)
            {
                padPush = false;
            }

            if (horizontalAxes > 0 && !padPush)
            {

                if (posX < targets.Count-1)
                {
                    posX++;

                }
                padPush = true;

            }
            else if (horizontalAxes < 0 && !padPush)
            {
                if (posX > 0)
                {
                    posX--;
                }
                padPush = true;
            }



        }
    }

   
    /*public void AddnewComp(IA npc)
    {
        join.Add(npc);
    }*/

    public Transform getTarget()
    {
        Transform tr = null;
        int i = 0;
        foreach (var item in targets)
        {
            if (i==posX) {
                tr = item.Value;
            }
           
        }
        
        return tr;
    }

    GameObject getElement(int x,int y)
    {
        GameObject element = null;
        if (indexPanel == 2) //estamos es distribución de tabla 2d
        {
            foreach (Vector2 key in singleElements.Keys)
            {
                Vector2 posElement = new Vector2(x,y);
                if (posElement.Equals(key)) {
                    element = singleElements[key];
                    singleElements[key].GetComponent<Image>().color = Color.green;
                }
                else
                {
                    singleElements[key].GetComponent<Image>().color = Color.white;

                }

            }
        }
        else //estamos es distribución de tabla 1d
        {
            if (indexPanel==0) {
                for (int i = 0; i < globalElements.Count;i++)
                {
                    if (i==x)
                    {
                        element = globalElements[i];
                        globalElements[i].GetComponent<Image>().color = Color.green;
                    }
                    else
                    {
                        globalElements[i].GetComponent<Image>().color = Color.white;
                    }
                }
            }else if (indexPanel == 1)
            {
                for (int i = 0; i < groupElements.Count; i++)
                {
                    if (i == x)
                    {
                        element = groupElements[i];
                        groupElements[i].GetComponent<Image>().color = Color.green;

                    }
                    else
                    {
                        groupElements[i].GetComponent<Image>().color = Color.white;

                    }
                }
            }
        }

        return element;
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(playerControl.transform.position, rangeImp);
       

    }
}
