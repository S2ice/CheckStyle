using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OfferAndFindAPI.Models
{
    public partial class User
    {
        public int? IdUser { get; set; }
        public string? Firstname { get; set; }
        public int? IdRole { get; set; }
        public int? IdStatus { get; set; }
        public string? Name { get; set; }
        public string? Patronymic { get; set; }

        public string? Login { get; set; } = null!;

        public string? Password { get; set; } = null!;

        public string? EMail { get; set; } = null!;
    }
}
