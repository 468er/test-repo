using UnityEngine.Audio;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class audio_manager : MonoBehaviour
{
    public Sound[] Sounds;
    public AudioSource[] ooga_sources = new AudioSource[2];
    public AudioSource[] all_audio_srcs;
    public int amountofsounds = 0;

    //these are private stuff
    pause_behaivor final_audio_script;
    pause_behaivor ooga_audio_script;
    pause_behaivor inga_audioscript;
    TextMeshProUGUI final_audio_txt;
    Slider final_audio_slider;
    TextMeshProUGUI ooga_audio_txt;
    Slider ooga_audio_slider;
    TextMeshProUGUI inga_audio_txt;
    Slider inga_audio_slider;
    public Sound a;
    public Slider[] all_button_toggle_sliders;
    //public GameObject[] LeanTweeners = new GameObject[9999];
    //public int LeanTweenarrnumb = 0;
    // Start is called before the first frame update
    //this is for the level creating
    public int ActualPath = 0;
    public GameObject game_manager;
    private void Awake()
    {
        foreach (Sound s in Sounds)
        {
            s.position = amountofsounds;
            amountofsounds++;
        }
        all_audio_srcs = new AudioSource[amountofsounds];
        int i = 0;
        foreach (Sound s in Sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            //if (s.IFSFXis1_2ifOOGA == 1)
            //{
            //    s.volume = PlayerPrefs.GetFloat("o_o_g_a_audio_val", 1f);
            //    ooga_sources[i] = s.source;
            //}
            s.source.clip = s.Clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.clip_length = s.Clip.length;
            //s.source.outputAudioMixerGroup = s.test;

            all_audio_srcs[i] = s.source;
            i++;
        }
        //for each thing in lean tweeners, delete on load unntil they are all deleted
    }
    private void Start()
    {
        Play("MenuTheme");
        //AudioListener.volume = PlayerPrefs.GetFloat("final_audio_val", 1f);
        //int i = 0;
        //foreach (Sound s in Sounds)
        //{
        //    if (i < 2)
        //    {
        //        i++;
        //    }
        //    else
        //    {
        //        string sound_name = Sounds[i].name;

        //    }
        //}
        //if (SceneManager.GetActiveScene().name == "OpeningMenu")
        //{
        //    Play("MainMenu");
        //}
        //final_audio_script = GameObject.Find("Slider").GetComponent<pause_behaivor>();
        //ooga_audio_script = GameObject.Find("Slider (1)").GetComponent<pause_behaivor>();
        //inga_audioscript = GameObject.Find("Slider (2)").GetComponent<pause_behaivor>();
        //final_audio_script.on_value_changed_is_true = false;
        //ooga_audio_script.on_value_changed_is_true = false;
        //inga_audioscript.on_value_changed_is_true = false;
        //Stop("choosing_cow_theme");
        //final_audio_slider = GameObject.Find("Slider").GetComponent<Slider>();
        //int Final_volume;
        //Final_volume = (int)(AudioListener.volume * 100f);
        //final_audio_txt = GameObject.Find("Final Audio 2").GetComponent<TextMeshProUGUI>();
        //final_audio_txt.text = (AudioListener.volume * 100f) + "";
        //final_audio_slider.value = Final_volume;
        //ooga_audio_slider = GameObject.Find("Slider (1)").GetComponent<Slider>();
        //ooga_audio_txt = GameObject.Find("oogatext").GetComponent<TextMeshProUGUI>();
        //Final_volume = (int)(Sounds[1].source.volume * 100);
        //ooga_audio_txt.text = "" + Final_volume;
        //ooga_audio_slider.value = Final_volume;
        //inga_audio_slider = GameObject.Find("Slider (2)").GetComponent<Slider>();
        //inga_audio_txt = GameObject.Find("ingatext").GetComponent<TextMeshProUGUI>();
        //Final_volume = (int)(PlayerPrefs.GetFloat(SceneManager.GetActiveScene().name, 1f) * 100);
        //inga_audio_slider.value = Final_volume;
        //inga_audio_txt.text = Final_volume.ToString();

        //inga_audioscript.on_value_changed_is_true = true;
        //final_audio_script.on_value_changed_is_true = true;
        //ooga_audio_script.on_value_changed_is_true = true;

    }



    public void Play(string name)
    {
        Sound s = Array.Find(Sounds, Sound => Sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound:" + name + "not found!");
            return;
        }
        s.source.Play();
    }
    public void GetClip(string name, out AudioClip clip)
    {
        Sound s = Array.Find(Sounds, Sound => Sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound:" + name + "not found!");
            clip = null;
            return;
        }
        clip = s.source.clip;
        return;
    }
    public void GetClip2(string name, out AudioClip clip)
    {
        Sound s = Array.Find(Sounds, Sound => Sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound:" + name + "not found!");
            clip = null;
            return;
        }
        clip = s.Clip;
        return;
    }
    public void GetPathBasedOnClip(AudioClip clip, out string name)
    {
        a = Array.Find(Sounds, Sound => Sound.Clip == clip);

        name = a.name;
    }
    public void GetSoundInfoBasedOnClip(AudioClip clip, out Sound name)
    {
        Sound s = Array.Find(Sounds, Sound => Sound.Clip == clip);

        name = s;
    }
    public void Pause(string name)
    {
        Sound s = Array.Find(Sounds, Sound => Sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound:" + name + "not found!");
            return;
        }
        s.source.Pause();
    }
    public void UnPause(string name)
    {
        Sound s = Array.Find(Sounds, Sound => Sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound:" + name + "not found!");
            return;
        }
        s.source.UnPause();
    }
    public void UpdateVolume(string name)
    {
        Sound s = Array.Find(Sounds, Sound => Sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound:" + name + "not found!");
            return;
        }
        s.source.volume = PlayerPrefs.GetFloat(name, s.source.volume);
    }
    public void Stop(string name)
    {
        Sound s = Array.Find(Sounds, Sound => Sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound:" + name + "not found!");
            return;
        }
        s.source.Stop();
    }
    public void UpdateAudios()
    {
        int i = 0;
        foreach (Slider item in all_button_toggle_sliders)
        {
            all_audio_srcs[i + 2].volume = all_button_toggle_sliders[i].value / 100f;
            i++;
        }
    }
}
