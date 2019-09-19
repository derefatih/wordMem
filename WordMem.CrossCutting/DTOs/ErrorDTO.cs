using System;

namespace WordMem.CrossCutting.DTOs
{
    public class ErrorDTO
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}