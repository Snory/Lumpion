using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    Collider2D _collider;
    public float Radius { get; set; }

    private void Awake()
    {
        _collider = GetComponentInChildren<Collider2D>();
        Radius = Mathf.Max(Mathf.Abs(_collider.bounds.min.x), Mathf.Abs(_collider.bounds.min.y), Mathf.Abs(_collider.bounds.max.x), Mathf.Abs(_collider.bounds.max.y));
    }


}
