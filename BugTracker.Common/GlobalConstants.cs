namespace BugTracker.Common
{
    public static class GlobalConstants
    {
        public const string SystemName = "BugTracker";

        public const string AdministratorRoleName = "Administrator";

        public const string ManagerRoleName = "Manager";

        public const string ContributorsRoleName = "Contributors";

        public const string ReaderRoleName = "Reader";

        public const string AdministratorOrManagerOrContributorsOrReader = AdministratorRoleName + "," + ManagerRoleName + "," + ContributorsRoleName + "," + ReaderRoleName;
    }
}
