using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootStep : MonoBehaviour
{
    [SerializeField] List<AudioClip> _clips;
    [SerializeField] AudioSource _rightSource;
    [SerializeField] AudioSource _leftSource;
    [SerializeField] float _walkingStepInterval;
    [SerializeField] float _runningStepInterval;
    float effectiveStepInterval;

    float currentInterval;
    bool _rightLeg;
    bool _isMoving;
    private void Awake()
    {
        effectiveStepInterval = _walkingStepInterval;
    }
    private void Update()
    {
        if (_isMoving)
        {
            currentInterval += Time.deltaTime;

            if (currentInterval > effectiveStepInterval)
            {
                currentInterval = 0f;
                PlayStepSound();
            }
        }
    }
    void PlayStepSound()
    {
       
        AudioSource source = _rightLeg ? _rightSource : _leftSource;
        AudioClip clip = GetRandomClip();
        PlayFootStep(source, clip);
        _rightLeg = !_rightLeg;
        
    }
    void PlayFootStep(AudioSource source , AudioClip clip)
    {
        if (clip != null)
        {
            source.clip = clip;
            MakeRandom(source);
            source.Play();
        }
    }
    AudioClip GetRandomClip()
    {
        AudioClip clip = null;

        if (_clips.Count > 0)
        {
            if(_clips.Count > 1)
            {
                int index = Random.Range(1, _clips.Count);
                AudioClip clipToChange = _clips[0];
                clip = _clips[index];
                _clips[0] = clip;
                _clips[index] = clipToChange;
            }
            clip = _clips[0];
        }
        return clip;
    }
    public void SetInterval(bool isRunning)
    {
        effectiveStepInterval = isRunning ? _runningStepInterval : _walkingStepInterval;
    }
    public void IsMoving(bool isMoving)
    {
        _isMoving = isMoving;
    }
   void MakeRandom(AudioSource source)
    {
        source.volume = 1.0f + Random.Range(-0.2f, 0.2f);
        source.pitch = 1.0f + Random.Range(-0.2f, 0.2f);
    }
}
