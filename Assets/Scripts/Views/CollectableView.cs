using Leopotam.EcsLite;
using UnityEngine;

public class CollectableView : MonoBehaviour
{
   private EcsPackedEntityWithWorld _entity;

   public void Init(EcsPackedEntityWithWorld entity)
   {
      _entity = entity;
   }

   private void OnTriggerEnter(Collider other)
   {
      if (other.CompareTag(Constants.PlayerTag))
      {
         if (_entity.Unpack(out EcsWorld unpackedWorld, out int unpackedWithWorld))
         {
            var hits = unpackedWorld.GetPool<CollidePlayer>();
            hits.Add(unpackedWithWorld);
         }
      }
   }
}
