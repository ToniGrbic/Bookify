using System;
using System.Collections.Generic;
using System.Text;

namespace Bookify.Domain.DTOs
{
    public class ApiUserDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public ApiAddressDto? Address { get; set; }
        public string? Website { get; set; }
    }

    public class ApiAddressDto
    {
        public string? Street { get; set; }
        public string? City { get; set; }
        public ApiGeoDto? Geo { get; set; }
    }

    public class ApiGeoDto
    {
        public string? Lat { get; set; }
        public string? Lng { get; set; }
    }

}
