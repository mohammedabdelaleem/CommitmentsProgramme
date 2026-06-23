using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommitmentsProgramme.Domain.Entities;

public class CommitmentsAttendances
{
  public int CommitmentId { get; set; }
  public Commitment Commitment { get; set; } = default!;

  public int AttendanceId { get; set; }
  public Attendance Attendance { get; set; } = default!;
}