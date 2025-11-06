using System;

namespace MyLittleEquipmentTrader.Domain.Entities
{
    public class AccessRole
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public string Description { get; set; }
        public string Permissions { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastUpdated { get; set; }
        public bool IsDefault { get; set; }
        public string RoleType { get; set; }
        public bool IsAssignable { get; set; }
        public int RoleLevel { get; set; }
        public string PermissionsGroup { get; set; }
        public bool IsGlobal { get; set; }
    }
}
