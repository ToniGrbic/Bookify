namespace Bookify.Domain.Common.Model
{
    public class GetAllResponse<TValue> /*where TValue : Entity*/
    {
        public IEnumerable<TValue> Values { get; init; }

        public GetAllResponse(IEnumerable<TValue> values)
        {
            Values = values;
        }

        public GetAllResponse()
        {
            Values = Enumerable.Empty<TValue>();
        }
    }
}
