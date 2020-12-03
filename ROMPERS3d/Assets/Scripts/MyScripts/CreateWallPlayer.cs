using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateWallPlayer : MonoBehaviour
{

    public float delayToCreate;
    public float timeToAnimStart;
    public Transform[] pointSpawnWall;

    private int indexWall;
    private bool onAction;
    private GameObject prefWall;
    private ControlInteract controlInteract;
    private PlayerControl playerControl;
    private List<GameObject> wallsCreate;
    private float scaleY;
    private Vector3 scale;
    private int auxMaxIncrement;
    private SetEffects setEffects;

    // Start is called before the first frame update
    void Start()
    {
        wallsCreate = new List<GameObject>();
        auxMaxIncrement = CanvasControlParent.instance.menuCurrent.maxIncrement;
        controlInteract = GetComponent<ControlInteract>();
        playerControl = GetComponent<PlayerControl>();
        setEffects = GetComponent<SetEffects>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!Manager.instance.fullStop) {

            //Destroy last wall has been maked
            if (InputControl.instance.getButtonsControl("Button2") && !Manager.instance.onTriggerMenu)
            {
                createOnGroundFrontWall();
                StartCoroutine("IdelayCreateWall");
                destroyLastWall();
            }

            //OnGrow

            if (InputControl.instance.getButtonsControlOnPress("Button3") && 
                (playerControl.horizontalControlPhysic.getAxis().Equals(Vector3.zero) || (playerControl.horizontalControlPhysic.getAxis().x > 0 || playerControl.horizontalControlPhysic.getAxis().x < 0)) && playerControl.horizontalControlPhysic.getAxis().z==0 )
            {
                
                    if (!onAction) {

                        if (!controlInteract.CheckFront() && auxMaxIncrement > 0)
                        {
                            if (playerControl.Grounded)
                            {
                                indexWall = 0;
                                auxMaxIncrement--;
                                createOnGroundFrontWall();
                            }

                        }
                        else
                        {
                            //automatic destroy
                            destroyAllWalls();
                            auxMaxIncrement = CanvasControlParent.instance.menuCurrent.maxIncrement;
                        }

                        CanvasControlParent.instance.menuCurrent.setNumCreate(auxMaxIncrement);

                    }
                    if (prefWall != null) {
                        prefWall.GetComponent<IWall>().growUpWall();
                    }
                

            }
            else if (InputControl.instance.getButtonsControlOnPress("Button3") && playerControl.horizontalControlPhysic.getAxis().z > 0)
            {

                if (!onAction)
                {
                    if (!controlInteract.CheckTop() && auxMaxIncrement > 0)
                    {
                        indexWall = 2;
                        auxMaxIncrement--;
                        createOnGroundTopWall();

                    }
                    else if (auxMaxIncrement <= 0)
                    {
                        destroyAllWalls();
                        auxMaxIncrement = CanvasControlParent.instance.menuCurrent.maxIncrement;
                    }
                    CanvasControlParent.instance.menuCurrent.setNumCreate(auxMaxIncrement);
                }

                if (prefWall != null)
                {
                    prefWall.GetComponent<IWall>().growUpWall();
                }

            }
            else if (InputControl.instance.getButtonsControlOnPress("Button3") && playerControl.horizontalControlPhysic.getAxis().z < 0 )
            {
                if (!onAction)
                {
                    if (controlInteract.CheckDown() && auxMaxIncrement > 0)
                    {
                        indexWall = 1;
                        auxMaxIncrement--;
                        createOnGroundDownWall();
                    }
                    else if (!controlInteract.CheckDown() && CanvasControlParent.instance.getMaterialSkillOnUse().superMat.typeSmaterial==SuperMaterial.TypeSmaterial.metal && auxMaxIncrement > 0)
                    {
                        indexWall = 1;
                        auxMaxIncrement--;
                        createOnGroundDownWall();
                    }
                    else if (auxMaxIncrement <= 0)
                    {
                        destroyAllWalls();
                        auxMaxIncrement = CanvasControlParent.instance.menuCurrent.maxIncrement;
                    }

                    CanvasControlParent.instance.menuCurrent.setNumCreate(auxMaxIncrement);

                }

                if (prefWall != null)
                {
                    prefWall.GetComponent<IWall>().growUpWall();
                }
            }

            //RELEASE BUTTON
            if (InputControl.instance.getButtonsControlOnRelease("Button3"))
            {
                //si el material es blando tipo muelle  
                if (prefWall!=null) {

                    prefWall.GetComponent<IWall>().ActiveSpecial();
                    prefWall = null;
                    onAction = false;
                    playerControl.canMove = true;

                }
                else
                {
                    onAction = false;
                    playerControl.canMove = true;

                }
                StartCoroutine("IdelayCreateWall");

            }
        }
    }

    void createOnGroundFrontWall()
    {
        onAction = true;
        playerControl.canMove = false;
        prefWall = Instantiate(CanvasControlParent.instance.getMaterialSkillOnUse().superMat.Prefabs[indexWall], pointSpawnWall[0].position, Quaternion.identity);
        prefWall.transform.eulerAngles = transform.eulerAngles;
        prefWall.transform.position = new Vector3(pointSpawnWall[0].position.x, pointSpawnWall[0].position.y, pointSpawnWall[0].position.z);
        scale = prefWall.transform.localScale;
        wallsCreate.Add(prefWall);
    }

    void createOnGroundDownWall()
    {
       
        onAction = true;
        playerControl.canMove = false; 
        prefWall = Instantiate(CanvasControlParent.instance.getMaterialSkillOnUse().superMat.Prefabs[indexWall], pointSpawnWall[2].position, Quaternion.identity);
        prefWall.transform.eulerAngles = transform.eulerAngles;
        prefWall.transform.position = new Vector3(pointSpawnWall[2].position.x, pointSpawnWall[2].position.y, pointSpawnWall[2].position.z);
        transform.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
        scale = prefWall.transform.localScale;
        wallsCreate.Add(prefWall);
    }

    
    void createOnGroundTopWall()
    {
        onAction = true;
        playerControl.canMove = false;
        prefWall = Instantiate(CanvasControlParent.instance.getMaterialSkillOnUse().superMat.Prefabs[indexWall], pointSpawnWall[1].position, Quaternion.identity);
        prefWall.transform.eulerAngles = transform.eulerAngles;
        prefWall.transform.position = new Vector3(pointSpawnWall[1].position.x, pointSpawnWall[1].position.y, pointSpawnWall[1].position.z);
        prefWall.GetComponent<TopWall>().setPlayer(this.transform);
        scale = prefWall.transform.localScale;
        wallsCreate.Add(prefWall);
        
    }

    IEnumerator restoreOnGroundDownWall()
    {
        float timeAnim = 0.1f;

        while (timeAnim>0) {
            scale.y = Mathf.Lerp(prefWall.transform.localScale.y, 50, Time.deltaTime * 40);
            scale.x = Mathf.Lerp(prefWall.transform.localScale.x, 50, Time.deltaTime * 40);
            prefWall.transform.localScale = scale;
            timeAnim -= Time.deltaTime;
            yield return null;
        }

        prefWall = null;
       // StartCoroutine("IdelayCreateWall");
    }



    IEnumerator IdelayCreateWall()
    {
        yield return new WaitForSeconds(delayToCreate);
        onAction = false;
        playerControl.canMove = true;
    }

    void cancelMoveWhileGrowDown()
    {
        transform.GetComponent<Rigidbody>().isKinematic = false;
        transform.GetComponent<Rigidbody>().useGravity = true;
    }

    void resetToMoveWhileGrowDown()
    {
        transform.GetComponent<Rigidbody>().isKinematic = true;
        transform.GetComponent<Rigidbody>().useGravity = false;
    }

    void destroyAllWalls()
    {
        foreach (GameObject go in wallsCreate)
        {
            Destroy(go,0.1f);
        }
    }

    public void DestroyThisWall(GameObject go)
    {
        wallsCreate.Remove(go);
        Destroy(go);
        auxMaxIncrement++;
        CanvasControlParent.instance.menuCurrent.setNumCreate(auxMaxIncrement);

    }


    public bool getOnAction()
    {
        return onAction;
    }

    void destroyLastWall()
    {
        if (wallsCreate.Count>0) {
            Destroy(wallsCreate[wallsCreate.Count - 1]);
            wallsCreate.RemoveAt(wallsCreate.Count - 1);
            auxMaxIncrement++;
            CanvasControlParent.instance.menuCurrent.setNumCreate(auxMaxIncrement);
        }
    }

}
