using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMoney : MonoBehaviour
{
    public int value;
    public int multiple;

    private SetEffects setEffects;
    // Start is called before the first frame update
    void Start()
    {
        setEffects = GetComponent<SetEffects>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag=="Player")
        {
            CanvasControlParent.instance.setItemMoneyItem(value);
            setEffects.PlayFx("fxItemMoney");
            setEffects.PlaySx("SxItem");
            Destroy(gameObject);
        }
    }
}
