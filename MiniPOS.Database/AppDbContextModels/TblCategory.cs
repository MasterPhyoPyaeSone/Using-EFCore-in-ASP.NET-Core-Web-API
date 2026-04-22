using System;
using System.Collections.Generic;

namespace MiniPOS.Database.AppDbContextModels;

public partial class TblCategory
{
    public int CategoryId { get; set; }

    public string CategoryName { get; set; } = null!;

    public bool? DeleteFlag { get; set; }
}
