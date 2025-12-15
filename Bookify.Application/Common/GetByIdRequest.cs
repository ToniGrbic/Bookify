namespace Bookify.Application.Common.Model
{
    public class GetByIdRequest
    {
        public int Id { get; init; }
        public GetByIdRequest(int id)
        {
            Id = id;
        }

        public GetByIdRequest()
        {
            
        }
    }

    public class GetByIdRequest<T>
    {
        public T Id { get; set; }

        public GetByIdRequest(T id)
        {
            Id = id;
        }
    }
}
