using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropStone : MonoBehaviour
{
    [SerializeField]
    GameObject _stonePrefab;
    [SerializeField]
    Transform _stoneOrigin;
    [SerializeField]
    Transform _stoneContainer;
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            DropStoneToGround();
        }
    }

    void DropStoneToGround()
    {
        if (_stonePrefab != null  && _stoneOrigin != null)
        {
            MarkerStone stone = Instantiate(_stonePrefab, _stoneOrigin.position, Quaternion.identity).GetComponent<MarkerStone>();
            stone.transform.parent = _stoneContainer;
            stone.SetColor(GetRandomColor());
        }
    }
    Color GetRandomColor()
    {
        return Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
    }
}
