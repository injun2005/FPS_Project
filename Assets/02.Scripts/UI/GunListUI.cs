using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;
using System;
using UnityEditor.Events;
public class GunListUI : MonoBehaviour
{
    [SerializeField] private GunPanel _gunPanelPrefab;
    private List<GunPanel> _panelList;

    [SerializeField] private AudioClip _changeClip;
    [SerializeField] private float _transitionTime = 0.2f;

    private AudioSource _audioSource;

    [Header("초기 위치값")]
    [SerializeField] private Vector2 _initAnchorPos;
    private float _xDelta = 7f;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _panelList = new List<GunPanel>();
    }

    public void InitUIPanel(List<Weapon> weaponList, int nowIndex)
    {
        List<Weapon> cloneList = weaponList.ToList(); // 리스트 복제, 안에 있는 원소는 참조로 복제

        for (int i = 0; i < nowIndex; i++)
        {
            Weapon first =cloneList.First();
            cloneList.Remove(first);
            cloneList.Add(first);
        }

        cloneList.Reverse();
        _panelList.Clear();
        for (int i = 0; i < cloneList.Count; i++)
        {
            GunPanel panel = null;

            if (i < transform.childCount)
            {
                panel = transform.GetChild(i).GetComponent<GunPanel>();
            }
            else
            {
                panel = Instantiate(_gunPanelPrefab, transform);
            }

            RectTransform rectTrm = panel.GetComponent<RectTransform>();
            rectTrm.anchoredPosition = _initAnchorPos + new Vector2((cloneList.Count - i - 1) * _xDelta, 0);

            if (i != cloneList.Count - 1)
            {
                rectTrm.localScale = Vector3.one * 0.9f;
            }
            panel.Init(cloneList[i]);
            if (cloneList[i] != null)
                panel.UpdateBullet(cloneList[i].Ammo);
            else
                panel.UpdateBullet(0);
            _panelList.Add(panel);
        }
        _panelList.Reverse();

        ConnectAmmoTextEvent();
    }

    private void PlaySound(AudioClip clip)
    {
        _audioSource.Stop();
        _audioSource.clip = clip;
        _audioSource.Play();
    }
    private void ConnectAmmoTextEvent()
    {
        GunPanel first = _panelList.First();
        first._Weapon?.OnChangedAmmo.AddListener((amount) => {
            first.UpdateBullet(amount);
        });
    }
    #region 무기 변경 UI 닷트윈
    public void ChangeWeaponUI(bool isPrev, Action CallBack = null)
    {
        GunPanel first = _panelList.First();
        GunPanel last = _panelList.Last();
        GunPanel next = _panelList[1];

        first._Weapon?.OnChangedAmmo.RemoveAllListeners();//첫번째 무기의 리스너 제거

        Sequence seq = DOTween.Sequence();
        if (isPrev)
        {
            seq.Append(first.RectTrm.DOScale(Vector3.one * 0.9f, _transitionTime));
            seq.Join(first.RectTrm.DOAnchorPos(_initAnchorPos + new Vector2(_xDelta, 0), _transitionTime));
            for (int i = 1; i < _panelList.Count - 1; i++)
            {
                seq.Join(_panelList[i].RectTrm.DOAnchorPos(
                    _initAnchorPos + new Vector2(_xDelta * (i + 1), 0),
                    _transitionTime));
            }
            seq.Join(last.RectTrm.DOScale(Vector3.one, _transitionTime));
            seq.Join(last.RectTrm.DOAnchorPos(_initAnchorPos + new Vector2(0, 82), _transitionTime));

            seq.AppendCallback(() =>
            {
                last.RectTrm.SetAsLastSibling();
                _panelList.RemoveAt(_panelList.Count - 1);
                _panelList.Insert(0, last); //맨 앞으로
            });

            seq.Append(last.RectTrm.DOAnchorPos(_initAnchorPos, _transitionTime));

        }
        else
        {
            seq.Append(first.RectTrm.DOScale(Vector3.one * 0.9f, _transitionTime));
            seq.Join(first.RectTrm.DOAnchorPos(_initAnchorPos + new Vector2(0, 82), _transitionTime));

            seq.Join(next.RectTrm.DOScale(Vector3.one, _transitionTime));
            seq.Join(next.RectTrm.DOAnchorPos(_initAnchorPos, _transitionTime));

            for (int i = 2; i < _panelList.Count; i++)
            {
                seq.Join(_panelList[i].RectTrm.DOAnchorPos(
                    _initAnchorPos + new Vector2(_xDelta * (i - 1), 0),
                    _transitionTime));
            }

            seq.AppendCallback(() =>
            {
                first.RectTrm.SetAsFirstSibling(); //첫번째 자식으로 설정한다.
                _panelList.RemoveAt(0);
                _panelList.Add(first);
            });

            seq.Append(first.RectTrm.DOAnchorPos(
                _initAnchorPos + new Vector2(_xDelta * (_panelList.Count - 1), 0),
                _transitionTime));

        }

        seq.AppendCallback(() =>
        {
            PlaySound(_changeClip);
            ConnectAmmoTextEvent();
            CallBack?.Invoke();
        });
    }
    #endregion
}
