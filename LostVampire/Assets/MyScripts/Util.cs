using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Text;
//if exist
//using UnityEditor;
using UnityEngine.UI;
using System.Globalization;
using System.Threading;
using LitJson;

public static class Util
{

    private static string path = "Logs/";
    private static Mutex mut = new Mutex();
    private static bool activo;

    public static bool isNumber(char c)
    {
        char[] permitidos = {'0','1','2','3','4','5','6','7','8','9'};
        bool check = false;
        for (int i=0;i<permitidos.Length;i++)
        {
            if (permitidos[i]==c)
            {
                check = true;
            }
        }
        return check;
    }
    public static string getPath()
    {
        return path;
    }
    public static void setPath(string pth)
    {
        path = pth;
    }

   

    public static void printLog(string data)
    {
        if (activo)
        {
            mut.WaitOne();
            DateTime localDate = DateTime.Now;
            string full = path + "log_" + localDate.ToString("yyyyMMdd") + "_end.txt";
            if (File.Exists(full))
            {
                StreamWriter w = new StreamWriter(full, true);
                w.WriteLine(data + " :" + localDate.ToString());
                w.Close();
            }
            else
            {
                StreamWriter w = File.CreateText(full);
                w.WriteLine(data + " :" + localDate.ToString());
                w.Close();
            }
            mut.ReleaseMutex();
        }
    }

   
    
public static List<string> DirectorySearch(string ruta)
    {
        List<string> dirNameStr = new List<string>();
        try
        {
            foreach (string d in Directory.GetDirectories(ruta))
            {
                dirNameStr.Add(Path.GetFileName(d));
                DirectorySearch(d);
            }

            foreach (string f in Directory.GetFiles(ruta))
            {

                if (Path.GetExtension(f) == ".png")
                {
                    //Debug.Log("fuentes/imgFont/" + dirNameStr);
                    // imgFonts = Resources.LoadAll<Sprite>( "fuentes/imgFont/"+ dirNameStr + Path.GetFileName(f));
                }
            }

        }
        catch (System.Exception ex)
        {
            Debug.Log(ex.Message);
        }

        return dirNameStr;
    }
    //checkea si un componente determinado existe
        public static bool HasComponent<T>(this GameObject flag) where T : Component
        {
            return flag.GetComponent<T>() != null;
        }

   
    //leer archivo linea a linea
    public static List<string> leerArchivo(string path_)
    {

        FileStream filestream = new FileStream(path_, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
        StreamReader reader = new StreamReader(filestream);
        List<string> lista = new List<string>();
        string text = "";

        do
        {
            text = reader.ReadLine();
            //defecto de escritura

            if (text != null)
            {
                lista.Add(text);
            }
        } while (text != null);

        return lista;

    }

    public static byte[] LeerBinary(string nameFile)
    {
        byte[] buff = null;
        FileStream fs = new FileStream(nameFile, FileMode.Open,FileAccess.Read);
        BinaryReader br = new BinaryReader(fs);
        long numBytes = new FileInfo(nameFile).Length;
        buff = br.ReadBytes((int)numBytes);
        return buff;
    }

    //lee todo en bloque
    public static string LeerArchivoFull(string path_)
    {
        string text = "";
        if (File.Exists(path_))
        {
            text = File.ReadAllText(path_);
        }
        return text;
    }

    

    public static void setColorLuces(GameObject obj)
    {
        Transform[] luces = obj.transform.GetComponentsInChildren<Transform>(true);
        foreach (Transform l in luces)
        {
            if (l.GetComponent<Light>() != null)
            {
                l.GetComponent<Light>().color = obj.GetComponent<Renderer>().material.color;
            }
        }
    }

    public static int convertToInt(float value)
    {
        int convertValue = 0;
        if (value<0)
        {
            convertValue = -1;
        }else if (value>0)
        {
            convertValue = 1;

        }
        
        return convertValue;
    }

    public static Vector3 getMousePointWorld(bool usePad)
    {
        Vector3 mousePos = Vector3.zero;
        if (!usePad) {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        else
        {
           float stickH = Input.GetAxisRaw("stickH");
           float stickV = Input.GetAxisRaw("stickV");
           mousePos = Vector3.right * stickH + Vector3.forward * -stickV;
        }
        return mousePos;
    }

    public static Vector3 AngleLerp(Vector3 StartAngle, Vector3 FinishAngle, float t)
    {
        float xLerp = Mathf.LerpAngle(StartAngle.x, FinishAngle.x, t);
        float yLerp = Mathf.LerpAngle(StartAngle.y, FinishAngle.y, t);
        float zLerp = Mathf.LerpAngle(StartAngle.z, FinishAngle.z, t);
        Vector3 Lerped = new Vector3(xLerp, yLerp, zLerp);
        return Lerped;
    }

     public static ModelDialog[] loadData(string path)
     {
         ModelDialog[] genObj = null;

         try
         {
            string filePath =  path.Replace(".json", "");
            TextAsset dataObj = Resources.Load<TextAsset>(filePath);
            genObj = JsonHelper.FromJson<ModelDialog>(dataObj.text);
           
         }
         catch (Exception e)
         {
             Debug.Log(e);
         }

         return genObj;
     }
}
