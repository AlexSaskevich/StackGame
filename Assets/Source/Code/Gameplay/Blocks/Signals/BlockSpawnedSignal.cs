using UnityEngine;

namespace Source.Code.Gameplay.Blocks.Signals
{
    public struct BlockSpawnedSignal
    {
        public BlockSpawnedSignal(Vector3 position)
        {
            Position = position;
        }

        public Vector3 Position { get; private set; }
    }
}