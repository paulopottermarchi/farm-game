using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance; // Singleton instance of AudioManager

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Ensure the UIController persists across scene loads
        }
        else
        {
            Destroy(gameObject); // If another instance exists, destroy this one to maintain singleton pattern
        }
    }

    public AudioSource titleMusic; // Reference to the title music AudioSource
    public AudioSource[] bgMusic; // Array of background music AudioSources
    private int currentTrack;

    private bool isPaused;

    public AudioSource[] sfx; // Array of sound effects AudioSources    

    private void Start()
    {
        currentTrack = -1; // Initialize current track index
    }

    private void Update()
    {
        if (isPaused == false)
        {

            if (bgMusic[currentTrack].isPlaying == false)
            {
                PlayNextBGM(); // Automatically play the next background music track if the current one is not playing
            }
        }
    }

    public void StopMusic()
    {
        foreach (AudioSource track in bgMusic)
        {
            track.Stop(); // Stop all background music tracks
        }

        titleMusic.Stop(); // Stop the title music
    }

    public void PlayTitle()
    {
        StopMusic(); // Stop any currently playing music
        titleMusic.Play(); // Play the title music
    }

    public void PlayNextBGM()
    {
        StopMusic(); // Stop any currently playing music

        currentTrack++; // Move to the next track

        if (currentTrack >= bgMusic.Length)
        {
            currentTrack = 0; // Loop back to the first track if at the end of the array
        }

        bgMusic[currentTrack].Play(); // Play the next background music track
    }

    public void PauseMusic()
    {
        isPaused = true; // Set the paused state to true

        bgMusic[currentTrack].Pause(); // Pause the currently playing background music
    }

    public void ResumeMusic()
    {
        isPaused = false; // Set the paused state to false

        bgMusic[currentTrack].Play(); // Resume the currently paused background music
    }

    public void PlaySFX(int sfxToPlay)
    {
        sfx[sfxToPlay].Stop(); // Stop the sound effect if it's already playing
        sfx[sfxToPlay].Play(); // Play the specified sound effect
    }

    public void PlaySFXPitchAdjusted(int sfxToPlay)
    {
        sfx[sfxToPlay].pitch = Random.Range(0.8f, 1.2f); // Randomly adjust the pitch of the sound effect

        PlaySFX(sfxToPlay); // Play the sound effect with the adjusted pitch
    }
}
