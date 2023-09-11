using System;
using System.Collections;
using System.Collections.Generic;
using EasyButtons;
using FMODUnity;
using TMPro;
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
        new[] { "Can I join?" },
        new[] { "May I enter?" },
        new[] { "Permission to party?" },
        new[] { "Is it my turn?" },
        new[] { "Is it my go?" },
        new[] { "Am I invited?" },
        new[] { "Let me in!" },
        new[] { "Please let me through!" },
        new[] { "Is this the spot?" },
        new[] { "Here for the party!" },
        new[] { "I'm on the list!" },
        new[] { "Am I approved?" },
        new[] { "Ready to celebrate!" },
        new[] { "Party time?" },
        new[] { "Is this the entrance?" },
        new[] { "Hello, party!" },
        new[] { "Hi, bouncer!" },
        new[] { "Ready to have fun!" },
        new[] { "I'm here for the animals!" },
        new[] { "Can I come in?" },
        new[] { "Am I allowed?" },
        new[] { "Let's get wild!" },
        new[] { "Is this the right place?" },
        new[] { "Am I welcomed?" },
        new[] { "Is this the party zone?" },
        new[] { "Count me in!" },
        new[] { "Ready to roar!" },
        new[] { "Time to party!" },
        new[] { "I'm here to celebrate!" },
        new[] { "Ready to go wild!" },
        new[] { "Excited to join!" },
        new[] { "Am I cleared?" },
        new[] { "Party, here I come!" },
        new[] { "Let's make some noise!" },
        new[] { "I'm on the guest list!" },
        new[] { "Ready for some animal fun!" },
        new[] { "I'm here for the festivities!" },
        new[] { "I want in!" },
        new[] { "Can I get in on this?" },
        new[] { "Time to dance!" },
        new[] { "Am I on the roster?" },
        new[] { "Is this where the party's at?" },
        new[] { "Party central, right?" },
        new[] { "I've got my dancing shoes!" },
        new[] { "Let me join the party!" },
        new[] { "Ready to have a blast!" },
        new[] { "Is this the wild party?" },
        new[] { "Can I enter, please?" },
        new[] { "Permission to dance!" },
        new[] { "I'm in the party spirit!" },
        new[] { "Ready to get the party started!" },
        new[] { "Party animal reporting!" },
        new[] { "Can I be part of the fun?" },
        new[] { "Count me among the guests!" },
        new[] { "Party vibes!" },
        new[] { "Am I good to go?" },
        new[] { "Let's party like animals!" },
        new[] { "I'm here to celebrate with you!" },
        new[] { "I come in peace and party!" },
        new[] { "Is this where the action is?" },
        new[] { "Party, party, party!" },
        new[] { "Let me join the festivities!" },
        new[] { "Ready to have a hoot!" },
        new[] { "Let's make memories!" },
        new[] { "Am I part of the gang?" },
        new[] { "Can I party with you?" },
        new[] { "I'm here for the animal-themed fun!" },
        new[] { "Party mode activated!" },
        new[] { "Is this where the party begins?" },
        new[] { "Ready to groove!" },
        new[] { "Let me in on the fun!" },
        new[] { "Permission to party, granted?" },
        new[] { "I'm here for the good times!" },
        new[] { "Party on my mind!" },
        new[] { "Am I allowed to enter?" },
        new[] { "Party animal here!" },
        new[] { "Let's get the party started!" },
        new[] { "I'm here to dance!" },
        new[] { "Ready to have a blast!" },
        new[] { "Is this the party entrance?" },
        new[] { "Am I in the right place?" },
        new[] { "Party, party, party!" },
        new[] { "Let me join the festivities!" },
        new[] { "Ready to have a hoot!" },
        new[] { "Let's make memories!" },
        new[] { "Am I part of the gang?" },
        new[] { "Can I party with you?" },
        new[] { "I'm here for the animal-themed fun!" },
        new[] { "Party mode activated!" },
        new[] { "Is this where the party begins?" },
        new[] { "Ready to groove!" },
        new[] { "Let me in on the fun!" },
        new[] { "Permission to party, granted?" },
        new[] { "I'm here for the good times!" },
        new[] { "Party on my mind!" },
        new[] { "Am I allowed to enter?" },
        new[] { "Party animal here!" },
        new[] { "Let's get the party started!" },
        new[] { "I'm here to dance!" },
        new[] { "Ready to have a blast!" },
        new[] { "Is this the party entrance?" },
        new[] { "Am I in the right place?" },
        new[] { "Ready to have a wild time!" },
        new[] { "Let's celebrate together!" },
        new[] { "Am I part of the party crew?" },
        new[] { "I'm here for the animal antics!" },
        new[] { "Party up ahead?" },
        new[] { "Can I join the fun?" },
        new[] { "Ready to party like crazy!" },
        new[] { "Is this the party spot?" },
        new[] { "Party, here I am!" },
        new[] { "Let's make it a party to remember!" },
        new[] { "Am I invited to the party?" },
        new[] { "I've got my dancing shoes on!" },
        new[] { "Let me join the animal-themed bash!" },
        new[] { "Is this where the party's at?" },
        new[] { "Party animal reporting for duty!" }
    };

    public static string[][] badDialogues = new[]
    {
        new[] { "Excuse me, but I belong here." },
        new[] { "Step aside, I'm entering." },
        new[] { "I'm coming through, no questions." },
        new[] { "I don't wait in lines." },
        new[] { "Make way, party crasher!" },
        new[] { "Out of my way, bouncer!" },
        new[] { "Move it or lose it!" },
        new[] { "I'm getting in, one way or another." },
        new[] { "You can't stop me from partying!" },
        new[] { "I'm not taking 'no' for an answer." },
        new[] { "This is happening, like it or not!" },
        new[] { "Step off, I'm coming in." },
        new[] { "You're not stopping me tonight." },
        new[] { "I demand entrance!" },
        new[] { "I'm on a mission to party!" },
        new[] { "This party is mine now!" },
        new[] { "Let's cut the nonsense, I'm in." },
        new[] { "I'm taking control of this party!" },
        new[] { "This is a party takeover!" },
        new[] { "No more waiting, I'm entering." },
        new[] { "I'm here to party, move aside!" },
        new[] { "Time to clear a path for me." },
        new[] { "I'm not asking, I'm telling." },
        new[] { "I'm getting in, guaranteed." },
        new[] { "Party's not complete without me!" },
        new[] { "Outta my way, I'm VIP." },
        new[] { "Don't test me, I'm getting in." },
        new[] { "I'll party here whether you like it or not." },
        new[] { "I'll be partying inside, no doubt." },
        new[] { "Move aside, party animal incoming!" },
        new[] { "Step aside, I'm the life of this party." },
        new[] { "This is my party now!" },
        new[] { "You won't deny me this party!" },
        new[] { "I'm not waiting any longer." },
        new[] { "I'm crashing this party, no ifs or buts." },
        new[] { "Step back, I'm partying hard!" },
        new[] { "I'm unstoppable when it comes to parties." },
        new[] { "You're no match for my party spirit!" },
        new[] { "I'll break in if I have to!" },
        new[] { "This party's got my name written all over it." },
        new[] { "I'm the party kingpin here." },
        new[] { "I don't take 'no' for an answer!" },
        new[] { "This bouncer won't keep me out." },
        new[] { "You're looking at the party boss!" },
        new[] { "Move or be moved, your choice." },
        new[] { "I'm partying here, like it or not!" },
        new[] { "I'm making my entrance, get ready." },
        new[] { "Nobody can stop my party vibe!" },
        new[] { "I'm taking over this party!" },
        new[] { "I've got a date with this party, and it starts now!" },
        new[] { "Party time, whether you like it or not!" },
        new[] { "I'm not asking, I'm demanding entry!" },
        new[] { "Party crasher, reporting for duty!" },
        new[] { "Move out of my way, or else!" },
        new[] { "I'm not taking 'no' as an answer!" },
        new[] { "You can't deny me the party of a lifetime!" },
        new[] { "I'm crashing this party, deal with it!" },
        new[] { "This party is mine to conquer!" },
        new[] { "Outta my way, party animal coming through!" },
        new[] { "Step aside, and nobody gets hurt!" },
        new[] { "I'm getting in, no matter what!" },
        new[] { "I'll do whatever it takes to get in!" },
        new[] { "You'll regret stopping me!" },
        new[] { "I'm not backing down, bouncer!" },
        new[] { "This party won't be the same without me!" },
        new[] { "My party energy is unstoppable!" },
        new[] { "I'm getting in, whether you like it or not!" },
        new[] { "You won't deny me this epic party!" },
        new[] { "I'm not waiting any longer!" },
        new[] { "I'm crashing this party, ready or not!" },
        new[] { "Step back, and let me through!" },
        new[] { "I'll break through any obstacle!" },
        new[] { "This party's destiny is in my hands!" },
        new[] { "You're no match for my party determination!" },
        new[] { "I'll bulldoze my way in!" },
        new[] { "This party's mine, and nobody can stop me!" },
        new[] { "I'm taking charge of this party, period!" },
        new[] { "I won't take 'no' for an answer!" },
        new[] { "This bouncer can't keep me out!" },
        new[] { "You're messing with the party master!" },
        new[] { "Move aside or face the consequences!" },
        new[] { "This party belongs to me now!" },
        new[] { "I'm partying here, whether you like it or not!" },
        new[] { "I'm storming this party, and that's final!" },
        new[] { "Party time, and nothing's stopping me!" },
        new[] { "I'm not asking, I'm demanding entry!" },
        new[] { "Party crasher, reporting for duty!" },
        new[] { "Move out of my way, or else!" },
        new[] { "I'm not taking 'no' as an answer!" },
        new[] { "You can't deny me the party of a lifetime!" },
        new[] { "I'm crashing this party, deal with it!" },
        new[] { "This party is mine to conquer!" },
        new[] { "Outta my way, party animal coming through!" },
        new[] { "Step aside, and nobody gets hurt!" },
        new[] { "I'm getting in, no matter what!" },
        new[] { "I'll do whatever it takes to get in!" },
        new[] { "You'll regret stopping me!" },
        new[] { "I'm not backing down, bouncer!" },
        new[] { "This party won't be the same without me!" },
        new[] { "My party energy is unstoppable!" },
        new[] { "I'm getting in, whether you like it or not!" },
        new[] { "You won't deny me this epic party!" },
        new[] { "I'm not waiting any longer!" },
        new[] { "I'm crashing this party, ready or not!" },
        new[] { "Step back, and let me through!" },
        new[] { "I'll break through any obstacle!" },
        new[] { "This party's destiny is in my hands!" },
        new[] { "You're no match for my party determination!" },
        new[] { "I'll bulldoze my way in!" },
        new[] { "This party's mine, and nobody can stop me!" },
        new[] { "I'm taking charge of this party, period!" },
        new[] { "I won't take 'no' for an answer!" },
        new[] { "This bouncer can't keep me out!" },
        new[] { "You're messing with the party master!" },
        new[] { "Move aside or face the consequences!" },
        new[] { "This party belongs to me now!" },
        new[] { "I'm partying here, whether you like it or not!" },
        new[] { "I'm storming this party, and that's final!" },
        new[] { "Party time, and nothing's stopping me!" }
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