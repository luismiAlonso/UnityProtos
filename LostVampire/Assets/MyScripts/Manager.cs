using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{

    #region Singleton
    private static Manager manager;
    public static Manager instance
    {
        get
        {
            if (manager == null)
            {
                manager = FindObjectOfType<Manager>();
            }
            return manager;
        }
    }

    #endregion Singleton
    
    private ModelDialog[] dialogos;
    public PlayerControl playerControl;
    public bool fullStop;
    [HideInInspector]
    public float life;
    [HideInInspector]
    public float mana;
    [HideInInspector]
    public int indexLevel;


    private void Awake()
    {
        ReadSequences();
    }
    // Start is called before the first frame update
    void Start()
    {
        life = playerControl.setthing.life;
        mana = playerControl.setthing.mana;
        CanvasManager.instance.healhtBar.setHealht(life);
        CanvasManager.instance.manaBar.setMana(mana);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ReadSequences()
    {      
      dialogos = Util.loadData("JSONmodels/JSONsequence_dialog.json");
    }


    public ModelDialog getModelDialog(string reference)
    {
        ModelDialog md = null;
        for (int i=0;i< dialogos.Length;i++)
        {

            if (dialogos[i].reference == reference) {
                md = dialogos[i];
            }
        }
        return md;
    }

    public void Restart()
    {
        SceneManager.LoadScene("intro");
    }
}
