using System;
using CodeChallenge.DTOs;

namespace CodeChallenge.Exceptions
{
    public class AppException : Exception
    {
        public ResponseDto ExceptionResult { get; set; }

        public AppException(string message, int statusCode)
        {
            ExceptionResult = new ResponseDto(
                WeatherData: null,
                StatusCode: statusCode,
                Messages: new List<string> { message });
        }
    }
}

