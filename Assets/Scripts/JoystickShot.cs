using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class JoystickShot : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField]
    private Image ballSizeIndicator;
    [SerializeField]
    Vector3 indicatorStartScale;

    private PlayerController player;

    public float framesTook;
    public float maxFramesShot;
    public float bulletSizeAmplifier;

    private bool isHolding = false;
    private bool isHoldingTooLong = false;

    void Start()
    {
        player = GameObject.Find("BallPlayer").GetComponent<PlayerController>();
        ballSizeIndicator.rectTransform.localScale = indicatorStartScale;

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!isHoldingTooLong)
        {
            isHolding = true;
        }
    }

    void Update()
    {
        if (isHolding)
        {
            framesTook++;
            bulletSizeAmplifier = framesTook / maxFramesShot;

            if (bulletSizeAmplifier <= 1)
            {
                ballSizeIndicator.rectTransform.localScale = indicatorStartScale * bulletSizeAmplifier;
            }
        }

        if (!isHolding)
        {
            ballSizeIndicator.rectTransform.localScale *= 0;
        }

        if (framesTook >= maxFramesShot)
        {
            isHoldingTooLong = true;
            bulletSizeAmplifier = 0;
            player.Shot();
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!isHoldingTooLong)
        {
            player.Shot();
            framesTook = 0;
            isHolding = false;
        }
    }
}

