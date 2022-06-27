using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitEffectFeedback : FeedBack
{
    [SerializeField] private GameObject panel;

    public override void CompletePrevFeedBack()
    {
      
    }

    public override void CreateFeedBack()
    {
        StartCoroutine(OnActivePanelCoroutine());
    }

    private IEnumerator OnActivePanelCoroutine()
    {
        panel.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        panel.SetActive(false);
    }
}
