namespace MagicVilla_Web.Models
{
    public class newApiResponse
    {
        public class ApiResponse<T>
        {
            public int StatusCode { get; set; }
            public bool IsSuccess { get; set; }
            public string ErrorMassages { get; set; }
            public T Result { get; set; }
        }
    }
}
