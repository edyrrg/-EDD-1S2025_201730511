namespace Fase2.src.models
{
    public class MyError(EntityType entity, string reason)
    {
        public EntityType Entity { get;} = entity;
        public string Reason { get;} = reason;
    }
}