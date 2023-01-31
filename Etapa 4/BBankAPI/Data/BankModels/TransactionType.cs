﻿using System;
using System.Collections.Generic;

namespace BBankAPI.Data.BankModels;

public partial class TransactionType
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public DateTime RegDate { get; set; }

    public virtual ICollection<BankTransaction> BankTransactions { get; } = new List<BankTransaction>();
}
