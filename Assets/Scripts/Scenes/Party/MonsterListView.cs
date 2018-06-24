using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Monster
{
    public class MonsterListView : MonoBehaviour
    {
        public delegate void OnMonsterListViewItemSelected(MonsterListItemView view);
        public OnMonsterListViewItemSelected ItemSelectedHandler;

        [SerializeField]
        Transform content;

        Dictionary<int, MonsterListItemView> monsterListItemViews = new Dictionary<int, MonsterListItemView>();

        private void Awake()
        {
            InitializeListView();
        }

        public void InitializeListView()
        {
            if (MonsterDataManager.Instance.PlayerMonsterData.Count > 0)
            {
                monsterListItemViews = new Dictionary<int, MonsterListItemView>();

                foreach (var entity in MonsterDataManager.Instance.PlayerMonsterData.Values)
                {
                    var itemView = GenertateMonsterListItemView();
                    itemView.UpdateItem(entity);
                    itemView.ClickedHandler += OnClickedMonsterListItem;
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

        public void OnClickedMonsterListItem(MonsterListItemView itemView)
        {
            if (ItemSelectedHandler != null)
                ItemSelectedHandler(itemView);
        }

        public void UpdateSelectedMember(List<int> memberIds)
        {
            foreach (var itemView in monsterListItemViews.Values)
            {
                if (itemView.Entity != null && memberIds.Contains(itemView.Entity.monsterId))
                    itemView.GetComponent<SelectedMarkView>().SetSelected(true);
                else
                    itemView.GetComponent<SelectedMarkView>().SetSelected(false);
            }
        }
    }
}
