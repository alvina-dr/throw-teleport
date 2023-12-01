using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class Drop : MonoBehaviour
{
    public Player currentPlayer;
    public UnityEvent OnGet;

    private void Start()
    {
        transform.DORotate(new Vector3(0, 360, 0), 1).SetLoops(-1, LoopType.Restart);
        transform.DOMoveY(transform.position.y + .2f, 3).SetLoops(-1, LoopType.Yoyo);
    }
    void Update()
    {
        if (currentPlayer == null) return;
        transform.position = Vector3.Lerp(transform.position, currentPlayer.transform.position, Time.deltaTime * 3);
        if (Vector3.Distance(transform.position, currentPlayer.transform.position) < 2)
        {
            OnGet?.Invoke();
            transform.DOKill();
            Destroy(gameObject);
        }
    }

    public void AddMaterial(int _value)
    {
        PermanentDataHolder.Instance.AddMaterial(_value);
        GPCtrl.Instance.UICtrl.materialCount.SetText(PermanentDataHolder.Instance.currentMaterial.ToString());
        GPCtrl.Instance.AudioCtrl.PlaySound(GPCtrl.Instance.GeneralData.soundList.lootExperience);
    }
}
