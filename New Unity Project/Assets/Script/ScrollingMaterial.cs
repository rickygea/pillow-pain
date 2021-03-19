using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingMaterial : MonoBehaviour
{
    [SerializeField] private Vector2 scrollSpeed = Vector2.zero;

    [SerializeField] private Material mat = null;

    private Vector2 _scrollOffset = Vector2.zero;

    // Update is called once per frame
    void Update()
    {
        mat.mainTextureOffset = _scrollOffset;

        _scrollOffset += scrollSpeed * Time.deltaTime;

        if (_scrollOffset.x >= 1 || _scrollOffset.x <= -1)
            _scrollOffset.x = 0;
        if (_scrollOffset.y >= 1 || _scrollOffset.y <= -1)
            _scrollOffset.y = 0;
    }
}
