using UnityEngine;
using UnityEngine.Audio;

/* 
    References Barckeys video on "Introduction to AUDIO in Unity".
    https://www.youtube.com/watch?v=6OT43pvUyfY
*/
[System.Serializable]
public class Sounds
{

    [Header("Sound Controls")]
    public string name;
    public AudioClip soundClip; //The actual sound file

    [Range(0f,1f)]
    public float volume; //The volume of a sound clip
    [Range(0f,1f)]
    public float spatialBlend = 1;
    public bool loop;

    [HideInInspector]
    public AudioSource source;
   
}

