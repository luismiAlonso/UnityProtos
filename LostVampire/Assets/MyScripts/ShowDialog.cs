using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowDialog : MonoBehaviour
{
    public string referenceDialog;
    public SequenceCreator sequenceCreator; 
    public bool activo;
    private int currentIndexDialog;

    public void openDialog()
    {
        if (!activo ) {
            ModelDialog md=Manager.instance.getModelDialog(referenceDialog);
            sequenceCreator.createSequence(md,currentIndexDialog,this);
            activo = true;
            Manager.instance.fullStop = true;
        }
    }


}
