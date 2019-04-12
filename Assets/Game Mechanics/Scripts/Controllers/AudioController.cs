using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour {

    [SerializeField] AudioClip[] slashingSoundEffectArray;
    [SerializeField] AudioClip arrowSound;
    [SerializeField] AudioClip unitUpgraded;
    [SerializeField] AudioClip unitDeployed;

    [SerializeField] AudioClip playerDefeated;
    [SerializeField] AudioClip levelComplete;

    [SerializeField] AudioClip bossEntered;

    [SerializeField] AudioClip inGameBackgroundMusic;
    [SerializeField] AudioClip mainMenuBackgroundMusic;

    [SerializeField] AudioClip openPanelSound;
    [SerializeField] AudioClip startLevelSound;

    [SerializeField] AudioClip playerLostLifeSound;

    [SerializeField] AudioClip buttonClickedSound;

    [SerializeField] AudioSource audioSource;
    Vector3 cameraPosition;

    private void Start()
    {
        EnsureOneInstance(gameObject);
    }

    public void PlayMainMenuMusic()
    {
        if (audioSource.clip != mainMenuBackgroundMusic)
        {
            audioSource.clip = mainMenuBackgroundMusic;
            audioSource.Play();
        }
    }

    public void PlayInGameBackgroundMusic()
    {
        audioSource.clip = inGameBackgroundMusic;
        audioSource.volume = 0.3f;
        audioSource.Play();
    }

    public void PlayOpenPanelSound()
    {
        PlayAudio(openPanelSound, 0.2f);
    }

    public void PlayStartLevelSound()
    {
        PlayAudio(startLevelSound, 0.7f);
    }

    public void PlayRandomSlashSound()
    {
        int randomNumber = Random.Range(0, slashingSoundEffectArray.Length);
        PlayAudio(slashingSoundEffectArray[randomNumber], 0.3f);
    }

    public void PlayArrowShootSound()
    {
        PlayAudio(arrowSound, 0.2f);
    }

    public void PlayUnitUpgraded()
    {
        PlayAudio(unitUpgraded, 0.5f);
    }

    public void PlayUnitDeployed()
    {
        PlayAudio(unitDeployed, 0.5f);
    }

    public void PlayPlayerDefeated()
    {
        PlayAudio(playerDefeated, 1.0f);
    }

    public void PlayLevelComplete()
    {
        PlayAudio(levelComplete, 0.5f);
    }

    public void PlayBossEntered()
    {
        PlayAudio(bossEntered, 0.5f);
    }

    public void PlayLostLifeSound()
    {
        PlayAudio(playerLostLifeSound, 0.5f);
    }

    public void PlayButtonClicked()
    {
        PlayAudio(buttonClickedSound, 0.3f);
    }

    private void PlayAudio(AudioClip audio, float volume)
    {
        cameraPosition = Camera.main.transform.position;
        AudioSource.PlayClipAtPoint(audio, cameraPosition, volume);
    }

    private void EnsureOneInstance(GameObject gameObject)
    {
        AudioController[] audioControllers = FindObjectsOfType<AudioController>();

        if (audioControllers.Length > 1)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }
}
