using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript: MonoBehaviour
{

    public static AudioClip jumpSound, frogSound, opossumSound, hawkSound, itemSound;
    public static AudioSource audioSrc;

    // Start is called before the first frame update
    void Start()
    {
        jumpSound = Resources.Load<AudioClip>("Audio/se/jump");
        frogSound = Resources.Load<AudioClip>("Audio/se/frog");
        opossumSound = Resources.Load<AudioClip>("Audio/se/opossum");
        hawkSound = Resources.Load<AudioClip>("Audio/se/hawk");
        itemSound = Resources.Load<AudioClip>("Audio/se/item");

        audioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void PlaySound(string clip) {

        switch (clip) {
            case "jump":
                audioSrc.PlayOneShot(jumpSound);
                break;
            case "frog":
                audioSrc.PlayOneShot(frogSound);
                break;
            case "opossum":
                audioSrc.PlayOneShot(opossumSound);
                break;
            case "hawk":
                audioSrc.PlayOneShot(hawkSound);
                break;
            case "item":
                audioSrc.PlayOneShot(itemSound);
                break;
        }

    }
}
