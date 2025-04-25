using Fase3.src.models;

namespace Fase3.src.builders
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
