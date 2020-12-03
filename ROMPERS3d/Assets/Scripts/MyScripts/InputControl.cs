using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System;

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
    public bool forceChangeControllerInput;
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
        return "UNKNOWN";
    }

    public Vector2 getAxisControl()
    {
        Vector2 axis = Vector2.zero;

        if (Manager.instance.GlobalUsePad)
        {
            if (!forceChangeControllerInput && (diviceName== "XBOX" || diviceName== "OTHER")) {

                axis = new Vector2(Input.GetAxisRaw("PadAxisH"), Input.GetAxisRaw("PadAxisV"));
            }
            else if(forceChangeControllerInput)
            {
                axis = new Vector2(Input.GetAxisRaw("PadAxisH_play"), Input.GetAxisRaw("PadAxisV_play"));

            }
        }
        else
        {
            axis = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        }

        return axis;
    }

    public bool getAxixMouseWeel()
    {
        bool input = false;

        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            input = true;
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            input = true;
        }

        return input;
    }

    public bool getButtonsControl(string name)
    {
        bool input=false;

        if (Manager.instance.GlobalUsePad)
        {

            if (!forceChangeControllerInput) {

                if (name == "Button0") {

                    input = Input.GetButtonDown("buttonY");

                } else if (name == "Button1")
                {
                    input = Input.GetButtonDown("buttonB");

                } else if (name == "Button2")
                {
                    input = Input.GetButtonDown("buttonX");

                } else if (name == "Button3")
                {
                    input = Input.GetButtonDown("buttonA");

                }else if (name == "Button4")
                {
                    input = Input.GetButtonDown("buttonRB");
                }
                else if (name == "Button5")
                {
                    input = Input.GetButtonDown("buttonLB");
                }

            }
            else
            {
                if (name == "Button0")
                {
                    input = Input.GetButtonDown("buttonA");
                }
                else if (name == "Button1")
                {
                    input = Input.GetButtonDown("buttonX");

                }
                else if (name == "Button2")
                {
                    input = Input.GetButtonDown("buttonY");

                }
                else if (name == "Button3")
                {
                    input = Input.GetButtonDown("buttonB");
                }
                else if (name == "Button4")
                {
                    input = Input.GetButtonDown("buttonRB");
                }
                else if (name == "Button5")
                {
                    input = Input.GetButtonDown("buttonLB");
                }
            }
        }
        else
        {
            if (name == "Button0") 
            {
                input = Input.GetKeyDown(KeyCode.Space);
            }
            else if (name == "Button1")
            {
                input = Input.GetButtonDown("Fire2");
            }
            else if (name == "Button2")
            {
                input = Input.GetKeyDown(KeyCode.Q);
            }
            else if (name == "Button3")
            {
                input = Input.GetButtonDown("Fire1");
            }
            else if (name == "Button4")
            {
                input = Input.GetKeyDown(KeyCode.Alpha1);
             
            }
            else if (name == "Button5")
            {
                input = Input.GetKeyDown(KeyCode.Alpha2);
                  
            }
        }

        return input;
    }

    public bool getButtonsControlOnRelease(string name)
    {
        bool input = false;

        if (Manager.instance.GlobalUsePad)
        {

            if (!forceChangeControllerInput)
            {

                if (name == "Button0")
                {

                    input = Input.GetButtonUp("buttonY");

                }
                else if (name == "Button1")
                {
                    input = Input.GetButtonUp("buttonB");

                }
                else if (name == "Button2")
                {
                    input = Input.GetButtonUp("buttonX");

                }
                else if (name == "Button3")
                {
                    input = Input.GetButtonUp("buttonA");
                }

            }
            else
            {
                if (name == "Button0")
                {
                    input = Input.GetButtonUp("buttonA");
                }
                else if (name == "Button1")
                {
                    input = Input.GetButtonUp("buttonX");

                }
                else if (name == "Button2")
                {
                    input = Input.GetButtonUp("buttonY");

                }
                else if (name == "Button3")
                {
                    input = Input.GetButtonUp("buttonB");
                }
            }
        }
        else
        {
            if (name == "Button0")
            {
                input = Input.GetKeyUp(KeyCode.Space);
            }
            else if (name == "Button1")
            {
                input = Input.GetButtonUp("Fire2");
            }
            else if (name == "Button2")
            {
                input = Input.GetKeyUp(KeyCode.Q);
            }
            else if (name == "Button3")
            {
                input = Input.GetButtonUp("Fire1");
            }
        }

        return input;
    }

    public bool getButtonsControlOnPress(string name)
    {
        bool input = false;

        if (Manager.instance.GlobalUsePad)
        {

            if (!forceChangeControllerInput)
            {

                if (name == "Button0")
                {

                    input = Input.GetButton("buttonY");

                }
                else if (name == "Button1")
                {
                    input = Input.GetButton("buttonB");

                }
                else if (name == "Button2")
                {
                    input = Input.GetButton("buttonX");

                }
                else if (name == "Button3")
                {
                    input = Input.GetButton("buttonA");
                }

            }
            else
            {
                if (name == "Button0")
                {
                    input = Input.GetButton("buttonA");
                }
                else if (name == "Button1")
                {
                    input = Input.GetButton("buttonX");

                }
                else if (name == "Button2")
                {
                    input = Input.GetButton("buttonY");
                }
                else if (name == "Button3")
                {
                    input = Input.GetButton("buttonB");
                }
            }
        }
        else
        {
            if (name == "Button0")
            {
                input = Input.GetKey(KeyCode.Space);
            }
            else if (name == "Button1")
            {
                input = Input.GetButton("Fire2");
            }
            else if (name == "Button2")
            {
                input = Input.GetKey(KeyCode.Q);             
            }
            else if (name == "Button3")
            {
                input = Input.GetButton("Fire1");
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

    /*List<USBDeviceInfo> GetUSBDevices()
    {
        List<USBDeviceInfo> devices = new List<USBDeviceInfo>();

        System.Management.ManagementObjectCollection  collection;
        using (var searcher = new System.Management.ManagementObjectSearcher(@"Select * From Win32_USBHub"))
            collection = searcher.Get();

        foreach (var device in collection)
        {
            devices.Add(new USBDeviceInfo(
            (string)device.GetPropertyValue("DeviceID"),
            (string)device.GetPropertyValue("PNPDeviceID"),
            (string)device.GetPropertyValue("Description")
            ));
        }

        collection.Dispose();
        return devices;
    }*/

}
