﻿using System;
using System.Collections.Generic;

namespace BBankAPI.Data.BankModels;

public partial class Account
{
    public int Id { get; set; }

    public int AccountType { get; set; }

    public int? ClientId { get; set; }

    public decimal Balance { get; set; }

    public DateTime RegDate { get; set; }

    public virtual AccountType AccountTypeNavigation { get; set; } = null!;

    public virtual ICollection<BankTransaction> BankTransactions { get; } = new List<BankTransaction>();

    public virtual Client? Client { get; set; }
}
