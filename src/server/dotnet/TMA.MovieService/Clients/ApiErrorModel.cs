using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TMA.MovieService.Clients
{
    public class ApiErrorModel
    {
        public string Message { get; set; }
    }

    public class ApiResponse
    {
        public List<ApiErrorModel> Errors { get; set; }

        public bool HasErrors => Errors?.Any() ?? false;

        public bool IsSuccess => !HasErrors;

        public void Merge(ApiResponse response)
        {
            Errors = (Errors ?? new List<ApiErrorModel>()).Concat(response.Errors ?? new List<ApiErrorModel>()).ToList();
        }

        public void AddError(string error)
        {
            Errors = (Errors ?? new List<ApiErrorModel>());

            Errors.Add(new ApiErrorModel()
            {
                Message = error
            });
        }
    }

    public class ApiResponse<TData>: ApiResponse
    {
        public ApiResponse() { }
        public ApiResponse(TData data)
        {
            Data = data;
        }
        public TData Data { get; set; }
    }
}
