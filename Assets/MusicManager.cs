using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager   {

    bool is_music_on = true;
    public bool isMusicOn()
    {
        return this.is_music_on;
    }
    public void setMusicOn(bool val)
    {
        this.is_music_on = val;
        PlayerPrefs.SetInt("music", this.is_music_on ? 1 : 0);
        PlayerPrefs.Save();
    }
    MusicManager()
    {
        is_music_on = PlayerPrefs.GetInt("music", 1) == 1;
    }
    public static MusicManager Instance = new MusicManager();
}
