using System;
using System.Collections;
using System.Collections.Generic;
using EasyButtons;
using FMODUnity;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class Dialogue : MonoBehaviour
{
    private TextMeshProUGUI text;
    private StudioEventEmitter audio;

    // Chat GPT Prompts

    // list 100 short exclamation for what one would say to ask the bouncer if they're allowed into the animal themed party.
    //
    //     List it like this:
    // new[] { "Hello" },
    // new[] { "Hey" },
    // new[] { "Yo" },

    // now make a similar list but more agressive

    public static string[][] goodDialogues = new[]
    {
        new[] { "Hello!" },
        new[] { "Hey there!" },
        new[] { "Hi!" },
        new[] { "Excuse me!" },
        new[] { "Pardon me!" },
        new[] { "Is this the place?" },
        new[] { "Am I on the list?" },
        new[] { "Can I <color=#HEX_COLOR>join<color=#HEX_COLOR>?" },
        new[] { "May I enter?" },
        new[] { "Permission to <color=#HEX_COLOR>party<color=#HEX_COLOR>?" },
        new[] { "Is it my <color=#HEX_COLOR>turn<color=#HEX_COLOR>?" },
        new[] { "Is it my <color=#HEX_COLOR>go<color=#HEX_COLOR>?" },
        new[] { "Am I <color=#HEX_COLOR>invited<color=#HEX_COLOR>?" },
        new[] { "Let me in!" },
        new[] { "Please let me <color=#HEX_COLOR>through<color=#HEX_COLOR>!" },
        new[] { "Is this the <color=#HEX_COLOR>spot<color=#HEX_COLOR>?" },
        new[] { "Here for the <color=#HEX_COLOR>party<color=#HEX_COLOR>!" },
        new[] { "I'm on the <color=#HEX_COLOR>list<color=#HEX_COLOR>!" },
        new[] { "Am I <color=#HEX_COLOR>approved<color=#HEX_COLOR>?" },
        new[] { "Ready to <color=#HEX_COLOR>celebrate<color=#HEX_COLOR>!" },
        new[] { "<color=#HEX_COLOR>Party<color=#HEX_COLOR> time?" },
        new[] { "Is this the <color=#HEX_COLOR>entrance<color=#HEX_COLOR>?" },
        new[] { "Hello, <color=#HEX_COLOR>party<color=#HEX_COLOR>!" },
        new[] { "Hi, <color=#HEX_COLOR>bouncer<color=#HEX_COLOR>!" },
        new[] { "Ready to have <color=#HEX_COLOR>fun<color=#HEX_COLOR>!" },
        new[] { "I'm here for the <color=#HEX_COLOR>animals<color=#HEX_COLOR>!" },
        new[] { "Can I <color=#HEX_COLOR>come in<color=#HEX_COLOR>?" },
        new[] { "Am I <color=#HEX_COLOR>allowed<color=#HEX_COLOR>?" },
        new[] { "Let's get <color=#HEX_COLOR>wild<color=#HEX_COLOR>!" },
        new[] { "Is this the <color=#HEX_COLOR>right place<color=#HEX_COLOR>?" },
        new[] { "Am I <color=#HEX_COLOR>welcomed<color=#HEX_COLOR>?" },
        new[] { "Is this the <color=#HEX_COLOR>party zone<color=#HEX_COLOR>?" },
        new[] { "Count me in!" },
        new[] { "Ready to <color=#HEX_COLOR>roar<color=#HEX_COLOR>!" },
        new[] { "Time to <color=#HEX_COLOR>party<color=#HEX_COLOR>!" },
        new[] { "I'm here to <color=#HEX_COLOR>celebrate<color=#HEX_COLOR>!" },
        new[] { "Ready to go <color=#HEX_COLOR>wild<color=#HEX_COLOR>!" },
        new[] { "Excited to <color=#HEX_COLOR>join<color=#HEX_COLOR>!" },
        new[] { "Am I <color=#HEX_COLOR>cleared<color=#HEX_COLOR>?" },
        new[] { "<color=#HEX_COLOR>Party<color=#HEX_COLOR>, here I come!" },
        new[] { "Let's make some <color=#HEX_COLOR>noise<color=#HEX_COLOR>!" },
        new[] { "I'm on the <color=#HEX_COLOR>guest list<color=#HEX_COLOR>!" },
        new[] { "Ready for some <color=#HEX_COLOR>animal<color=#HEX_COLOR> <color=#HEX_COLOR>fun<color=#HEX_COLOR>!" },
        new[] { "I'm here for the <color=#HEX_COLOR>festivities<color=#HEX_COLOR>!" },
        new[] { "I want in!" },
        new[] { "Can I get in on <color=#HEX_COLOR>this<color=#HEX_COLOR>?" },
        new[] { "Time to <color=#HEX_COLOR>dance<color=#HEX_COLOR>!" },
        new[] { "Am I on the <color=#HEX_COLOR>roster<color=#HEX_COLOR>?" },
        new[] { "Is this where the <color=#HEX_COLOR>party's at<color=#HEX_COLOR>?" },
        new[] { "<color=#HEX_COLOR>Party<color=#HEX_COLOR> central, right?" },
        new[] { "I've got my <color=#HEX_COLOR>dancing<color=#HEX_COLOR> shoes!" },
        new[] { "Let me <color=#HEX_COLOR>join<color=#HEX_COLOR> the <color=#HEX_COLOR>party<color=#HEX_COLOR>!" },
        new[] { "Ready to have a <color=#HEX_COLOR>blast<color=#HEX_COLOR>!" },
        new[] { "Is this the <color=#HEX_COLOR>wild<color=#HEX_COLOR> <color=#HEX_COLOR>party<color=#HEX_COLOR>?" },
        new[] { "Can I <color=#HEX_COLOR>enter<color=#HEX_COLOR>, please?" },
        new[] { "Permission to <color=#HEX_COLOR>dance<color=#HEX_COLOR>!" },
        new[] { "I'm in the <color=#HEX_COLOR>party<color=#HEX_COLOR> spirit!" },
        new[] { "Ready to get the <color=#HEX_COLOR>party<color=#HEX_COLOR> started!" },
        new[] { "<color=#HEX_COLOR>Party<color=#HEX_COLOR> <color=#HEX_COLOR>animal<color=#HEX_COLOR> reporting!" },
        new[] { "Can I be part of the <color=#HEX_COLOR>fun<color=#HEX_COLOR>?" },
        new[] { "Count me among the <color=#HEX_COLOR>guests<color=#HEX_COLOR>!" },
        new[] { "<color=#HEX_COLOR>Party<color=#HEX_COLOR> vibes!" },
        new[] { "Am I good to go?" },
        new[] { "Let's <color=#HEX_COLOR>party<color=#HEX_COLOR> like <color=#HEX_COLOR>animals<color=#HEX_COLOR>!" },
        new[] { "I'm here to <color=#HEX_COLOR>celebrate<color=#HEX_COLOR> with you!" },
        new[] { "I come in <color=#HEX_COLOR>peace<color=#HEX_COLOR> and <color=#HEX_COLOR>party<color=#HEX_COLOR>!" },
        new[] { "I'm here to <color=#HEX_COLOR>party<color=#HEX_COLOR>!" },
    };

    public static string[][] badDialogues = new[]
    {
        new[] { "Excuse me, but I belong here." },
        new[] { "Step aside, I'm <color=#HEX_COLOR>entering</color>." },
        new[] { "I'm <color=#HEX_COLOR>coming through</color>, no questions." },
        new[] { "I don't wait in <color=#HEX_COLOR>lines</color>." },
        new[] { "Make way, party <color=#HEX_COLOR>crasher</color>!" },
        new[] { "Out of my way, <color=#HEX_COLOR>bouncer</color>!" },
        new[] { "Move it or <color=#HEX_COLOR>lose it</color>!" },
        new[] { "I'm <color=#HEX_COLOR>getting in</color>, one way or <color=#HEX_COLOR>another</color>." },
        new[] { "You can't stop me from <color=#HEX_COLOR>partying</color>!" },
        new[] { "I'm not taking '<color=#HEX_COLOR>no</color>' for an <color=#HEX_COLOR>answer</color>." },
        new[] { "This is <color=#HEX_COLOR>happening</color>, like it or not!" },
        new[] { "Step off, I'm <color=#HEX_COLOR>coming in</color>." },
        new[] { "You're not stopping me <color=#HEX_COLOR>tonight</color>." },
        new[] { "I <color=#HEX_COLOR>demand</color> <color=#HEX_COLOR>entrance</color>!" },
        new[] { "I'm on a <color=#HEX_COLOR>mission</color> to <color=#HEX_COLOR>party</color>!" },
        new[] { "This <color=#HEX_COLOR>party</color> is <color=#HEX_COLOR>mine</color> now!" },
        new[] { "Let's cut the <color=#HEX_COLOR>nonsense</color>, I'm <color=#HEX_COLOR>in</color>." },
        new[] { "I'm <color=#HEX_COLOR>taking control</color> of this <color=#HEX_COLOR>party</color>!" },
        new[] { "This is a <color=#HEX_COLOR>party takeover</color>!" },
        new[] { "No more <color=#HEX_COLOR>waiting</color>, I'm <color=#HEX_COLOR>entering</color>." },
        new[] { "I'm <color=#HEX_COLOR>here</color> to <color=#HEX_COLOR>party</color>, move aside!" },
        new[] { "Time to <color=#HEX_COLOR>clear a path</color> for me." },
        new[] { "I'm not <color=#HEX_COLOR>asking</color>, I'm <color=#HEX_COLOR>telling</color>." },
        new[] { "I'm <color=#HEX_COLOR>getting in</color>, <color=#HEX_COLOR>guaranteed</color>." },
        new[] { "Party's not <color=#HEX_COLOR>complete</color> without me!" },
        new[] { "Outta my way, I'm <color=#HEX_COLOR>VIP</color>." },
        new[] { "Don't <color=#HEX_COLOR>test me</color>, I'm <color=#HEX_COLOR>getting in</color>." },
        new[] { "I'll party here whether you like it or not!" },
        new[] { "I'll be partying <color=#HEX_COLOR>inside</color>, no doubt." },
        new[] { "Move aside, party <color=#HEX_COLOR>animal</color> <color=#HEX_COLOR>incoming</color>!" },
        new[] { "Step aside, I'm the <color=#HEX_COLOR>life</color> of this <color=#HEX_COLOR>party</color>." },
        new[] { "This is my <color=#HEX_COLOR>party</color> now!" },
        new[] { "You won't <color=#HEX_COLOR>deny</color> me this <color=#HEX_COLOR>party</color>!" },
        new[] { "I'm not <color=#HEX_COLOR>waiting</color> any longer." },
        new[]
        {
            "I'm <color=#HEX_COLOR>crashing this party</color>, no <color=#HEX_COLOR>ifs</color> or <color=#HEX_COLOR>buts</color>."
        },
        new[] { "Step back, I'm <color=#HEX_COLOR>partying</color> hard!" },
        new[] { "I'm <color=#HEX_COLOR>unstoppable</color> when it comes to <color=#HEX_COLOR>parties</color>." },
        new[] { "You're no <color=#HEX_COLOR>match</color> for my <color=#HEX_COLOR>party spirit</color>!" },
        new[] { "I'll break in if I have to!" },
        new[] { "This <color=#HEX_COLOR>party's</color> got my <color=#HEX_COLOR>name</color> written all over it." },
        new[] { "I'm the <color=#HEX_COLOR>party kingpin</color> here." },
        new[] { "I don't take '<color=#HEX_COLOR>no</color>' for an <color=#HEX_COLOR>answer</color>!" },
        new[] { "This <color=#HEX_COLOR>bouncer</color> won't keep me <color=#HEX_COLOR>out</color>." },
        new[] { "You're looking at the <color=#HEX_COLOR>party boss</color>!" },
        new[] { "Move or be moved, your choice." },
        new[] { "I'm <color=#HEX_COLOR>partying</color> here, like it or not!" },
        new[] { "I'm making my <color=#HEX_COLOR>entrance</color>, get <color=#HEX_COLOR>ready</color>." },
        new[] { "Nobody can <color=#HEX_COLOR>stop</color> my <color=#HEX_COLOR>party vibe</color>!" },
        new[] { "I'm <color=#HEX_COLOR>taking over</color> this <color=#HEX_COLOR>party</color>!" },
        new[]
        {
            "I've got a <color=#HEX_COLOR>date</color> with this <color=#HEX_COLOR>party</color>, and it starts <color=#HEX_COLOR>now</color>!"
        },
        new[] { "Party <color=#HEX_COLOR>time</color>, whether you like it or not!" },
        new[] { "I'm not <color=#HEX_COLOR>asking</color>, I'm <color=#HEX_COLOR>demanding entry</color>!" },
        new[] { "Party <color=#HEX_COLOR>crasher</color>, reporting for <color=#HEX_COLOR>duty</color>!" },
        new[] { "Move out of my way, or <color=#HEX_COLOR>else</color>!" },
        new[] { "I'm not <color=#HEX_COLOR>taking 'no' as an answer</color>!" },
        new[] { "You can't <color=#HEX_COLOR>deny</color> me the <color=#HEX_COLOR>party of a lifetime</color>!" },
        new[] { "I'm <color=#HEX_COLOR>crashing this party</color>, <color=#HEX_COLOR>deal with it</color>!" },
        new[]
        {
            "This <color=#HEX_COLOR>party</color> is <color=#HEX_COLOR>mine</color> to <color=#HEX_COLOR>conquer</color>!"
        },
        new[] { "Outta my way, party <color=#HEX_COLOR>animal</color> <color=#HEX_COLOR>coming through</color>!" },
        new[] { "Step aside, and <color=#HEX_COLOR>nobody gets hurt</color>!" },
        new[] { "I'm <color=#HEX_COLOR>getting in</color>, no <color=#HEX_COLOR>matter what</color>!" },
        new[] { "I'll do <color=#HEX_COLOR>whatever it takes</color> to get <color=#HEX_COLOR>in</color>!" },
        new[] { "You'll <color=#HEX_COLOR>regret</color> <color=#HEX_COLOR>stopping</color> me!" },
        new[] { "I'm not <color=#HEX_COLOR>backing down</color>, <color=#HEX_COLOR>bouncer</color>!" },
        new[] { "This <color=#HEX_COLOR>party</color> won't be the <color=#HEX_COLOR>same</color> without me!" },
        new[] { "My <color=#HEX_COLOR>party energy</color> is <color=#HEX_COLOR>unstoppable</color>!" },
        new[] { "I'm <color=#HEX_COLOR>getting in</color>, whether you <color=#HEX_COLOR>like it or not</color>!" },
        new[] { "You won't <color=#HEX_COLOR>deny me</color> this <color=#HEX_COLOR>epic party</color>!" },
        new[] { "I'm not <color=#HEX_COLOR>waiting</color> any longer!" },
        new[] { "I'm <color=#HEX_COLOR>crashing this party</color>, <color=#HEX_COLOR>ready or not</color>!" },
        new[] { "Step back, and <color=#HEX_COLOR>let me through</color>!" },
        new[] { "I'll <color=#HEX_COLOR>break through</color> any <color=#HEX_COLOR>obstacle</color>!" },
        new[] { "This <color=#HEX_COLOR>party's destiny</color> is in my <color=#HEX_COLOR>hands</color>!" },
        new[] { "You're no <color=#HEX_COLOR>match</color> for my <color=#HEX_COLOR>party determination</color>!" },
        new[] { "I'll <color=#HEX_COLOR>bulldoze</color> my way <color=#HEX_COLOR>in</color>!" },
        new[]
        {
            "This <color=#HEX_COLOR>party's mine</color>, and <color=#HEX_COLOR>nobody</color> can <color=#HEX_COLOR>stop me</color>!"
        },
        new[]
        {
            "I'm <color=#HEX_COLOR>taking charge</color> of this <color=#HEX_COLOR>party</color>, <color=#HEX_COLOR>period</color>!"
        },
        new[] { "I won't <color=#HEX_COLOR>take 'no' for an answer</color>!" },
        new[] { "This <color=#HEX_COLOR>bouncer</color> can't keep me <color=#HEX_COLOR>out</color>!" },
        new[] { "You're messing with the <color=#HEX_COLOR>party master</color>!" },
        new[] { "Move aside or face the <color=#HEX_COLOR>consequences</color>!" },
        new[] { "This <color=#HEX_COLOR>party</color> belongs to me <color=#HEX_COLOR>now</color>!" },
        new[] { "I'm <color=#HEX_COLOR>partying</color> here, whether you <color=#HEX_COLOR>like it or not</color>!" },
        new[] { "I'm <color=#HEX_COLOR>storming this party</color>, and <color=#HEX_COLOR>that's final</color>!" },
        new[] { "Party <color=#HEX_COLOR>time</color>, and <color=#HEX_COLOR>nothing's stopping me</color>!" }
    };

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
        audio = GetComponent<StudioEventEmitter>();
    }

    public void SetPatronData(PatronData patronData)
    {
        SetDialogue(patronData.discussion[0]);
    }

    [Button]
    public void SetDialogue(string dialogue)
    {
        StartCoroutine(CR_SetDialogue(dialogue));
    }

    IEnumerator CR_SetDialogue(string dialogue)
    {
        Color c = Random.ColorHSV(0f, 1f, 0.5f, 0.7f, 1f, 1f);
        string cs = c.ToHexString();
        print(cs);
        cs = cs.Substring(0, cs.Length - 2);
        print(cs);
        dialogue = dialogue.Replace("HEX_COLOR", cs);
        text.SetText(dialogue);
        int totalCharacters = dialogue.Length;
        int currentCharacters = 0;
        float p = Random.Range(0f, 1f);
        audio.Play();
        audio.SetParameter("Pitch Down", p);
        while (currentCharacters < totalCharacters)
        {
            currentCharacters++;
            text.maxVisibleCharacters = currentCharacters;
            yield return new WaitForSeconds(0.05f);
        }

        audio.Stop();
    }
}