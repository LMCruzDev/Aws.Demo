namespace Aws.Demo.Api.Data.Abstraction
{
    public interface ITable<THashKey, TRangeKey>
    {
        THashKey HashKey { get; set; }

        TRangeKey RangeKey { get; set; }
    }
}