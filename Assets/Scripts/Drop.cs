using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
using Sirenix.OdinInspector;

public class Drop : MonoBehaviour
{
    public Player currentPlayer;
    public UnityEvent OnGet;
    [BoxGroup("ID")]
    [ReadOnly]
    public string id;

    #region IDSetup
    [BoxGroup("ID")]
    [Button]
    private void SetId()
    {
        id = System.Guid.NewGuid().ToString();
    }

    public static bool IsUniqueMonster(string ID)
    {
        Enemy[] enemyArray = FindObjectsOfType<Enemy>();
        for (int i = 0; i < enemyArray.Length; i++)
        {
            if (ID == enemyArray[i].id) return false;
        }
        return true;
    }
    #endregion IDSetup

    private void Start()
    {
        transform.DORotate(new Vector3(0, 360, 0), 1, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Restart);
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
            PermanentDataHolder.Instance.dropPickUpID.Add(id);
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
