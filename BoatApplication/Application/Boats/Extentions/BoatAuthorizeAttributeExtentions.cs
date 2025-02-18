namespace BoatApplication.Application.Boats.Attributes
{
    public static class BoatAuthorizeAttributeExtentions
    {
        public static IEnumerable<string> GetBoatPolicies(this IEnumerable<BoatAuthorizeAttribute> attributes)
        {
            var attributesWithRoles = attributes.Where(a => !string.IsNullOrWhiteSpace(a.Policy));
            if (!attributesWithRoles.Any())
                return new List<string>();
            return attributesWithRoles.SelectMany(a => a.Policy.Split(','));
        }
    }
}
