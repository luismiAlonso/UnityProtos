using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimateActor : MonoBehaviour
{
    public Sprite[] frames;
    public float frameRate;
    public bool stop;
    public Image image; 

    private float indexFrame;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!stop)
        {
            if (frames!=null && frames.Length>0) {
                indexFrame = Time.time * frameRate;
                indexFrame = indexFrame % frames.Length;
                image.sprite = frames[(int)indexFrame];
            }
        }
    }

    public void setSpriteSequence(string namePacket)
    {
        frames = Resources.LoadAll<Sprite>(namePacket);

    }

    public void stopResume(bool b)
    {
        stop = b;
    }

    public void Reset()
    {
        indexFrame = 0;
    }

    public void active(bool b)
    {
       
       this.gameObject.SetActive(b);
       
    }
}
