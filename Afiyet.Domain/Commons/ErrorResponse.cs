using Newtonsoft.Json;

namespace Afiyet.Domain.Commons
{
    public class ErrorResponse
    {
        [JsonIgnore]
        public int? Code { get; set; }

        public string Message { get; set; }

        public ErrorResponse(int? code = null, string message = null)
        {
            this.Code = code;
            this.Message = message;
        }
    }
}
