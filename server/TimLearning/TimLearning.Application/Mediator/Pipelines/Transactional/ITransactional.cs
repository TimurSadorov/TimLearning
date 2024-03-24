using System.Data;

namespace TimLearning.Application.Mediator.Pipelines.Transactional;

public interface ITransactional
{
    static virtual IsolationLevel IsolationLevel => IsolationLevel.ReadCommitted;
}
