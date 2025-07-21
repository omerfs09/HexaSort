using System.Collections.Generic;

using UnityEngine;

// Enum for different item types
public enum ItemType
{
    Hexagon,
    HexagonSlot
}

// Interface for poolable objects
public interface IPoolable
{
    void OnSpawn();
    void OnDespawn();
}

public class PoolManager : MonoBehaviour
{

    public static PoolManager Instance;

    [System.Serializable]
    public class PoolData
    {
        public ItemType itemType;
        public GameObject prefab;
        public int poolSize;
    }

    public List<PoolData> poolDataList;
    
    private Dictionary<ItemType, Queue<IPoolable>> poolDictionary;
    private Dictionary<ItemType, GameObject> prefabDictionary;

    // Yeni: Havuzda olan aktif dýþý objeleri izlemek için
    private Dictionary<ItemType, HashSet<IPoolable>> pooledItemsSet;

    void Awake()
    {
        Instance = this;

        poolDictionary = new Dictionary<ItemType, Queue<IPoolable>>();
        prefabDictionary = new Dictionary<ItemType, GameObject>();
        pooledItemsSet = new Dictionary<ItemType, HashSet<IPoolable>>();

        foreach (var poolData in poolDataList)
        {
            var queue = new Queue<IPoolable>();
            var set = new HashSet<IPoolable>();

            poolDictionary.Add(poolData.itemType, queue);
            prefabDictionary.Add(poolData.itemType, poolData.prefab);
            pooledItemsSet.Add(poolData.itemType, set);

            for (int i = 0; i < poolData.poolSize; i++)
            {
                CreateNewItem(poolData.itemType);
            }
        }
    }

    private IPoolable CreateNewItem(ItemType itemType, bool setActive = false)
    {
        if (!prefabDictionary.ContainsKey(itemType))
        {
            Debug.LogError($"No prefab found for {itemType}");
            return null;
        }

        GameObject obj = Instantiate(prefabDictionary[itemType], transform);
        IPoolable poolable = obj.GetComponent<IPoolable>();

        if (poolable == null)
        {
            Debug.LogError($"Prefab {itemType} does not implement IPoolable");
            Destroy(obj);
            return null;
        }

        obj.SetActive(setActive);

        if (!setActive)
        {
            poolDictionary[itemType].Enqueue(poolable);
            pooledItemsSet[itemType].Add(poolable);
        }
        else
        {
            poolable.OnSpawn();
        }

        return poolable;
    }

    public void ReturnItem(ItemType itemType, IPoolable item)
    {
        if (pooledItemsSet[itemType].Contains(item))
        {
            Debug.LogWarning($"Trying to return an already pooled item of type {itemType}");
            return;
        }

        (item as MonoBehaviour).gameObject.SetActive(false);
        (item as MonoBehaviour).transform.SetParent(transform);
        item.OnDespawn();

        poolDictionary[itemType].Enqueue(item);
        pooledItemsSet[itemType].Add(item);
    }

    public IPoolable GetItem(ItemType itemType)
    {
        if (poolDictionary.ContainsKey(itemType) && poolDictionary[itemType].Count > 0)
        {
            IPoolable item = poolDictionary[itemType].Dequeue();
            pooledItemsSet[itemType].Remove(item);

            (item as MonoBehaviour).gameObject.SetActive(true);
            item.OnSpawn();
            return item;
        }
        else
        {
            return CreateNewItem(itemType, true);
        }
    }
}