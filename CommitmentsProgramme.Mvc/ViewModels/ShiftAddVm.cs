using System.ComponentModel.DataAnnotations;

namespace CommitmentsProgramme.Mvc.ViewModels;

public class ShiftAddVm
{

    public string Name { get; set; }

    public string Phone { get; set; }
    public int EnumSelect { get; set; }
        public DateOnly dateOnly { get; set; }
        }