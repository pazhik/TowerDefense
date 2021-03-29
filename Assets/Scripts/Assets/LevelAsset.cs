using EnemySpawn;
using Turret;
using TurretSpawn;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

namespace Assets
{
    [CreateAssetMenu(menuName = "Assets/Level Asset", fileName = "Level Asset")]
    public class LevelAsset : ScriptableObject
    {
        public SceneAsset SceneAsset;
        public SpawnWavesAsset SpawnWavesAsset;
        [FormerlySerializedAs("TurretMarkerAsset")] public TurretMarkerAsset TurretMarketAsset;
    }
}