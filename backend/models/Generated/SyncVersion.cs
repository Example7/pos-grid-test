using System;
using System.Collections.Generic;

namespace DevExpress.Models.Generated;

/// <summary>
/// Tabela przechowująca wersje synchronizacji zdarzeń magazynowych
/// </summary>
public partial class SyncVersion
{
    public long Version { get; set; }

    public DateTime SyncedAt { get; set; }
}
