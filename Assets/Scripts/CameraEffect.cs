using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEffect : MonoBehaviour
{
    [SerializeField] private Shader _shader;


    private Material _material;


    private void OnValidate()
    {
        if (_shader != null)
        {
            _material = new Material(_shader);
        }
    }
    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (_material == null)
        {
            Graphics.Blit(source, destination);
            return;
        }

        Graphics.Blit(source, destination, _material);
    }
}
