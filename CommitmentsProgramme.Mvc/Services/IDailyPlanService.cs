using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommitmentsProgramme.Mvc.Services;

public interface IDailyPlanService
{

    Task<DailyPlanVm> GetForEditAsync(int? id);

    Task SaveAsync(DailyPlanVm vm, string userFullName);
}