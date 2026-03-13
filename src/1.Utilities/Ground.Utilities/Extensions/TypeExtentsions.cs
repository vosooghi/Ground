namespace Ground.Utilities.Extensions
{
    /// <summary>
    /// Provides extension methods for the Type class, including a method to check if a type is a subclass of a raw generic type.
    /// </summary>
    public static class TypeExtentsions
        {

            /// <summary>
            /// Determines whether the specified type is a subclass of a raw generic type.
            /// </summary>
            /// <param name="toCheck">The type to check.</param>
            /// <param name="generic">The generic type to compare against.</param>
            /// <returns>True if the type is a subclass of the specified generic type; otherwise, false.</returns>
            public static bool IsSubclassOfRawGeneric(this Type toCheck, Type generic)
            {
                while (toCheck != null && toCheck != typeof(object))
                {
                    var cur = toCheck.IsGenericType ? toCheck.GetGenericTypeDefinition() : toCheck;
                    if (generic == cur)
                    {
                        return true;
                    }
                    toCheck = toCheck.BaseType;
                }
                return false;
            }
        }
}
