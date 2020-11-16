using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atack : MonoBehaviour
{
    public enum TypeAtack { shoot, rode, bomb }
    public TypeAtack typeAtack;
    public float timeToNextAtack;

    [HideInInspector]
    public bool isAtack;

    private ShootNPC shootNPC;
    private RodeNPC rodeNPC;
    private BombingNPC bombingNPC;
   

    // Start is called before the first frame update
    void Start()
    {
        shootNPC = GetComponent<ShootNPC>();
        rodeNPC = GetComponent<RodeNPC>();
        bombingNPC = GetComponent<BombingNPC>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void actacking()
    {
        if (typeAtack==TypeAtack.shoot && !isAtack)
        {
            shootNPC.shoot();
            StartCoroutine("timeAtack");
        }
    }

    IEnumerator timeAtack()
    {
        isAtack = true;
        yield return new WaitForSeconds(timeToNextAtack);
        isAtack = false;

    }


}
