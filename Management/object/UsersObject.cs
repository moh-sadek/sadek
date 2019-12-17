using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Management.objects
{
    public partial class UsersObject
    {
        public long UserId { get; set; }
        public string LoginName { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public short UserType { get; set; }
        public string Email { get; set; }
        public short Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public short LoginTryAttempts { get; set; }
        public DateTime? LastLoginOn { get; set; }
        public string Photo { get; set; }
        public bool? SearchByRegistryNumber { get; set; }
        public bool? SearchByNationalId { get; set; }
        public bool? SearchByMotherName { get; set; }
        public bool? SearchByName { get; set; }
        public DateTime CreatedOn { get; set; }
        public short Status { get; set; }
        public int[] OfficeId { get; set; }
        public long CreatedBy { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public long? NationalId { get; set; }
        public int? PersonId { get; set; }
        public string Phone { get; set; }
        public bool? DeathInfoPrivilege { get; set; }
        public bool? DeathEntryPrivilege { get; set; }
        public int? SearchDeathsQouta { get; set; }
        public long? HospitalId { get; set; }
        public int? SearchQouta { get; set; }
    }
}
