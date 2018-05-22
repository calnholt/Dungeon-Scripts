using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour 
{
    private CoinManager coinManager;
    private AudioSource sfx;
    private bool isCollected = false;

	private void Start()
	{
        sfx = GetComponent<AudioSource>();
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
        if (other.gameObject.CompareTag("Player") && !isCollected)
        {
            isCollected = true;
            StartCoroutine(CoinCollect());
        }
	}

    IEnumerator CoinCollect()
    {
        sfx.Play();
        coinManager.CollectCoin();
        yield return new WaitForSeconds(sfx.clip.length);
        gameObject.SetActive(false);
    }

    public void OnSectionReset()
    {
        isCollected = false;
        coinManager.ResetCoin();
        gameObject.SetActive(true);
    }

    public bool IsCollected()
    {
        return isCollected;
    }

    public void SetCoinManager(CoinManager cm)
    {
        coinManager = cm;
    }

}
