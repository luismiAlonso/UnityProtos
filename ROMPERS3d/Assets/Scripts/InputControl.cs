﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class InputControl : MonoBehaviour
{
    #region Singleton
    private static InputControl inputControl;

    public static InputControl instance
    {
        get
        {
            if (inputControl == null)
            {
                inputControl = FindObjectOfType<InputControl>();
            }
            return inputControl;
        }
    }

    #endregion Singleton

    public enum ControllerTypeConnected { Xbox, Playstation,KeyBoard, Other }
    [HideInInspector]
    public ControllerTypeConnected controllerTypeConnected;

    string diviceName;

    private void Start()
    {
      ControllerConnected();
    }

    private void ControllerConnected()
    {
        if (getControllerType() == "XBOX")
        {
            controllerTypeConnected = ControllerTypeConnected.Xbox;
            diviceName = "XBOX";
            Debug.Log("You connected an xbox controller!");
            
        }
        else if (getControllerType() == "PS")
        {
            controllerTypeConnected = ControllerTypeConnected.Playstation;
            diviceName = "PS";
            Debug.Log("You connected a playstation controller!");
        }
        else if(getControllerType() == "wirless")
        {
            controllerTypeConnected = ControllerTypeConnected.Other;
            diviceName = "wirless";
            Debug.Log("PS???");
        }
        else
        {
            controllerTypeConnected = ControllerTypeConnected.Other;
            diviceName = "OTHER";
            Debug.Log("You connected a other controller!");
        }
    }

    private string getControllerType()
    {
        string[] joystickNames = Input.GetJoystickNames();

        foreach (string joystickName in joystickNames)
        {
            if (joystickName.ToLower().Contains("xbox"))
            {
                return "XBOX";
            }
            else if (joystickName.ToLower().Contains("playstation"))
            {
                return "PS";
            }
            else if (joystickName.ToLower().Contains("wirless"))
            {
                return "wirless";
            }
            else 
            {
                return "OTHER";
            }
        }
        return "OTHER";
    }

    public Vector2 getAxisControl()
    {
        Vector2 axis = Vector2.zero;

        if (Manager.instance.GlobalUsePad)
        {
            axis = new Vector2(Input.GetAxisRaw("PadAxisH"), Input.GetAxisRaw("PadAxisV"));
        }
        else
        {
            axis = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        }

        return axis;
    }

    public bool getButtonsControl(string name)
    {
        bool input=false;

        if (Manager.instance.GlobalUsePad)
        {
            if (name=="Button0") {

                input = Input.GetButtonDown("buttonY");

            }else if (name=="Button1")
            {
                input = Input.GetButtonDown("buttonB");

            }else if (name == "Button2")
            {
                input = Input.GetButtonDown("buttonX");

            }else if (name == "Button3")
            {
                input = Input.GetButtonDown("buttonA");
            }
        }
        else
        {
            if (name == "Button0")
            {
                input = Input.GetKeyDown(KeyCode.Q);
            }
            else if (name == "Button1")
            {
                input = Input.GetKeyDown(KeyCode.LeftShift);

            }
            else if (name == "Button2")
            {
                input = Input.GetKeyDown(KeyCode.Space);
            }
            else if (name == "Button3")
            {
                input = Input.GetKeyDown(KeyCode.E);
            }
        }

        return input;
    }

    public Vector3 getAxisFree()
    {
        Vector3 freePos = Vector3.zero;
        if (!Manager.instance.GlobalUsePad)
        {
            freePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        else
        {
            float stickH = Input.GetAxisRaw("stickH");
            float stickV = Input.GetAxisRaw("stickV");
            freePos = Vector3.right * stickH + Vector3.forward * -stickV;
        }

        return freePos;
    }
}
