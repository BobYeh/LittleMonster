using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Monster
{
    public class MonsterIconView : MonoBehaviour
    {
        [SerializeField]
        Image icon;

        public void UpdateView(int masterId)
        {
            ResourceUtilities.Instance.LoadMonsterIcon(masterId, (sprite) =>
            {
                icon.sprite = sprite;
                gameObject.SetActive(true);
            });
        }
    }
}
