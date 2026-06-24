using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommitmentsProgramme.Domain.Entities
{
    public class Attendance : BaseEntity
    {
        [Required(ErrorMessage = "{0} مطلوب")]
        [Display(Name = "الحضور")]
        [StringLength(500)]
        public string Title { get; set; }

        public bool IsDeleted { get; set; }

        public ICollection<CommitmentsAttendances> CommitmentsAttendances { get; set; } = [];

    }
}
