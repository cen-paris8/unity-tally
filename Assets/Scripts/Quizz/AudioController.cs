using UnityEngine;

// Attached to Prefac AudioClipCommand
public class AudioController : MonoBehaviour
{
    private AudioSource audioSource;
    private float m_MySliderValue = .2f;
    //Value from the slider, and it converts to volume level


    public void LoadAudio(string clipName)
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = Resources.Load(clipName) as AudioClip;
        audioSource.volume = 0.3f;
        audioSource.Play();
    }
    public void UnloadAudio()
    {
        if (gameObject.GetComponent<AudioSource>() != null)
        {
            audioSource = gameObject.GetComponent<AudioSource>();
            Object.Destroy(audioSource);
        }
        
    }

    void OnGUI()
    {
        //Create a horizontal Slider that controls volume levels. Its highest value is 1 and lowest is 0
        float positionX = transform.position.x;
        float positionY = transform.position.y;
        m_MySliderValue = GUI.VerticalSlider(new Rect(positionX -75 , positionY + 1000, positionX -65, positionY+100), m_MySliderValue, 0.0F, 1.0F);
        //Makes the volume of the Audio match the Slider value.
        audioSource.volume = m_MySliderValue;
    }
}
