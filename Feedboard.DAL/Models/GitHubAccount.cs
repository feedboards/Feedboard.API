using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Feedboard.DAL.Models;

[Index("CreatedAt", Name = "IX_GitHubAccounts")]
public partial class GitHubAccount
{
    [Key]
    [StringLength(450)]
    [Unicode(false)]
    public string UserId { get; set; } = null!;

    [StringLength(512)]
    [Unicode(false)]
    public string AccessToken { get; set; } = null!;

    [StringLength(128)]
    [Unicode(false)]
    public string Scopes { get; set; } = null!;

    [StringLength(256)]
    [Unicode(false)]
    public string Username { get; set; } = null!;

    [StringLength(450)]
    [Unicode(false)]
    public string Email { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public bool IsActive { get; set; }

    [StringLength(450)]
    [Unicode(false)]
    public string? PublicEmail { get; set; }
}
