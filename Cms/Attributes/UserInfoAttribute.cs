namespace Cms.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class UserInfoAttribute : Attribute
    {
        public Type[] DbContextType { get; set; }
        public UserInfoAttribute(Type[] dbContextType)
        {

            this.DbContextType = dbContextType;

        }
    }
}
