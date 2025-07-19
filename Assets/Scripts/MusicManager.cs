using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource villageMusic;
    public AudioSource battleMusic;

    void Start()
    {
        PlayVillageMusic();
    }

    public void PlayBattleMusic()
    {
        if (!battleMusic.isPlaying)
        {
            villageMusic.Stop();
            battleMusic.Play();
        }
    }

    public void PlayVillageMusic()
    {
        if (!villageMusic.isPlaying)
        {
            battleMusic.Stop();
            villageMusic.Play();
        }
    }
}
