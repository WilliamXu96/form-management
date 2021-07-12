using XCZ.FormManagement.Dto;

namespace XCZ.FormBuildManagement.Dto
{
    public class FormBuildDto : FormDto
    {
        public string EntityName { get; set; }

        public string TableName { get; set; }

        public string Remark { get; set; }
    }
}
