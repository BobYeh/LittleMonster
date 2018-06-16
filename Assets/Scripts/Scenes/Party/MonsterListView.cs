using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Monster
{
    public class MonsterListView : MonoBehaviour
    {
        [SerializeField]
        Transform content;

        Dictionary<int, MonsterListItemView> monsterListItemViews = new Dictionary<int, MonsterListItemView>();

        private void Awake()
        {
            IntializeListView();
        }

        public void IntializeListView()
        {
            if (MonsterDataManager.Instance.PlayerMonsterData.Count > 0)
            {
                foreach (var entity in MonsterDataManager.Instance.PlayerMonsterData.Values)
                {
                    monsterListItemViews = new Dictionary<int, MonsterListItemView>();

                    var itemView = GenertateMonsterListItemView();
                    itemView.UpdateItem(entity);
                    monsterListItemViews.Add(entity.monsterId, itemView);
                }
            }
        }

        public MonsterListItemView GenertateMonsterListItemView()
        {
            var prefab = Instantiate(Resources.Load<GameObject>(PrefabPath.MONSTER_LIST_ITEM_VIEW));
            prefab.transform.SetParent(content, false);
            return prefab.GetComponent<MonsterListItemView>();
        }
    }
}
