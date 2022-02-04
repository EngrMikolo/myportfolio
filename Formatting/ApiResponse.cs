using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace michealogundero.Formatting
{
    public class ApiResponse
    {
        public enum ApiResponseStatus
        {
            Successful = 0,
            BadRequest,
            Failed,
            UnknownError
        }

        public static Dictionary<ApiResponseStatus, string> GetApiResponseMessages()
        {
            var responseMessages = new Dictionary<ApiResponseStatus, string>
            {
                { ApiResponseStatus.Successful, "00" },
                { ApiResponseStatus.BadRequest, "01" },
                { ApiResponseStatus.Failed, "02" },
                { ApiResponseStatus.UnknownError, "03" }
            };

            return responseMessages;
        }
    }
}
