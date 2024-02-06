namespace TimLearning.Shared.BaseEntities;

public interface IIdHolder<TKey>
{
    public TKey Id { get; set; }
}
