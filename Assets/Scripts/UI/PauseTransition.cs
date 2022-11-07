using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseTransition : MonoBehaviour
{
    [SerializeField] AudioClip[] pauseWoosh;
    [SerializeField] float startPoint = -1350;
    [SerializeField] float endPoint = -250;
    float currentPoint;
    RectTransform UItransform;
    void Awake()
    {
        UItransform = GetComponent<RectTransform>();
    }

    void OnEnable()
    {
        currentPoint = startPoint;
        UItransform.transform.localPosition = new Vector3(UItransform.transform.localPosition.x, currentPoint, UItransform.transform.localPosition.z);
        SFXController.Instance.PlaySFX(pauseWoosh);
        StartCoroutine(FlyUp());
    }

    IEnumerator FlyUp()
    {
        while (UItransform.transform.localPosition.y * 0.9f < endPoint) 
        {
            currentPoint *= 0.9f;
            UItransform.transform.localPosition = new Vector3(UItransform.transform.localPosition.x, currentPoint, UItransform.transform.localPosition.z);
            yield return new WaitForSecondsRealtime(0.02f);
        }
        UItransform.transform.localPosition = new Vector3(UItransform.transform.localPosition.x, endPoint, UItransform.transform.localPosition.z);
    }
}
