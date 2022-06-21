using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FeedBack : MonoBehaviour
{
    public abstract void CreateFeedBack();
    public abstract void CompletePrevFeedBack();

    private void OnDestroy()
    {
        CompletePrevFeedBack();
    }
    private void OnDisable()
    {
        CompletePrevFeedBack();
    }
}
