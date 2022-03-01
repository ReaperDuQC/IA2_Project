using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightSystem : MonoBehaviour
{
    [SerializeField] Light _light;
    [SerializeField] float _nightDuration;
    [SerializeField] bool _isNight = false;
    [SerializeField] bool _useCycle = true;
    float _currentDuration;

    void Update()
    {
        if (_useCycle)
        {
            _currentDuration += Time.deltaTime;
            float ratio = _currentDuration / _nightDuration;

            if (ratio > 1f)
            {
                _currentDuration = 0f;
                _isNight = !_isNight;
            }
        }
    }

    public bool IsNight()
    {
        return _isNight;
    }
}
