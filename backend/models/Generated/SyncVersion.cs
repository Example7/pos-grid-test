using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DevExpress.Models.Generated;

/// <summary>
/// Tabela przechowująca wersje synchronizacji zdarzeń magazynowych
/// </summary>
public partial class SyncVersion
{
    [Key]
    public long Version { get; set; }

    public DateTime SyncedAt { get; set; }
}
