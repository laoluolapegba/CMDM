using System.Data.Entity.ModelConfiguration;

namespace CMdm.Data
{
    //internal class CmdmEntityTypeConfiguration<T>
    //{
    //}
    public abstract class CmdmEntityTypeConfiguration<T> : EntityTypeConfiguration<T> where T : class
    {
        protected CmdmEntityTypeConfiguration()
        {
            PostInitialize();
        }

        /// <summary>
        /// Developers can override this method in custom partial classes
        /// in order to add some custom initialization code to constructors
        /// </summary>
        protected virtual void PostInitialize()
        {

        }
    }
}