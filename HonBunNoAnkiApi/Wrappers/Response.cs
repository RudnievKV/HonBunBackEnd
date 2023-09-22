using System.Collections.Generic;

namespace HonbunNoAnkiApi.Wrappers
{
    public class Response<T>
    {
        //[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public T Data { get; set; }
        public bool Succeeded { get; set; }
        //[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<string> Errors { get; set; }

        public Response()
        {
            Succeeded = true;
            Errors = null;
        }
        public Response(T data)
        {
            Succeeded = true;
            Data = data;
            Errors = null;
        }
        public Response(List<string> errors)
        {
            Succeeded = false;
            Data = default;
            Errors = errors;
        }
    }
}
