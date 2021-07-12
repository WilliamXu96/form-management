using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using XCZ.FormManagement.Dto;

namespace XCZ.FormBuildManagement.Dto
{
    public class UpdateFormDto
    {
        [Required]
        [MaxLength(50)]
        public string EntityName { get; set; }

        [Required]
        [MaxLength(50)]
        public string TableName { get; set; }

        [MaxLength(200)]
        public string Remark { get; set; }

        public string Namespace { get; set; }

        public List<Field> Fields { get; set; }
    }
}
