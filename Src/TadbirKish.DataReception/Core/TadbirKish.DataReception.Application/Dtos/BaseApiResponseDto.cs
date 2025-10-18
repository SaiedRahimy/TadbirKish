using System.Collections.Generic;

namespace TadbirKish.DataReception.Application.Dtos
{
    public class BaseApiResponseDto<T>
    {
        public BaseApiResponseDto(T data, bool success, List<string> errors = null)
        {
            Data = data;
            Success = success;
            Errors = errors;
        }

        public T Data { get; set; }
        public bool Success { get; set; }
        public List<string> Errors { get; set; }
    }
}
