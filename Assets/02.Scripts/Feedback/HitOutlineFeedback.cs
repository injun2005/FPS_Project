using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitOutlineFeedback : FeedBack
{
    private SkinnedMeshRenderer _renderer;
    [SerializeField]
    private float _flashTime = 0.1f;
    [SerializeField]
    private Material _flashMat = null;
    private Shader _originalMatShader;
    private void Awake()
    {
        _renderer = transform.parent.Find("Mesh").GetComponent<SkinnedMeshRenderer>();
        _originalMatShader = _renderer.material.shader;
    }
    public override void CompletePrevFeedBack()
    {
        StopAllCoroutines();
        _renderer.material.SetInt("_MakeHitColor", 0);
        _renderer.material.shader = _originalMatShader;
    }

    public override void CreateFeedBack()
    {
        if (_renderer.material.HasProperty("_MakeHitColor") == false)
        {
            _renderer.material.shader = _flashMat.shader;//예비 매터리얼로 교체
        }

        _renderer.material.SetInt("_MakeHitColor", 1);
        StartCoroutine(WaitBeforeChangeBack());
    }
    IEnumerator WaitBeforeChangeBack()
    {
        yield return new WaitForSeconds(_flashTime);
        if (_renderer.material.HasProperty("_MakeHitColor") == true)
        {
            _renderer.material.SetInt("_MakeHitColor", 0);
        }
        else
        {
            _renderer.material.shader = _originalMatShader;
        }
    }
}
