using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public AudioClip[] clips;

    public void Start(){
        Debug.Log(this.name);
    }

    public void PlayClip(int clipId){
        AudioSource.PlayClipAtPoint(clips[clipId], new Vector3(0f,0f,0f), 0.80f);
    }
}
