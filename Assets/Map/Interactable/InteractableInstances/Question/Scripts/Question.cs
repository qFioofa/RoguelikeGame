using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Question : Interactable {

    [SerializeField] private QuestionHandle QuestionHandler;
    [SerializeField] private AudioClip audioClipTriggerd;
    protected override void Interact(){
        SoundFXManager.PlaySoundClipForce(audioClipTriggerd,transform);
        QuestionHandler.Open();
    }
}
