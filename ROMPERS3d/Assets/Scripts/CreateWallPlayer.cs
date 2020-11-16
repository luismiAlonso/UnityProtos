using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateWallPlayer : MonoBehaviour
{

    public float delayToCreate;
    public float timeToAnimStart;
    public int maxCreateWalls;
    public Transform[] pointSpawnWall;
    public SuperMaterial[] superMaterialPrefb;
    public int indexWall;
    public int indexSuperMaterial;

    private bool onAction;
    private GameObject prefWall;
    private PlayerController playerController;
    private List<GameObject> wallsCreate;
    private float scaleY;
    private Vector3 scale;
    private int auxMaxIncrement;

    // Start is called before the first frame update
    void Start()
    {
        wallsCreate = new List<GameObject>();
        playerController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        //Destroy last wall has been maked
        if(InputControl.instance.getButtonsControl("Button2"))
        {
            //createOnGroundFrontWall();
            // StartCoroutine("IdelayCreateWall");
            destroyLastWall();
        }

        //OnGrow

        if (InputControl.instance.getButtonsControlOnPress("Button3")  && (playerController.getAxis().Equals(Vector3.zero) || (playerController.getAxis().x > 0 || playerController.getAxis().x < 0)))
        {

            if (!onAction) {

                if (auxMaxIncrement < maxCreateWalls)
                {
                    indexWall = 0;
                    auxMaxIncrement++;
                    createOnGroundFrontWall();

                }
                else
                {
                    //automatic destroy
                    destroyAllWalls();
                    auxMaxIncrement = 0;
                }
            }
            GrowUpWall();      

        }
        else if (InputControl.instance.getButtonsControlOnPress("Button3") && playerController.getAxis().z > 0)
        {
            if (!onAction)
            {
                if (!playerController.controlInteract.CheckTop() && auxMaxIncrement < maxCreateWalls)
                {
                    indexWall = 2;
                    auxMaxIncrement++;
                    createOnGroundTopWall();
 
                }else if (auxMaxIncrement >= maxCreateWalls)
                {
                    destroyAllWalls();
                    auxMaxIncrement = 0;
                }
            }
            //GrowUpWall();

        }
        else if (InputControl.instance.getButtonsControlOnPress("Button3") && playerController.getAxis().z < 0)
        {

            if (!onAction)
            {
                if (!playerController.controlInteract.CheckDown() && auxMaxIncrement < maxCreateWalls) {

                    indexWall = 1;
                    auxMaxIncrement++;
                    cancelMoveWhileGrowDown();
                    createOnGroundDownWall();

                }
                else if (auxMaxIncrement >= maxCreateWalls)
                {
                    destroyAllWalls();
                    auxMaxIncrement = 0;
                }
               
            }

            //activate rigidbody
            
            GrowDownGoma();
           
        }

        if (InputControl.instance.getButtonsControlOnRelease("Button3"))
        {
            //si el material es blando tipo muelle
            if (wallsCreate[wallsCreate.Count - 1].GetComponent<DownWall>()!=null)
            {
                //jump
                resetToMoveWhileGrowDown();
                playerController.jumpControl.remoteJump(6);
                StartCoroutine("restoreOnGroundDownWall");
                StartCoroutine("IdelayCreateWall");

            }
            else
            {
                prefWall = null;
                resetToMoveWhileGrowDown();
                StartCoroutine("IdelayCreateWall");
            }
            

        }
    }

    void createOnGroundFrontWall()
    {
        onAction = true;
        playerController.checkers.canMove = false;
        prefWall = Instantiate(superMaterialPrefb[indexSuperMaterial].Prefabs[indexWall], pointSpawnWall[0].position, Quaternion.identity);
        prefWall.transform.eulerAngles = transform.eulerAngles;
        prefWall.transform.position = new Vector3(pointSpawnWall[0].position.x,0, pointSpawnWall[0].position.z);
        scale = prefWall.transform.localScale;
        wallsCreate.Add(prefWall);
    }

    void createOnGroundDownWall()
    {
       
        onAction = true;
        playerController.checkers.canMove = false;
        prefWall = Instantiate(superMaterialPrefb[indexSuperMaterial].Prefabs[indexWall], pointSpawnWall[2].position, Quaternion.identity);
        prefWall.transform.eulerAngles = transform.eulerAngles;
        prefWall.transform.position = new Vector3(pointSpawnWall[2].position.x, pointSpawnWall[2].position.y, pointSpawnWall[2].position.z);
        scale = prefWall.transform.localScale;
        wallsCreate.Add(prefWall);
    }

    
    void createOnGroundTopWall()
    {
        onAction = true;
        playerController.checkers.canMove = false;
        prefWall = Instantiate(superMaterialPrefb[indexSuperMaterial].Prefabs[indexWall], pointSpawnWall[1].position, Quaternion.identity);
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
            onAction = true;
            scale.y = Mathf.Lerp(prefWall.transform.localScale.y, 50, Time.deltaTime * 40);
            scale.x = Mathf.Lerp(prefWall.transform.localScale.x, 50, Time.deltaTime * 40);
            prefWall.transform.localScale = scale;
            timeAnim -= Time.deltaTime;
            yield return null;
        }

        prefWall = null;
        StartCoroutine("IdelayCreateWall");
    }



    IEnumerator IdelayCreateWall()
    {
        yield return new WaitForSeconds(delayToCreate);
        onAction = false;
        playerController.checkers.canMove = true;
    }

    void GrowUpWall()
    {
        if (prefWall != null)
        {
            scale.y = Mathf.Lerp(prefWall.transform.localScale.y, superMaterialPrefb[indexSuperMaterial].Prefabs[indexWall].GetComponent<IWall>().getMaxIncrement().y, Time.deltaTime * superMaterialPrefb[indexSuperMaterial].Prefabs[indexWall].GetComponent<IWall>().getTimeScale().y);

            prefWall.transform.localScale = scale;
        }

    }

   void GrowDownGoma()
    {
        if (prefWall != null)
        {
            scale.y = Mathf.Lerp(prefWall.transform.localScale.y, superMaterialPrefb[indexSuperMaterial].Prefabs[indexWall].GetComponent<IWall>().getMaxIncrement().y, Time.deltaTime * superMaterialPrefb[indexSuperMaterial].Prefabs[indexWall].GetComponent<IWall>().getTimeScale().y);
            scale.x = Mathf.Lerp(prefWall.transform.localScale.x, superMaterialPrefb[indexSuperMaterial].Prefabs[indexWall].GetComponent<IWall>().getMaxIncrement().z, Time.deltaTime * superMaterialPrefb[indexSuperMaterial].Prefabs[indexWall].GetComponent<IWall>().getTimeScale().z);
            prefWall.transform.localScale = scale;
        }
    }

    void cancelMoveWhileGrowDown()
    {
        playerController.characterController.enabled = false;
        transform.GetComponent<Rigidbody>().isKinematic = false;
        transform.GetComponent<Rigidbody>().useGravity = true;
    }

    void resetToMoveWhileGrowDown()
    {
        playerController.characterController.enabled = true;
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
        }
    }

}
