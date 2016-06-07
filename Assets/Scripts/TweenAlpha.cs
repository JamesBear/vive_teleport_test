using UnityEngine;
using System.Collections;

public class TweenAlpha : MonoBehaviour {

    public enum State
    {
        Out,
        FadingIn,
        In,
        FadingOut,
    }
    public SpriteRenderer sprite;
    
    private System.Action endCallback;
    private State state = State.Out;
    private float _value;
    private float startTime;
    private float fadeLength;

	// Use this for initialization
	void Start () {
	    if (sprite)
        {
            sprite.gameObject.SetActive(false);
        }
	}
	
	// Update is called once per frame
	void Update () {
	    if (state == State.FadingIn)
        {
            float t = (Time.time - startTime) / fadeLength;
            Apply(t);
            if (t >= 1)
            {
                OnFadeInEnd();
            }
        }
        else if (state == State.FadingOut)
        {
            float t = (Time.time - startTime) / fadeLength;
            Apply(t);
            if (t >= 1)
            {
                OnFadeOutEnd();
            }
        }
	}

    void OnFadeOutEnd()
    {
        state = State.Out;
        sprite.gameObject.SetActive(false);
        if (endCallback != null)
        {
            endCallback();
        }
    }

    void OnFadeInEnd()
    {
        state = State.In;
        if (endCallback != null)
        {
            endCallback();
        }
    }

    public State GetState()
    {
        return state;
    }

    public void StartFadeIn(float duration, System.Action callback)
    {
        endCallback = callback;
        state = State.FadingIn;
        fadeLength = duration;
        startTime = Time.time;
        sprite.gameObject.SetActive(true);
        Apply(0);
    }

    public void StartFadeOut(float duration, System.Action callback)
    {
        endCallback = callback;
        state = State.FadingOut;
        fadeLength = duration;
        startTime = Time.time;
        sprite.gameObject.SetActive(false);
        Apply(1);
    }

    void Apply(float t)
    {
        t = Mathf.Clamp01(t);
        var color = sprite.color;
        color.a = t;
        sprite.color = color;
    }
}
