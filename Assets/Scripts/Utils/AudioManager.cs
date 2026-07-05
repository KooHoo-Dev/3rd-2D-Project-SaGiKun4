using Jay;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : SingletonBase<AudioManager>
{
    public enum Sfx
    {
        Sparrow = 0,
        Pigeon,
        Peacock,
        Raven,
        Get,
        Wing,
        Damaged,
        Whip,
        Hit,
        Buy,
        Reroll
    }

    public enum Bgm
    {
        MainMenu,
        GameMenu,
        Adventure,
        Defense
    }

    public AudioClip[] bgmClips;
    public float bgmVolume;
    private AudioSource bgmPlayer;

    public AudioClip[] sfxClips;
    public float sfxVolume;
    private AudioSource[] sfxPlayer;

    public int channels;
    private int channelIndex;

    protected override void OnInitialize()
    {
        base.OnInitialize();
        Init();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    protected override void OnDispose()
    {
        base.OnDispose();
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Init()
    {
        GameObject bgmObject = new GameObject("BgmPlayer");
        bgmObject.transform.parent = transform;
        bgmPlayer = bgmObject.AddComponent<AudioSource>();
        bgmPlayer.playOnAwake = false;
        bgmPlayer.loop = true;
        bgmPlayer.volume = bgmVolume;

        GameObject sfxObject = new GameObject("SfxPlayer");
        sfxObject.transform.parent = transform;
        sfxPlayer = new AudioSource[channels];

        for (int i = 0; i < sfxPlayer.Length; i++)
        {
            sfxPlayer[i] = sfxObject.AddComponent<AudioSource>();
            sfxPlayer[i].playOnAwake = false;
            sfxPlayer[i].volume = sfxVolume;
        }
    }

    public void PlayBgm(Bgm bgm)
    {
        bgmPlayer.clip = bgmClips[(int)bgm];
        bgmPlayer.Play();
    }

    public void StopBgm()
    {
        bgmPlayer.Stop();
    }

    public void PlaySfx(Sfx sfx, float pitch = 1f)
    {
        for (int i = 0; i < sfxPlayer.Length; i++)
        {
            int loopIndex = (i + channelIndex) % sfxPlayer.Length;

            if (sfxPlayer[loopIndex].isPlaying)
                continue;

            channelIndex = loopIndex;
            sfxPlayer[loopIndex].clip = sfxClips[(int)sfx];
            sfxPlayer[i].pitch = pitch;
            sfxPlayer[loopIndex].Play();
            break;
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        switch (scene.name)
        {
            case "MainMenu":
                PlayBgm(Bgm.MainMenu);
                break;

            case "GameMenu":
                PlayBgm(Bgm.GameMenu);
                break;

            case "Adventure":
                PlayBgm(Bgm.Adventure);
                break;

            case "Defence":
                PlayBgm(Bgm.Defense);
                break;

            case "LoadingScene":
                StopBgm();
                break;
        }
    }

    public void SetBgmVolume(float volume)
    {
        bgmVolume = volume;
        bgmPlayer.volume = volume;
    }

    public void SetSfxVolume(float volume)
    {
        for (int i = 0; i < sfxPlayer.Length; i++)
        {
            sfxPlayer[i].volume = volume;
        }
    }
}
