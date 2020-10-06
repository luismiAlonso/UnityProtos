using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class DataArmaDistancia
{

    public  Sprite sprite;
    public GameObject bullet;
    public Transform propietario;
    public float timeDelay;
    public int type;

    public DataArmaDistancia(Sprite _sprite, GameObject _bullet,Transform _propietario, float _timeDelay, int _type)
    {
        sprite = _sprite;
        bullet = _bullet;
        propietario = _propietario;
        timeDelay= _timeDelay;
        type = _type;
    }
}
