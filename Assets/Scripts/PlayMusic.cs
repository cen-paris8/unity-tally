using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMusic : MonoBehaviour
{
    public AudioClip newAudioClip;
    // Start is called before the first frame update
    void Start()
    {
        // Create Clip
        int samplerate = 44100;
        AudioClip myClip = AudioClip.Create("Satie", samplerate * 10, 1, samplerate, true, OnAudioRead, OnAudioSetPosition);

        AudioSource audio = GetComponent<AudioSource>();

        // audio.clip = newAudioClip; // myClip;
        audio.clip = myClip;
        audio.Play();


        // TO Test
        //AudioSource audio2 = gameObject.AddComponent<AudioSource>();
        //audio2.PlayOneShot((AudioClip)Resources.Load("Satie"));
        //////        AudioSource audio2 = gameObject.AddComponent<AudioSource>();
        //////        AudioClip clip = (AudioClip)Resources.Load("Satie");
        //////        if (clip != null)
        //////        {
        //////            audio.PlayOneShot(clip, 1.0F);​
        //////}
        //////        else
        //////        {
        //////            Debug.Log("Attempted to play missing audio clip by name" + audioClipName);​
    //////}​

        // To Test other solution
        //AudioSource.PlayClipAtPoint(myClip, transform.position);

       

    }

    void OnAudioRead(float[] data)
    {
        Debug.Log("data count : " + data.Length);
        Debug.Log("data[0] : " + data[0]);
        int position = 0;
        int samplerate = 44100;
        float frequency = 440;
        int count = 0;
        while (count < data.Length)
        {
            data[count] = Mathf.Sin(2 * Mathf.PI * frequency * position / samplerate);
            Debug.Log("data[count] : " + data[count]);
            position++;
            count++;
        }
    }

    void OnAudioSetPosition(int newPosition)
    {
        Debug.Log("newPosition : " + newPosition);
        int position = 0;
        position = newPosition;
    }

}
