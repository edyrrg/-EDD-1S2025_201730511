using Fase2.src.models;

namespace Fase2.src.builders
{
    public class ErrorReportBuilder
    {
        private List<MyError> _duplicateEntities = [];
        public ErrorReportBuilder()
        {
            _duplicateEntities = [];
        }
        public ErrorReportBuilder AddDuplicateEntity(EntityType entityType, string reason)
        
        {
            _duplicateEntities.Add(new MyError(entityType, reason));
            return this;
        }

        public ErrorReport Build()
        {
            return new ErrorReport(_duplicateEntities);
        }
        
        public ErrorReportBuilder Reset()
        {
            _duplicateEntities = [];
            return this;
        }
    }
}
