using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Feedboard.DAL.Models;

[Index("CreatedAt", Name = "AzureAccounts_CreatedAt_index")]
public partial class AzureAccount
{
    [Key]
    public string Email { get; set; } = null!;

    [StringLength(4500)]
    [Unicode(false)]
    public string IdToken { get; set; } = null!;

    [StringLength(4500)]
    [Unicode(false)]
    public string AccessToken { get; set; } = null!;

    [StringLength(4500)]
    [Unicode(false)]
    public string RefreshToken { get; set; } = null!;

    public DateTime AccessTokenExpiredAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public bool IsActive { get; set; }
}
