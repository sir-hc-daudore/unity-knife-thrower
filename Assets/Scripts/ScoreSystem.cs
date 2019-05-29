using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreSystem : MonoBehaviour {

    public GameObject retryUi;
    public Text scoreText;
    public int scorePerValidHit = 1;

    private int score = 0;

	// Use this for initialization
	void Start ()
    {
        MessageService.Subscribe(MessageType.Target_Hit, OnTargetHit);
        MessageService.Subscribe(MessageType.Knife_Hit, OnKnifeHit);

        scoreText.text = score.ToString();
        retryUi.SetActive(false);

    }

    private void OnDestroy()
    {
        MessageService.Unsubscribe(MessageType.Target_Hit, OnTargetHit);
        MessageService.Unsubscribe(MessageType.Knife_Hit, OnKnifeHit);
    }

    private void OnTargetHit(MessageArguments args)
    {
        score += scorePerValidHit;
        scoreText.text = score.ToString();
    }

    private void OnKnifeHit(MessageArguments args)
    {
        retryUi.SetActive(true);
    }
}
