using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using System.Collections;

///<summary>
/// The audio manager deals with playing audio, stopping audio and loading audio.
///References Barckeys video on "Introduction to AUDIO in Unity". https://www.youtube.com/watch?v=6OT43pvUyfY
///</summary
public class AudioManager : MonoBehaviour
{
    public Sounds[] sounds; //Ref to Sounds class (main sounds)
    public static AudioManager mngInstance; 
    float oldVolume = 0f;

    // Start is called before the first frame update
    void Awake(){

        //Makes sure there is a single audio manager
        if(mngInstance == null){
            mngInstance = this;
        }

        //Adds the different sound ref/groupings
        AddAudioSources(sounds);  
    }

    //Plays once at the start before the first frame
    private void Start(){
        oldVolume = PlayerPrefs.GetFloat("Volume");

        if(SceneManager.GetActiveScene().name == "MainMenu" || SceneManager.GetActiveScene().name == "Credits")
            PlaySound("MusicMenu", sounds);
    }
    
    //Called once every frame
    private void Update() {
        if(oldVolume != PlayerPrefs.GetFloat("Volume"))
            UpdateVolume(sounds); 
    }

    
    ///<summary>
    /////Initalises all the sources
    ///</summary>
    ///<param name="itArray"> The array holding all the sounds </param>
    private void AddAudioSources(Sounds[] itArray){

        //Adds each audio source in our array, including its volume properties
        foreach(Sounds i in itArray){
            i.source = gameObject.AddComponent<AudioSource>();         
            i.source.clip = i.soundClip;   
            i.source.volume = i.volume;   
            i.source.loop = i.loop; 
            i.startVolume = i.volume;
        }
    }

    
    ///<summary>
    ///Finds the sound we want in the sounds array and then plays it.
    ///</summary>
    ///<param name="soundName"> Name of the sound to look for </param>
    ///<param name="soundArray"> The array holding all the sounds </param>
    public void PlaySound(string soundName, Sounds[] soundArray){

        if(soundName !=null && soundArray !=null){
            Sounds foundSound = Array.Find(soundArray, sound => sound.name == soundName);
              
            //Checks if the sounds exsits, if so then play the sound otherwise have a console message
            if(foundSound != null && foundSound.source !=null)
                foundSound.source.Play();
            else{
                Debug.Log("The sound: " +  soundName + " was not found");
                return;
            } 
        }
    }

    ///<summary>
    ///Finds the sound we want in the sounds array and then stops it
    ///</summary>
    ///<param name="soundName"> Name of the sound to look for </param>
    ///<param name="soundArray"> The array holding all the sounds </param>  
    public void StopSound(string soundName, Sounds[] soundArray){

        if(soundName !=null && soundArray !=null){
            Sounds foundSound = Array.Find(soundArray, sound => sound.name == soundName);
            
            //Checks if the sounds exsits, if so then play the sound otherwise have a console message
            if(foundSound != null && foundSound.source !=null)
                foundSound.source.Stop();
            else{
                Debug.Log("The sound: " +  soundName + " was not found");
                return;
            }
        }
    }

    ///<summary>
    ///Deals with changing the volume. Does this by getting the new volume from
    ///the player pref and then times the start volume of each volume by that
    /// pref number and then nets the old volume to the new volume
    ///</summary>
    ///<param name="itArray"> The array holding all the sounds </param>  
    private void UpdateVolume(Sounds[] itArray){

        float newVolume = PlayerPrefs.GetFloat("Volume");

        //Changes the volume of all sources
        foreach(Sounds i in itArray)
            i.source.volume = i.startVolume * newVolume;   

        oldVolume = newVolume;
    }


    ///<summary>
    ///Decreases the volume by one every second
    ///</summary>
    ///<param name="soundName"> Name of the sound to look for </param>
    ///<param name="soundArray"> The array holding all the sounds </param>  
    public IEnumerator DecreaseVolume(string soundName, Sounds[] soundArray){
        if(soundName !=null && soundArray !=null){
            Sounds foundSound = Array.Find(soundArray, sound => sound.name == soundName);
            
            if(foundSound != null){
                while(foundSound.volume > 0){
                    yield return new WaitForSeconds(1f);
                    foundSound.volume -=1;
                }
            }
        }
    }
}
