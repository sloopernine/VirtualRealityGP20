using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionSoundController : MonoBehaviour
{
    public static CollisionSoundController instance;

    public CollisionSoundList sounds;

    
    [SerializeField] List<AudioSource> audioSources;

    private AudioSource source;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        //DontDestroyOnLoad(gameObject);

        source = GetComponent<AudioSource>();
    }

    public void Play(CollisionSoundMaterial mat)
    {
        SoundCue_Collision cue = instance.GetCue(mat);

        source.clip = cue.sounds[Random.Range(0, cue.sounds.Count)];
        source.pitch = Random.Range(0.8f, 1.2f);
        source.Play();


    }


    private SoundCue_Collision GetCue(CollisionSoundMaterial mat)
    {
        SoundCue_Collision cue = sounds.soundList.Find(x => x.material == mat);
        if (cue == null)
        {
            Debug.LogWarning($"No Cue set for {mat}");
            return null;
        }
        return cue;
    }

}
