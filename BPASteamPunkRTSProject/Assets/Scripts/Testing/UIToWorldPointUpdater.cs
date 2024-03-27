using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIToWorldPointUpdater : MonoBehaviour
{
    public GameObject ParentTile;
    // Update is called once per frame
    void Update()
    {
        transform.position = Camera.main.WorldToScreenPoint(ParentTile.transform.position);
        transform.localScale = transform.localScale * (4f / Camera.main.orthographicSize);
    }
}
