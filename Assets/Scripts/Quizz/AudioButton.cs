using UnityEngine;
using UnityEngine.UI;

// Attached to Button Play/Stop in AudioClipCommand
public class AudioButton : MonoBehaviour
{
    public Sprite SubstitutePlay;
    public Sprite SubstituteStop;
    public Button playPauseButton;

    public void Stop()
    {

        AudioSource audioSource = GetComponent<AudioSource>();
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
            playPauseButton.GetComponent<Image>().sprite = SubstitutePlay;
        }
        else
        {
            audioSource.Play();
            playPauseButton.GetComponent<Image>().sprite = SubstituteStop;
        }

    }

    public void Pause()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.Pause();
        GetComponent<Image>().sprite = SubstitutePlay;

    }
}
