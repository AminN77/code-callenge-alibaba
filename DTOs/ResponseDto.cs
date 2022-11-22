using System;
using CodeChallenge.DAL;
using Newtonsoft.Json;

namespace CodeChallenge.DTOs
{
	public record ResponseDto(Weather WeatherData, int StatusCode, List<string> Messages)
	{
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}

