using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommitmentsProgramme.Domain.Entities;

public class CommitmentBranch
{
  public int CommitmentId { get; set; }
  public Commitment Commitment { get; set; } = default!;

  public int BranchId { get; set; }
  public Branch Branch { get; set; } = default!;
}