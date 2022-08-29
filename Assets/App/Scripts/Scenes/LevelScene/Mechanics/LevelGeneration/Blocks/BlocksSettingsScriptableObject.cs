using UnityEngine;

namespace Blocks
{
    [CreateAssetMenu(fileName = "new settings", menuName = "BlocksSettings")]
    public class BlocksSettingsScriptableObject : ScriptableObject
    {
        public float destroyRestoreAnimDuration = 0.2f;
    }
}