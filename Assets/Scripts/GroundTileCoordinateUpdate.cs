using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundTileCoordinateUpdate : MonoBehaviour
{
    private static readonly int TileCoords = Shader.PropertyToID("_tileCoords");

    void Awake()
    {
        var position = transform.localPosition;                                                 
        GetComponent<MeshRenderer>().material.SetVector(TileCoords, new Vector4(position.x * -0.1f, position.z * -0.1f, 0 , 0));
    }
}
