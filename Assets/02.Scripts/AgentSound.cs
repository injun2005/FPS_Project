using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentSound : AudioPlayer
{
    [SerializeField] private AudioClip _hitClip = null, _deathClip = null, _voiceLineClip = null, _attackSound = null;

    public void PlayHitSound()
    {
        PlayClipWithVariablePitch(_hitClip);
    }
    public void PlayDeathSound()
    {
        PlayClip(_deathClip);
    }
    public void PlayVoiceSound()
    {
        PlayClipWithVariablePitch(_voiceLineClip);
    }
    public void PlayAttackSound()
    {
        PlayClip(_attackSound);
    }
}
