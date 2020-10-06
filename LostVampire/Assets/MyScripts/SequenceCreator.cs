using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SequenceCreator : MonoBehaviour
{
    public GameObject story;
    //public bool auto;
    //public float timeTransition;
    private GameObject panelLeft;
    private GameObject panelRight;
    private AnimateActor ImageLeft;
    private AnimateActor ImageRigth;
    private AnimateText textLeft;
    private AnimateText textRight;
    private List<ActionButton> leftOptions = new List<ActionButton>();
    private List<ActionButton> rightOptions = new List<ActionButton>();

    private int cursorOption=0;
    private bool padPush;
    private int currentOption;
    private bool activeOptions;

    private ShowDialog showDialog;
    private ModelDialog md;
    private int ordeSequence;
    private string currentActor;

    // Start is called before the first frame update
    void Start()
    {
        Transform[] panels = story.GetComponentsInChildren<Transform>(true);
        foreach (Transform panel in panels)
        {
            if (panel.name== "ActorLeft")
            {
                ImageLeft = panel.GetComponent<AnimateActor>();

            }else if (panel.name== "ActorRight")
            {
                ImageRigth = panel.GetComponent<AnimateActor>();

            }else if (panel.name== "dialogLeft")
            {
                textLeft = panel.GetComponent<AnimateText>();

            }else if (panel.name == "dialogRight")
            {
                textRight = panel.GetComponent<AnimateText>();

            }else if (panel.name == "Panel_left")
            {
                panelLeft = panel.gameObject;
            }
            else if (panel.name == "Panel_rigth")
            {
                panelRight = panel.gameObject;
            }
            else if (panel.tag=="optionsButton")
            {
                if (panel.GetComponent<ActionButton>()!=null && panel.GetComponent<ActionButton>().location=="left") {

                    leftOptions.Add(panel.GetComponent<ActionButton>());

                }else if (panel.GetComponent<ActionButton>() != null && panel.GetComponent<ActionButton>().location == "rigth")
                {

                    rightOptions.Add(panel.GetComponent<ActionButton>());
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (showDialog!=null && showDialog.activo)
        {
            if (Input.GetKeyDown(KeyCode.Joystick1Button1))
            {

                ordeSequence++;

                if (ordeSequence < md.panelInteractive.Length && md.panelInteractive[ordeSequence] != 1)
                {

                    if (ordeSequence < md.animations.Length)
                    {

                        enaBlePanel();
                    }
                    else
                    {

                        closePanelDialog();
                        showDialog.activo = false;
                        Manager.instance.fullStop = false;
                        ordeSequence = 0;

                    }
                }
                else //seleccionar una opcion                                                              
                {
                    enaBlePanel();
                    activeOptions = true;
                    enableOptions();

                }

            }

            if (activeOptions) {
                inputOption();
            }
        }
    }

    public void createSequence(ModelDialog _md,int actualOrder,ShowDialog _showDialog)
    {
        md = _md;
        currentActor = md.animations[ordeSequence];
        story.SetActive(true);
        panelLeft.SetActive(true);
        showDialog = _showDialog;
        ordeSequence = actualOrder;
        enaBlePanel();
    }

    void enaBlePanel()
    {

        if (currentActor == md.animations[ordeSequence]) {

            if (panelRight.activeSelf) {

                ImageRigth.setSpriteSequence(md.animations[ordeSequence]);
                textLeft.setAnimateText(md.textos[ordeSequence]);
            }
            else if (panelLeft.activeSelf)
            {
                ImageLeft.setSpriteSequence(md.animations[ordeSequence]);
                textLeft.setAnimateText(md.textos[ordeSequence]);

            }

        }
        else
        {
            if (panelRight.activeSelf) {

                panelRight.SetActive(false);
                panelLeft.SetActive(true);
                textLeft.setAnimateText(md.textos[ordeSequence]);
                ImageLeft.setSpriteSequence(md.animations[ordeSequence]);
       

            }else if (panelLeft.activeSelf)
            {
                panelLeft.SetActive(false);      
                panelRight.SetActive(true);
                textRight.setAnimateText(md.textos[ordeSequence]);
                ImageRigth.setSpriteSequence(md.animations[ordeSequence]);
               
        
            }
        }

        currentActor = md.animations[ordeSequence];

    }

    void enableOptions()
    {
        if (panelRight.activeSelf)
        {
            foreach (ActionButton ac in rightOptions)
            {
                ac.gameObject.SetActive(true);
            }
            currentOption = 1;
        }
        else if (panelLeft.activeSelf)
        {
            foreach (ActionButton ac in  leftOptions)
            {
                ac.gameObject.SetActive(true);
            }
            currentOption = 0;

        }
    }

    void inputOption()
    {
        List<ActionButton> options = null;

        if (currentOption==0) {

          options = leftOptions;

        }else if (currentOption == 1)
        {
            options = rightOptions;
        }

        float horizontalAxes = Input.GetAxisRaw("PadAxisH");

        if (horizontalAxes == 0 )
        {
            padPush = false;
        }

        if (horizontalAxes > 0 && !padPush)
        {

            if (cursorOption < options.Count-1)
            {
                cursorOption++;
                resetOption(options);
            }
            padPush = true;

        }
        else if (horizontalAxes < 0 && !padPush)
        {
            if (cursorOption > 0)
            {
                cursorOption--;
                resetOption(options);
            }

            padPush = true;
        }

        options[cursorOption].gameObject.GetComponent<Text>().color = Color.green;
    }

    void resetOption(List<ActionButton> options)
    {
        foreach (ActionButton ac in options)
        {
            ac.gameObject.GetComponent<Text>().color = Color.white;
        }
    }

    void closePanelDialog()
    {
        story.SetActive(false);
        panelLeft.SetActive(false);
        panelRight.SetActive(false);
    }

    /*public void setOrdeSequence(int order)
    {
        ordeSequence = order;
    }*/
}
