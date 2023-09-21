using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class CoinController : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] int _vertexCount;
    [SerializeField] Vector3 _targetScale;
    [SerializeField] float _entryDuration;
    [SerializeField] float _exitDuration;

    GameObject _logic;
    CoinSpawner _spawner;
    ScoreSystem _scoreSystem;

    bool isCircle;

    void Awake()
    {
        _logic = GameObject.Find("Logic");
        _spawner = _logic.GetComponent<CoinSpawner>();
        _scoreSystem = _logic.GetComponent<ScoreSystem>();
        // Debug.Log(_scoreSystem);

        CheckVertices();
        SetRotation();
        StartCoroutine(ScaleUp());
        StartCoroutine(FadeColor());
    }

    // Eases the coin in at spawn - scales from 0 to Target Scale.
    IEnumerator ScaleUp()
    {
        float time = 0;
        Vector3 startScale = new Vector3(0, 0, 0);

        while (time < _entryDuration)
        {
            float t = time / _entryDuration;
            t = t * t * (10f - 1f * t);

            transform.localScale = Vector3.Lerp(startScale, _targetScale, t);
            time += Time.deltaTime;
            yield return null;
        }
        
        // Starts scaling the coin down after Target Scale has been met.
        transform.localScale = _targetScale;
        StartCoroutine(ScaleDown());
    }

    // Smoothly reduces the scale of the coin before destroying it.
    IEnumerator ScaleDown()
    {
        float time = 0;
        Vector3 startScale = transform.localScale;
        Vector3 newTargetScale = new Vector3 (0, 0, 0);

        while (time < _exitDuration)
        {
            float t = time / _exitDuration;
            t = t * t * (10f - 1f * t);

            transform.localScale = Vector3.Lerp(startScale, newTargetScale, t);
            time += Time.deltaTime;
            yield return null;
        }

        // Destroys the coin after scale has been reduced to 0.
        // Also checks if the coin was circular, in which case HP is reduced by 1.

        if (_vertexCount == 0)
        {
            _scoreSystem.DecreaseHealth(1);
        }

        transform.localScale = newTargetScale;
        DestroyCoin();
    }

    IEnumerator FadeColor()
    {
        SpriteRenderer sprite = gameObject.GetComponent<SpriteRenderer>();

        float time = 0;
        float duration = _entryDuration + _exitDuration;
        Color startColor = sprite.color;
        Color targetColor = new Color(1, 1, 1, 0);

        while (time < duration)
        {
            sprite.color = Color.Lerp(startColor, targetColor, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        sprite.color = targetColor;
    }

    // Same as ScaleDown but quicker.
    // Intended to be initiated by clicking as opposed to being automated.
    IEnumerator ScaleDownQuick()
    {
        float time = 0;
        float duration = 0.5f;
        Vector3 startScale = transform.localScale;
        Vector3 newTargetScale = new Vector3(0, 0, 0);

        while (time < duration)
        {
            float t = time / duration;
            t = t * t * (10f - 1f * t);

            transform.localScale = Vector3.Lerp(startScale, newTargetScale, t);
            time += Time.deltaTime;
            yield return null;
        }

        // Destroys the coin after scale has been reduced to 0.
        transform.localScale = newTargetScale;
        DestroyCoin();
    }

    void CheckVertices()
    {
        if(_vertexCount == 0)
        {
            isCircle = true;
        }
        else if(_vertexCount != 0)
        {
            isCircle = false;
        }
    }

    void SetRotation()
    {
        transform.Rotate(0, 0, Random.Range(0, 360), Space.Self);
    }

    void DestroyCoin()
    {
        Destroy(gameObject);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Collider2D collider = GetComponent<Collider2D>();

        if (isCircle == true)
        {
            // Debug.Log("Circle. +1");

            _spawner.DecreaseInterval();
            _scoreSystem.IncreaseScore();
        }
        else if (isCircle != true)
        {
            // Debug.Log("Polygon. -" + _vertexCount);

            _scoreSystem.DecreaseHealth(_vertexCount);
        }
        else
        {
            Debug.Log("Something wrong.");
        }

        StopAllCoroutines();
        StartCoroutine(ScaleDownQuick());

        collider.enabled = false;
    }
}
