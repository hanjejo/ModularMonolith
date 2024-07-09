using System.Text.RegularExpressions;

namespace ModularMonolith.ModelBinders
{
    public class UlidRouteConstraint : IRouteConstraint
    {
        private static readonly Regex UlidRegex = new Regex(
            @"^[0-9A-HJKMNP-TV-Z]{26}$",
            RegexOptions.Compiled | RegexOptions.IgnoreCase);

        public bool Match(
            HttpContext httpContext,
            IRouter route,
            string routeKey,
            RouteValueDictionary values,
            RouteDirection routeDirection)
        {
            if (!values.TryGetValue(routeKey, out var value) || value == null)
            {
                return false;
            }

            var valueString = Convert.ToString(value);
            return UlidRegex.IsMatch(valueString);
        }
    }
}
