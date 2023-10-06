using System.Reflection;

namespace PocketZone.SaveLoad
{
    public static class ReflectionExtensions
    {
        private const BindingFlags _bindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;

        public static T GetFieldValue<T>(this object obj, string name)
        {
            var field = obj.GetType().GetField(name, _bindingFlags);

            return (T)field?.GetValue(obj);
        }

        public static void SetFieldValue<T>(this object obj, string name, T value)
        {
            var field = obj.GetType().GetField(name, _bindingFlags);

            field?.SetValue(obj, value);
        }
    }
}