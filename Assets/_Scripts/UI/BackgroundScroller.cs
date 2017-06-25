using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour {

    public float speed = 0;

    private float pos = 0f;
    private Renderer rend;

    void Awake () {
        rend = GetComponent<Renderer> ();
        if (rend == null ) {
            Debug.LogError ( "Background scroller was attach to " + gameObject.name + " without renderer" );
        }
    }

    void Update () {
        if ( rend != null ) {
            pos += speed;
            if ( pos > 1f ) {
                pos -= 1f;
            }
            rend.material.mainTextureOffset = new Vector2 ( pos, 0 );
        }
	}
}
