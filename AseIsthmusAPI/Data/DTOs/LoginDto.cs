﻿namespace AseIsthmusAPI.Data.DTOs
{
    public class LoginDto
    {
        public string? EmailAddress { get; set; }

        public string? Pw { get; set; }

        public string? Name { get; set; }
        public int PersonId { get; set; }

        public int? RoleId { get; set; }

        public string? RoleDescription { get; set; }

    }
}
