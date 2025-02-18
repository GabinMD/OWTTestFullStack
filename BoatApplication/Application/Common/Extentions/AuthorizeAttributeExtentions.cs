namespace BoatApplication.Application.Common.Attributes
{
    public static class AuthorizeAttributeExtentions
    {
        public static IEnumerable<string> GetRoles(this IEnumerable<AuthorizeAttribute> attributes)
        {
            var attributesWithRoles = attributes.Where(a => !string.IsNullOrWhiteSpace(a.Roles));
            if (!attributesWithRoles.Any())
                return new List<string>();
            return attributesWithRoles.SelectMany(a => a.Roles.Split(','));
        }

        public static IEnumerable<string> GetPolicies(this IEnumerable<AuthorizeAttribute> attributes)
        {
            var attributesWithRoles = attributes.Where(a => !string.IsNullOrWhiteSpace(a.Policy));
            if (!attributesWithRoles.Any())
                return new List<string>();
            return attributesWithRoles.SelectMany(a => a.Policy.Split(','));
        }
    }
}
