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
    float _effectiveStepInterval;
    float _initialVolume;
    float _currentInterval;
    bool _rightLeg;
    bool _isMoving;
    private void Awake()
    {
        _effectiveStepInterval = _walkingStepInterval;
        _initialVolume = _rightSource.volume;
    }
    private void Update()
    {
        if (_isMoving)
        {
            _currentInterval += Time.deltaTime;

            if (_currentInterval > _effectiveStepInterval)
            {
                _currentInterval = 0f;
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
        _effectiveStepInterval = isRunning ? _runningStepInterval : _walkingStepInterval;
    }
    public void IsMoving(bool isMoving)
    {
        _isMoving = isMoving;
    }
   void MakeRandom(AudioSource source)
    {
        source.volume = _initialVolume + Random.Range(-0.02f, 0.02f);
        source.pitch = 1.0f + Random.Range(-0.05f, 0.5f);
    }
}
