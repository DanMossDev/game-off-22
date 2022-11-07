using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISounds : MonoBehaviour
{
    [SerializeField] AudioClip[] hoverSounds;
    [SerializeField] AudioClip[] clickSounds;
    
    public void OnHover()
    {
        SFXController.Instance.PlaySFX(hoverSounds);
    }

    public void OnUIClick()
    {
        SFXController.Instance.PlaySFX(clickSounds);
    }
}
