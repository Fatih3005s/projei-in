using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject objectToSpawn; // Spawn edilecek objenin prefab'ı
    [SerializeField] private float spawnInterval = 3f; // Objeler arasındaki süre (saniye)
    [SerializeField] private bool spawnRight = true; // Spawn edilen objelerin başlangıçta sağa mı gideceği?
    [SerializeField] private float despawnDelay = 5f; // Spawn edilen objenin yok olma gecikmesi (saniye)

    void Start()
    {
        InvokeRepeating("Spawn", 0f, spawnInterval);
    }

    void Spawn()
    {
        // Spawn edilecek objenin başlangıç rotasyonunu belirle
        Quaternion rotation = Quaternion.identity;
        if (!spawnRight)
        {
            rotation = Quaternion.Euler(0f, 180f, 0f); // Eğer sağa gitmiyorsa, objeyi 180 derece döndür
        }

        // Objeyi spawn et
        GameObject spawnedObject = Instantiate(objectToSpawn, transform.position, rotation);

        // Spawn edilen objeyi belirli bir süre sonra yok et
        Destroy(spawnedObject, despawnDelay);
    }
}
