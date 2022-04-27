namespace XCZ.Permissions
{
    public static class FormPermissions
    {
        public const string FormManagement = "FormManagement";

        public static class Form
        {
            public const string Default = FormManagement + ".Form";
            public const string Delete = Default + ".Delete";
            public const string Update = Default + ".Update";
            public const string Create = Default + ".Create";
        }

        public static class FormBuild
        {
            public const string Default = FormManagement + ".FormBuild";
            public const string Build = Default + ".Build";
            public const string Update = Default + ".Update";
            public const string Create = Default + ".Create";
            public const string Download = Default + ".Download";
        }
    }
}
