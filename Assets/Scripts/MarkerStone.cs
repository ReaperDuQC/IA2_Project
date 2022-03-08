using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerStone : MonoBehaviour
{
    Renderer _renderer;
    Light _light;
    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _light = GetComponentInChildren<Light>();
    }
    public void SetColor(Color newColor)
    {
        _renderer.material.SetColor("_Color", newColor);
        _light.color = newColor;
    }
}
