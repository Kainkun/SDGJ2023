using System;
using System.Collections;
using System.Collections.Generic;
using EasyButtons;
using FMODUnity;
using TMPro;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    private TextMeshProUGUI text;
    private StudioEventEmitter audio;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
        audio = GetComponent<StudioEventEmitter>();
    }

    public void SetPatronData(PatronData patronData)
    {
        if (patronData.discussion is { Length: > 0 })
        {
            transform.parent.gameObject.SetActive(true);
            SetDialogue(patronData.discussion[0]);
        }
        else
        {
            transform.parent.gameObject.SetActive(false);
        }
    }

    [Button]
    public void SetDialogue(string dialogue)
    {
        StartCoroutine(CR_SetDialogue(dialogue));
    }

    IEnumerator CR_SetDialogue(string dialogue)
    {
        text.SetText(dialogue);
        int totalCharacters = dialogue.Length;
        int currentCharacters = 0;
        audio.Play();
        while (currentCharacters < totalCharacters)
        {
            currentCharacters++;
            text.maxVisibleCharacters = currentCharacters;
            yield return new WaitForSeconds(0.05f);
        }

        audio.Stop();
    }
}