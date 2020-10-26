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
    public bool fullStop;
    public bool GlobalUsePad;
    public bool faceControl;
    public AudioSource[] themesLevel;
    
    [HideInInspector]
    public float life;
    [HideInInspector]
    public float mana;
    [HideInInspector]
    public int indexLevel;
    [HideInInspector]
    public PlayerControl playerControl;


    private void Awake()
    {
        ReadSequences();
        findTargetPlayer();
    }

    // Start is called before the first frame update
    void Start()
    {
        life = playerControl.setthing.life;
        mana = playerControl.setthing.mana;
        CanvasManager cvm = GameObject.Find("Canvas").GetComponent<CanvasManager>();
        cvm.healhtBar.setHealht(life);
        cvm.manaBar.setMana(mana);
        StartMusicLevel();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerControl==null)
        {
            findTargetPlayer();
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            indexLevel++;
            SceneManager.LoadScene("level" + indexLevel);
        }
    }

    void findTargetPlayer()
    {
        playerControl = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
    }

    void StartMusicLevel()
    {
        if (themesLevel.Length>0) {
            themesLevel[indexLevel].Play();
        }
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
